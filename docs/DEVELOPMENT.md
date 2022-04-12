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

