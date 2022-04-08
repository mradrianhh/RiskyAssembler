﻿namespace RiskyAssembler.Core.Computer
{
    public class Computer
    {
        public const string SP = "x2";
        public const string FP = "x8";

        public Clock Clock { get; set; }
        public CPU CPU { get; set; }

        private Memory.Memory _memory;
        private Memory.CodeSegment _code;
        private Memory.DataSegment _data;
        private Memory.StackSegment _stack;

        public Computer()
        {
            Clock = new Clock(1);
            CPU = new CPU();
            _memory = new Memory.Memory(1024);
            _code = new Memory.CodeSegment(_memory, 0, 100);
            _data = new Memory.DataSegment(_memory, 100, 800);
            _stack = new Memory.StackSegment(_memory, 800, 1024);

            Clock.onTick += CPU.Cycle;
        }

        public void Reset()
        {
            Buses.Clear();
            Clock.Start();
        }

        private void ConfigureRegisters()
        {
            CPU.PC = 0;
            CPU.STATUS = 0;

            CPU.Registers.ClearRegisters();
            CPU.Registers[SP] = 1024;
            CPU.Registers[FP] = 1024;
        }
    }
}
