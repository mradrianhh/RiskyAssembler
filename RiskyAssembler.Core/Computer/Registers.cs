namespace RiskyAssembler.Core.Computer
{
    unsafe public class Registers
    {
        Dictionary<string, Register> _registers;

        public int Length { get; private set; }

        public Registers()
        {
            _registers = new Dictionary<string, Register>();

            for(var i = 0; i < 32; i++)
            {
                Register register = new Register();
                _registers.Add(string.Format("x{0}", i), register);
            }

            Length = _registers.Count;
        }

        public Register this[string identifier]
        {
            get { return _registers[identifier]; }
            set { _registers[identifier] = value; }
        }

        public void ClearRegisters()
        {
            foreach(var key in _registers.Keys)
            {
                _registers[key] = 0;
            }
        }
    }
}
