using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using static System.Console;

namespace t
{
    /// <summary>
    /// Cancellation Token について調べてみる
    /// 参考:https://qiita.com/TsuyoshiUshio@github/items/b2d23b37b410a2cfd330
    /// </summary>
    class CancellationToken_Practice5
    {
        //Cancellation Token とは

        //Cancellation Token は、非同期処理を実施するときに、非同期処理をキャンセルするための仕組み。
        //非同期のルーチンを実行した後で、どうやってその処理を取り消せばいいか？そういう時に使う。

        //ExecAsync → TimerEventAsync → EternalLoopAsync → CancelHappen

        private async Task EternalLoopAsync(CancellationToken ct)
        {
            while(true)
            {
                //トークンのキャンセル以降は全てスローorリターン
                //一旦スローorリターンされたらこのスレッドはもう起動されない→TimerEventAsyncのawaitに処理を返す
                //その際、timerオブジェクトはまだ生きてるのでイベントも継続して発生する。
                //そのためずっと19000ミリ秒(19秒後)にCancelHappenを起動する
                
                //タスクのキャンセルがされていたら例外をスロー
                ct.ThrowIfCancellationRequested();

                //タスクがキャンセルされていたらリターンする
                //if (ct.IsCancellationRequested) { return; }

                await Task.Delay(6000);
                WriteLine($"Eternal Loop: {DateTime.Now}");
            }
        }

        /// <summary>
        /// メイン処理
        /// </summary>
        /// <returns></returns>
        public async Task ExecAsync()
        {
            try
            {
                //TimerEventAsyncメソッドの処理が終わるまで待機
                await TimerEventAsync();
            }
            catch(OperationCanceledException ex)
            {
                //スローされたらここにくる
                WriteLine($"Canceled!: {ex}");
            }
        }

        private CancellationTokenSource cts;

        /// <summary>
        /// タイマーイベント発生定義　19000ミリ秒後にCancelHappensイベントを起動
        /// </summary>
        /// <returns></returns>
        private async Task TimerEventAsync()
        {
            cts = new CancellationTokenSource();
            var timer = new System.Timers.Timer();

            //イベントハンドラ定義
            timer.Interval = 19000;
            timer.Enabled = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(CancelHappens);

            //非同期スタート
            await EternalLoopAsync(cts.Token);
        }

        /// <summary>
        /// CancellationTokenをキャンセルする
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void CancelHappens(object source,ElapsedEventArgs e)
        {
            WriteLine($"Cancel happens!: {DateTime.Now}");
            cts.Cancel();
        }
    }

    class Sample_CancellationToken
    {
        public void MainProgram()
        {
            new CancellationToken_Practice5().ExecAsync();
            WriteLine("非同期処理実施中　呼び出し元プログラムを並列して実行するには何かキーを押してください");
            ReadKey();
        }
    }
}
