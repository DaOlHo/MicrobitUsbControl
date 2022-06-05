using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace Sample_Animation
{
    internal class Program
    {

        static void Main(string[] args)
        {
            SerialPort serialPort = new SerialPort("COM3", 115200, Parity.None, 8, StopBits.One)
            {
                Handshake = Handshake.None,
                WriteTimeout = 500
            };

            serialPort.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
            serialPort.Open();

            Console.Read(); // Keep console open
        }

        static void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort serialPort = sender as SerialPort;
            string data = serialPort.ReadLine();

            if (data != null) Console.WriteLine(data);
        }
    }
}
