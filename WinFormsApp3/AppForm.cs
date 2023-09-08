
using WinFormsApp3.Source;

namespace WinFormsApp3
{
    public partial class AppForm : Form
    {
        public AppForm()
        {
            InitializeComponent();
        }

        MjpegStream mStream = new MjpegStream();
        CameraList cl = new CameraList("http://demo.macroscop.com:8080/configex?login=root");
        private void AppForm_Load(object sender, EventArgs e)
        {
            UpdateCameraButtonList();
            mStream.targetPb = pictureBox1;
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            mStream.Start();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            mStream.Stop();
        }

        private void timerListUpdate_Tick(object sender, EventArgs e)
        {
            UpdateCameraButtonList();
        }

        List<ChannelInfo> bufList = new List<ChannelInfo>();
        private void UpdateCameraButtonList()
        {
            cl.UpdateList();
            var firstNotSecond = cl.list.Except(bufList);
            var secondNotFirst = bufList.Except(cl.list);
            bool equal = (firstNotSecond.Count() + secondNotFirst.Count()) == 0;
            if (equal) return;
            bufList = new List<ChannelInfo>(cl.list);

            panelLeft.Controls.Clear();

            for (int i = 0; i < cl.list.Count; i++)
            {
                Button btn = new Button();
                btn.Text = cl.list[i].name;
                btn.Height = 28;
                string id = cl.list[i].idValue;
                btn.Click += delegate (object sender, EventArgs e)
                {
                    buttonSetCamera_Click(id);
                };
                btn.Dock = DockStyle.Top;
                panelLeft.Controls.Add(btn);
            }

            //{
            //    Button btn = new Button();
            //    btn.Text = "Пустая кнопка";
            //    btn.Height = 28;
            //    string id = "not_existing_id";
            //    btn.Click += delegate (object sender, EventArgs e)
            //    {
            //        buttonSetCamera_Click(id);
            //    };
            //    btn.Dock = DockStyle.Top;
            //    panelLeft.Controls.Add(btn);
            //}
        }

        private void buttonSetCamera_Click(string id)
        {
            mStream.streamUrl = $@"http://demo.macroscop.com:8080/mobile?login=root&channelid={id}&resolutionX=640&resolutionY=480&fps=25";
        }
    }
}
