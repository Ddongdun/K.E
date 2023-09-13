using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;

namespace Kinect_Source
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        KinectSensor objKS;
        //키넥트 변수
        public MainWindow()
        {
            InitializeComponent();
            objKS = KinectSensor.KinectSensors[0];
            //연결된 키넥트 저장 변수
            KinectSensor.KinectSensors.StatusChanged += new EventHandler<StatusChangedEventArgs>(Kinects_StatusChanged);
            //키넥트 상태가 변경될때 이벤트 생성
        }
        void Kinects_StatusChanged(object sender, StatusChangedEventArgs e)
        //키넥트 상태 변경 이벤트
        {
            TextBlock1.Text = objKS.Status.ToString();
            //키넥트 상태 출력
        }

        private void btnOK_Click_1(object sender, RoutedEventArgs e)
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

            TextBlock1.Text = strKinectInfo;
            //문자열 출력
        }
    }
}
