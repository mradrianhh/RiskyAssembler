using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RiskyAssembler.Common.Bitfields
{
    [TestClass]
    public class Bitfield16Tests
    {
        private Bitfield16 _bitfield;

        public Bitfield16Tests()
        {
            _bitfield = new Bitfield16();
        }


        [TestMethod]
        public void FieldShouldBeOfTypeUInt16()
        {
            Assert.IsInstanceOfType(_bitfield.Bitfield, typeof(UInt16));
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
        public void ShouldBeImplicitCastableToBitfield32()
        {
            Bitfield32 bitfield32 = _bitfield;

            Assert.IsInstanceOfType(bitfield32, typeof(Bitfield32));
        }

        [TestMethod]
        public void ShouldBeAbleToToggleIndividualBit()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void ShouldSupportBitwiseAND()
        {
            Bitfield16 a = 0;
            Bitfield16 b = 1;
            Bitfield16 result = a & b;

            Assert.AreEqual(0, (int)result);
        }

        [TestMethod]
        public void ShouldSupportBitwiseOR()
        {
            Bitfield16 a = 0;
            Bitfield16 b = 1;
            Bitfield16 result = a | b;

            Assert.AreEqual(1, (int)result);
        }

        [TestMethod]
        public void ShouldSupportBitwiseXOR()
        {
            Bitfield16 a = 1;
            Bitfield16 b = 1;
            Bitfield16 result = a ^ b;

            Assert.AreEqual(0, (int)result);
        }

        [TestMethod]
        public void ShouldSupportBitwiseUnary()
        {
            Bitfield16 a = UInt16.MaxValue;
            Bitfield16 result = ~a;

            Assert.AreEqual(0, (int)result);
        }

        [TestMethod]
        public void ShouldSupportBitwiseLeftShift()
        {
            Bitfield16 a = 1;
            Bitfield16 result = a << 1;

            Assert.AreEqual(2, (int)result);
        }

        [TestMethod]
        public void ShouldSupportBitwiseRightShift()
        {
            Bitfield16 a = 2;
            Bitfield16 result = a >> 1;

            Assert.AreEqual(1, (int)result);
        }

        [TestMethod]
        public void ShouldBeConvertableToUInt16()
        {
            _bitfield.Bitfield = 8;
            UInt16 bitfieldValue = _bitfield;

            Assert.IsInstanceOfType(bitfieldValue, typeof(UInt16));
        }

        [TestMethod]
        public void ShouldBeConvertableToInt()
        {
            _bitfield.Bitfield = 8;
            int bitfieldValue = _bitfield;

            Assert.IsInstanceOfType(bitfieldValue, typeof (int));
        }

        [TestMethod]
        public void BothByteShouldBeOfTypeBitfield8()
        {
            Assert.IsInstanceOfType(_bitfield.UpperByte, typeof(Bitfield8));
            Assert.IsInstanceOfType(_bitfield.LowerByte, typeof(Bitfield8));
        }

        [TestMethod]
        public void SettingByteShouldUpdateBitfield()
        {
            int valueBeforeUpdate = _bitfield;

            _bitfield.UpperByte = 1;

            Assert.AreNotEqual(valueBeforeUpdate, (int)_bitfield.Bitfield);

            valueBeforeUpdate = _bitfield;

            _bitfield.LowerByte = 1;

            Assert.AreNotEqual(valueBeforeUpdate, (int)_bitfield.Bitfield);
        }

        [TestMethod]
        public void SettingBitfieldShouldUpdateBytes()
        {
            int upperBefore = _bitfield.UpperByte;
            int lowerBefore = _bitfield.LowerByte;

            _bitfield.Bitfield = 257;

            Assert.AreNotEqual(upperBefore, _bitfield.UpperByte);
            Assert.AreNotEqual(lowerBefore, _bitfield.LowerByte);
        }
    }
}
