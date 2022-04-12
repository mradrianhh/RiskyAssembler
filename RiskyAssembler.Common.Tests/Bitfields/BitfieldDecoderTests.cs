using Microsoft.VisualStudio.TestTools.UnitTesting;
using RiskyAssembler.Common.Program;
using System;

namespace RiskyAssembler.Common.Bitfields
{
    [TestClass]
    public class BitfieldDecoderTests
    {
        [TestMethod]
        [TestCategory("ToInstruction")]
        [DataRow(0b00000_00000_000_00000_0110011, InstructionType.R, RegisterID.X0, RegisterID.X0, RegisterID.X0)]
        [DataRow(0b00001_00001_000_00001_0110011, InstructionType.R, RegisterID.X1, RegisterID.X1, RegisterID.X1)]
        [DataRow(0b00010_00010_000_00010_0110011, InstructionType.R, RegisterID.X2, RegisterID.X2, RegisterID.X2)]
        [DataRow(0b00011_00011_000_00011_0110011, InstructionType.R, RegisterID.X3, RegisterID.X3, RegisterID.X3)]
        public void RInstructionShouldHaveExpectedParameters(int bitfieldValue, InstructionType expectedType, RegisterID expectedRd, RegisterID expectedRs1, RegisterID expectedRs2)
        {
            Bitfield32 bitfield = new Bitfield32((UInt32)bitfieldValue);

            Instruction result = bitfield.ToInstruction();

            Assert.AreEqual(expectedType, result.Type);
            Assert.AreEqual(expectedRd, result.Rd);
            Assert.AreEqual(expectedRs1, result.Rs1);
            Assert.AreEqual(expectedRs2, result.Rs2);
        }

        [TestMethod]
        [TestCategory("ToInstruction")]
        [DataRow(0b00000_00000_000_00000_0010011, InstructionType.I, RegisterID.X0, RegisterID.X0)]
        [DataRow(0b00001_00001_000_00001_0010011, InstructionType.I, RegisterID.X1, RegisterID.X1)]
        [DataRow(0b00010_00010_000_00010_0010011, InstructionType.I, RegisterID.X2, RegisterID.X2)]
        [DataRow(0b00011_00011_000_00011_0010011, InstructionType.I, RegisterID.X3, RegisterID.X3)]
        public void IInstructionShouldHaveExpectedParameters(int bitfieldValue, InstructionType expectedType, RegisterID expectedRd, RegisterID expectedRs1)
        {
            Bitfield32 bitfield = new Bitfield32((UInt32)bitfieldValue);

            Instruction result = bitfield.ToInstruction();

            Assert.AreEqual(expectedType, result.Type);
            Assert.AreEqual(expectedRd, result.Rd);
            Assert.AreEqual(expectedRs1, result.Rs1);
        }


        [TestMethod]
        [TestCategory("ToInstruction")]
        [DataRow(0b00000_00000_000_00000_0100011, InstructionType.S, RegisterID.X0, RegisterID.X0)]
        [DataRow(0b00001_00001_000_00001_0100011, InstructionType.S, RegisterID.X1, RegisterID.X1)]
        [DataRow(0b00010_00010_000_00010_0100011, InstructionType.S, RegisterID.X2, RegisterID.X2)]
        [DataRow(0b00011_00011_000_00011_0100011, InstructionType.S, RegisterID.X3, RegisterID.X3)]
        public void SInstructionShouldHaveExpectedParameters(int bitfieldValue, InstructionType expectedType, RegisterID expectedRs1, RegisterID expectedRs2)
        {
            Bitfield32 bitfield = new Bitfield32((UInt32)bitfieldValue);

            Instruction result = bitfield.ToInstruction();

            Assert.AreEqual(expectedType, result.Type);
            Assert.AreEqual(expectedRs1, result.Rs1);
            Assert.AreEqual(expectedRs2, result.Rs2);
        }

        [TestMethod]
        [TestCategory("ToInstruction")]
        [DataRow(0b00000_00000_000_00000_1100011, InstructionType.B, RegisterID.X0, RegisterID.X0)]
        [DataRow(0b00001_00001_000_00001_1100011, InstructionType.B, RegisterID.X1, RegisterID.X1)]
        [DataRow(0b00010_00010_000_00010_1100011, InstructionType.B, RegisterID.X2, RegisterID.X2)]
        [DataRow(0b00011_00011_000_00011_1100011, InstructionType.B, RegisterID.X3, RegisterID.X3)]
        public void BInstructionShouldHaveExpectedParameters(int bitfieldValue, InstructionType expectedType, RegisterID expectedRs1, RegisterID expectedRs2)
        {
            Bitfield32 bitfield = new Bitfield32((UInt32)bitfieldValue);

            Instruction result = bitfield.ToInstruction();

            Assert.AreEqual(expectedType, result.Type);
            Assert.AreEqual(expectedRs1, result.Rs1);
            Assert.AreEqual(expectedRs2, result.Rs2);
        }
    }
}
