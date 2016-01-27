using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using wyDay.Controls;

namespace Pipes
{
    public partial class ClientForm : Form
    {
        private PipeClient pipeClient;
        private DmdBitmap dmdBitmap;

        public ClientForm()
        {
            InitializeComponent();
            CreateNewPipeClient();

            dmdBitmap = new DmdBitmap(128, 32);
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

        void pipeClient_MessageReceived(int eventType, byte[] message)
        {
            Invoke(new PipeClient.MessageReceivedHandler(DisplayReceivedMessage),
                new object[] { eventType, message });
        }

        void Log(string text)
        {
            tbReceived.Text += text + "\r\n";
        }

        void DisplayReceivedMessage(int eventType, byte[] data)
        {
            /*
            ASCIIEncoding encoder = new ASCIIEncoding();
            string str = encoder.GetString(message, 0, message.Length);

            if(str == "close")
            {
                pipeClient.Disconnect();

                CreateNewPipeClient();
                pipeClient.Connect(tbPipeName.Text);
            }

            tbReceived.Text += str + "\r\n";
             */

            //string text = string.Format("Bytes: {0} [", message.Length);
            //for (int i = 0; i < 10; i++)
            //{
            //    text += string.Format("{0},", message[i]);
            //}
            //text += "]";
            ////tbReceived.Text += text + "\r\n";

            // dmd frame event
            if (eventType == 1)     // todo: enums for event types.
            {
                dmdBitmap.UpdateBitmap(data);
                this.pictureBox1.Image = dmdBitmap.Bitmap;
            }

            // changed lamps event
            if(eventType == 2)  
            {
                //Log(string.Format("id:{0} on: {1}", data[0], data[1]));
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            dmdBitmap.ChangeColor();
            pipeClient.Connect(tbPipeName.Text);

            if (pipeClient.Connected)
                btnStart.Enabled = false;

            Log("Connected to server.");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            pipeClient.Disconnect();
        }

        private void btnSwtichOff_Click(object sender, EventArgs e)
        {
            SendSwitchEvent((int)numSwitchId.Value, false);
        }

        private void btnSwitchOn_Click(object sender, EventArgs e)
        {
            SendSwitchEvent((int)numSwitchId.Value, true);
        }

        private void SendSwitchEvent(int id, bool on)
        {
            byte[] switchData = new byte[2];
            switchData[0] = (byte)id;
            switchData[1] = (byte)(on ? 1 : 0);

            pipeClient.SendMessage(3, switchData);
        }

        private void btnSwitchPulse_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                SendSwitchEvent((int)numSwitchId.Value, true);
                Thread.Sleep(100);
                SendSwitchEvent((int)numSwitchId.Value, false);
            });
        }
    }
}