using RiskyAssembler.Common.Bitfields;

namespace RiskyAssembler.Common.Program
{
    public delegate void ExecuteHandler(Instruction instruction);

    /// <summary>
    /// An instruction is a Bitfield32 with a specified instruction type.
    /// </summary>
    public struct Instruction
    {
        public Opcode Type = Opcode.R;
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

}
