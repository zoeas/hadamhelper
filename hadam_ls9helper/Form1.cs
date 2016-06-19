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
using System.Diagnostics;
using System.Threading;

namespace hadam_ls9helper
{
    public partial class Form1 : Form
    {
        private const int SONG = 1;
        private const int PIANO = 2;
        private const int WMIC = 3;
        private const string AURORA_CH = "aurora4stardb";
        private const string AURORA_HOME = "aurora4staredit";

        private enum ShowWindowEnum { Hide = 0, ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3, Maximize = 3, ShowNormalNoActivate = 4, Show = 5, Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8, Restore = 9, ShowDefault = 10, ForceMinimized = 11 };

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PrintWindow(IntPtr hWnd, IntPtr hDC, uint nFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowNmae);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr param);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWind, StringBuilder className, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern IntPtr GetParent(IntPtr hWind);

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindow(IntPtr hWnd, int uCmd);
        private const int GW_HWNDFIRST = 0; // 젤 첨음
        private const int GW_HWNDLAST = 1;  // 마지막
        private const int GW_HWNDNEXT = 2;  // 바로 다음
        private const int GW_HWNDPREV = 3;  // 바로 이전
        private const int GW_OWNER = 4;     // 부모
        private const int GW_CHILD = 5;     // 첫번째 자식

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        const int WM_LBUTTONDOWN = 0x0201;
        const int WM_LBUTTONUP = 0x0202;
        const int BM_CLICK = 0x00F5;
        const int GCL_HICONSM = -34;
        const int GCL_HICON = -14;
        const int ICON_SMALL = 0;
        const int ICON_BIG = 1;
        const int ICON_SMALL2 = 2;
        const int WM_GETICON = 0x7F;

        [DllImport("user32.dll", EntryPoint = "GetClassLong")]
        public static extern uint GetClassLongPtr32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
        public static extern IntPtr GetClassLongPtr64(IntPtr hWnd, int nIndex);

        private const int SW_RESTORE = 9;
        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        private delegate bool EnumWindowProc(IntPtr childHandle, IntPtr pointer);
        private IntPtr _mainHandle;
        private IntPtr _ch32;
        private IntPtr _ch16;
        private IntPtr _chOnOff;
        private IntPtr _chSong;
        private IntPtr _chWMic1;
        private IntPtr _chWMic2;
        private IntPtr _chWMic3;
        private IntPtr _chWMic4;
        private IntPtr _chPiano;
        private IntPtr _chLeader;
        private IntPtr _chDownMain;


        /// <summary>
        /// 최상위 부모 프로세스와 자식 프로세스중 필요한 부분 일부는
        /// 이름이 특정되어있고 이후 필요한 자식프로세스들은 이름과 클래스가 전부 중복되어있다
        /// 그러므로 일단 부모 프로세스를 잡고 그 아래를 EnumChildWindows를 검색하여 잡는다 (두개)
        /// 이들은 각각 32-17, 16-1 번까지의 자식윈도우를 또 가지고 있는데
        /// 전부 클래스와 캡션이 중복되므로 순서에 의해서 찾아야된다
        /// 그러므로 각각의 부모윈도우에서 가장 첫번째에 있는 자식인 ch32와 ch16을 찾아저장하고
        /// 나머지 번호는 그걸 기준으로 순서로 찾으면된다
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
                IntPtr p = GetParent(childHandle);
                GetWindowText(p, sb, sb.Capacity);
                if(sb.ToString().Equals("CH 1-16"))
                {
                    childHandle = GetWindow(childHandle, GW_CHILD);
                    _ch16 = GetWindow(childHandle, GW_CHILD);
                } else if(sb.ToString().Equals("CH17-32"))
                {
                    childHandle = GetWindow(childHandle, GW_CHILD);
                    _ch32 = GetWindow(childHandle, GW_CHILD);
                }
            }
            return true;
        }

        // 원하는 번호의 채널을 찾았으면 이제 그 채널의 자식윈도우 중에
        // on off 를 담당하는 윈도우를 찾아야한다
        private bool GetChOnOffHandler(IntPtr onOff, IntPtr param)
        {
            StringBuilder sb = new StringBuilder();
            GetWindowText(onOff, sb, sb.Capacity);
            if(sb.ToString().Equals("onofOn"))
            {
                _chOnOff = onOff;
                return false;           // 찾으면 더 이상 검색할 필요없이 바로 멈춘다
            }
            return true;
        }

        public Form1()
        {
            InitializeComponent();
            FindChSetHnd();
            FindChHnd();
            InitButton();
        }

        private void FindChSetHnd()
        {
            _mainHandle = FindWindow(null, "LS9 2");  // 최상위 핸들 찾고

            if (_mainHandle != null)
            {
                EnumChildWindows(_mainHandle, GetChildHandler, IntPtr.Zero); // 자식들 검색해서 채널세트 32,16을 찾아낸다
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
            //32, 21, 20, 19, 18
            EnumChildWindows(_ch32, GetChOnOffHandler, IntPtr.Zero);
            _chSong = _chOnOff;

            IntPtr ch21 = ChSelector(11, _ch32);
            EnumChildWindows(ch21, GetChOnOffHandler, IntPtr.Zero);
            _chWMic4 = _chOnOff;

            IntPtr ch20 = ChSelector(1, ch21);
            EnumChildWindows(ch20, GetChOnOffHandler, IntPtr.Zero);
            _chWMic3 = _chOnOff;

            IntPtr ch19 = ChSelector(1, ch20);
            EnumChildWindows(ch19, GetChOnOffHandler, IntPtr.Zero);
            _chWMic2 = _chOnOff;

            IntPtr ch18 = ChSelector(1, ch19);
            EnumChildWindows(ch18, GetChOnOffHandler, IntPtr.Zero);
            _chWMic1 = _chOnOff;

            // 16, 8
            EnumChildWindows(_ch16, GetChOnOffHandler, IntPtr.Zero);
            _chPiano = _chOnOff;

            IntPtr ch9 = ChSelector(7, _ch16);
            EnumChildWindows(ch9, GetChOnOffHandler, IntPtr.Zero);
            _chLeader = _chOnOff;

            IntPtr ch8 = ChSelector(1, ch9);
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

        /* 특정윈도우 부분의 그림을 가져온답시고 아이콘 핸들을 고려했지만 이건 말그대로 그 프로그램 자체의 아이콘을 가져온다
                public static IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex)
                {
                    if (IntPtr.Size > 4)
                        return GetClassLongPtr64(hWnd, nIndex);
                    else
                        return new IntPtr(GetClassLongPtr32(hWnd, nIndex));
                }

                private Icon GetAppIcon(IntPtr hwnd)
                {
                    IntPtr iconHandle = SendMessage(hwnd, WM_GETICON, ICON_SMALL2, 0);
                    if (iconHandle == IntPtr.Zero)
                        iconHandle = SendMessage(hwnd, WM_GETICON, ICON_SMALL, 0);
                    if (iconHandle == IntPtr.Zero)
                        iconHandle = SendMessage(hwnd, WM_GETICON, ICON_BIG, 0);
                    if (iconHandle == IntPtr.Zero)
                        iconHandle = GetClassLongPtr(hwnd, GCL_HICON);
                    if (iconHandle == IntPtr.Zero)
                        iconHandle = GetClassLongPtr(hwnd, GCL_HICONSM);

                    if (iconHandle == IntPtr.Zero)
                        return null;

                    Icon icn = Icon.FromHandle(iconHandle);

                    return icn;
                }
        */

        private void InitButton()
        {
            btn_downMain.Image = CheckON(_chDownMain);
            btn_cMic.Image = CheckON(_chSong);
            btn_pMic.Image = CheckON(_chPiano);

            if (CheckONwMic(_chWMic1) || CheckONwMic(_chWMic2) || CheckONwMic(_chWMic3) || CheckONwMic(_chWMic4))
            {
                btn_wMic.Image = hadam_ls9helper.Properties.Resources.music_on;
            } else
            {
                btn_wMic.Image = hadam_ls9helper.Properties.Resources.music_off;
            }
        }

        // DC(디바이스 컨텍스트)로부터 그래픽을 가져온다
        private Bitmap CaptrueOnOff(IntPtr hWnd)
        {
            ShowWindow(_mainHandle, ShowWindowEnum.ShowDefault);   // 최소화창은 캡쳐가 안되므로 일단 최소화상태를 취소한다(단, SetForegroundWindow 처럼 포커스를 주는 것은 아니다)

            Rectangle rectForm = Rectangle.Empty;

            // DC로부터 그래픽 정보를 얻어서 크기정보 사각형을 얻는다
            using (Graphics g = Graphics.FromHdc(GetWindowDC(hWnd)))
            {
                rectForm = Rectangle.Round(g.VisibleClipBounds);
            }

            // 그 크기 그대로 비트맵을 만들어서 그래픽객체로 저장, 그리고 그 객체로부터 DC의 핸들을 얻는다
            Bitmap pImage = new Bitmap(rectForm.Width, rectForm.Height);
            Graphics graphics = Graphics.FromImage(pImage);

            IntPtr hDC = graphics.GetHdc();

            try
            {
                // hWnd의 윈도우를 찍어서 hDC에게 넘겨줘서 저장한다
                // nFlags : 0=include border , 1=client area only
                PrintWindow(hWnd, hDC, (uint)1);
            }
            finally
            {
                // 저장은 끝났으므로 DC의 핸들을 해제한다
                graphics.ReleaseHdc(hDC);
            }
            return pImage;
        }

        /// <summary>
        /// 아랫강대상외 버튼 클릭시 다른 버튼도 같이 클릭효과를 받을 것인지 결정
        /// 체크리스트에서 같이 체크된 버튼끼리만 작동하도록 함
        /// 예를들어 피아노와 코러스가 체크시 마이크를 누르면 
        /// 피아노,코러스에는 영향이 없고 
        /// 피아노를 누르면 코러스도 같이 눌림
        /// </summary>
        /// <param name="mic">누른 버튼의 종류(중복실행방지)</param>
        /// <param name="self">자신의 체크여부</param>
        private void Connection(int mic, bool self)
        {
            // 자기자신이 체크되어있을때만 다른 버튼도 실행
            if (self)
            {
                if (cBox_pMic.Checked && mic != PIANO)
                {
                    SendMessage(_chPiano, WM_LBUTTONDOWN, 0, 0);
                    SendMessage(_chPiano, WM_LBUTTONUP, 0, 0);
                }
                if (cBox_cMic.Checked && mic != SONG)
                {
                    SendMessage(_chSong, WM_LBUTTONDOWN, 0, 0);
                    SendMessage(_chSong, WM_LBUTTONUP, 0, 0);
                }
                if (cBox_wMics.Checked && mic != WMIC)
                {
                    if (cBox_wMic1.Checked)
                    {
                        SendMessage(_chWMic1, WM_LBUTTONDOWN, 0, 0);
                        SendMessage(_chWMic1, WM_LBUTTONUP, 0, 0);
                    }
                    if (cBox_wMic2.Checked)
                    {
                        SendMessage(_chWMic2, WM_LBUTTONDOWN, 0, 0);
                        SendMessage(_chWMic2, WM_LBUTTONUP, 0, 0);
                    }
                    if (cBox_wMic3.Checked)
                    {
                        SendMessage(_chWMic3, WM_LBUTTONDOWN, 0, 0);
                        SendMessage(_chWMic3, WM_LBUTTONUP, 0, 0);
                    }
                    if (cBox_wMic4.Checked)
                    {
                        SendMessage(_chWMic4, WM_LBUTTONDOWN, 0, 0);
                        SendMessage(_chWMic4, WM_LBUTTONUP, 0, 0);
                    }
                }
            }
            Thread.Sleep(500);
            btn_pMic.Image = CheckON(_chPiano);
            btn_cMic.Image = CheckON(_chSong);
            if (CheckONwMic(_chWMic1) || CheckONwMic(_chWMic2) || CheckONwMic(_chWMic3) || CheckONwMic(_chWMic4))
            {
                btn_wMic.Image = hadam_ls9helper.Properties.Resources.music_on;
            }
            else
            {
                btn_wMic.Image = hadam_ls9helper.Properties.Resources.music_off;
            }
        }

        private Bitmap CheckON(IntPtr hwnd)
        {
            Bitmap bmp = CaptrueOnOff(hwnd);
            Color c = bmp.GetPixel(5, 5);

            Console.WriteLine(c.GetBrightness());
            if(c.GetBrightness() > 0)
            {
                return hadam_ls9helper.Properties.Resources.music_on;
            }
            return hadam_ls9helper.Properties.Resources.music_off;
        }

        private bool CheckONwMic(IntPtr hwnd)
        {
            Bitmap bmp = CaptrueOnOff(hwnd);
            Color c = bmp.GetPixel(5, 5);
            return (c.GetBrightness() > 0) ? true : false;
        }

        // 아랫강대상
        private void btn_downMain_Click(object sender, EventArgs e)
        {
            SendMessage(_chDownMain, WM_LBUTTONDOWN, 0, 0);
            SendMessage(_chDownMain, WM_LBUTTONUP, 0, 0);
            SetforeGroundAurora();
            Thread.Sleep(500);
            btn_downMain.Image = CheckON(_chDownMain);
        }

        // 피아노
        private void btn_pMic_Click(object sender, EventArgs e)
        {
            SendMessage(_chPiano, WM_LBUTTONDOWN, 0, 0);
            SendMessage(_chPiano, WM_LBUTTONUP, 0, 0);
            SetforeGroundAurora();
            Connection(PIANO, cBox_pMic.Checked);
        }

        // 무선
        private void btn_wMic_Click(object sender, EventArgs e)
        {
            if (cBox_wMic1.Checked)
            {
                SendMessage(_chWMic1, WM_LBUTTONDOWN, 0, 0);
                SendMessage(_chWMic1, WM_LBUTTONUP, 0, 0);
            }
            if (cBox_wMic2.Checked)
            {
                SendMessage(_chWMic2, WM_LBUTTONDOWN, 0, 0);
                SendMessage(_chWMic2, WM_LBUTTONUP, 0, 0);
            }
            if (cBox_wMic3.Checked)
            {
                SendMessage(_chWMic3, WM_LBUTTONDOWN, 0, 0);
                SendMessage(_chWMic3, WM_LBUTTONUP, 0, 0);
            }
            if (cBox_wMic4.Checked)
            {
                SendMessage(_chWMic4, WM_LBUTTONDOWN, 0, 0);
                SendMessage(_chWMic4, WM_LBUTTONUP, 0, 0);
            }
            Connection(WMIC, cBox_wMics.Checked);
            SetforeGroundAurora();
        }

        // 찬양대
        private void btn_cMic_Click(object sender, EventArgs e)
        {
            SendMessage(_chSong, WM_LBUTTONDOWN, 0, 0);
            SendMessage(_chSong, WM_LBUTTONUP, 0, 0);
            Connection(SONG, cBox_cMic.Checked);
            SetforeGroundAurora();
        }

        private void SetforeGroundAurora()
        {
            Process[] p = Process.GetProcessesByName(AURORA_CH);
            if(p.GetLength(0) > 0)
            {
                //ShowWindow(p[0].MainWindowHandle, SW_RESTORE);   // 최소화 윈도우 복구
                SetForegroundWindow(p[0].MainWindowHandle);
            } else
            {
                MessageBox.Show("오로라 프로그램이 없습니다");
            }
        }

        private void btn_leader_Click(object sender, EventArgs e)
        {
            SendMessage(_chLeader, WM_LBUTTONDOWN, 0, 0);
            SendMessage(_chLeader, WM_LBUTTONUP, 0, 0);
            SetforeGroundAurora();
            Thread.Sleep(500);                      // 이거 없으면 제대로 못가져옴
            btn_leader.Image = CheckON(_chLeader);  // on off 이미지 교체
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            btn_leader.Image = CheckON(_chLeader);
            btn_downMain.Image = CheckON(_chDownMain);
            btn_pMic.Image = CheckON(_chPiano);
            btn_cMic.Image = CheckON(_chSong);
            if (CheckONwMic(_chWMic1) || CheckONwMic(_chWMic2) || CheckONwMic(_chWMic3) || CheckONwMic(_chWMic4))
            {
                btn_wMic.Image = hadam_ls9helper.Properties.Resources.music_on;
            }
            else
            {
                btn_wMic.Image = hadam_ls9helper.Properties.Resources.music_off;
            }
        }

    }
}
