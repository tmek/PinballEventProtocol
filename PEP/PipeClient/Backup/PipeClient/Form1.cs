using System;
using System.Text;
using System.Windows.Forms;
using wyDay.Controls;

namespace Pipes
{
    public partial class Form1 : Form
    {
        private PipeClient pipeClient;

        public Form1()
        {
            InitializeComponent();
            CreateNewPipeClient();
        }

        void CreateNewPipeClient()
        {
            if(pipeClient != null)
            {
                pipeClient.MessageReceived -= pipeClient_MessageReceived;
                pipeClient.ServerDisconnected -= pipeClient_ServerDisconnected; 
            }

            pipeClient = new PipeClient();
            pipeClient.MessageReceived += pipeClient_MessageReceived;
            pipeClient.ServerDisconnected += pipeClient_ServerDisconnected;
        }

        void pipeClient_ServerDisconnected()
        {
            Invoke(new PipeClient.ServerDisconnectedHandler(EnableStartButton));
        }

        void EnableStartButton()
        {
            btnStart.Enabled = true;
        }

        void pipeClient_MessageReceived(byte[] message)
        {
            Invoke(new PipeClient.MessageReceivedHandler(DisplayReceivedMessage),
                new object[] { message });
        }

        void DisplayReceivedMessage(byte[] message)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            string str = encoder.GetString(message, 0, message.Length);

            if(str == "close")
            {
                pipeClient.Disconnect();

                CreateNewPipeClient();
                pipeClient.Connect(tbPipeName.Text);
            }

            tbReceived.Text += str + "\r\n";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            pipeClient.Connect(tbPipeName.Text);

            if (pipeClient.Connected)
                btnStart.Enabled = false;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();

            pipeClient.SendMessage(encoder.GetBytes(tbSend.Text));
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbReceived.Clear();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            pipeClient.Disconnect();
            EnableStartButton();
        }
    }
}