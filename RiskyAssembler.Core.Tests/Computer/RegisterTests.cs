using Microsoft.VisualStudio.TestTools.UnitTesting;
using RiskyAssembler.Common.Bitfields;
using System;

namespace RiskyAssembler.Core.Computer
{
    [TestClass]
    public class RegisterTests
    {
        private Register _register;

        public RegisterTests()
        {
            _register = new Register();
        }

        [TestMethod]
        public void ShouldBeConvertableToBitfield32()
        {
            Bitfield32 bitfield = _register;

            Assert.IsInstanceOfType(bitfield, typeof(Bitfield32));
        }

        [TestMethod]
        public void IntShouldBeAssignable()
        {
            _register = 1;

            Assert.IsInstanceOfType(_register, typeof(Register));
        }

        [TestMethod]
        public void BitfieldShouldBeAssignable()
        {
            _register = new Bitfield8();

            Assert.IsInstanceOfType(_register, typeof(Register));

            _register = new Bitfield16();

            Assert.IsInstanceOfType(_register, typeof(Register));

            _register = new Bitfield32();

            Assert.IsInstanceOfType(_register, typeof(Register));
        }

        [TestMethod]
        public void UIntShouldBeAssignable()
        {
            _register = (UInt16)1;

            Assert.IsInstanceOfType(_register, typeof(Register));

            _register = (UInt32)1;

            Assert.IsInstanceOfType(_register, typeof(Register));
        }
    }
}
