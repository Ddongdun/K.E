using Microsoft.Kinect;
namespace Kinect_WindowsForm
{
    public partial class Form1 : Form
    {
        KinectSensor objKS;
        public Form1()
        {
            InitializeComponent();
            objKS = KinectSensor.KinectSensors[0];
            //����� Ű��Ʈ ���� ����
            KinectSensor.KinectSensors.StatusChanged += new EventHandler<StatusChangedEventArgs>(Kinects_StatusChanged);
            //Ű��Ʈ ���°� ����ɶ� �̺�Ʈ ����
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int iNumKinect = KinectSensor.KinectSensors.Count;
            //Ű��Ʈ ���� ���� ����

            String strKinectInfo = null;
            //���ڿ� ����
            strKinectInfo += iNumKinect.ToString("Ű��Ʈ ��: 0��");
            strKinectInfo += "\n";
            strKinectInfo += objKS.UniqueKinectId;
            //���� ���̵� ���
            strKinectInfo += "\n";
            strKinectInfo += objKS.DeviceConnectionId;
            //����� Ű��Ʈ ���̵� ���
            strKinectInfo += "\n";
            strKinectInfo += objKS.Status.ToString();
            //���� Ű��Ʈ ���� ���

            strKinectInfo += "\n";
            strKinectInfo += "Ű��Ʈ ���� ���� : " + objKS.IsRunning;
            //Ű��Ʈ on, off ���� ���

            TextBox1.Text = strKinectInfo;
            //���ڿ� ���
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        void Kinects_StatusChanged(object sender, StatusChangedEventArgs e)
        //Ű��Ʈ ���� ���� �̺�Ʈ
        {
            TextBox1.Text = objKS.Status.ToString();
            //Ű��Ʈ ���� ���
        }
    }
}