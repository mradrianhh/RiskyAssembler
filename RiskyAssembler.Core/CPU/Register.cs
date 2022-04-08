using RiskyAssembler.Core.Bitfields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskyAssembler.Core.CPU
{
    public struct Register
    {
        private Bitfield32 _bitfield;

        public Register()
        {
            _bitfield = new Bitfield32();
        }

        public Register(int value)
        {
            _bitfield = value;
        }

        public static implicit operator Bitfield32(Register register)
        {
            return register._bitfield; 
        }

        public static implicit operator Register(int a)
        {
            return new Register(a);
        }

        public static implicit operator Register(UInt32 a)
        {
            return new Register((int)a);
        }

        public static implicit operator Register(UInt16 a)
        {
            return new Register(a);
        }

        public static implicit operator Register(byte a)
        {
            return new Register(a);
        }

        public static implicit operator Register(Bitfield32 bitfield)
        {
            return new Register(bitfield);
        }

        public static implicit operator Register(Bitfield16 bitfield)
        {
            return new Register(bitfield);
        }

        public static implicit operator Register(Bitfield8 bitfield)
        {
            return new Register(bitfield);
        }
    }
}
