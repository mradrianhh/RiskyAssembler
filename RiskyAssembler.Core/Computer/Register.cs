using RiskyAssembler.Core.Bitfields;

namespace RiskyAssembler.Core.Computer
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

        public static implicit operator int(Register register)
        {
            return register._bitfield;
        }

        public static Register operator +(Register register, int a)
        {
            return new Register(register._bitfield + a);
        }

        public static Register operator -(Register register, int a)
        {
            return new Register(register._bitfield - a);
        }

        public static Register operator *(Register register, int a)
        {
            return new Register(register._bitfield * a);
        }

        public static Register operator /(Register register, int a)
        {
            return new Register(register._bitfield / a);
        }
    }
}
