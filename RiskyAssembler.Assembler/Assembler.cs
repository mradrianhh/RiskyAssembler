using RiskyAssembler.Common.Program;

namespace RiskyAssembler.Assembler
{
    /// <summary>
    /// The assembler parses the file, then constructs the program based on the linker script.
    /// </summary>
    public class Assembler
    {
        private IFormatter _formatter;
        private IParser _parser;

        public Assembler(IFormatter formatter, IParser parser)
        {
            _formatter = formatter;
            _parser = parser;
        }

        public Program Assemble(string path)
        {
            // ProgramBuilder builder = new ProgramBuilder();
            Program program = new Program();

            string formattedAsmPath = _formatter.Format(path);

            foreach(var line in File.ReadAllLines(formattedAsmPath))
            {
                Instruction instruction = _parser.Parse(line);
            }

            // return builder.Build();
            return program;
        }
    }
}
