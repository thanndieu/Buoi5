using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace Thui
{
    public partial class Form1 : Form
    {
        Socket client;
        byte[] data;
        private static void CallAccept(IAsyncResult iar)
        {
            Socket server = (Socket)iar.AsyncState;
            Socket Client = server.EndAccept(iar);

        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 9050);
            sock.Bind(iep);
            sock.Listen(5);
            sock.BeginAccept(new AsyncCallback(CallAccept), sock);
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            listBox1.Items.Add(text);
            textBox2.Text = "";
            data = new byte[1024];
            data = Encoding.ASCII.GetBytes(text);
            client.Send(data);
            client.Receive(data);
            listBox1.Items.Add(Encoding.ASCII.GetString(data));
        }
        
    }
}
