using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace RiskyAssembler.Assembler
{
    [TestClass]
    public class FormatterTests
    {
        private Formatter _formatter;

        public FormatterTests()
        {
            _formatter = new Formatter();
        }

        [TestMethod]
        public void ShouldCreateLstFile()
        {
            _formatter.Format("test.asm");

            Assert.IsTrue(File.Exists("test.txt"));
        }
    }
}
