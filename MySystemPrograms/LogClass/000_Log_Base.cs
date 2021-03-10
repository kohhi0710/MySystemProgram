using System;
using System.IO;
using static FunctionModule;
using static SystemModule;

namespace t
{
    /// <summary>
    /// ログスーパークラス
    /// </summary>
    class Log_Base
    {
        protected bool _LogOpenFlg;                                                           //ログオープンフラグ(false:close true:open)
        private string _LogFilePath = _PathConfig.GetLogFolderPath() + @"\SystemLog.log";     //ログファイルパス

        private FileStream _fs;   //ログ出力Stream
        private StreamWriter _sw; //ログ出力writer

        protected string _LogMsg;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected Log_Base()
        {
            _LogOpenFlg = false;
            _LogMsg = "";
        }

        /// <summary>
        /// ログファイルをオープンする。
        /// 出力ディレクトリが存在しなければ生成する。
        /// 出力ファイルが存在しなければ、生成する。
        /// 出力ファイルが存在すれば、追加出力を行う。
        /// ログファイル名はタスク名称_ログ日付.LOG で出力する。
        /// ログオープンフラグをTrue(オープン)にする。
        /// </summary>
        protected void OpenLog()
        {
            if (_LogOpenFlg == true)
                return;

            try
            {
                _fs = null;
                _sw = null;

                //ログフォルダが存在しなければ作成
                if (Directory.Exists(_PathConfig.GetLogFolderPath()) == false)
                    Directory.CreateDirectory(_PathConfig.GetLogFolderPath());

                //ログファイルが存在しなければ作成
                if(File.Exists(_LogFilePath) == false)
                {
                    StreamWriter wf = File.CreateText(_LogFilePath);
                    wf.Close();
                }

                _fs = File.OpenWrite(_LogFilePath);
                _sw = new StreamWriter(_fs);
                _sw.BaseStream.Seek(0, SeekOrigin.End);

                //フラグをopenにする
                _LogOpenFlg = true;
            }
            catch(Exception ex)
            {
                CloseLog();
                throw ex;
            }
        }

        /// <summary>
        /// ログファイルをCloseする
        /// </summary>
        protected void CloseLog()
        {
            //closeならreturn
            if (_LogOpenFlg == false)
                return;

            try
            {
                if (_sw != null)
                    _sw.Close();
            }
            catch(Exception ex)
            {
                //例外処理なし
            }

            try
            {
                if (_fs != null)
                    _fs.Close();
            }
            catch (Exception ex)
            {
                //例外処理なし
            }

            _LogOpenFlg = false;
        }

        /// <summary>
        /// ログファイルへデータを出力する
        /// </summary>
        /// <param name="Msg">ログメッセージ</param>
        protected void WriteLog(string Msg)
        {
            _LogMsg = Msg;

            if (_LogOpenFlg == false)
                throw new Exception();

            //ログを出力する
            //日付+時刻+ログで出力
            string SetLog = "";
            if(_LogMsg != "")
            {
                string Date_Log = GetSystemDate_YYYYMMDD("/");
                string Time_Log = GetSystemTime_HHmmss(":");
                SetLog = Date_Log + " " + Time_Log + " " + _LogMsg;
            }

            _sw.WriteLine(SetLog);
            _sw.Flush();
        }
    }
}
