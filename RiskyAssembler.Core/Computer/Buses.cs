using RiskyAssembler.Common.Bitfields;

namespace RiskyAssembler.Core.Computer
{
    public static class Buses
    {
        public static Queue<Bitfield32> InstructionBus { get; set; } = new Queue<Bitfield32>();

        public static void Clear()
        {
            InstructionBus.Clear();
        }
    }
}
