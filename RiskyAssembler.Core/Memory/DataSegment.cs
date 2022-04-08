namespace RiskyAssembler.Core.Memory
{
    public class DataSegment
    {
        private byte[] _data;
        public int Length { get; private set; }

        public DataSegment(Memory memory, int dstart, int dend)
        {
            _data = memory.GetSegment(dstart, dend);
            Length = _data.Length;
        }

        public byte this[int address]
        {
            get => _data[address];
            set => _data[address] = value;
        }
    }
}
