# Plan moving ahead

1. Implement a bitfield
    - Requirements
        - Must be able to concatenate bitfields. I.e: Bitfield(8) + Bitfield(8) = Bitfield(16)
        - Must be able to split bitfields. I.e Bitfield(16)[0:7] = Bitfield(8)
        - Must be able to toggle individual bits. I.e Bitfield(16)[0] = 1.
        - Must support manipulating bits through bitwise-operators. I.e Bitfield(16) & Bitfield(16) = Bitfield(16)
        - Must support bitmasking.
        - Must be able to decode bitfield into instruction.
        - Must be able to convert bitfield into integer.
        - Must be able to print bitfield as string.
        - Must be able to decompose bitfields into bytes(bitfield8).
    - Implementation
        - 3 types of bitfield: Bitfield32, Bitfield16, Bitfield8, which wraps UInt32, UInt16, Byte respectively.
        - Bitfield16 consists of a upper bitfield8 and a lower bitfield8.
        - Bitfield32 consists of a upper bitfield16 and a lower bitfield16.
        - To concatenate bitfields, you left shift the target by the size of the source. Example
            - You want to construct an instruction from 4 bytes picked from memory, with the first one being the LSB.
            - Start with an empty int32 bitfield.
            - Set it equal to the last byte(MSB).
            - Fetch the next byte, left shift the bitfield by 8 bits, perform an OR operation on the bitfield with the fetched byte.
            - Repeat.
            - Warning: If you attempt to push more then 4 bytes into an int32 bitfield, you will corrupt the bitfield.
    - Notes
        - Since each instruction is 32-bits, we can just treat it as a int32.
        - Also, one int32 consists of 4 chars, with 1 char being 8-bits = 1 byte.
        - One can make a conversion method from int32 to a string representation of the bitfield.
        - One can toggle individual bits using bitwise operations.
        - Bonus: you don't have to write the library in C and use interop.

2. Implement register
    - Requirements
        - Must be convertable to Bitfield32
    - Implementation
        - Is a wrapper around a Bitfield32.
        - Has an integer identifier.
        - Has a string symbolic name.

3. Implement memory
    - Implementation
        - For now, let it just be an array of bytes.
        - In the future, consider seperating SRAM and FLASH, and requiring a start-up script to run before the program runs.
        - Let the code be stored from the beginning of the array, meaning the available space for data storage will decrease as the code increases.
        - The stack should start at the end of the array and grow backwards, as specified.
        - In the future, the linked script will specify where the .text segment(program code) starts and ends, and where the different data segments(.bss, .data) starts and ends, and also where the stack starts and ends.
        - The startup script will specify exception handlers to deal with stack overflows and all other exceptions. It will also copy over the data and code from flash memory, and zero out the .bss section before it points the program counter at the main function in our code. This script will run everytime we press the reset button.
        - For now, we will just have to hardcode in the specification that should be given by the linker script and implemented in memory by the startup script.

5. Implement Code segment
    - Implementation
        - Readonly byte[] copied from memory
        - Wrapper around a byte array.

6. Implement Memory segment
    - Implementation
        - Byte[] copied from memory
        - Wrapper around a byte array.

7. Implement registers component.
    - Requirements
        - Must be able to retrieve a register with an string key indexer. I. e: Registers["x0"].
    - Implementation
        - Dictionary of registers and their identifier.

8. Implement CPU clock.
    - Requirements
        - Must run at a chosen frequency
        - Each time it ticks, it executes the Systick function in the CPU.

9. Implement CPU
    - Requirements
        - Must be able to toggle STATUS register.
        - Must be able to fetch instruction from memory.
        - Must be able to increment PC by 4 bytes.
        - Must throw AddressMisalignedException if instruction isn't four-byte aligned in memory.
        - Must be able to load and store in memory(data segment).
    - Implementation
        - 32 general registers
        - PC and STATUS special registers
        - For now, fetches instructions sequentially.
        - In the future, caches instructions into an instruction bus.
        - Has a cycle method that gets invoked by the clock everytime it ticks.

10. Implement Computer
    - Implementation
        - When the clock ticks, the cpu executes a cpu cycle.

11. Implement stack segment
    - Implementation
        - Is a byte[] copied from memory.
        - Starts at the end of the memory, grows backwards.

12. Implement instruction decoding
    - Requirements
        - Each instruction should be decoded into an Instruction object.
        - You should be able to encode an Instruction object back into a bitfield for debugging and UI purposes.
        - Each instruction should have a display string consisting of the instruction address, and the actual instruction commands written by the programmer without the comments. I.e "00: LB   x1, 2(x0)"
        - Each instruction should have a Command associated with it which actually executes the instruction.

13. Implement symbol table

14. Implement Program class

# Assembler

## ABI

There must be an ABI definition file which defines the macros, or symbolic names, for the registers.

The ABI must act as an intermediary which the parser parses according to. That is, if it reads zero in the assembly code,
that corresponds to register x0.

The ABI provides a mapping between the symbolic tokens used by the programmer, and the tokens which get converted to machine language.

### Register usage

x0(zero) always contains the value 0, this cannot be changed.

By convention, when you wish to preserve some values before a function call, you place them in s-registers(s0-s11), which corresponds to registers
x8, x9 and x18-x27, with x8(s0) also acting as the frame pointer.

Temporary values which need not be reserved, are placed in the temporary registers(t-registers), t0-t6, which corresponds to x5-x7 and x28-x31.

When passing function arguments, the convention is to place them in the a-registers(a0-a7), which corresponds to x10-x17, with a0 holding the result
after the function has completed.

When entering a subroutine, the current address is saved in the ra(x1) register, so that it can be restored upon returning from the subroutine.

The last important register, is x2, the stackpointer, which tracks the next available byte in the stack.

The desired goal of these conventions, are to reduce memory access from the CPU. In the past, constantly pushing and popping values of the stack to
preserve them, was the norm, and we still have to do that today, you can only imagine trying to perform recursive algorithms without that capability.
The main problem we're trying to address when coming up with this convention, is the fact that the internal memory access of the CPU is insanely fast compared to the memory access which are required, even when only manipulating the stack, so by allowing arguments to be preserved in the registers, we reduce the total overhead by removing stack manipulations when possible.

### Syntax

A typical instruction has the following format:

OPCODE rd, rs1, rs2

with rd being the destination register, and rs1/rs2 being the source registers. All risc-v instructions has 3 arguments, if not, they are pseudo-instructions, which are aliases for one or more risc-v instructions which, again, do have 3 arguments in actuality.


Comments are denoted by a '#', as such:

OPCODE rd, rs1, rs2 # A comment

with everything trailing the '#' on the same line being considered a comment.


Labels are strings with no whitespace that ends in a colon, ':'. An example of a valid label is:

do_instruction:
    OPCODE rd, rd1, rd2 # Doing an instruction

Labels are syntactic sugar that relieves the programmer of the task of remembering the address of an instruction in code. That is, it's the assemblers job to associate a label with an instructions address. Labels are not an instruction, they are simply a variable which gets resolved by the assembler for usage in jumps and branches.


Tokens are separated by whitespace, commas or both. For the instruction above, the tokens would be do_instruction, OPCODE, rd, rs1 and rs2.

## Parser

Parser interface
    - void Format()
    - void Parse()
    - void LoadInstructions()

### Example program

To help pinpoint how we need to parse a risc-v assembly file, we'll provide a basic example program which helps illustrate the syntax.

ADDI x2, x0, 1 # Mov 1 to x2.

loop:
    SUB x1, x1, x2 # Decrement x1.
    SW  x1, 4(x0) # Store 4 + x0 in x1.
    BLT x0, x1, loop # If x0 < x1, jump to the address associated with the 'loop' label.


### Format()

Remove comments.

ADDI x2, x0, 1

loop:
    SUB x1, x1, x2
    SW  x1, 4(x0)
    BLT x0, x1, loop

Convert tab to spaces. 1 tab = 4 spaces.

ADDI x2, x0, 1

loop:
    SUB x1, x1, x2
    SW  x1, 4(x0)
    BLT x0, x1, loop

Remove unneccessary spaces.

ADDI x2, x0, 1
loop:
SUB x1, x1, x2
SW  x1, 4(x0)
BLT x0, x1, loop

Remove commas.

ADDI x2 x0 1
loop:
SUB x1 x1 x2
SW  x1 4(x0)
BLT x0 x1 loop

### Parse()

1. ADDI x2 x0 1
2. loop:
3. SUB x1 x1 x2
4. SW  x1 4(x0)
5. BLT x0 x1 loop

Parse lines.

Is it a label, or an instruction?

If it is an instruction, parse the tokens, convert, and construct the instruction.

If it is a label, resolve it to the address sharing the same line number. 
    If there is no instruction, resolve it to the instruction on the next line number.

- Instructions
    1. ADDI x2 x0 1
    2. SUB x1 x1 x2
    3. SW x1 4(x0)
    4. BLT x0 x1 loop

- Labels
    - loop -> SUB x1 x1 x2

### Token types

- Assembler directive
    - Starts with '.'. Ex: .align 2
- Arguments
    - Immediate
        - Constant value. Ex: 2, .align 2. ADDI x1, x0, 2.
    - Register
        - Symbol. Ex: x0, x1. ADDI x1, x0, 2.
- Opcode
    - First token in an instruction, followed by 3 arguments

### Syntax rules

- Assembler directives must be on their own line
- Single-line macros must be on their own line
- For a multi-line macro, the opening and ending token must be on their own line, with the macro code contained within it.
- A label can be on the preceeding or the same line as the instruction it points to.
- An opcode is followed by 3 arguments, pseudo-instructions are converted before parsing the file to machine code.
- Everything trailing the comment sign, '#', will be removed.

### Parsing Algorithm

foreach(line in file)
    tokens := line.Split(" ")
    foreach(token in tokens)
        token = token.Replace(",", "") // Remove commas from token
    
    switch(GetTokenType(token))
        case AssemblerDirective:
            ...
        case Argument:
            ...
        case Opcode:
            ...

ADDI x1, x0, 2
ADDI x2, x0, 10

decrease_x1:
    SUB x2, x2, x1
    BGT x2, 0, decrease_x1
end:
    BEQ x0, x0, end


- ADDI
    - x1
    - x0
    - 2
- ADDI
    - x2
    - x0
    - 10
- SUB
    - x2
    - x2
    - x1
- BGT
    - x2
    - 0
    - decrease_x1
- BEQ
    - x0
    - x0
    - end

# Computer

Loads a program and stores it in memory.

# Assembler

Assembles a binary output file.
Assembles a byte[] which is the program.

# Parser

Parses each line given from assembler.

# Formatter

Formats the file.

# Instruction format

Opcode defines the instruction type(R/I/S/B/U/J).

Immediates are always sign-extended. Sign bit for all immediates is always bit 31.

Sign extension are performed to increase the number of bits. For example,
given the binary representation of the decimal value 10 with 6 bits, 00 1010, if we were to sign-extend it to
16 bits, the result of the operation would be 0000 0000 0000 1010.

Sign extension also seeks to preserve the number's sign(positive/negative), so if we were to sign-extend the 
decimal value -15 - represented in two's complement as 11 1111 0001 - to 16 bits, the result would be 
1111 1111 1111 0001, thus preserving both the sign and the value.

## R

Rd is the destination register which stores the result of the operation performed on the source registers.
Rs1 and Rs2 are the operands of the operation. The source registers.
Funct3 and funct7 defines the operation.

## I

Rd is the destination register which stores the result of the operation performed on the source register and the immediate(constant).
Rs1 and Imm are the operans of the operation. Rs1 is a source register, and Imm is a constant value defined by 12 bits, that gets sign-extended to 32 bits.
Funct3 defines the operation.

## S

Rs2 is the register we store the content in.
The sum of rs1 and the imm is the address. Imm again gets sign-extended to 32 bits.
Funct3 defines the type of store(SW/SH/SB).

## B

Rs1 and rs2 are the operands the instruction tests the condition on.
The immediate defines the address it will jump to if the condition tests true, meaning the pc is incremented by the immediate.
If it tests false, pc is incremented by four as usual. 
The immediate gets sign-extended from 12 bits to 32.

## U

Rd denotes the register we wish to store the immediate in.
Imm denotes the constant value we wish to store in Rd. 
Important note: since this is an upper-immediate instruction, we will sign-extend it to the left(at the LSB).

## J

Imm denotes the address we are going to jump to, or the offset we're going to increment the pc by.
Rd stores the pc incremented by four.

# Instruction decoding

## Opcode

The opcode, which denotes the instruction type, is decoded from bit[6:0].

You simply create a 6-bit bitmask to extract it's integer value, and cast it to the InstructionType enum to store in the instruction's
Type field.

## Rd

When applicable, rd is always of the same width and at the same position, so you can extract it similarly to the opcode by bitmasking
and casting to the RegisterID enum.

## Rs1

Same as Rd.

## Rs2

Same as Rd.

## Funct3

Funct3 is also always stored at the same position, and is always 3 bits wide, so it can be extracted the same way as Rd. When extracted, it can be stored as an unsigned integer(we don't want it to suddenly store negative values) in the instruction's funct3 field.

## Funct7

Same as funct3, but is 7 bits wide.

## Imm

Immediates are more complicated, because the actual bits of the immediate field itself aren't necessarily stored in order within the 32-bit instruction.
The position of the bits of the immediate are dependent on the instruction type.
Immediates are always sign-extended, and their sign-bit is stored in bit 31.




