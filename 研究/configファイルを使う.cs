using System.Configuration;

namespace t
{
    /// <summary>
    /// Configファイルを使用し、書き込みと読み出しを実行する
    /// 参考:https://qiita.com/ikenohotori/items/10c3a79254ea52496904
    /// </summary>
    class ConfigFileMake
    {
        public string FileName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Class { get; set; }
        public string Code { get; set; }
        public string Reason { get; set; }
        public string Approver { get; set; }

        /// <summary>
        /// Configファイルを読み込み、データを取得
        /// </summary>
        /// <param name="ConfigFilePath">Configファイルパス</param>
        public void ConfigLoad(string ConfigFilePath)
        {
            //パス取得
            string _ConfigFilePath = ConfigFilePath;
            _ConfigFilePath = @"C:\Folder\test.config"; //サンプル

            //Configファイルを読み込んだオブジェクトを作成
            var ExeFileMap = new ExeConfigurationFileMap { ExeConfigFilename = _ConfigFilePath };
            var Config = ConfigurationManager.OpenMappedExeConfiguration(ExeFileMap, ConfigurationUserLevel.None);

            // config読みだし
            FileName = Config.AppSettings.Settings["FileName"].Value;
            StartTime = Config.AppSettings.Settings["StartTime"].Value;
            EndTime = Config.AppSettings.Settings["EndTime"].Value;
            Class = Config.AppSettings.Settings["Class"].Value;
            Code = Config.AppSettings.Settings["Code"].Value;
            Reason = Config.AppSettings.Settings["Reason"].Value;
            Approver = Config.AppSettings.Settings["Approver"].Value;
        }

        /// <summary>
        /// Configファイルに書き込んで保存する
        /// </summary>
        /// <param name="ConfigFilePath">Configファイルパス</param>
        public void ConfigSave(string ConfigFilePath)
        {
            //パス取得
            string _ConfigFilePath = ConfigFilePath;
            _ConfigFilePath = @"C:\Folder\test.config"; //サンプル

            //Configファイルを読み込んだオブジェクトを作成
            var ExeFileMap = new ExeConfigurationFileMap { ExeConfigFilename = _ConfigFilePath };
            var Config = ConfigurationManager.OpenMappedExeConfiguration(ExeFileMap, ConfigurationUserLevel.None);

            // config書き込み
            Config.AppSettings.Settings["FileName"].Value = FileName;
            Config.AppSettings.Settings["StartTime"].Value = StartTime;
            Config.AppSettings.Settings["EndTime"].Value = EndTime;
            Config.AppSettings.Settings["Class"].Value = Class;
            Config.AppSettings.Settings["Code"].Value = Code;
            Config.AppSettings.Settings["Reason"].Value = Reason;
            Config.AppSettings.Settings["Approver"].Value = Approver;

            //Configファイルにオブジェクトの値を書き込み
            Config.Save();
        }
    }
}
