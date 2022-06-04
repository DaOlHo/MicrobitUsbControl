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

            RotatableHeart rotatableHeart = new RotatableHeart(serialPort);

            serialPort.DataReceived += new SerialDataReceivedEventHandler(rotatableHeart.sp_DataReceived);
            serialPort.Open();

            Console.Read(); // Keep console open
        }

        static void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort serialPort = sender as SerialPort;
            string data = serialPort.ReadLine();

            if (data != null) Console.WriteLine($"Button pressed: {data}");
        }
    }

    class RotatableHeart
    {
        int heartRotation;
        string currentFrame;
        SerialPort serialPort;

        public RotatableHeart(SerialPort serialPort)
        {
            heartRotation = 0;
            currentFrame = "0101010101100010101000100";
            this.serialPort = serialPort;
        }

        public void displayCurrentFrame()
        {
            serialPort.WriteLine(currentFrame);
            Console.WriteLine($"Frame out: {currentFrame}");
        }

        public void rotateFrame(int rotQuarter)
        {
            if (heartRotation == 0 && rotQuarter == -1) heartRotation = 3;
            else if (heartRotation == 3 && rotQuarter == 1) heartRotation = 0;
            else heartRotation += rotQuarter;

            switch (heartRotation)
            {
                case 0: currentFrame = "0101010101100010101000100"; break;
                case 1: currentFrame = "0011001001100100100100110"; break;
                case 2: currentFrame = "0010001010100011010101010"; break;
                case 3: currentFrame = "0110010010010011001001100"; break;
            }
        }

        public void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort serialPort = sender as SerialPort;
            string data = serialPort.ReadLine().Trim();

            if (data == "A" || data == "AB" || data == "B")
            {
                Console.WriteLine($"Button pressed: {data}");

                int rotQuarter = 0;

                if (data == "A") rotQuarter = -1;
                else if (data == "B") rotQuarter = 1;

                rotateFrame(rotQuarter);
                displayCurrentFrame();
            }
        }
    }
}