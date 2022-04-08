using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RiskyAssembler.Core.Computer
{
    [TestClass]
    public class ComputerTests
    {
        private Computer _computer;

        public ComputerTests()
        {
            _computer = new Computer();
        }

        [TestMethod]
        public void ShouldHaveClock()
        {
            Assert.IsNotNull(_computer.Clock);
        }

        [TestMethod]
        public void ShouldHaveCPU()
        {
            Assert.IsNotNull(_computer.CPU);
        }
    }
}
