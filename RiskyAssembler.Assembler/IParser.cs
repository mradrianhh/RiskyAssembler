using RiskyAssembler.Common.Bitfields;
using RiskyAssembler.Common.Program;

namespace RiskyAssembler.Assembler
{
    public interface IParser
    {
        Bitfield32 Parse(string file);
    }
}