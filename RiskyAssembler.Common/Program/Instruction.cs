using RiskyAssembler.Common.Bitfields;

namespace RiskyAssembler.Common.Program
{
    public delegate void ExecuteHandler(Instruction instruction);

    /// <summary>
    /// An instruction is a Bitfield32 with a specified instruction type.
    /// </summary>
    public struct Instruction
    {
        public InstructionType Type = InstructionType.R;
        public RegisterID? Rd = null;
        public RegisterID? Rs1 = null;
        public RegisterID? Rs2 = null;
        public int? Imm = null;
        public ExecuteHandler ExecuteHandler = null;
        public Bitfield32 Bitfield = 0;

        public Instruction()
        {

        }

        public static implicit operator Instruction(Bitfield32 bitfield)
        {
            return bitfield.ToInstruction();
        }

        public static implicit operator Bitfield32(Instruction instruction)
        {
            return instruction.Bitfield;
        }
    }

    /// <summary>
    /// Decoded from the opcode field of the instruction.
    /// </summary>
    public enum InstructionType
    {
        R = 0b0110011, 
        I = 0b0010011, 
        S = 0b0100011, 
        B = 0b1100011
    }

    /// <summary>
    /// The registers decoded from the rd, rs1 and rs2 field of the instruction.
    /// </summary>
    public enum RegisterID
    {
        X0 = 0, X1, X2, X3, X4, X5, X6,
        X7, X8, X9, X10, X11, X12, X13,
        X14, X15, X16, X17, X18, X19, X20,
        X21, X22, X23, X24, X25, X26, X27,
        X28, X29, X30, X31, INVALID = -1
    }

}
