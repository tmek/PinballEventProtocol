using System;
using System.Text;
using System.Windows.Forms;
using wyUpdate;
using VPinMAMELib;

namespace Pipes
{
    public partial class ServerForm : Form
    {
        private PipeServer pipeServer = new PipeServer();
        private IController vpm = new Controller();
        private byte[] dmdFrameBytes;

        public ServerForm()
        {
            InitializeComponent();

            pipeServer.MessageReceived += pipeServer_MessageReceived;
            pipeServer.ClientDisconnected += pipeServer_ClientDisconnected;
            pipeServer.ClientConnected += pipeServer_ClientConnected;
        }

        void pipeServer_ClientConnected()
        {
            Invoke(new PipeServer.ClientConnectedHandler(ClientConnected));
        }

        void pipeServer_ClientDisconnected()
        {
            Invoke(new PipeServer.ClientDisconnectedHandler(ClientDisconnected));
        }

        void Log(string text)
        {
            tbReceived.Text += text + "\r\n";
        }

        void ClientConnected()
        {
            Log("Client connected.");
        }

        void ClientDisconnected()
        {
            Log("Client disconnected. " + "Total connected clients: " + pipeServer.TotalConnectedClients);
        }

        void pipeServer_MessageReceived(int eventType, byte[] message)
        {
            Invoke(new PipeServer.MessageReceivedHandler(DisplayMessageReceived),
                new object[] { eventType, message });   
        }

        void DisplayMessageReceived(int eventType, byte[] message)
        {
            //ASCIIEncoding encoder = new ASCIIEncoding();
            //string str = encoder.GetString(message, 0, message.Length);

            //tbReceived.Text += str + "\r\n";

            if (eventType == 3)     // todo: enums for event types.
            {
                SetSwitches(message);
            }

        }

        private void SetSwitches(byte[] switchData)
        {
            for (int i = 0; i < switchData.Length; i += 2)
            {
                byte switchId = switchData[i];
                byte switchOn = switchData[i + 1];

                vpm.Switch[switchId] = switchOn == 1 ? true : false ;

                Console.WriteLine("switch {0} = {1}", switchId, switchOn);
            }
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

            pipeServer.SendMessage(0, messageBuffer);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbReceived.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            vpm.GameName = "ft_l5";
            //vpm.HandleKeyboard = true;
            vpm.Run();

            dmdFrameBytes = new byte[32 * 128];  // +4 for event code
            
            timer1.Start();
        }



        private void SendDmdFrameEvent()
        {
            var rawDmdPixels = vpm.RawDmdPixels as object[];
            if (rawDmdPixels == null) return;
            for (int i = 0; i < rawDmdPixels.Length; i++)
            {
                dmdFrameBytes[i] = (byte)rawDmdPixels[i];
            }
            pipeServer.SendMessage(1, dmdFrameBytes);
        }

        private void SendChangedLampsEvent()
        {
            var changedLamps = vpm.ChangedLamps as object[,];
            if (changedLamps == null) return;

            byte[] data = new byte[changedLamps.Length];
            int index = 0;

            int count = changedLamps.Length / 2;
            for (int i = 0; i < count; i++)
            {
                int lampID = (int)changedLamps[i, 0];
                bool lampOn = (int)changedLamps[i, 1] == 1 ? true : false;

                data[index++] = (byte)lampID;
                data[index++] = lampOn ? (byte)1 : (byte)0;
            }

            pipeServer.SendMessage(2, data);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SendDmdFrameEvent();

            SendChangedLampsEvent();
        }
    }
}