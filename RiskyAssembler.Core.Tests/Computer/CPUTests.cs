using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RiskyAssembler.Core.Computer
{

    [TestClass]
    public class CPUTests
    {
        [TestMethod]
        public void ShouldHave32GeneralRegisters()
        {
            CPU cpu = new CPU();

            Assert.AreEqual(32, cpu.Registers.Length);
        }

        [TestMethod]
        public void ShouldHavePCAndSTATUSRegister()
        {
            CPU cpu = new CPU();

            Assert.IsNotNull(cpu.PC);
            Assert.IsNotNull(cpu.STATUS);
        }

        [TestMethod]
        public void ShouldBeAbleToToggleSTATUS()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void ShouldBeAbleToFetchInstructionFromMemory()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void ShouldIncrementPCByFour()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void ShouldBeAbleToLoadStoreMemory()
        {
            Assert.Inconclusive();
        }
    }
}
