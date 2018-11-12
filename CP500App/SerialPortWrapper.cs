using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP500App
{
    class SerialPortWrapper    
    {
        private string _portName;
        private int _baudRate;
        private Parity _parity;
        private int _dataBits;
        private StopBits _stopBits;
        private string delim = "\r\n";

        private SerialPort port;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="parity"></param>
        /// <param name="dataBits"></param>
        /// <param name="stopBits"></param>
        public SerialPortWrapper(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            _portName = portName;
            _baudRate = baudRate;
            _parity = parity;
            _dataBits = dataBits;
            _stopBits = stopBits;

            port = new SerialPort(_portName, _baudRate, _parity, _dataBits, _stopBits);
        }

        public string PortName { get => _portName; set => _portName = value; }
        public int BaudRate { get => _baudRate; set => _baudRate = value; }
        public Parity Parity { get => _parity; set => _parity = value; }
        public int DataBits { get => _dataBits; set => _dataBits = value; }
        public StopBits StopBits { get => _stopBits; set => _stopBits = value; }

        /// <summary>
        /// Open connection
        /// </summary>
        public void Open()
        {
            port.Handshake = Handshake.RequestToSend;
            // port.DataReceived += new SerialDataReceivedEventHandler(Read_ReceivedHandler);
            port.Open();
            System.Threading.Thread.Sleep(2000);        
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            port.Close();
            port.Dispose();
        }

        /// <summary>
        /// Read a message from CP-500
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Read_ReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = (SerialPort)sender;
            byte[] buf = new byte[1024];
            int len = port.Read(buf, 0, 1024);
            string s = Encoding.GetEncoding("UTF-8").GetString(buf, 0, len);
            Console.Write("received message:" + s);
        }

        /// <summary>
        /// Get position information
        /// </summary>
        /// <returns></returns>
        public  string getPositionInfo()
        {
            port.Write("Q:" + delim);
            // TODO: これ, readのほうがいいのか？
            return port.ReadLine();        
        }
    }
}
