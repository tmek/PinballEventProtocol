using System;
using System.Text;
using System.Windows.Forms;
using wyUpdate;

namespace Pipes
{
    public partial class Form1 : Form
    {
        private PipeServer pipeServer = new PipeServer();

        public Form1()
        {
            InitializeComponent();

            pipeServer.MessageReceived += pipeServer_MessageReceived;
            pipeServer.ClientDisconnected += pipeServer_ClientDisconnected;
        }

        void pipeServer_ClientDisconnected()
        {
            Invoke(new PipeServer.ClientDisconnectedHandler(ClientDisconnected));
        }

        void ClientDisconnected()
        {
            MessageBox.Show("Total connected clients: " + pipeServer.TotalConnectedClients);
        }

        void pipeServer_MessageReceived(byte[] message)
        {
            Invoke(new PipeServer.MessageReceivedHandler(DisplayMessageReceived),
                new object[] { message });   
        }

        void DisplayMessageReceived(byte[] message)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            string str = encoder.GetString(message, 0, message.Length);

            tbReceived.Text += str + "\r\n";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //start the pipe server if it's not already running
            if (!pipeServer.Running)
            {
                pipeServer.Start(tbPipeName.Text);
                btnStart.Enabled = false;
            }
            else
                MessageBox.Show("Server already running.");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] messageBuffer = encoder.GetBytes(tbSend.Text);

            pipeServer.SendMessage(messageBuffer);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbReceived.Clear();
        }
    }
}