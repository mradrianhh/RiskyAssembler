using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskyAssembler.Core.Bitfields
{
    public struct Bitfield8
    {
        public byte Bitfield { get; set; }

        public Bitfield8(byte value = 0)
        {
            Bitfield = value;
        }

        #region Overloads and castings

        public static implicit operator Bitfield16(Bitfield8 bitfield8)
        {
            Bitfield16 bitfield16 = new Bitfield16();
            bitfield16.Bitfield = bitfield8.Bitfield;
            return bitfield16;
        }

        public static implicit operator Bitfield32(Bitfield8 bitfield8)
        {
            Bitfield32 bitfield32 = new Bitfield32();
            bitfield32.Bitfield = bitfield8.Bitfield;
            return bitfield32;
        }

        public static implicit operator byte(Bitfield8 bitfield8)
        {
            return bitfield8.Bitfield;
        }

        public static implicit operator int(Bitfield8 bitfield8)
        {
            return bitfield8.Bitfield;
        }

        public static implicit operator Bitfield8(int a)
        {
            return new Bitfield8((byte)a);
        }

        public static implicit operator Bitfield8(byte a)
        {
            Bitfield8 bitfield = new Bitfield8();
            bitfield.Bitfield = a;
            return bitfield;
        }

        public static Bitfield8 operator &(Bitfield8 a, Bitfield8 b)
        {
            Bitfield8 result = new Bitfield8();
            result.Bitfield = (byte)(a.Bitfield & b.Bitfield);
            return result;
        }

        public static Bitfield8 operator |(Bitfield8 a, Bitfield8 b)
        {
            Bitfield8 result = new Bitfield8();
            result.Bitfield = (byte)(a.Bitfield | b.Bitfield);
            return result;
        }

        public static Bitfield8 operator ^(Bitfield8 a, Bitfield8 b)
        {
            Bitfield8 result = new Bitfield8();
            result.Bitfield = (byte)(a.Bitfield ^ b.Bitfield);
            return result;
        }

        public static Bitfield8 operator ~(Bitfield8 a)
        {
            Bitfield8 result = new Bitfield8();
            result.Bitfield = (byte)(~a.Bitfield);
            return result;
        }

        public static Bitfield8 operator <<(Bitfield8 bitfield, int count)
        {
            Bitfield8 result = new Bitfield8();
            result.Bitfield = (byte)(bitfield.Bitfield << count);
            return result;
        }

        public static Bitfield8 operator >>(Bitfield8 bitfield, int count)
        {
            Bitfield8 result = new Bitfield8();
            result.Bitfield = (byte)(bitfield.Bitfield >> count);
            return result;
        }
        
        #endregion
    }
}
