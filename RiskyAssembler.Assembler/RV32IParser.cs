using RiskyAssembler.Common.Bitfields;
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

        public HashSet<string> RegisterTokens = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "X0", "X1", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9", "X10", "X11",
            "X12", "X13", "X14", "X15", "X16", "X17", "X18", "X19", "X20", "X21", "X22", "X23",
            "X24", "X25", "X26", "X27", "X28", "X29", "X30", "X31"
        };

        public Dictionary<string, Operation> OperationMap = new Dictionary<string, Operation>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Formats the file, then parses each instruction into machine-code.
        /// </summary>
        /// <param name="file">The file to parse.</param>
        /// <returns>The binary-converted file.</returns>
        public Bitfield32 Parse(string line)
        {
            Bitfield32 instruction = new Bitfield32();
            string[] tokens = GetTokens(line);

            for(int i = 0; i < tokens.Length; i++)
            {
                switch (GetTokenType(tokens[i]))
                {
                    case TokenType.OPCODE:
                        break;
                    case TokenType.IMMEDIATE:
                        break;
                    case TokenType.REGISTER:
                        break;
                }
            }

            return instruction;
        }

        /// <summary>
        /// Retrieves all the tokens in a line.
        /// </summary>
        /// <param name="line">The line to retrieve from.</param>
        /// <returns>An array of tokens.</returns>
        public string[] GetTokens(string line)
        {
            return line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(token => token.Replace(",", "")).ToArray();
        }

        /// <summary>
        /// Determines the type of the token.
        /// </summary>
        /// <param name="token">The token to test.</param>
        /// <returns>The determined token type.</returns>
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
            if (token.Contains("0x", StringComparison.OrdinalIgnoreCase))
                return true;

            if (token.Contains("0b", StringComparison.OrdinalIgnoreCase))
                return true;

            return token.All(ch => char.IsDigit(ch));
        }

        public bool IsRegisterType(string token)
        {
            return RegisterTokens.Contains(token);
        }
    }
}
