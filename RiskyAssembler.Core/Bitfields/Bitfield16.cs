using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskyAssembler.Core.Bitfields
{
    public class Bitfield16
    {
        public UInt16 Bitfield { get; set; }

        public Bitfield16()
        {
            Bitfield = 0;
        }

        public static implicit operator Bitfield32(Bitfield16 bitfield16)
        {
            Bitfield32 bitfield32 = new Bitfield32();
            bitfield32.Bitfield = bitfield16.Bitfield;
            return bitfield32;
        }

        public static implicit operator UInt16(Bitfield16 bitfield16)
        {
            return bitfield16.Bitfield;
        }

        public static implicit operator int(Bitfield16 bitfield16)
        {
            return bitfield16.Bitfield;
        }

        public static implicit operator Bitfield16(UInt16 a)
        {
            Bitfield16 bitfield = new Bitfield16();
            bitfield.Bitfield = a;
            return bitfield;
        }

        public static Bitfield16 operator &(Bitfield16 a, Bitfield16 b)
        {
            Bitfield16 result = new Bitfield16();
            result.Bitfield = (byte)(a.Bitfield & b.Bitfield);
            return result;
        }

        public static Bitfield16 operator |(Bitfield16 a, Bitfield16 b)
        {
            Bitfield16 result = new Bitfield16();
            result.Bitfield = (byte)(a.Bitfield | b.Bitfield);
            return result;
        }

        public static Bitfield16 operator ^(Bitfield16 a, Bitfield16 b)
        {
            Bitfield16 result = new Bitfield16();
            result.Bitfield = (byte)(a.Bitfield ^ b.Bitfield);
            return result;
        }

        public static Bitfield16 operator ~(Bitfield16 a)
        {
            Bitfield16 result = new Bitfield16();
            result.Bitfield = (byte)(~a.Bitfield);
            return result;
        }

        public static Bitfield16 operator <<(Bitfield16 bitfield, int count)
        {
            Bitfield16 result = new Bitfield16();
            result.Bitfield = (byte)(bitfield.Bitfield << count);
            return result;
        }

        public static Bitfield16 operator >>(Bitfield16 bitfield, int count)
        {
            Bitfield16 result = new Bitfield16();
            result.Bitfield = (byte)(bitfield.Bitfield >> count);
            return result;
        }
    }
}
