using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FunctionModule;
using static SystemModule;

namespace t
{
    class SystemControl_Base
    {
        protected string _RootPath; //ルートパス

        private System.Threading.Mutex _Mutex; //ミューテックス
        public string _ProgramName; //プログラム名

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SystemControl_Base()
        {
            Init();
            _ProgramName = Process.GetCurrentProcess().MainModule.FileName;
        }

        /// <summary>
        /// 例外のスロー
        /// </summary>
        public void NewException(Exception ex_arg)
        {
            if(ex_arg.GetType() == typeof(ProgramErrorException))
            {
                ProgramErrorException ex = (ProgramErrorException)ex_arg;
                throw new ProgramErrorException(ex.GetMsgCode(), ex.GetAppName(), ex.GetClassName(),
                                                ex.GetMethodName(), ex.GetAppMessage(), ex.GetSysMessage());
            }
            else
            {
                throw new ProgramErrorException("9999", _ProgramName, this.GetType().Name,
                                                "", "システムエラーが発生しました。", ex_arg.Message);
            }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        private void Init()
        {
            _ProgramName = "";
        }

        /// <summary>
        /// ルートパスを定義して取得
        /// </summary>
        /// <returns>ルートパス(Project、Base、Batch、BatchEXEのどれか)</returns>
        protected string GetRootPathDefine()
        {
            string SystemRootPath = System.AppDomain.CurrentDomain.BaseDirectory;

            //ルートフォルダの存在チェック
            if (System.IO.Directory.Exists(SystemRootPath) == false)
                throw new Exception("ルートフォルダーが存在しません。");

            return SystemRootPath;
        }

        /// <summary>
        /// ProgramConfigインスタンス生成
        /// </summary>
        public void SetConfig()
        {
            _ProgramConfig = new ProgramConfig();
        }

        /// <summary>
        /// 二重起動チェック
        /// </summary>
        /// <returns></returns>
        protected bool SubProcessCheck()
        {
            //ミューテックスクラスのインスタンス生成
            _Mutex = new System.Threading.Mutex(false, "SubProcess");

            //Mutexの所有権を要求
            if (_Mutex.WaitOne(0, false) == false)
                return true;

            //プロセスを取得
            string AppName = Process.GetCurrentProcess().MainModule.FileName;
            var ps = Process.GetProcessesByName(AppName);

            bool ProcessFlg = false;
            
            foreach(var item in ps)
            {
                ProcessFlg = true;
                break;
            }

            //起動済ならreturn
            if (ProcessFlg)
                return true;

            return false;
        }

        /// <summary>
        /// 二重起動チェック(バッチ用)
        /// </summary>
        /// <returns></returns>
        protected bool SubProcessCheck_Batch()
        {
            //ミューテックスクラスのインスタンス生成
            _Mutex = new System.Threading.Mutex(false, Process.GetCurrentProcess().MainModule.FileName.Replace(".exe",""));

            //Mutexの所有権を要求
            if (_Mutex.WaitOne(0, false) == false)
                return true;

            //プロセスを取得
            string AppName = Process.GetCurrentProcess().MainModule.FileName;
            var ps = Process.GetProcessesByName(AppName);

            bool ProcessFlg = false;

            foreach (var item in ps)
            {
                ProcessFlg = true;
                break;
            }

            //起動済ならreturn
            if (ProcessFlg)
                return true;

            return false;
        }

        /// <summary>
        /// Mutexの解放
        /// </summary>
        protected void EndMutex()
        {
            if (_Mutex != null)
                _Mutex.ReleaseMutex();
        }

        /// <summary>
        /// ログクラスインスタンス初期化
        /// </summary>
        protected void InitLog()
        {
            _Log_Method = new Log_Method();
        }

        /// <summary>
        /// プログラム開始ログ出力
        /// </summary>
        public void PutLogProgramStart()
        {
            if (_Log_Method == null)
                return;

            string AppMsg = "【" + _ProgramName + " 開始】";
            _Log_Method.PutLog(3, AppMsg, "", "");
        }

        /// <summary>
        /// プログラム終了ログ出力
        /// </summary>
        public void PutLogProgramEnd()
        {
            if (_Log_Method == null)
                return;

            string AppMsg = "【" + _ProgramName + " 終了】";
            _Log_Method.PutLog(3, AppMsg, "", "");
        }


        /// <summary>
        /// ユーザーログインログ出力
        /// </summary>
        public void PutLogProgramLogin()
        {
            if (_Log_Method == null)
                return;

            string AppMsg = _ProgramConfig._UserID + ":" + _ProgramConfig._UserName + " " + "【ログイン】";
            _Log_Method.PutLog(3, AppMsg, "", "");
        }

        /// <summary>
        /// 処理開始ログ出力
        /// </summary>
        /// <param name="ProcessName"></param>
        public void PutLogProcessStart(string ProcessName)
        {
            if (_Log_Method == null)
                return;

            string AppMsg = _ProgramConfig._UserID + ":" + _ProgramConfig._UserName + " " + "＊" + ProcessName + " 開始----------＊";
            _Log_Method.PutLog(3, AppMsg, "", "");
        }

        /// <summary>
        /// 処理終了ログ出力
        /// </summary>
        /// <param name="ProcessName"></param>
        public void PutLogProcessEnd(string ProcessName)
        {
            if (_Log_Method == null)
                return;

            string AppMsg = _ProgramConfig._UserID + ":" + _ProgramConfig._UserName + " " + "＊" + ProcessName + " 終了----------＊";
            _Log_Method.PutLog(3, AppMsg, "", "");
        }

        /// <summary>
        /// 例外ログ出力
        /// </summary>
        /// <param name="ex"></param>
        public void PutLogException(Exception ex)
        {
            if (_Log_Method == null)
                return;

            if(ex.GetType() == typeof(ProgramErrorException))
            {
                var PEex = (ProgramErrorException)ex;

                string AppMsg = _ProgramConfig._UserID + ":" + _ProgramConfig._UserName + " " + PEex.GetAppName() +
                                " プログラムエラー(" + PEex.GetMsgCode() + ")";
                string AddMsg = "Class=" + PEex.GetClassName() + Environment.NewLine +
                                "Method=" + PEex.GetMethodName() + Environment.NewLine + 
                                PEex.GetAppMessage();
                string SysMsg = PEex.GetSysMessage();
                _Log_Method.PutLog(5, AppMsg, AddMsg,SysMsg);
            }
            else
            {
                string AppMsg = _ProgramConfig._UserID + ":" + _ProgramConfig._UserName + " " + _ProgramName + "プログラムエラー";
                string SysMsg = ex.ToString();
                _Log_Method.PutLog(5, AppMsg, "", SysMsg);
            }
        }
    }
}
