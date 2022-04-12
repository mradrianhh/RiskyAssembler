using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RiskyAssembler.Common.Bitfields
{
    [TestClass]
    public class Bitfield8Tests
    {
        private Bitfield8 _bitfield;

        public Bitfield8Tests()
        {
            _bitfield = new Bitfield8();
        }


        [TestMethod]
        public void FieldShouldBeOfTypeByte()
        {
            Assert.IsInstanceOfType(_bitfield.Bitfield, typeof(byte));
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
        public void ShouldBeImplicitCastableToBitfield16()
        {
            Bitfield16 bitfield16 = _bitfield;

            Assert.IsInstanceOfType(bitfield16, typeof(Bitfield16));
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
            Bitfield8 a = 0;
            Bitfield8 b = 1;
            Bitfield8 result = a & b;

            Assert.AreEqual(0, (int)result);
        }

        [TestMethod]
        public void ShouldSupportBitwiseOR()
        {
            Bitfield8 a = 0;
            Bitfield8 b = 1;
            Bitfield8 result = a | b;

            Assert.AreEqual(1, (int)result);
        }

        [TestMethod]
        public void ShouldSupportBitwiseXOR()
        {
            Bitfield8 a = 1;
            Bitfield8 b = 1;
            Bitfield8 result = a ^ b;

            Assert.AreEqual(0, (int)result);
        }

        [TestMethod]
        public void ShouldSupportBitwiseUnary()
        {
            Bitfield8 a = byte.MaxValue;
            Bitfield8 result = ~a;

            Assert.AreEqual(0, (int)result);
        }

        [TestMethod]
        public void ShouldSupportBitwiseLeftShift()
        {
            Bitfield8 a = 1;
            Bitfield8 result = a << 1;

            Assert.AreEqual(2, (int)result);
        }
         
        [TestMethod]
        public void ShouldSupportBitwiseRightShift()
        {
            Bitfield8 a = 2;
            Bitfield8 result = a >> 1;

            Assert.AreEqual(1, (int)result);
        }

        [TestMethod]
        public void ShouldBeConvertableToByte()
        {
            _bitfield.Bitfield = 8;
            byte bitfieldValue = _bitfield;

            Assert.IsInstanceOfType(bitfieldValue, typeof(byte));
        }

        [TestMethod]
        public void ShouldBeConvertableToInt()
        {
            _bitfield.Bitfield = 8;
            int bitfieldValue = _bitfield;

            Assert.IsInstanceOfType(bitfieldValue, typeof(int));
        }
    }
}
