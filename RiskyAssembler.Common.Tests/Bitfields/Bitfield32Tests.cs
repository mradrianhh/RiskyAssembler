using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RiskyAssembler.Common.Bitfields
{
    [TestClass]
    public class Bitfield32Tests
    {
        private Bitfield32 _bitfield;

        public Bitfield32Tests()
        {
            _bitfield = new Bitfield32();
        }


        [TestMethod]
        public void FieldShouldBeOfTypeUInt32()
        {
            Assert.IsInstanceOfType(_bitfield.Bitfield, typeof(UInt32));
        }

        [TestMethod]
        public void ShouldBeInitializedToZero()
        {
            Assert.AreEqual(0, (int)_bitfield.Bitfield);
        }

        [TestMethod]
        public void ShouldConvertToString()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void ShouldBeAbleToToggleIndividualBit()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void ShouldSupportBitwiseAND()
        {
            Bitfield32 a = 0;
            Bitfield32 b = 1;
            Bitfield32 result = a & b;

            Assert.AreEqual(0, (int)result);
        }

        [TestMethod]
        public void ShouldSupportBitwiseOR()
        {
            Bitfield32 a = 0;
            Bitfield32 b = 1;
            Bitfield32 result = a | b;

            Assert.AreEqual(1, (int)result);
        }

        [TestMethod]
        public void ShouldSupportBitwiseXOR()
        {
            Bitfield32 a = 1;
            Bitfield32 b = 1;
            Bitfield32 result = a ^ b;

            Assert.AreEqual(0, (int)result);
        }

        [TestMethod]
        public void ShouldSupportBitwiseUnary()
        {
            Bitfield32 a = UInt32.MaxValue;
            Bitfield32 result = ~a;

            Assert.AreEqual(0, (int)result);
        }

        [TestMethod]
        public void ShouldSupportBitwiseLeftShift()
        {
            Bitfield32 a = 1;
            Bitfield32 result = a << 1;

            Assert.AreEqual(2, (int)result);
        }

        [TestMethod]
        public void ShouldSupportBitwiseRightShift()
        {
            Bitfield32 a = 2;
            Bitfield32 result = a >> 1;

            Assert.AreEqual(1, (int)result);
        }

        [TestMethod]
        public void ShouldBeConvertableToUInt32()
        {
            _bitfield.Bitfield = 8;
            UInt32 bitfieldValue = _bitfield;

            Assert.IsInstanceOfType(bitfieldValue, typeof(UInt32));
        }

        [TestMethod]
        public void ShouldBeConvertableToInt()
        {
            _bitfield.Bitfield = 8;
            int bitfieldValue = _bitfield;

            Assert.IsInstanceOfType(bitfieldValue, typeof(int));
        }

        [TestMethod]
        public void BothHalfwordShouldBeOfTypeBitfield16()
        {
            Assert.IsInstanceOfType(_bitfield.UpperHalfword, typeof(Bitfield16));
            Assert.IsInstanceOfType(_bitfield.LowerHalfword, typeof(Bitfield16));
        }

        [TestMethod]
        public void SettingHalfwordShouldUpdateBitfield()
        {
            int valueBeforeUpdate = _bitfield;

            _bitfield.UpperHalfword = 1;

            Assert.AreNotEqual(valueBeforeUpdate, (int)_bitfield.Bitfield);

            valueBeforeUpdate = _bitfield;

            _bitfield.LowerHalfword = 1;

            Assert.AreNotEqual(valueBeforeUpdate, (int)_bitfield.Bitfield);
        }

        [TestMethod]
        public void SettingBitfieldShouldUpdateHalfwords()
        {
            int upperBefore = _bitfield.UpperHalfword;
            int lowerBefore = _bitfield.LowerHalfword;

            _bitfield.Bitfield = 65537;

            Assert.AreNotEqual(upperBefore, _bitfield.UpperHalfword);
            Assert.AreNotEqual(lowerBefore, _bitfield.LowerHalfword);
        }
    }
}
