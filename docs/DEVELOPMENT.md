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

7. Implement CPU
    - Requirements
        - Must be able to toggle STATUS register.
        - Must be able to fetch instruction from memory.
        - Must be able to increment PC by 4 bytes.
        - Must throw AddressMisalignedException if instruction isn't four-byte aligned in memory.
    - Implementation
        - 32 general registers
        - PC and STATUS special registers
        - For now, fetches instructions sequentially.
        - In the future, caches instructions into an instruction bus.

8. Implement instruction decoding
    - Requirements
        - Each instruction should be decoded into an Instruction object.
        - You should be able to encode an Instruction object back into a bitfield for debugging and UI purposes.
        - Each instruction should have a display string consisting of the instruction address, and the actual instruction commands written by the programmer without the comments. I.e "00: LB   x1, 2(x0)"
        - Each instruction should have a Command associated with it which actually executes the instruction.