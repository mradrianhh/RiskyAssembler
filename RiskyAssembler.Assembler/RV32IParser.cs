using RiskyAssembler.Common.Program;

namespace RiskyAssembler.Assembler
{
    /// <summary>
    /// The parser reads the file after it has been formatted, reads each line and parses it if it's an instruction.
    /// </summary>
    public class RV32IParser : IParser
    {
        public HashSet<string> OpcodeTokens = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "ADD", "SUB", "XOR", "OR", "AND", "SLL", "SRL", "SRA",
            "SLT", "SLTU", "ADDI", "XORI", "ORI", "ANDI", "SLLI",
            "SRLI", "SRAI", "SLTI", "SLTIU", "LB", "LH", "LW", 
            "LBU", "LHU", "SB", "SH", "SW", "BEQ", "BNE", "BLT",
            "BGE", "BLTU", "BGEU", "JAL", "JALR", "LUI", "AUIPC"
        };

        /// <summary>
        /// Formats the file, then parses each instruction into machine-code.
        /// </summary>
        /// <param name="file">The file to parse.</param>
        /// <returns>The binary-converted file.</returns>
        public Instruction Parse(string line)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves all the tokens in a line.
        /// </summary>
        /// <param name="line">The line to retrieve from.</param>
        /// <returns>An array of tokens.</returns>
        public string[] GetTokens(string line)
        {
            throw new NotImplementedException();
        }

        public TokenType GetTokenType(string token)
        {
            if (IsOpcodeType(token))
                return TokenType.OPCODE;

            if (IsRegisterType(token))
                return TokenType.REGISTER;

            if (IsImmediateType(token))
                return TokenType.IMMEDIATE;

            return TokenType.INVALID;
        }

        public bool IsOpcodeType(string token)
        {
            return OpcodeTokens.Contains(token);
        }

        public bool IsImmediateType(string token)
        {
            throw new NotImplementedException();
        }

        public bool IsRegisterType(string token)
        {
            throw new NotImplementedException();
        }
    }
}
