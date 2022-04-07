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

2. Implement instruction decoding
    - Requirements
        - Each instruction should be decoded into an Instruction object.
        - You should be able to encode an Instruction object back into a bitfield for debugging and UI purposes.
        - Each instruction should have a display string consisting of the instruction address, and the actual instruction commands written by the programmer without the comments. I.e "00: LB   x1, 2(x0)"
        - Each instruction should have a Command associated with it which actually executes the instruction.