using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace RiskyAssembler.Assembler
{
    [TestClass]
    public class FormatterTests
    {
        [TestMethod]
        public void ShouldCreateLstFile()
        {
            Formatter.Format("test.asm");

            Assert.IsTrue(File.Exists("test.txt"));
        }
    }
}
