namespace RiskyAssembler.Core.Memory
{
    public class StackSegment
    {
        private byte[] _stack;
        public int Length { get; private set; }

        public StackSegment(Memory memory, int sstart, int send)
        {
            _stack = memory.GetSegment(sstart, send);
            Length = _stack.Length;
        }

        public byte this[int address]
        {
            get => _stack[address];
            set => _stack[address] = value;
        }
    }
}
