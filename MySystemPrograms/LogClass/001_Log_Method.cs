using System;
using static SystemModule;

namespace t
{
    /// <summary>
    /// ログ制御クラス
    /// </summary>
    class Log_Method : Log_Base
    {
        private const int _MaxLogRetryTimes = 10;    //リトライ上限
        private const int _LogSleepTime = 100;       //Sleep時間(ミリ秒)

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Log_Method() : base()
        {
            //スーパークラスのコンストラクタ処理
        }

        /// <summary>
        /// ログ出力メイン処理
        /// ログレベル(レベル1～6):TRACE-DEBUG-INFO-WARN-ERROR-FATAL
        /// </summary>
        /// <param name="LogLevel">ログレベル</param>
        /// <param name="AppMsg">アプリケーションメッセージ</param>
        /// <param name="AddMsg">追加メッセージ</param>
        /// <param name="SysMsg">システムメッセージ</param>
        public void PutLog(int LogLevel,string AppMsg,string AddMsg,string SysMsg)
        {
            string SetMsg = "";

            //ログレベルでログの出力方法を変更
            switch (LogLevel)
            {
                //TRACE:トレースログ、デバッグ情報よりも、更に詳細な情報
                case 1: //TRACE
                    SetMsg = "[TRACE] " + AppMsg.TrimStart();
                    break;

                //DEBUG:デバッグログ、システムの動作状況に関する詳細な情報
                case 2: //DEBUG
                    SetMsg = "[DEBUG] " + AppMsg.TrimStart();
                    break;

                //INFO:通常ログ
                case 3: //INFO
                    SetMsg = "[INFO] " + AppMsg.TrimStart();
                    break;

                //WARN:警告ログ、実行時に生じた異常とは言い切れないが正常とも異なる何らかの予期しない問題
                case 4: //WARN
                    SetMsg = "[WARN] " + AppMsg.TrimStart();
                    break;

                //ERROR:通常エラーログ、予期しないその他の実行時エラー
                case 5: //ERROR
                    SetMsg = "[ERROR] " + AppMsg.TrimStart();
                    break;

                //FATAL:致命的なエラーログ、プログラムの異常終了を伴うようなもの
                case 6: //FATAL
                    SetMsg = "[FATAL] " + AppMsg.TrimStart();
                    break;
            }

            string UserID = _ProgramConfig._UserID;
            string UserName = _ProgramConfig._UserName;

            SetMsg = ": " + UserID + " " + UserName + " " + SetMsg;
            SetLogMsg(SetMsg, AddMsg, SysMsg);
        }

        /// <summary>
        /// メッセージ編集
        /// </summary>
        /// <param name="AppMsg">アプリケーションメッセージ</param>
        /// <param name="AddMsg">追加メッセージ</param>
        /// <param name="SysMsg">システムメッセージ</param>
        private void SetLogMsg(string AppMsg, string AddMsg, string SysMsg)
        {
            string SetLog = "";

            if (AppMsg != "")
            {
                string App = AppMsg;

                string Add = "";
                if (AddMsg != "")
                    Add = AddMsg;

                string Sys = "";
                if (SysMsg != "")
                    Sys = SysMsg.Trim();

                SetLog = SetLog + App;

                if (Add.Trim() != "")
                    SetLog = SetLog + Environment.NewLine + " " + Add;

                if (Sys.Trim() != "")
                    SetLog = SetLog + Environment.NewLine + " " + Sys;
            }

            string NewChar = Environment.NewLine + ">                   :";
            _LogMsg = SetLog.Replace(Environment.NewLine, NewChar);

            LogPutThread();
        }

        /// <summary>
        /// ログファイルへデータを書き込む。
        /// </summary>
        private void LogPutThread()
        {
            int RetryCount = 0;
            bool RetryFlg = true;

            while(RetryFlg)
            {
                try
                {
                    OpenLog();
                    WriteLog(_LogMsg);
                    CloseLog();

                    RetryFlg = false;
                }
                catch(System.IO.IOException ex)
                {
                    CloseLog();

                    //リトライ
                    RetryCount += 1;

                    if (RetryCount == _MaxLogRetryTimes)
                        throw ex;

                    System.Threading.Thread.Sleep(_LogSleepTime);
                }
                catch(Exception ex)
                {
                    CloseLog();
                    throw ex;
                }
            }
        }
    }
}
