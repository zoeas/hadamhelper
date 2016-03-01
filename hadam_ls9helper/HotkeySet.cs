using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hadam_ls9helper
{
    partial class Form1
    {
        /////////////////////////////////////////////////////////////////////////////////
        //
        // 위에서 정의한 KeyboardHooker class를 이용하여 후킹을 구현한다.
        // 여기서는 ALT키와 숫자 1키의 조합(ALT+1)을 인식하도록 한다.
        // 사람 손이 아무리 정확해도 키조합에서는 먼저 KeyUp되고 나중에 KeyUp되는 키가 있다.
        // 이것을 캐치하여 키조합이 눌린 후 둘 다 떨어지는 순간에 캐치한 이벤트에 대한
        // 특정 행위를 실행하도록 한다.
        //
        /////////////////////////////////////////////////////////////////////////////////
        private bool bAltAndNum;//Alt+숫자가 같이 눌린 상태(나중에 1 이외의 숫자도 쓸지 모르니..)
        private bool bAltOrNum;//Alt+숫자 이후 Alt만 남거나 숫자키만 남거나 한 상태

        //1. 후킹할 이벤트를 등록한다.
        event KeyboardHooker.HookedKeyboardUserEventHandler HookedKeyboardNofity;

        //2 .이벤트가 발생하면 호출된다.
        private long Form1_HookedKeyboardNofity(int iKeyWhatHappened, int vkCode)
        {
            //일단은 기본적으로 키 이벤트를 흘려보내기 위해서 0으로 세팅
            long lResult = 0;

            /////////////////////////////////////////////////////////////////////////////////
            //
            // Hook은 디버그모드에서 잡을 수 없기 때문에
            // 디버그용으로 다음의 코드를 사용한다.
            //textBox1.Text += Environment.NewLine + "iKeyWhatHappened=" + iKeyWhatHappened.ToString();
            //textBox1.Text += Environment.NewLine + "vkCode=" + vkCode.ToString();
            //
            /////////////////////////////////////////////////////////////////////////////////
            //
            // 키 조합에 의해 키가 눌리는 순간 동시에 눌린것을 확인 후 조치를 취한다.
            // L-Alt의 KeyDown: iKeyWhatHappened=32
            // L-Alt의 KeyUp: iKeyWhatHappened=160
            // 후킹은 했지만 키 이벤트는 얌전히 보내준다.
            // 만약 ALT+1에 대한 키 이벤트를 현재 활성화된 윈도우에 보내고싶지 않으면
            // 아래if문들의 lResult값을 모두 1을 주도록 하자.
            //
            /////////////////////////////////////////////////////////////////////////////////
            if (vkCode == 49 && iKeyWhatHappened == 32) //ALT + 1
            {
                bAltAndNum = true;
                bAltOrNum = false;
                lResult = 0;
            }
            else if (bAltAndNum && iKeyWhatHappened == 128)
            {
                bAltAndNum = false;
                bAltOrNum = true;
                lResult = 0;
            }
            else if (bAltAndNum && vkCode == 49)
            {
                bAltAndNum = false;
                bAltOrNum = true;
                lResult = 0;
            }
            else if (!bAltAndNum && bAltOrNum && (vkCode == 49 || vkCode == 164))
            {
                bAltOrNum = false;
                lResult = 0;
                timer1.Interval = 50;
                timer1.Start();
            }
            else
            {
                //나머지 키들은 얌전히 보내준다.
                bAltAndNum = false;
                bAltOrNum = false;
                lResult = 0;
            }


            return lResult;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //3. 후크 이벤트를 연결한다.
            HookedKeyboardNofity += new KeyboardHooker.HookedKeyboardUserEventHandler(Form1_HookedKeyboardNofity);

            //4. 자동으로 훅을 시작한다. 여기서 훅에 의한 이벤트를 연결시킨다.
            KeyboardHooker.Hook(HookedKeyboardNofity);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            KeyboardHooker.UnHook();
        }

        /// <summary>
        /// 핫키를 실행시킨다. 약간의 시간 딜레이를 주고 실행시키기 위하여 별도의 쓰레드에서 실행시킨다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {

            /////////////////////////////////////////////////////////////////////////////////
            //
            // ALT+1키 조합이 완성되었을 때 어떤 행위를 해주기 위한 쓰레드 부분이다.
            // 윈도우 폼에서는 타이머가 쓰레드역할을 잘 해주므로 편하게 이렇게 쓴다.
            // 일반적으로 키보드 후킹 후 매크로 명령을 많이 사용하므로
            // 이 부분은 각자 알아서 매크로 명령을 쓰도록 한다.
            // SendMessage/PostMessage/keybd_event등을 사용하면 된다.
            //
            /////////////////////////////////////////////////////////////////////////////////

            timer1.Stop(); //타이머가 반복해서 동작하지 않도록 한다. 이게 아래로 내려가면 작동하지 않는다 이유는
                           // btn_cMic_Click 이 메서드에 MessageBox를 보여주는게 있는데 그걸 부르면 이게 작동하지 않는듯
            btn_cMic_Click(null, null); // 단축키 alt+1이 들어오면 찬양대 마이크 버튼 눌려짐

        }
    }
}
