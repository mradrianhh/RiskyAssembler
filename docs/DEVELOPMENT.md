# Plan moving ahead

1. Implement a bitfield.
    - Requirements
        - Must be able to concatenate bitfields. I.e: Bitfield(8) + Bitfield(8) = Bitfield(16)
        - Must be able to split bitfields. I.e Bitfield(16)[0:7] = Bitfield(8)
        - Must be able to toggle individual bits. I.e Bitfield(16)[0] = 1.
        - Must support manipulating bits through bitwise-operators. I.e Bitfield(16) & Bitfield(16) = Bitfield(16)
        - Must support bitmasking.
        - Must be able to decode bitfield into instruction.
        - Must be able to convert bitfield into integer.
        - Must be able to print bitfield as string.
        - Must be able to access bit by index similar to an array.
    - Implementation
        - Implemented using a struct.
        - Implement methods using extension methods.
        - Implemented similar to an array.
        - Worst case scenario, implemented using an array of chars, meaning each bit will take up 8 bits in actual memory.

2. Implement instruction decoding.
    -Requirements
        - Each instruction should be decoded into an Instruction object.
        - You should be able to encode an Instruction object back into a bitfield for debugging and UI purposes.
        - Each instruction should have a display string consisting of the instruction address, and the actual instruction commands written by the programmer without the comments. I.e "00: LB   x1, 2(x0)"
        - Each instruction should have a Command associated with it which actually executes the instruction.