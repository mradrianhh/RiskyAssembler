namespace RiskyAssembler.Common.Bitfields
{
    public struct Bitfield32
    {
        private UInt32 _bitfield = 0;
        public UInt32 Bitfield
        {
            get { return _bitfield; }
            set
            {
                _bitfield = value;
                CalculateBytes();
            }
        }

        private Bitfield16 _upperHalfword = 0;
        public Bitfield16 UpperHalfword
        {
            get { return _upperHalfword; }
            set
            {
                _upperHalfword = value;
                CalculateBitfield();
            }
        }

        private Bitfield16 _lowerHalfword = 0;
        public Bitfield16 LowerHalfword
        {
            get { return _lowerHalfword; }
            set
            {
                _lowerHalfword = value;
                CalculateBitfield();
            }
        }

        public Bitfield32(UInt32 value)
        {
            Bitfield = value;
        }

        public Bitfield32(Bitfield16 upper, Bitfield16 lower)
        {
            // Set manually to avoid calling CalculateBitfield twice.
            _upperHalfword = upper;
            _lowerHalfword = lower;
            CalculateBitfield();
        }

        #region Overloads and castings

        public static implicit operator UInt32(Bitfield32 bitfield32)
        {
            return bitfield32.Bitfield;
        }

        public static implicit operator int(Bitfield32 bitfield32)
        {
            return (int)bitfield32.Bitfield;
        }

        public static implicit operator Bitfield32(int a)
        {
            return new Bitfield32((UInt32)a);
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
            result.Bitfield = (UInt32)(a.Bitfield & b.Bitfield);
            return result;
        }

        public static Bitfield32 operator |(Bitfield32 a, Bitfield32 b)
        {
            Bitfield32 result = new Bitfield32();
            result.Bitfield = (UInt32)(a.Bitfield | b.Bitfield);
            return result;
        }

        public static Bitfield32 operator ^(Bitfield32 a, Bitfield32 b)
        {
            Bitfield32 result = new Bitfield32();
            result.Bitfield = (UInt32)(a.Bitfield ^ b.Bitfield);
            return result;
        }

        public static Bitfield32 operator ~(Bitfield32 a)
        {
            Bitfield32 result = new Bitfield32();
            result.Bitfield = (UInt32)(~a.Bitfield);
            return result;
        }

        public static Bitfield32 operator <<(Bitfield32 bitfield, int count)
        {
            Bitfield32 result = new Bitfield32();
            result.Bitfield = (UInt32)(bitfield.Bitfield << count);
            return result;
        }

        public static Bitfield32 operator >>(Bitfield32 bitfield, int count)
        {
            Bitfield32 result = new Bitfield32();
            result.Bitfield = (UInt32)(bitfield.Bitfield >> count);
            return result;
        }

        #endregion

        private void CalculateBitfield()
        {
            Bitfield32 upper = _upperHalfword;
            upper <<= 16;
            Bitfield32 lower = _lowerHalfword;
            _bitfield = upper | lower;
        }

        private void CalculateBytes()
        {
            // By shifting the upper 16 bits down 16 bits, we naturally get the UpperHalfword extracted due to C# truncating the UInt32 into a UInt16.
            UInt32 upperMask = 0xFFFF << 8;
            UInt32 upper = (UInt32)(_bitfield & upperMask);
            _upperHalfword = upper >> 16;

            // Again, C# truncates the upper 16 bits leaving us with the LowerHalfword.
            UInt32 lowerMask = 0xFFFF;
            UInt32 lower = (UInt32)(_bitfield & lowerMask);
            _lowerHalfword = lower;
        }
    }
}
