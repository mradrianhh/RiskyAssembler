namespace RiskyAssembler.Core.Computer
{
    public interface ICPU
    {
        int CycleCount { get; set; }
        Register PC { get; set; }
        Registers Registers { get; set; }
        Register STATUS { get; set; }

        void Cycle();
    }
}