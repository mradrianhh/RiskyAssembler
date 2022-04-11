namespace RiskyAssembler.Core.Computer
{
    public interface IClock
    {
        double Frequency { get; set; }
        double TickTime { get; set; }

        event TickHandler Ticked;

        void Start();
        void Start(int frequency);
        void Stop();
        void Tick();
    }
}