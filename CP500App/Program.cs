using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP500App
{
    class Program
    {
        static void Main(string[] args)
        {
            SerialPortWrapper port = new SerialPortWrapper("COM4", 38400, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);

            port.Open();

            Console.WriteLine(port.getPositionInfo());

        }
    }
}
