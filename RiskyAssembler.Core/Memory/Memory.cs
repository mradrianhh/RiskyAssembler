using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskyAssembler.Core.Memory
{
    public struct Memory
    {
        private byte[] _cells;

        public Memory(int byteSize)
        {
            _cells = new byte[byteSize];
        }

        public byte this[int address]
        {
            get => _cells[address];
            set => _cells[address] = value;
        }

        public byte[] GetSegment(int start, int end)
        {
            byte[] result = new byte[end - start];
            Array.Copy(_cells, start, result, 0, end - start);
            return result;
        }
    }
}
