using Microsoft.VisualStudio.TestTools.UnitTesting;
using RiskyAssembler.Common.Program;
using System;

namespace RiskyAssembler.Common.Bitfields
{
    [TestClass]
    public class BitfieldEncoderTests
    {
        [TestMethod]
        [TestCategory("Instruction")]
        [DataRow(Opcode.R, (UInt32)0b0000000_00000_00000_000_00001_0000001, (UInt32)0b0000000_00000_00000_000_00001_0110011)]
        public void ShouldSetOpcode(Opcode opcode, UInt32 initialBitfield, UInt32 expectedBitfield)
        {
            Bitfield32 bitfield = initialBitfield;
            
            bitfield.SetOpcode(opcode);

            Assert.AreEqual(expectedBitfield, bitfield.Bitfield);
        }
        
        [TestMethod]
        [TestCategory("Instruction")]
        [DataRow(RegisterID.X0, (UInt32)0b0000000_00000_00000_000_00000_0000000, (UInt32)0b0000000_00000_00000_000_00000_0000000)]
        [DataRow(RegisterID.X1, (UInt32)0b0000000_00000_00000_000_00000_0000000, (UInt32)0b0000000_00000_00000_000_00001_0000000)]
        [DataRow(RegisterID.X1, (UInt32)0b1010010_01001_00101_010_01001_0010001, (UInt32)0b1010010_01001_00101_010_00001_0010001)]
        [DataRow(RegisterID.X0, (UInt32)0b1010010_01001_00101_010_01001_0010001, (UInt32)0b1010010_01001_00101_010_00000_0010001)]
        public void ShouldSetRd(RegisterID rd, UInt32 initialBitfield, UInt32 expectedBitfield)
        {
            Bitfield32 bitfield = initialBitfield;
            
            bitfield.SetRd(rd);

            Assert.AreEqual(expectedBitfield, bitfield.Bitfield);
        }

        [TestMethod]
        [TestCategory("Instruction")]
        [DataRow(RegisterID.X0, (UInt32)0b0000000_00000_00000_000_00000_0000000, (UInt32)0b0000000_00000_00000_000_00000_0000000)]
        [DataRow(RegisterID.X1, (UInt32)0b0000000_00000_00000_000_00000_0000000, (UInt32)0b0000000_00000_00001_000_00000_0000000)]
        [DataRow(RegisterID.X1, (UInt32)0b1010010_01001_00101_010_01001_0010001, (UInt32)0b1010010_01001_00001_010_01001_0010001)]
        [DataRow(RegisterID.X0, (UInt32)0b1010010_01001_00101_010_01001_0010001, (UInt32)0b1010010_01001_00000_010_01001_0010001)]
        public void ShouldSetRs1(RegisterID rs1, UInt32 initialBitfield, UInt32 expectedBitfield)
        {
            Bitfield32 bitfield = initialBitfield;

            bitfield.SetRs1(rs1);

            Assert.AreEqual(expectedBitfield, bitfield.Bitfield);
        }

        [TestMethod]
        [TestCategory("Instruction")]
        [DataRow(RegisterID.X0, (UInt32)0b0000000_00000_00000_000_00000_0000000, (UInt32)0b0000000_00000_00000_000_00000_0000000)]
        [DataRow(RegisterID.X1, (UInt32)0b0000000_00000_00000_000_00000_0000000, (UInt32)0b0000000_00001_00000_000_00000_0000000)]
        [DataRow(RegisterID.X1, (UInt32)0b1010010_01001_00101_010_01001_0010001, (UInt32)0b1010010_00001_00101_010_01001_0010001)]
        [DataRow(RegisterID.X0, (UInt32)0b1010010_01001_00101_010_01001_0010001, (UInt32)0b1010010_00000_00101_010_01001_0010001)]
        public void ShouldSetRs2(RegisterID rs2, UInt32 initialBitfield, UInt32 expectedBitfield)
        {
            Bitfield32 bitfield = initialBitfield;

            bitfield.SetRs2(rs2);

            Assert.AreEqual(expectedBitfield, bitfield.Bitfield);
        }
    }
}
