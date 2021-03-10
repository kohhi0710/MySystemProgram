using System;
using System.Diagnostics;

namespace t
{
    //外部プロセス操作クラス
    class ExternalProcessOperation
    {
        public ProcessStartInfo _Process { get; set; }
        public string _FilePath { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="FilePath">外部プロセスファイルパス</param>
        public ExternalProcessOperation(string FilePath)
        {
            _FilePath = FilePath;
            _Process = new ProcessStartInfo();

            _Process.FileName = FilePath;
            _Process.WindowStyle = ProcessWindowStyle.Minimized;
            _Process.UseShellExecute = false;
        }

        /// <summary>
        /// コンストラクタ　コマンドライン引数を外部プロセスに入力
        /// </summary>
        /// <param name="FilePath">外部プロセスファイルパス</param>
        /// <param name="Arg">コマンドライン引数</param>
        public ExternalProcessOperation(string FilePath, string Arg)
        {
            _FilePath = FilePath;
            _Process = new ProcessStartInfo();

            _Process.FileName = FilePath;
            _Process.WindowStyle = ProcessWindowStyle.Minimized;
            _Process.UseShellExecute = false;
            _Process.Arguments = Arg;
        }

        /// <summary>
        /// 外部プロセスの起動
        /// </summary>
        public void Start()
        {
            try
            {
                if (System.IO.File.Exists(_FilePath) == false)
                    throw new Exception(String.Format("指定されたディレクトリ「{0}」は存在しません。処理を中断します。", _FilePath));

                //起動
                Process.Start(_Process);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
