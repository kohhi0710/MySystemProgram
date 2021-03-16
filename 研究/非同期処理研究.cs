using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace t
{
    /// <summary>
    /// 並列処理の研究
    /// 参考:https://qiita.com/takutoy/items/c384fcb439d345a9a0d3
    /// Action型参考:https://www.fenet.jp/dotnet/column/language/5375/
    /// </summary>
    class AsyncAwaitParallelTest
    {
        public void MainProgram()
        {
            //FireParallel();

            //Task.Run(() => ConcurrentDownloadThrottle());

            //var t = WakeOnTime(Convert.ToDateTime("2021/03/16 13:43"));
            //WriteLine(t.Result);

            //Sample_ParallelEnumerable();

            Task.Run(() => Sample_Exception());

            ReadLine();
        }

        /// <summary>
        /// 並列処理の実行
        /// </summary>
        /// <returns></returns>
        private void FireParallel()
        {
            //並列処理を実行する。全ての並列処理が終了すれば次のステップにすすむ
            //※CPUのコア数やスレッドプールの空き状況によっては、同時に動かないこともある。
            Parallel.Invoke(new ParallelOptions() { MaxDegreeOfParallelism = 4 }, //最大同時並列数:4
                            () => { TimeCount(); },
                            new Action(ProgramA),  //Invokeの引数はAction型
                            () => { ProgramB(); }, //メソッドがAction型の戻り値でない場合は置き換える
                            () =>                  //直接処理を書いてもOK
                            {
                                int n = 5000;
                                ProgramC(n);
                            });

            WriteLine("すべての並列処理が終了しました。");
        }

        private void ProgramA()
        {
            WriteLine("プログラムA：処理を開始します。10秒後に処理が完了します。");
            Thread.Sleep(10000);
            WriteLine("プログラムA：処理が完了しました。");
        }

        private async Task ProgramB()
        {
            WriteLine("プログラムB：処理を開始します。15秒後に処理が完了します。");
            Thread.Sleep(15000);
            WriteLine("プログラムB：処理が完了しました。");
        }

        private void ProgramC(int n)
        {
            WriteLine("プログラムC：処理を開始します。5秒後に処理が完了します。");
            Thread.Sleep(n);
            WriteLine("プログラムC：処理が完了しました。");
        }

        private void TimeCount()
        {
            for(int i = 0; i < 20; i++)
            {
                WriteLine("タイムカウント:現在{0}秒です。",i);
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// 複数のサイトからHTMLを同時にダウンロードする
        /// </summary>
        /// <returns></returns>
        private async Task ConcurrentDownloadThrottle()
        {
            var urls = new[]{
                                "http://www.amazon.com/",
                                "http://www.apple.com/",
                                "http://www.facebook.com/",
                                "http://www.google.com/",
                                "http://www.microsoft.com/",
                                "http://www.twitter.com/",
                            };

            var _HttpClient = new HttpClient();        //HttpClientクラス
            var _SemaphoreSlim = new SemaphoreSlim(2); //最大同時実行数:2

            var downloadTasks = urls.Select(async url =>
            {
                await _SemaphoreSlim.WaitAsync();
                WriteLine($"ダウンロード開始 : {url}");

                try
                {
                    return await _HttpClient.GetStringAsync(url);
                }
                finally
                {
                    WriteLine($"ダウンロード終了 : {url}");
                    _SemaphoreSlim.Release();
                }
            });

            var _HTMLs = await Task.WhenAll(downloadTasks);
        }

        /// <summary>
        /// 時刻になったら起こしてくれるTask
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public Task<string> WakeOnTime(DateTime time)
        {
            if(time < DateTime.Now)
            {
                //すぐに終わらせられる(完了している)なら、Task.FromResult()を使う
                return Task.FromResult("起きろ！");
            }

            //待ち時間が発生するようなタスクにはTaskCompletionSource<string>
            var tcs = new TaskCompletionSource<string>();

            Timer timer = null;
            timer = new Timer(delegate
            {
                timer.Dispose();
                tcs.TrySetResult("起きて");
            });

            int WaitMilliSeconds = (int)(time - DateTime.Now).TotalMilliseconds;
            timer.Change(WaitMilliSeconds, Timeout.Infinite);

            return tcs.Task;
        }

        /// <summary>
        /// LINQの並列処理
        /// ファイルのハッシュ値を計算するプログラム
        /// </summary>
        public void Sample_ParallelEnumerable()
        {
            var files = Directory.GetFiles(Environment.SystemDirectory, "*.exe");

            //AsParallel()以降のコードが並列化される
            //A(ファイル読み込み)はシングルスレッドで実行され、
            //B(ハッシュ値計算)はマルチスレッドで実行される
            var filehash = files
                           .Select(f => new { File = f, Data = File.ReadAllBytes(f) }) //A
                           .AsParallel()
                           .WithDegreeOfParallelism(4) //最大同時並列数:4 省略可能
                           .Select(f => new { File = f.File, Hash = SHA256.Create().ComputeHash(f.Data) }) //B
                           .ToArray();

            foreach(var item in filehash)
            {
                WriteLine($"{item.File} : {item.Hash}");
            }
        }

        /// <summary>
        /// 例外処理
        /// </summary>
        public async Task Sample_Exception()
        {
            //TaskやParallelの中で発生した例外はAggregateExceptionとしてCatchできる

            try
            {
                Parallel.Invoke(
                    () => throw new ArgumentException(),
                    () => throw new InvalidOperationException(),
                    () => throw new FormatException());
            }
            catch(AggregateException ex)
            {
                //原因となったExceptionが知りたい場合は、FlattenやInnerExceptionsが便利
                var exceptions = ex.Flatten().InnerExceptions;

                foreach(var item in exceptions)
                {
                    WriteLine(item.GetType());
                }
            }

            //ただしTaskをawaitする場合は元のExceptionでcatchできる

            try
            {
                var addresses = await Dns.GetHostAddressesAsync("Example.jp");
            }
            catch(SocketException ex)
            {
                WriteLine(ex.ToString());
            }

            //Taskの中で発生した(Catchされていない)例外をまとめて処理したい場合はTaskScheduler.UnobservedTaskException

            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                AggregateException ex = e.Exception;

                WriteLine(ex.ToString());

                e.SetObserved(); //.NET Framework 4.0だとこれをしないとアプリが死ぬようになってた
            };
        }
    }
}
