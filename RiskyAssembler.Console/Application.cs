using RiskyAssembler.Core.Computer;
using RiskyAssembler.Assembler;

// Usage example:

// RiskyAssembler assemble -f/--file [filename]

// Open file [filename].asm
// program = Assembler.Assemble([filename].asm)
// Write program to file [filename].o

// RiskyAssembler create vm -n/--name [name] -fq/--frequency [hz] --startup [startup_script]

// Clock clock = new Clock(hz);
// CPU cpu = new CPU();

// Computer computer = new Computer(clock, cpu);

// RiskyAssembler list vm

// Output:
// [name] [created_date] [status]

// RiskyAssembler load [vm_name] -o [program_file] -l [linker_script]

// RiskyAssembler reset [vm_name] -d [detached]

// computer.Reset();

namespace RiskyAssembler.Console
{
    public class Application
    {
        public static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World!");
        }
    }
}
