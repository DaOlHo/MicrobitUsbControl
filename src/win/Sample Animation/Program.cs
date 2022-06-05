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

            displayAnimation(serialPort);

            Console.Read(); // Keep console open
        }

        static void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort serialPort = sender as SerialPort;
            string data = serialPort.ReadLine();

            if (data != null) Console.WriteLine(data);
        }

        // In this example, displays two frames of a heart
        static void displayAnimation(SerialPort serialPort)
        {
            const String frame0 = "0101010101100010101000100";
            const String frame1 = "0000001010011100010000000";

            int frame = 0;

            while (true)
            {
                if (frame == 0)
                {
                    string frameCommand = $"Display.matrix {frame0}";

                    serialPort.WriteLine(frameCommand);
                    Console.WriteLine(frameCommand);

                    frame++;
                }
                else
                {
                    string frameCommand = $"Display.matrix {frame1}";

                    serialPort.WriteLine(frameCommand);
                    Console.WriteLine(frameCommand);

                    frame--;
                }

                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
