namespace RiskyAssembler.Common.Program
{
    /// <summary>
    /// Decoded from the opcode field of the instruction.
    /// </summary>
    public enum Opcode
    {
        R = 0b0110011, 
        I = 0b0010011, 
        S = 0b0100011, 
        B = 0b1100011
    }

}
