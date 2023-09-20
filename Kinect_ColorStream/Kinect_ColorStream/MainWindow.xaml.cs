using Microsoft.Kinect;
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

namespace Kinect_ColorStream
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        KinectSensor objKS = null;
        public MainWindow()
        {
            InitializeComponent();
            InitializeKinect();
            
        }
        void InitializeKinect()
        {
            objKS = KinectSensor.KinectSensors[0];

            objKS.ColorStream.Enable();
            objKS.ColorFrameReady += new EventHandler<ColorImageFrameReadyEventArgs>(objKS_ColorFrameReady);
            objKS.Start();
        }
        void objKS_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            ColorImageFrame imgParam = e.OpenColorImageFrame();
            //화면을 프레임으로 연다
            if(imgParam == null)
            //화면이 없다면
            {
                return;
                //아무것도 리턴하지 않는다
            }
            byte[] imageBits = new byte[imgParam.PixelDataLength];
            //화면을 바이트로 나누기 위한 변수
            imgParam.CopyPixelDataTo(imageBits);
            //화면을 바이트로 나눈다

            BitmapSource imgSource = null;
            //비트맵 소스
            imgSource = BitmapSource.Create(imgParam.Width, imgParam.Height, 96, 96, PixelFormats.Bgr32, null, imageBits, imgParam.Width * imgParam.BytesPerPixel);
            //비트맵 생성
            //픽셀 너비, 픽셀 높이, dpi x값, dpi y값, 픽셀포멧, 비트맵팔레트, 픽셀
            image1.Source = imgSource;
            //화면에 뿌려준다
            FramePerSecond(imgParam);
        }

        long m_orgFrame = -1;
        long m_orgTime = -1;
        long m_saveTime = 0;
        long m_saveFrame = 0;
        //시간과 프레임을 기록하기 위한 변수

        void FramePerSecond(ColorImageFrame imgFrame)
        //FPS 함수
        {
            long lgFrame = (long)imgFrame.FrameNumber;
            //이미지의 프레임을 저장
            long lgTime = (long)imgFrame.Timestamp;
            //이미지의 타임스탬프를 저장
            
            if(m_orgFrame <= 0)
            //저장된 프레임이 없다면
            {
                m_orgFrame = lgFrame;
                //저장
            }
            if(m_orgTime <= 0)
            //저장된 시간이 없다면
            {
                m_orgTime = lgTime;
                //저장
            }
            lgTime = lgTime - m_orgTime;
            //실행된 시간 저장
            lgFrame = lgFrame - m_orgFrame;
            //기록된 프레임 저장

            textBlock1.Text = "Total Frame : " + lgFrame.ToString();
            //회면에 총 프레임 표시
            textBlock2.Text = "Elapsed Time : " + lgTime.ToString();
            //화면에 총 실행시간 표시

            long lgAvgFrame = 0;
            //평균 프레임 저장 변수
            if(lgFrame > 0 && lgTime > 0)
            //기록된 프레임과 시간이 있다면
            {
                lgAvgFrame = (lgFrame * 1000) / lgTime;
                //평균 프레임을 구한다
            }
            textBlock3.Text = "FPS : " + lgAvgFrame.ToString();
            //화면 출력
            if(lgTime - m_saveTime > 1000)
            {
                long lgTemp = lgFrame - m_saveFrame;
                lgTemp = lgTemp * 1000 / (lgTime - m_saveTime);
                //초당 프레임을 구한다

                textBlock4.Text = "Frame(the nearest 1 second : " + lgTemp.ToString();
                //화면 출력
                m_saveTime = lgTime;
                m_saveFrame = lgFrame;
            }
        }
    }
    
}
