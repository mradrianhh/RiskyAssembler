using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RiskyAssembler.Core.Memory
{
    [TestClass]
    public class MemoryTests
    {

        [TestMethod]
        public void IndexerShouldReturnByte()
        {
            Memory memory = new Memory(1024);
            var memoryCell = memory[0];

            Assert.IsInstanceOfType(memoryCell, typeof(byte));
        }

        [TestMethod]
        [DataRow(0, 1024, 1024)]
        [DataRow(0, 10, 10)]
        [DataRow(58, 250, 250-58)]
        public void GetSegmentShouldReturnArrayOfProperLength(int start, int end, int expectedLength)
        {
            Memory memory = new Memory(1024);
            byte[] segment = memory.GetSegment(start, end);

            Assert.AreEqual(expectedLength, segment.Length);
        }

    }
}
