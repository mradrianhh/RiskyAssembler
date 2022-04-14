using RiskyAssembler.Common.Program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskyAssembler.Common.Bitfields
{
    public static class BitfieldEncoder
    {

        #region Encode instruction

        /// <summary>
        /// Sets the 7 first bits of the bitfield to the specified opcode.
        /// </summary>
        /// <param name="bitfield">Extension method instance.</param>
        /// <param name="opcode">Opcode to set.</param>
        public static void SetOpcode(this ref Bitfield32 bitfield, Opcode opcode)
        {
            // Clear bit[6:0]
            int clearMask = ~(0b1111111);
            bitfield &= clearMask;

            // Set bit[6:0] to opcode.
            bitfield |= (int)opcode;
        }

        /// <summary>
        /// Sets bit[11:7] to the value of the specified destination register.
        /// </summary>
        /// <param name="bitfield">Extension method instance.</param>
        /// <param name="rd">Destination register.</param>
        public static void SetRd(this ref Bitfield32 bitfield, RegisterID rd)
        {
            // Clear bit[11:7]
            int clearMask = ~(0b11111 << 7);
            bitfield &= clearMask;

            // Set rd in position [11:7]
            int rdField = (int)rd << 7;

            // Set bit[11:7] to rd.
            bitfield |= rdField;
        }

        /// <summary>
        /// Sets bit[19:15] to the value of the specified rs1 source register.
        /// </summary>
        /// <param name="bitfield">Extension method instance.</param>
        /// <param name="rd">Source register.</param>
        public static void SetRs1(this ref Bitfield32 bitfield, RegisterID rs1)
        {
            // Clear bit[19:15]
            int clearMask = ~(0b11111 << 15);
            bitfield &= clearMask;

            // Set rs1 in position [19:15]
            int rs1Field = (int)rs1 << 15;

            // Set bit[19:15] to rs1.
            bitfield |= rs1Field;
        }

        /// <summary>
        /// Sets bit[24:20] to the value of the specified rs2 source register.
        /// </summary>
        /// <param name="bitfield">Extension method instance.</param>
        /// <param name="rd">Source register.</param>
        public static void SetRs2(this ref Bitfield32 bitfield, RegisterID rs2)
        {
            // Clear bit[24:20]
            int clearMask = ~(0b11111 << 20);
            bitfield &= clearMask;

            // Set rs1 in position [24:20]
            int rs2Field = (int)rs2 << 20;

            // Set bit[24:20] to rs2.
            bitfield |= rs2Field;
        }

        #endregion
    }
}
