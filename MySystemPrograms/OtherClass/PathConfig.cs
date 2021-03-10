using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FunctionModule;

namespace t
{
    /// <summary>
    /// Path操作クラス
    /// </summary>
    class PathConfig
    {
        //システム関連
        private string _LogFolder = "Log";
        private string _CSVFolder = "CSV";

        //Excel関連
        private string _ExcelTemplateFolder = "ExcelTemplate";
        private string _ExcelWorkFolder = "ExcelWork";
        private string _ExcelSetFolder = "ExcelSet";

        private string _RootPath;
        
        /// <summary>
        /// コンストラクタ ルートパス設定、各種フォルダ生成
        /// </summary>
        /// <param name="RootPath"></param>
        public PathConfig(string RootPath)
        {
            _RootPath = RootPath;

            //ログフォルダ作成
            if (System.IO.Directory.Exists(_RootPath + _LogFolder) == false)
                System.IO.Directory.CreateDirectory(_RootPath + _LogFolder);

            //CSVフォルダ作成
            if (System.IO.Directory.Exists(_RootPath + _CSVFolder) == false)
                System.IO.Directory.CreateDirectory(_RootPath + _CSVFolder);

            //Excelテンプレートフォルダ作成
            if (System.IO.Directory.Exists(_RootPath + _ExcelTemplateFolder) == false)
                System.IO.Directory.CreateDirectory(_RootPath + _ExcelTemplateFolder);

            //Excelワークフォルダ作成
            if (System.IO.Directory.Exists(_RootPath + _ExcelWorkFolder) == false)
                System.IO.Directory.CreateDirectory(_RootPath + _ExcelWorkFolder);

            //Excelセットフォルダ作成
            if (System.IO.Directory.Exists(_RootPath + _ExcelSetFolder) == false)
                System.IO.Directory.CreateDirectory(_RootPath + _ExcelSetFolder);
        }
         
        /// <summary>
        /// Logフォルダパス取得
        /// </summary>
        /// <returns></returns>
        public string GetLogFolderPath()
        {
            return _RootPath + _LogFolder;
        }

        /// <summary>
        /// CSVフォルダパス取得
        /// </summary>
        /// <returns></returns>
        public string GetCSVFolderPath()
        {
            return _RootPath + _CSVFolder;
        }

        /// <summary>
        /// CSVファイルパス取得
        /// </summary>
        /// <param name="FileName">ファイル名(拡張子なし)</param>
        /// <returns></returns>
        public string GetCsvFilePath(string FileName)
        {
            string Date = GetSystemDate_YYYYMMDD("");

            return _RootPath + _CSVFolder + @"\" + FileName + "_" + Date + ".csv";
        }

        /// <summary>
        /// Excelテンプレートフォルダパス
        /// </summary>
        /// <returns></returns>
        public string GetExcelFormFolderPath()
        {
            return _RootPath + _ExcelTemplateFolder;
        }

        /// <summary>
        /// Excelテンプレートファイルパス
        /// </summary>
        /// <param name="FileName">ファイル名(拡張子なし)</param>
        /// <returns></returns>
        public string GetExcelTemplateFilePath(string FileName)
        {
            return _RootPath + _ExcelTemplateFolder + @"\" + FileName + ".xlsx";
        }

        /// <summary>
        /// Excelワークフォルダパス
        /// </summary>
        /// <returns></returns>
        public string GetExcelTempFolderPath()
        {
            return _RootPath + _ExcelWorkFolder;
        }

        /// <summary>
        /// Excelワークファイルパス
        /// </summary>
        /// <param name="FileName">ファイル名(拡張子なし)</param>
        /// <returns></returns>
        public string GetExcelWorkFilePath(string FileName)
        {
            return _RootPath + _ExcelWorkFolder + @"\" + FileName + ".xlsx";
        }

        /// <summary>
        /// Excelセットフォルダパス
        /// </summary>
        /// <returns></returns>
        public string GetExcelSetFolderPath()
        {
            return _RootPath + _ExcelSetFolder;
        }

        /// <summary>
        /// Excelセットファイルパス
        /// </summary>
        /// <param name="FileName">ファイル名(拡張子なし)</param>
        /// <returns></returns>
        public string GetExcelSetFilePath(string FileName)
        {
            return _RootPath + _ExcelSetFolder + @"\" + FileName + ".xlsx";
        }
    }
}
