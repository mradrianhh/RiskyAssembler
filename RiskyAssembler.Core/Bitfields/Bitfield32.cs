using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskyAssembler.Core.Bitfields
{
    public class Bitfield32
    {
        public UInt32 Bitfield { get; set; }

        public Bitfield32()
        {
            Bitfield = 0;
        }

        public static implicit operator UInt32(Bitfield32 bitfield32)
        {
            return bitfield32.Bitfield;
        }

        public static implicit operator int(Bitfield32 bitfield32)
        {
            return (int)bitfield32.Bitfield;
        }

        public static implicit operator Bitfield32(UInt32 a)
        {
            Bitfield32 bitfield = new Bitfield32();
            bitfield.Bitfield = a;
            return bitfield;
        }

        public static Bitfield32 operator &(Bitfield32 a, Bitfield32 b)
        {
            Bitfield32 result = new Bitfield32();
            result.Bitfield = (byte)(a.Bitfield & b.Bitfield);
            return result;
        }

        public static Bitfield32 operator |(Bitfield32 a, Bitfield32 b)
        {
            Bitfield32 result = new Bitfield32();
            result.Bitfield = (byte)(a.Bitfield | b.Bitfield);
            return result;
        }

        public static Bitfield32 operator ^(Bitfield32 a, Bitfield32 b)
        {
            Bitfield32 result = new Bitfield32();
            result.Bitfield = (byte)(a.Bitfield ^ b.Bitfield);
            return result;
        }

        public static Bitfield32 operator ~(Bitfield32 a)
        {
            Bitfield32 result = new Bitfield32();
            result.Bitfield = (byte)(~a.Bitfield);
            return result;
        }

        public static Bitfield32 operator <<(Bitfield32 bitfield, int count)
        {
            Bitfield32 result = new Bitfield32();
            result.Bitfield = (byte)(bitfield.Bitfield << count);
            return result;
        }

        public static Bitfield32 operator >>(Bitfield32 bitfield, int count)
        {
            Bitfield32 result = new Bitfield32();
            result.Bitfield = (byte)(bitfield.Bitfield >> count);
            return result;
        }
    }
}
