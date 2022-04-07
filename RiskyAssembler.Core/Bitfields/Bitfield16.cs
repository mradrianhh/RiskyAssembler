using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskyAssembler.Core.Bitfields
{
    public class Bitfield16
    {
        private UInt16 _bitfield;
        public UInt16 Bitfield
        {
            get { return _bitfield; }
            set
            {
                _bitfield = value;
                CalculateBytes();
            }
        }

        private Bitfield8 _upperByte;
        public Bitfield8 UpperByte
        {
            get { return _upperByte; }
            set
            {
                _upperByte = value;
                CalculateBitfield();
            }
        }

        private Bitfield8 _lowerByte;
        public Bitfield8 LowerByte
        {
            get { return _lowerByte; }
            set
            {
                _lowerByte = value;
                CalculateBitfield();
            }
        }

        public Bitfield16()
        {
            Bitfield = 0;
        }

        public Bitfield16(UInt16 value)
        {
            Bitfield = value;
        }

        public Bitfield16(Bitfield8 upper, Bitfield8 lower)
        {
            // Set manually to avoid calling CalculateBitfield twice.
            _upperByte = upper;
            _lowerByte = lower;
            CalculateBitfield();
        }

        #region Overloads and castings

        public static implicit operator Bitfield32(Bitfield16 bitfield16)
        {
            return new Bitfield32 (bitfield16.Bitfield);
        }

        public static implicit operator UInt16(Bitfield16 bitfield16)
        {
            return bitfield16.Bitfield;
        }

        public static implicit operator int(Bitfield16 bitfield16)
        {
            return bitfield16.Bitfield;
        }

        public static implicit operator Bitfield16(int a)
        {
            return new Bitfield16((UInt16)a);
        }

        public static implicit operator Bitfield16(UInt16 a)
        {
            return new Bitfield16(a);
        }

        public static implicit operator Bitfield16(UInt32 a)
        {
            return new Bitfield16((UInt16)a);
        }


        public static Bitfield16 operator &(Bitfield16 a, Bitfield16 b)
        {
            Bitfield16 result = new Bitfield16();
            result.Bitfield = (UInt16)(a.Bitfield & b.Bitfield);
            return result;
        }

        public static Bitfield16 operator |(Bitfield16 a, Bitfield16 b)
        {
            Bitfield16 result = new Bitfield16();
            result.Bitfield = (UInt16)(a.Bitfield | b.Bitfield);
            return result;
        }

        public static Bitfield16 operator ^(Bitfield16 a, Bitfield16 b)
        {
            Bitfield16 result = new Bitfield16();
            result.Bitfield = (UInt16)(a.Bitfield ^ b.Bitfield);
            return result;
        }

        public static Bitfield16 operator ~(Bitfield16 a)
        {
            Bitfield16 result = new Bitfield16();
            result.Bitfield = (UInt16)(~a.Bitfield);
            return result;
        }

        public static Bitfield16 operator <<(Bitfield16 bitfield, int count)
        {
            Bitfield16 result = new Bitfield16();
            result.Bitfield = (UInt16)(bitfield.Bitfield << count);
            return result;
        }

        public static Bitfield16 operator >>(Bitfield16 bitfield, int count)
        {
            Bitfield16 result = new Bitfield16();
            result.Bitfield = (UInt16)(bitfield.Bitfield >> count);
            return result;
        }

        #endregion

        private void CalculateBitfield()
        {
            Bitfield16 upper = _upperByte;
            upper <<= 8;
            Bitfield16 lower = _lowerByte;
            _bitfield = upper | lower;
        }

        private void CalculateBytes()
        {
            // By shifting the upper 8 bits down 8 bits, we naturally get the UpperByte extracted due to C# truncating the UInt16 into a byte.
            UInt16 upperMask = 0xFF << 8;
            UInt16 upper = (UInt16)(_bitfield & upperMask);
            _upperByte = upper >> 8;

            // Again, C# truncates the upper 8 bits leaving us with the lower byte.
            UInt16 lowerMask = 0xFF;
            UInt16 lower = (UInt16)(_bitfield & lowerMask);
            _lowerByte = lower;
        }
    }
}
