using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RiskyAssembler.Core.Memory
{
    [TestClass]
    public class DataSegmentTests
    {
        [TestMethod]
        [DataRow(0, 1024, 1024)]
        public void ShouldHaveProperLength(int dstart, int dend, int expectedLength)
        {
            Memory memory = new Memory(1024);
            DataSegment segment = new DataSegment(memory, 0, 1024);

            Assert.AreEqual(expectedLength, segment.Length);
        }

        [TestMethod]
        public void IndexerShouldReturnByte()
        {
            Memory memory = new Memory(1024);
            DataSegment segment = new DataSegment(memory, 0, 1024);

            Assert.IsInstanceOfType(segment[0], typeof(byte));
        }
    }
}
