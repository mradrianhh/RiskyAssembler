![](/docs/Assets/risc-v-logo.png)

# RiskyAssembler
RISC-V Assembler Simulator

Based on the [RISC-V Instruction Set](https://riscv.org/wp-content/uploads/2017/05/riscv-spec-v2.2.pdf)

# Implementation

At first, the RV32I(32-bit base integer instruction set) will only be implemented. Further extension of the assembler will happen in the future for including RV64I and RV128I.

## Instruction format

- There are 6 instruction formats
    - Register
    - Immediate
    - Upper immediate
    - Store
    - Branch
    - Jump

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
    - bit[7]        [11]
    - bit[8:11]     Imm[4:1] 
    - bit[12:14]    Funct3
    - bit[15:19]    Rs1
    - bit[20:24]    Rs2
    - bit[25:30]    Imm[10:5]
    - bit[31]       [12]

- Jump
    - bit[0:6]      Opcode
    - bit[7:11]     Rd
    - bit[12:19]    Imm[19:12]
    - bit[20]       [11]
    - bit[21:30]    Imm[10:1]
    - bit[31]       [20]

## Architecture

- Registers
    - 32 integer registers
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

## Assembly flow

1. Parse file
    1. Format the file
    2. Get the tokens