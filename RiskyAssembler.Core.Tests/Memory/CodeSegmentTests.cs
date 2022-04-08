using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RiskyAssembler.Core.Memory
{
    [TestClass]
    public class CodeSegmentTests
    {
        [TestMethod]
        [DataRow(0, 1024, 1024)]
        public void ShouldHaveProperLength(int cstart, int cend, int expectedLength)
        {
            Memory memory = new Memory(1024);
            CodeSegment segment = new CodeSegment(memory, cstart, cend);

            Assert.AreEqual(expectedLength, segment.Length);
        }

        [TestMethod]
        public void IndexerShouldReturnByte()
        {
            Memory memory = new Memory(1024);
            CodeSegment segment = new CodeSegment(memory, 0, 1024);

            Assert.IsInstanceOfType(segment[0], typeof(byte));
        }
    }
}
