using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RiskyAssembler.Assembler
{
    [TestClass]
    public class RV32IParserTests
    {
        private RV32IParser _parser;

        public RV32IParserTests()
        {
            _parser = new RV32IParser();
        }

        [TestMethod]
        [DataRow("ADDI", TokenType.OPCODE)]
        [DataRow("x0", TokenType.REGISTER)]
        [DataRow("1", TokenType.IMMEDIATE)]
        public void ShouldReturnExpectedTokenType(string token, TokenType expectedType)
        {
            TokenType actualType = _parser.GetTokenType(token);

            Assert.AreEqual(expectedType, actualType);
        }

        [TestMethod]
        public void ShouldReturnExpectedTokens()
        {
            string line = "ADDI x1, x0, 1";
            string[] expectedTokens = { "ADDI", "x1", "x0", "1" };

            string[] actualTokens = _parser.GetTokens(line);

            Assert.AreEqual(expectedTokens, actualTokens);
        }
    }
}
