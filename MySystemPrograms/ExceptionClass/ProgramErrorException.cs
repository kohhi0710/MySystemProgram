using System;
using static SystemModule;

namespace t
{
    /// <summary>
    /// プログラムエラーを知らせる独自例外クラス
    /// </summary>
    class ProgramErrorException:Exception
    {
        private string _MsgCode;    //メッセージコード
        private string _AppName;　　//アプリケーション名
        private string _ClassName;　//クラス名
        private string _MethodName; //メソッド名
        private string _AppMsg;　　 //アプリケーションメッセージ
        private string _SysMsg;　　 //システムメッセージ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="MsgCode">メッセージコード</param>
        /// <param name="AppName">アプリケーション名</param>
        /// <param name="ClassName">クラス名</param>
        /// <param name="MethodName">メソッド名</param>
        /// <param name="AppMsg">アプリケーションメッセージ</param>
        /// <param name="SysMsg">システムメッセージ</param>
        public ProgramErrorException(string MsgCode,string AppName,string ClassName,string MethodName,string AppMsg,string SysMsg)
        {
            _MsgCode = MsgCode;
            _AppName = AppName;
            _ClassName = ClassName;
            _MethodName = MethodName;
            _AppMsg = AppMsg;
            _SysMsg = SysMsg;

            _Log_Method.PutLog(5, GetPGErrMessage(), "", "");
        }

        /// <summary>
        /// エラーメッセージを編集して取得する
        /// </summary>
        /// <returns></returns>
        public string GetPGErrMessage()
        {
            string rtnValue = "";

            rtnValue = _AppName + " プログラムエラー(" + _MsgCode + ")" + Environment.NewLine +
                       "Class=" + _ClassName + Environment.NewLine +
                       "Method=" + _MethodName + Environment.NewLine +
                       _AppMsg + Environment.NewLine +
                       _SysMsg;

            return rtnValue;
        }

        public string GetMsgCode()
        {
            return _MsgCode;
        }

        public string GetAppName()
        {
            return _AppName;
        }

        public string GetClassName()
        {
            return _ClassName;
        }

        public string GetMethodName()
        {
            return _MethodName;
        }

        public string GetAppMessage()
        {
            return _AppMsg;
        }

        public string GetSysMessage()
        {
            return _SysMsg;
        }

    }
}
