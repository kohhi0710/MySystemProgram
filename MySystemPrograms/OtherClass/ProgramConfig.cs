using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace t
{
    /// <summary>
    /// 環境変数設定クラス
    /// </summary>
    class ProgramConfig
    {
        //Config.XMLのパス
        public string _ConfigPath = System.AppDomain.CurrentDomain.BaseDirectory + @"Config.xml";

        //プログラム情報
        public string _ProgramName;
        public string _Version;
        public string _LastUpDate;

        //SQL接続パラメータ
        public string _ServerName;
        public string _LoginUser;
        public string _LoginPassword;
        public string _DBName;

        //ユーザーパラメータ
        public string _UserID;      //ユーザーID
        public string _UserName;    //ユーザー名
        public string _MachineName; //端末名

        /// <summary>
        /// コンストラクタ Config.xmlを読み込んでパラメータに反映
        /// </summary>
        public ProgramConfig()
        {
            try
            {
                var XmlElm = XDocument.Load(_ConfigPath);
                var emp = (from p in XmlElm.Descendants("Config")
                           select p);

                foreach (var item in emp)
                {
                    _ProgramName = item.Element("ProgramName").Value;
                    _Version = item.Element("Version").Value;
                    _LastUpDate = item.Element("LastUpDate").Value;
                    _ServerName = item.Element("ServerName").Value;
                    _LoginUser = item.Element("LoginUser").Value;
                    _LoginPassword = item.Element("LoginPassword").Value;
                    _DBName = item.Element("DBName").Value;
                    _UserID = item.Element("UserID").Value;
                    _UserName = item.Element("UserName").Value;
                    _MachineName = Environment.MachineName;
                }
            }
            catch (Exception ex)
            {
                string ErrMsg = "環境定義エラー(" + _ConfigPath + ")";
            }
        }
    }
}
