![](/docs/Assets/risc-v-logo.png)

# RiskyAssembler
RISC-V Assembler Simulator

Based on the [RISC-V Instruction Set](https://riscv.org/wp-content/uploads/2017/05/riscv-spec-v2.2.pdf)

# Implementation

## Example Program

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