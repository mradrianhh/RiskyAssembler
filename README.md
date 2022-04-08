![](/docs/Assets/risc-v-logo.png)

# RiskyAssembler
RISC-V Assembler Simulator

Based on the [RISC-V Instruction Set](https://riscv.org/wp-content/uploads/2017/05/riscv-spec-v2.2.pdf)

# Architecture specifications

At first, the RV32I(32-bit base integer instruction set) will only be implemented. Further extension of the assembler will happen in the future for including RV64I and RV128I.

The RISC-V Architecture implements little-endian, meaning the least significant byte is at the lowest memory address.

The RISC-V Architecture is a Load-Store architecture,
meaning instructions only address registers, with store and load communicating with memory.

In instructions, you may see a notation such as LB, LH, LW, 
with all of them being Load instructions denoted by a character.

The character indicates the following:
- B - Byte(8-bits)
- H - Half(16-bits)
- W - Word(32-bits)

Instructions may also be denoted with U, indicating that they are unsigned.

## Instruction format

The instructions are a fixed 32-bit length, and are aligned on a four-byte boundary in memory.

- There are 6 instruction formats(R/I/S/B/U/J)
    - Register(R)
    - Immediate(I)
    - Upper immediate(U)
    - Store(S)
    - Branch(B)
    - Jump(J)

- Each instruction may consist of these bit-segments
    - opcode
    - rd
    - funct3
    - funct7
    - rs1
    - rs2
    - imm[a:b]

where the opcode in combination with funct3 and funct7 specifies the operation to be performed.

### Instruction format layouts

- Register
    - bit[0:6]      Opcode
    - bit[7:11]     Rd
    - bit[12:14]    Funct3
    - bit[15:19]    Rs1
    - bit[20:24]    Rs2
    - bit[25:31]    Funct7

- Immediate
    - bit[0:6]      Opcode
    - bit[7:11]     Rd
    - bit[12:14]    Funct3
    - bit[15:19]    Rs1
    - bit[20:31]    Imm[11:0]

- Upper immediate
    - bit[0:6]      Opcode
    - bit[7:11]     Rd
    - bit[12:31]    Imm[31:12]

- Store
    - bit[0:6]      Opcode
    - bit[7:11]     Imm[4:0]
    - bit[12:14]    Funct3
    - bit[15:19]    Rs1
    - bit[20:24]    Rs2
    - bit[25:31]    Imm[11:5]

- Branch
    - bit[0:6]      Opcode
    - bit[7]        Imm[11]
    - bit[8:11]     Imm[4:1] 
    - bit[12:14]    Funct3
    - bit[15:19]    Rs1
    - bit[20:24]    Rs2
    - bit[25:30]    Imm[10:5]
    - bit[31]       Imm[12]

- Jump
    - bit[0:6]      Opcode
    - bit[7:11]     Rd
    - bit[12:19]    Imm[19:12]
    - bit[20]       Imm[11]
    - bit[21:30]    Imm[10:1]
    - bit[31]       Imm[20]

Please note that the immediate value is the combination of the varius immediate subfields in the instruction. And the different immediate subfields are labeled with their bits in the final immediate value. By example in the jump instruction format:

Imm[20:1] = Imm[10:1] + Imm[11] + Imm[19:12] + Imm[20]

Please note also, that the sign bit of the immediate is always bit 31 in the instruction.

Furthermore, the rs1, rs2 and rd are always in the same position when they exist in the format.

The width of rd, rs1, and rs2 aren't 5 by coincidence. By having 5 bits, the number of values are 31, which corresponds to the amount of registers. Thus, rd, rs1 and rs2 encodes the corresponding registers.

#### Register-Immediate instructions (I)

- bit[0:6]      Opcode
- bit[7:11]     Rd
- bit[12:14]    Funct3
- bit[15:19]    Rs1
- bit[20:31]    Imm[11:0]

An example of a register-immediate instruction is:

ADDI rd, rs1, 0

with rd being the destination register encoded in bit[7:11], with
rs1, encoded in bit[15:19], being the register whose value is added to the immediate value 0, encoded in bit[20:31].

#### Register-Register instructions (R)

- bit[0:6]      Opcode
- bit[7:11]     Rd
- bit[12:14]    Funct3
- bit[15:19]    Rs1
- bit[20:24]    Rs2
- bit[25:31]    Funct7

An example of a register-register instruction is:

ADD rd, rs1, rs2

Again, rd is the destination register who is loaded with the value of the summation of rs1 and rs2, and the appropriate encoding are shown above.


## Architecture

### CPU

- Registers
    - 32 integer registers (x0 - x31)
        - Zero(x0) Always zero
        - ra(x1) Return address
        - sp(x2) Stack pointer
        - gp(x3) Global pointer
        - tp(x4) Thread pointer
        - t0(x5) Temporary/alternate return address
        - t1-2(x6-7) Temporary
        - s0/fp(x8) Saved register / frame pointer
        - s1(x9) Saved register
        - a0-1(x10-11) Function argument / return value
        - a2-7(x12-17) Function argument
        - s2-11(x18-27) Saved register
        - t3-6(x28-31) Temporary
    - Special registers
        - Program counter
        - Status(flags for overflow, lt, gt, equal, etc..)

### Virtual Computer

#### Startup and program flow

When we press reset, the computer runs the startup-script. Upon startup, the computer sets up the vector table and exception handlers, among them, the reset handler. So, upon resetting, the reset handler is activated and the computer copies over all the memory and the code from the flash drive to the RAM, zeroes out the .bss section, and jumps to the the main function of our program, from which the program flow is controlled.

Each instruction, is 32-bits wide, which is equivalent to 4 bytes. Meaning, that the first instruction will be at address 0, the next at address 4, and so on. Since the architecture is little-endian, the LSB will be at the lowest address.

Since the PC keeps the address of the next instruction in bytes, it gets incremented by 4 bytes each time. So, if we wish to increase the PC by 3 instructions, we have to increase it by 12 bytes.

Lets illustrate a sample program, and show the address of the instructions.

00:  ADDI x2, zero, 1   # Add Immediate: x2 ← 0  + 1   

04:  SUB x1, x1, x2     # Subtract: x1 ← x1 - 1

08:  SW  x1, 4(zero)    # Store word: x1 → [4 + 0]

12:  BLT zero, x1, -8   # Branch Less than: 0 < x1 => PC ← PC - 8 = 4

16:  HLT                # Halt, stop execution

#### Addressing memory

As you know by now, the program is stored in memory, each instruction of the program is 4 bytes long, the LSB is stored in the smallest address, and to move to the next instruction, we have to increase the program counter by 4(bytes).

This means that each memory cell is 1 byte(8-bits), and as such, if we wish to load the content of memory location 0 in register x1, we can just load the byte at address zero.

LB  x1, 0(x0)

We can also load two bytes, or a half-word.

LH  x1, 2(x0) # x1 <- [2] x1 gets content at address 2.

Or 4 bytes, or a word.

LW  x1, 4(x0) # x1 <- [4] x1 gets content at address 4.

#### The stack

In this implementation, the stack grows down in increments of a byte, with the stackpointer pointing at the next available byte. By moving the stackpointer down, we can allocate free space on the stack, creating what we refer the as a stack frame, with the framepointer pointing at the top of it.

On startup, we set the SP and FP to point to the top of the stack as this computer will have no operating system. It will work as a simple micro-controller, executing one program, and one program only: the one that is stored in the .text segment of it's memory.