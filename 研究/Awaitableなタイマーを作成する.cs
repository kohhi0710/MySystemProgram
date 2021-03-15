using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using static System.Console;

namespace t
{
    /// <summary>
    /// 参考:https://qiita.com/soyjoy9/items/bbf576f725feb97d069a
    /// <see cref="System.Timers.Timer"/>のインターバル時間の経過イベントを待機できるタスクを提供します。
    /// </summary>
    class AwaitableTimer : System.Timers.Timer
    {
        #region "Field"

        private TaskCompletionSource<DateTime> _tcs;

        #endregion

        #region "Constructor"

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public AwaitableTimer() : base()
        {
            Elapsed += OnElapsed;
        }

        /// <summary>
        /// インターバル時間、およびインターバル超過イベントを繰り返し発生させるかどうかを指定してタイマーを作成する
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="autoReset"></param>
        public AwaitableTimer(TimeSpan interval, bool autoReset) : base(interval.TotalMilliseconds)
        {
            AutoReset = autoReset;
            Elapsed += OnElapsed;
        }

        /// <summary>
        /// インターバル時間を指定して初期化
        /// </summary>
        /// <param name="interval"></param>
        public AwaitableTimer(TimeSpan interval) : base(interval.TotalMilliseconds)
        {
            Elapsed += OnElapsed;
        }

        #endregion

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            if (_tcs != null)
            {
                _tcs.TrySetResult(e.SignalTime);
                _tcs = null;
            }
        }

        private void OnTaskCanceled()
        {
            if (_tcs != null)
            {
                _tcs.TrySetException(new TaskCanceledException(_tcs.Task));
                _tcs = null;
            }
        }

        /// <summary>
        /// インターバル時間とインターバル経過イベントを繰り返し発生させるかどうかを指定して、タイマーを開始する。
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="autoReset"></param>
        /// <returns></returns>
        public static AwaitableTimer StartNew(TimeSpan interval, bool autoReset)
        {
            var timer = new AwaitableTimer(interval, autoReset);
            timer.Start();

            return timer;
        }

        /// <summary>
        /// タイマーの経過時間をリセットして再開
        /// </summary>
        public void Restart()
        {
            Stop();
            Start();
        }

        public async Task<DateTime> WaitElapsedAsync(CancellationToken token = default)
        {
            if (Enabled)
            {
                _tcs = new TaskCompletionSource<DateTime>(TaskCreationOptions.RunContinuationsAsynchronously);

                var register = token.Register(OnTaskCanceled);

                using (register)
                {
                    return await _tcs.Task;
                }
            }
            else
            {
                return DateTime.Now;
            }
        }
    }

    /// <summary>
    /// 使い方サンプル
    /// </summary>
    class TimerSample
    {
        static readonly Random rnd = new Random();

        public async Task Main_Sample()
        {
            while (true)
            {
                if (ReadKey().Key == ConsoleKey.Escape)
                    break;

                Clear();

                WriteLine("インターバル時間をミリ秒で指定");
                if (!double.TryParse(ReadLine(), out var interval))
                    continue;

                WriteLine("繰り返し回数を指定");
                if (!int.TryParse(ReadLine(), out var n))
                    continue;

                WriteLine("不具合の起こる確率を指定");
                if (!double.TryParse(ReadLine(), out var p))
                    continue;

                bool timeout = false;
                bool isRunning = false;

                async Task AsyncMethod()
                {
                    int delay = 0;

                    if (rnd.NextDouble() > p)
                    {
                        delay = (int)(0.5 * interval);
                    }
                    else
                    {
                        delay = (int)(2 * interval);
                    }

                    isRunning = true;
                    await Task.Delay(delay);
                    isRunning = false;
                }

                using (var intervalTimer = new AwaitableTimer(TimeSpan.FromMilliseconds(interval), true))
                {
                    intervalTimer.Elapsed += (s, e) =>
                    {
                        if (isRunning)
                            timeout = true;
                    };

                    intervalTimer.Start();

                    var startTime = DateTime.Now;
                    var previous = startTime;

                    for (var i = 0; i < n; i++)
                    {
                        await AsyncMethod();

                        if (timeout)
                        {
                            WriteLine("インターバル時間内に処理が終わりませんでした。");
                            timeout = false;
                            break;
                        }

                        var now = await intervalTimer.WaitElapsedAsync();
                        var period = now - previous;
                        previous = now;

                        WriteLine($"ラップ{i + 1:000}　経過時間(トータル)：{(now - startTime).TotalMilliseconds:f2} ms　経過時間(インターバル)：{period.TotalMilliseconds:f2} ms");
                    }
                }
            }

            WriteLine("終了");
        }
    }
}
