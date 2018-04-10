using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace SV
{
    public partial class Form1 : Form
    {
        Socket sock;
        byte[] data;
        void ReceivedData(IAsyncResult iar) 
        { 
            Socket remote = (Socket)iar.AsyncState; 
            int recv = remote.EndReceive(iar); 
            string receivedData = Encoding.ASCII.GetString(data, 0, recv); 
            Console.WriteLine(data); 
        }

        public static void Connected(IAsyncResult iar)
        {
            Socket sock = (Socket)iar.AsyncState;
            sock.EndConnect(iar);
        }
        private static void SendData(IAsyncResult iar)
        {
            Socket server = (Socket)iar.AsyncState;
            int sent = server.EndSend(iar);
        } 

        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            newsock.BeginConnect(iep, new AsyncCallback(Connected), newsock);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            listBox1.Items.Add(text);
            textBox2.Text = "";
            data = new byte[1024];
            data = Encoding.ASCII.GetBytes(text);
            
            sock.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendData), sock);


        }
    }
}
