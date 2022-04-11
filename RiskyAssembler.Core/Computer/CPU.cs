namespace RiskyAssembler.Core.Computer
{
    public class CPU : ICPU
    {
        public Registers Registers { get; set; }
        public Register PC { get; set; }
        public Register STATUS { get; set; }
        public int CycleCount { get; set; }

        public CPU()
        {
            Registers = new Registers();
            PC = new Register();
            STATUS = new Register();
        }

        public void Cycle()
        {
            System.Console.WriteLine($"Cycle {CycleCount} | PC {(int)PC}");
            CycleCount++;
            PC += 4;
        }

    }
}
