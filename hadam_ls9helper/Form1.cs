using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hadam_ls9helper
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowNmae);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr param);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWind, StringBuilder className, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindow(IntPtr hWnd, int uCmd);
        private const int GW_HWNDFIRST = 0; // 젤 첨음
        private const int GW_HWNDLAST = 1;  // 마지막
        private const int GW_HWNDNEXT = 2;  // 바로 다음
        private const int GW_HWNDPREV = 3;  // 바로 이전
        private const int GW_OWNER = 4;     // 부모
        private const int GW_CHILD = 5;     // 첫번째 자식

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        const int WM_LBUTTONDOWN = 0x0201;
        const int WM_LBUTTONUP = 0x0202;
        const int BM_CLICK = 0x00F5;

        private delegate bool EnumWindowProc(IntPtr childHandle, IntPtr pointer);
        private List<IntPtr> _chSetHndList = new List<IntPtr>();
        private IntPtr _chOnOff;
        private IntPtr _chSong;
        private IntPtr _chWMic1;
        private IntPtr _chWMic2;
        private IntPtr _chWMic3;
        private IntPtr _chWMic4;
        private IntPtr _chPiano;
        private IntPtr _chDownMain;


        /// <summary>
        /// 최상위 부모 프로세스와 자식 프로세스중 필요한 부분 일부는
        /// 이름이 특정되어있고 이후 필요한 자식프로세스들은 이름과 클래스가 전부 중복되어있다
        /// 그러므로 일단 부모 프로세스를 잡고
        /// 그 아래를 EnumChildWindows를 검색하여 잡는다 (두개)
        /// 각각 32-17, 16-1 번까지의 번호를 가지고 있다
        /// 이들은 전부 클래스와 캡션이 중복되므로 순서에 의해서 찾아야된다
        /// 즉 1번을 찾을려면 두번째 것의 가장 마지막 16번째 자식이 해당된다
        /// 그리고 거기서 이제 캡션으로 (onOfOn)을 찾으면 된다
        /// </summary>
        /// <param name="childHandle"></param>
        /// <param name="pointer"></param>
        /// <returns></returns>
        private bool GetChildHandler(IntPtr childHandle, IntPtr pointer)
        {
            StringBuilder sb = new StringBuilder();
            GetWindowText(childHandle, sb, sb.Capacity);
            if(sb.ToString().Equals("qt_viewport"))
            {
                _chSetHndList.Add(GetWindow(childHandle, GW_CHILD));     // 자식의 자식이 채널세트, 32, 16에 대한 핸들을 저장
            }
            return true;
        }

        // 최종적으로 enumchild 한번 더 돌려야된다
        private bool GetChOnOffHandler(IntPtr onOff, IntPtr param)
        {
            StringBuilder sb = new StringBuilder();
            GetWindowText(onOff, sb, sb.Capacity);
            if(sb.ToString().Equals("onofOn"))
            {
                _chOnOff = onOff;
                return false;           // 정확히 찾아낸후 멈춘다
            }
            return true;
        }

        public Form1()
        {
            InitializeComponent();
            FindChSetHnd();
            FindChHnd();
        }

        private void FindChSetHnd()
        {
            IntPtr handle = FindWindow(null, "LS9 2");  // 최상위 핸들 찾고
            if(handle != null)
            {
                EnumChildWindows(handle, GetChildHandler, IntPtr.Zero); // 자식들 검색해서 채널세트 32,16을 찾아낸다
            }
        }


        /// <summry>
        /// 원하는 채널은 
        /// 32 - 찬양대
        /// 21 - 무선 4
        /// 20 - 무선 3
        /// 19 - 무선 2
        /// 18 - 무선 1
        /// 16 - 피아노
        /// 8 - 아랫강대상
        /// 일곱개 채널이다. 32채널세트에서 무선1까지, 나머지 두개는 16채널세트에서 찾아낸다
        /// </summary>
        private void FindChHnd()
        {
            // 각채널세트의 첫번째 채널들
            IntPtr ch32 = GetWindow(_chSetHndList[0], GW_CHILD);
            IntPtr ch16 = GetWindow(_chSetHndList[1], GW_CHILD);

            //32, 21, 20, 19, 18
            EnumChildWindows(ch32, GetChOnOffHandler, IntPtr.Zero);
            _chSong = _chOnOff;

            IntPtr ch21 = ChSelector(9, ch32);
            EnumChildWindows(ch21, GetChOnOffHandler, IntPtr.Zero);
            _chWMic1 = _chOnOff;

            IntPtr ch20 = ChSelector(1, ch21);
            EnumChildWindows(ch20, GetChOnOffHandler, IntPtr.Zero);
            _chWMic2 = _chOnOff;

            IntPtr ch19 = ChSelector(1, ch20);
            EnumChildWindows(ch19, GetChOnOffHandler, IntPtr.Zero);
            _chWMic3 = _chOnOff;

            IntPtr ch18 = ChSelector(1, ch19);
            EnumChildWindows(ch18, GetChOnOffHandler, IntPtr.Zero);
            _chWMic4 = _chOnOff;

            // 16, 8
            EnumChildWindows(ch16, GetChOnOffHandler, IntPtr.Zero);
            _chPiano = _chOnOff;

            IntPtr ch8 = ChSelector(8, ch16);
            EnumChildWindows(ch8, GetChOnOffHandler, IntPtr.Zero);
            _chDownMain = _chOnOff;

        }

        // 주어진 숫자만큼 프로세서를 이동한다. 예를들어 32채널에서 29채널로 가고 싶다면 3 이동하면된다
        private IntPtr ChSelector(int mov, IntPtr wHnd)
        {
            for(int i= 0; i< mov; i++)
            {
                wHnd = GetWindow(wHnd, GW_HWNDNEXT);
            }
            return wHnd;
        }

        private void btn_downMain_Click(object sender, EventArgs e)
        {
            SendMessage(_chDownMain, WM_LBUTTONDOWN, 0, 0);
            SendMessage(_chDownMain, WM_LBUTTONUP, 0, 0);
        }

        private void btn_pMic_Click(object sender, EventArgs e)
        {
            Console.WriteLine(_chPiano.ToString());
            SendMessage(_chPiano, WM_LBUTTONDOWN, 0, 0);
            SendMessage(_chPiano, WM_LBUTTONUP, 0, 0);
        }

        private void btn_wMic_Click(object sender, EventArgs e)
        {
            SendMessage(_chWMic1, WM_LBUTTONDOWN, 0, 0);
            SendMessage(_chWMic1, WM_LBUTTONUP, 0, 0);
        }

        private void btn_cMic_Click(object sender, EventArgs e)
        {
            SendMessage(_chSong, WM_LBUTTONDOWN, 0, 0);
            SendMessage(_chSong, WM_LBUTTONUP, 0, 0);
        }
    }
}
