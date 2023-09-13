using Microsoft.Kinect;

namespace Kinect_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            KinectSensor objKS;
            objKS = KinectSensor.KinectSensors[0];

            while (true)
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

                Console.WriteLine(strKinectInfo);

                if(Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
    }
}
