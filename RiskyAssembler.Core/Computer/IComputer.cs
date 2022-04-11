namespace RiskyAssembler.Core.Computer
{
    public interface IComputer
    {
        IClock Clock { get; set; }
        ICPU CPU { get; set; }

        void Reset();
    }
}