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
using System.IO;

namespace Kinect_ImgPicture
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

        BitmapSource imgSource = null;

        void objKS_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            ColorImageFrame imgParam = e.OpenColorImageFrame();
            if(imgParam == null)
            {
                return;
            }
            byte[] imageBits = new byte[imgParam.PixelDataLength];
            imgParam.CopyPixelDataTo(imageBits);

            imgSource = BitmapSource.Create(imgParam.Width, imgParam.Height, 96, 96, PixelFormats.Bgr32, null, imageBits, imgParam.Width * imgParam.BytesPerPixel);
            image1.Source = imgSource;
        }

        private void btnImageFile_Click(object sender, RoutedEventArgs e)
        //버튼으로 사진 저장하는 함수
        {
            DateTime currentTime = DateTime.Now;
            //현재 시간 저장
            string filename = currentTime.ToString("yyyyMMddHHmmss");
            //시간을 문자열로 변환
            if(imgSource != null)
            //화면이 출력되고 있다면
            {
                BitmapEncoder encoder = null;
                //비트맵 인코더 선언
                encoder = new PngBitmapEncoder();
                //png, gif 바꿔서 저장 가능
                encoder.Frames.Add(BitmapFrame.Create(imgSource));
                //프레임 추가

                File.Delete("imgPicture.png");
                //파일 삭제
                FileStream fStream = new FileStream("C:\\Users\\andel\\OneDrive\\바탕 화면\\문서\\"+ filename + ".png", FileMode.Create, FileAccess.Write);
                //지정된 경로에 파일 만들고 쓰기
                encoder.Save(fStream);
                //저장
                fStream.Close();
                //종료
                System.Diagnostics.Process exe = new System.Diagnostics.Process();
                //시스템 파일 실행 
                exe.StartInfo.FileName = "C:\\Users\\andel\\OneDrive\\바탕 화면\\문서\\" + filename + ".png";
                //파일을 연다
                exe.Start();
                //시작
            }
        }
    }
}
