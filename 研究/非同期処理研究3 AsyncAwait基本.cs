using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace t
{
    /// <summary>
    /// asyncとawaitの基本的な使い方
    /// 参考:https://qiita.com/unsignedint/items/2bb663c8fb92ff0d2b5c
    /// </summary>
    class AsyncAwaitSample3
    {
        public void MainProgram()
        {
            //メモリ8GBだと3スレッドが限界っぽい(Caller3はメイン処理のReadkey到着後に起動)
            Task.Run(() => Caller1());
            Task.Run(() => Caller2());
            Task.Run(() => Caller3());

            //DoProcess実行中、メインで別の処理をやる
            for (int i = 1; i <= 20; i++)
                WriteLine($"メイン処理：{i}回目のループです。");

            ReadKey();
        }

        /// <summary>
        /// DoProcess1タスクを呼び出すメソッド 普通の非同期処理
        /// </summary>
        /// <returns></returns>
        private async Task Caller1()
        {
            //asyncを使うには戻り値はTaskを使用する。
            var process = DoProcess1();
            await process; //DoProcessが終わるまで待機
        }

        /// <summary>
        /// 非同期処理
        /// </summary>
        /// <returns></returns>
        private async Task DoProcess1()
        {
            WriteLine("DoProcess1を開始します。");
            Thread.Sleep(5000); //5秒待つ
            WriteLine("DoProcess1が終了しました。");
        }

        /// <summary>
        /// DoProcess2タスク(戻り値設定バージョン)を呼び出すメソッド
        /// </summary>
        /// <returns></returns>
        private async Task Caller2()
        {
            //asyncを使うには戻り値はTaskを使用する。
            var process = DoProcess2();
            await process; //DoProcessが終わるまで待機

            WriteLine($"DoProcess2(戻り値設定バージョン)から{process.Result}が返されました。");
        }

        /// <summary>
        /// 非同期処理　戻り値設定バージョン
        /// </summary>
        /// <returns></returns>
        private async Task<int> DoProcess2()
        {
            WriteLine("DoProcess2(戻り値設定バージョン)を開始します。");
            Thread.Sleep(7000); //7秒待つ
            int rtnValue = 7777;

            return rtnValue;
        }

        /// <summary>
        /// DoProcess3タスクを呼び出すメソッド　呼び出し元がasync定義されていない
        /// </summary>
        /// <returns></returns>
        private void Caller3()
        {
            DoProcess3().Wait();
        }

        /// <summary>
        /// 非同期処理
        /// </summary>
        /// <returns></returns>
        private async Task DoProcess3()
        {
            WriteLine("DoProcess3(呼び出し元がasync定義されていない)を開始します。");
            Thread.Sleep(9000); //9秒待つ
            WriteLine("DoProcess3(呼び出し元がasync定義されていない)が終了しました。");
        }
    }
}
