// TODO: Implement ExtractFunct3
// TODO: Implement ExtractFunct7

using RiskyAssembler.Common.Program;

namespace RiskyAssembler.Common.Bitfields
{
    /// <summary>
    /// Decodes a bitfield.
    /// </summary>
    public static class BitfieldDecoder
    {

        #region Bitfield-Instruction

        /// <summary>
        /// Decodes a 32-wide bitfield into an instruction.
        /// </summary>
        /// <param name="bitfield">The bitfield to decode.</param>
        /// <returns>A 32-bit instruction.</returns>
        /// <exception cref="NotImplementedException">Thrown if the decoder doesn't support the opcode.</exception>
        public static Instruction ToInstruction(this Bitfield32 bitfield)
        {
            Instruction instruction;

            Opcode type = DecodeOpcode(bitfield);
            switch (type)
            {
                case Opcode.R:
                    instruction = DecodeRInstruction(bitfield);
                    break;
                case Opcode.S:
                    instruction = DecodeSInstruction(bitfield);
                    break;
                case Opcode.I:
                    instruction = DecodeIInstruction(bitfield);
                    break;
                case Opcode.B:
                    instruction = DecodeBInstruction(bitfield);
                    break;
                default:
                    throw new NotImplementedException("Instruction type not supported yet.");
            }

            instruction.Bitfield = bitfield;

            return instruction;
        }

        private static Instruction DecodeRInstruction(Bitfield32 bitfield)
        {
            Instruction result = new Instruction();
            result.Type = Opcode.R;
            result.Rd = ExtractRd(bitfield);
            result.Rs1 = ExtractRs1(bitfield);
            result.Rs2 = ExtractRs2(bitfield);

            return result;
        }

        private static Instruction DecodeIInstruction(Bitfield32 bitfield)
        {
            Instruction result = new Instruction();
            result.Type = Opcode.I;
            result.Rd = ExtractRd(bitfield);
            result.Rs1 = ExtractRs1(bitfield);

            return result;
        }

        private static Instruction DecodeSInstruction(Bitfield32 bitfield)
        {
            Instruction result = new Instruction();
            result.Type = Opcode.S;
            result.Rs1 = ExtractRs1(bitfield);
            result.Rs2 = ExtractRs2(bitfield);

            return result;
        }

        private static Instruction DecodeBInstruction(Bitfield32 bitfield)
        {
            Instruction result = new Instruction();
            result.Type = Opcode.B;
            result.Rs1 = ExtractRs1(bitfield);
            result.Rs2 = ExtractRs2(bitfield);

            return result;
        }

        private static Opcode DecodeOpcode(Bitfield32 bitfield)
        {
            int opcodeMask = 0b1111111;
            int opcode = bitfield & opcodeMask;
            return (Opcode)opcode;
        }

        private static RegisterID ExtractRd(Bitfield32 bitfield)
        {
            return (RegisterID)ExtractBitfieldSubsection(bitfield, 0b11111, 7);
        }

        private static RegisterID ExtractRs1(Bitfield32 bitfield)
        {
            return (RegisterID)ExtractBitfieldSubsection(bitfield, 0b11111, 15);
        }

        private static RegisterID ExtractRs2(Bitfield32 bitfield)
        {
            return (RegisterID)ExtractBitfieldSubsection(bitfield, 0b11111, 20);
        }

        private static uint ExtractFunct3(Bitfield32 bitfield)
        {
            throw new NotImplementedException();
        }

        private static uint ExtractFunct7(Bitfield32 bitfield)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Extracts a subsection of a bitfield by masking and shifting.
        /// </summary>
        /// <param name="bitfield">The bitfield to extract from.</param>
        /// <param name="bitmask">The mask to extract with.</param>
        /// <param name="shiftCount">How far to shift the mask to the right to align it with the field you wish to extract.</param>
        /// <returns>The value of the extracted bitfield.</returns>
        private static int ExtractBitfieldSubsection(Bitfield32 bitfield, int bitmask, int shiftCount)
        {
            return (bitfield & (bitmask << shiftCount)) >> shiftCount;
        }
    }
}
