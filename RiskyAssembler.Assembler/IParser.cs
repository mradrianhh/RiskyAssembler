using RiskyAssembler.Common.Program;

namespace RiskyAssembler.Assembler
{
    public interface IParser
    {
        Instruction Parse(string file);
    }
}