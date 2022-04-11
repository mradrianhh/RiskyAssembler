using RiskyAssembler.Core.Computer;

Clock clock = new Clock(1);
CPU cpu = new CPU();

Computer computer = new Computer(clock, cpu);

computer.Reset();
