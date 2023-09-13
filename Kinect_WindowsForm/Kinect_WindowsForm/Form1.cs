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
            //연결된 키넥트 저장 변수
            KinectSensor.KinectSensors.StatusChanged += new EventHandler<StatusChangedEventArgs>(Kinects_StatusChanged);
            //키넥트 상태가 변경될때 이벤트 생성
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int iNumKinect = KinectSensor.KinectSensors.Count;
            //키넥트 개수 저장 변수

            String strKinectInfo = null;
            //문자열 선언
            strKinectInfo += iNumKinect.ToString("키넥트 수: 0개");
            strKinectInfo += "\n";
            strKinectInfo += objKS.UniqueKinectId;
            //고유 아이디 출력
            strKinectInfo += "\n";
            strKinectInfo += objKS.DeviceConnectionId;
            //연결된 키넥트 아이디 출력
            strKinectInfo += "\n";
            strKinectInfo += objKS.Status.ToString();
            //현재 키넥트 상태 출력

            strKinectInfo += "\n";
            strKinectInfo += "키넥트 동작 여부 : " + objKS.IsRunning;
            //키넥트 on, off 상태 출력

            TextBox1.Text = strKinectInfo;
            //문자열 출력
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        void Kinects_StatusChanged(object sender, StatusChangedEventArgs e)
        //키넥트 상태 변경 이벤트
        {
            TextBox1.Text = objKS.Status.ToString();
            //키넥트 상태 출력
        }
    }
}