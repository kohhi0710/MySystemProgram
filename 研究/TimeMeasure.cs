using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoderTest.その他
{
    class TimeMeasure
    {
        //プロパティ
        private Dictionary<string,TimeRecordItem> TimeRecordDic { get; set; }
        private string ClassName { get; set; }

        /// <summary>
        /// 【パブリックメソッド】時間を測定するメインメソッド
        /// </summary>
        /// <param name="key"></param>
        /// <param name="suffix"></param>
        public void Record(string key = null,string suffix = "")
        {
            //StackFlameを用いて呼び出し元のメソッドを取得するようにしている
            //開始・終了の判断をTimeSetクラスで行っている

            if (key == null)
            {
                //呼び出し元のメソッド名を取得
                key = new System.Diagnostics.StackFrame(1).GetMethod().Name;
            }

            //呼び出し元からsuffixに値が設定された場合
            if(!string.IsNullOrEmpty(suffix))
            {
                //呼び出し元メソッド名とsuffixの文字列を"_"で結合してKeyとする
                key = string.Join("_", key, suffix);
            }

            //Keyが存在するか？
            //存在していなかったらKeyとTimeRecordItemオブジェクトをリストに追加
            if(!TimeRecordDic.ContainsKey(key))
            {
                TimeRecordDic.Add(key, new TimeRecordItem(key, DateTime.Now));
            }
            else if(TimeRecordDic[key].IsEnd) //存在していたらそのKeyに対応したValue(IsEnd)の値をチェック
            {
                //IsEndがTrueならスタート時間をリストに追加
                TimeRecordDic[key].AddTimeSets(DateTime.Now);
            }
            else
            {
                //falseならエンド時間をリストに追加
                TimeRecordDic[key].TimeSets.Last().SetEndTime(DateTime.Now);
            }
        }

        public void OutputDebugWriteLine()
        {
            foreach(var content in GetOutputContents())
            {
                System.Diagnostics.Debug.WriteLine(content);
            }
        }

        public void OutputConsoleWriteLine()
        {
            foreach(var content in GetOutputContents())
            {
                Console.WriteLine(content);
            }
        }

        //プライベートメソッド
        private IEnumerable<string> GetOutputContents()
        {
            var TimeSets = new List<TimeSet>();

            foreach(var item in TimeRecordDic.Values)
            {
                TimeSets.AddRange(item.TimeSets);
            }

            //StartTimeでソートしてから取り出す
            foreach(var r in TimeSets.OrderBy(rr => rr.StartTime))
            {
                yield return string.Join(",", ClassName, r.Key, r.StartTime.ToString(), r.EndTime.ToString(), r.ExecutionTime.ToString());
            }
        }

        /// <summary>
        /// 【コンストラクタ】記録オブジェクトの格納リストと呼び出し元のクラス名を取得している
        /// </summary>
        public TimeMeasure()
        {
            TimeRecordDic = new Dictionary<string, TimeRecordItem>();
            ClassName = new System.Diagnostics.StackFrame(1).GetMethod().ReflectedType.FullName;
        }

        //プライベートクラス
        private class TimeRecordItem
        {
            //プロパティ
            public string Key { get; set; }
            public List<TimeSet> TimeSets { get; set; }
            public bool IsEnd { get { return TimeSets.Last().EndTime != null; } }

            //パブリックメソッド
            public void AddTimeSets(DateTime time)
            {
                TimeSets.Add(new TimeSet(Key, time));
            }

            //コンストラクタ
            public TimeRecordItem(string key,DateTime time)
            {
                Key = key;
                TimeSets = new List<TimeSet> { new TimeSet(key, time) };
            }
        }

        /// <summary>
        /// 時間を記録するクラス
        /// </summary>
        private class TimeSet
        {
            //プロパティ
            public string Key { get; private set; }
            public DateTime StartTime { get; private set; }
            public DateTime? EndTime { get; private set; }
            public decimal? ExecutionTime
            {
                get
                {
                    if(EndTime == null)
                    {
                        return null;
                    }

                    var diff = EndTime.Value - StartTime;

                    return (decimal)diff.TotalMilliseconds / 1000;
                }
            }

            //パブリックメソッド
            public void SetEndTime(DateTime time)
            {
                EndTime = time;
            }

            //コンストラクタ
            public TimeSet(String key,DateTime time)
            {
                Key = key;
                StartTime = time;
            }

        }

        //--------------------------------
        //テスト用メソッド
        //--------------------------------

        public static TimeMeasure _timeMeasure = new TimeMeasure();

        static void solve()
        {
            TimeMeasure pg = new TimeMeasure();

            _timeMeasure.Record();
            pg.TestMethod1();
            pg.TestMethod2();
            pg.TestMethod1();
            _timeMeasure.Record();
            _timeMeasure.OutputConsoleWriteLine();

        }

        private void TestMethod1()
        {
            _timeMeasure.Record();
            System.Threading.Thread.Sleep(1000);
            _timeMeasure.Record();
        }

        private void TestMethod2()
        {
            _timeMeasure.Record();
            _timeMeasure.Record(suffix: "処理1");
            System.Threading.Thread.Sleep(1000);
            _timeMeasure.Record(suffix: "処理1");
            System.Threading.Thread.Sleep(1000);
            _timeMeasure.Record();
        }


    }
}
