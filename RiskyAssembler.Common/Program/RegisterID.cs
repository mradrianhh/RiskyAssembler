﻿namespace RiskyAssembler.Common.Program
{
    /// <summary>
    /// The registers decoded from the rd, rs1 and rs2 field of the instruction.
    /// </summary>
    public enum RegisterID
    {
        X0 = 0, X1, X2, X3, X4, X5, X6,
        X7, X8, X9, X10, X11, X12, X13,
        X14, X15, X16, X17, X18, X19, X20,
        X21, X22, X23, X24, X25, X26, X27,
        X28, X29, X30, X31, INVALID = -1
    }

}
