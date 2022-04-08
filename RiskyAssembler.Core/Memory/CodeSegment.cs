namespace RiskyAssembler.Core.Memory
{
    public class CodeSegment
    {
        private readonly byte[] _code;
        public int Length { get; private set; }

        public CodeSegment(Memory memory, int _cstart, int _cend)
        {
            _code = memory.GetSegment(_cstart, _cend);
            Length = _code.Length;
        }

        public byte this[int address]
        {
            get => _code[address];
        }
    }
}
