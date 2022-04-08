using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RiskyAssembler.Core.Computer
{
    [TestClass]
    public class RegistersTests
    {
        [TestMethod]
        public void IndexerShouldReturnRegister()
        {
            Registers _registers = new Registers();

            Assert.IsInstanceOfType(_registers["x0"], typeof(Register));
        }

        [TestMethod]
        public void RegisterCountShouldBe32()
        {
            Registers registers = new Registers();

            Assert.AreEqual(32, registers.Length);
        }
    }
}
