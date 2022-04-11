using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace RiskyAssembler.Core.Computer
{
    [TestClass]
    public class ComputerTests
    {
        private Computer _computer;
        private Mock<IClock> mockClock;
        private Mock<ICPU> mockCPU;

        public ComputerTests()
        {
            mockClock = new Mock<IClock>();
            mockClock.SetupProperty(clock => clock.Frequency, 1);

            mockCPU = new Mock<ICPU>();

            _computer = new Computer(mockClock.Object, mockCPU.Object);
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
