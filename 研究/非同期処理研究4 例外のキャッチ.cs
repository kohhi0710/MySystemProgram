using System;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace t
{
    /// <summary>
    /// Taskの中で例外が起きた時のキャッチの仕方
    /// 参考:https://qiita.com/tera1707/items/d5a3bc12ffa5f80069a1
    /// </summary>
    class AsyncAwaitException4
    {
        //Taskで非同期処理をしている時に例外が発生すると、例外の種類に関わらず「AggregateException」が発生する。
        //元々の例外はどうすればキャッチすればいいのだろうか。

        public void MainProgram()
        {
            Sample1();
            Sample2();
            Sample3();
            Sample4();
        }

        /// <summary>
        /// awaitで待つ時のパターン
        /// </summary>
        private async void Sample1()
        {
            //awaitするときは、そこをtry catchで囲むだけで通常通りに例外がキャッチできる

            try
            {
                await Task.Run(() =>
                {
                    throw new NotImplementedException();
                });
            }
            catch(Exception ex)
            {
                WriteLine("Sample1:" + ex.GetType()); //Sample1:System.NotImplementedException
            }
        }

        /// <summary>
        /// Wait()で待つときのパターン
        /// </summary>
        private void Sample2()
        {
            //Wait()したときは、例外がAggregateExceptionに包まれて上がってくる。
            //実際に何の例外が起きたかは、AggregateExceptionのInnerExceptionプロパティを見る必要がある。

            try
            {
                Task.Run(() =>
                {
                    throw new NotImplementedException(); //Sample2_Outer: System.AggregateException
                }).Wait();
            }
            catch(Exception ex)
            {
                WriteLine("Sample2_Outer:" + ex.GetType());

                if(ex is AggregateException age)
                {
                    WriteLine("Sample2_Inner:" + age.InnerException.GetType()); // Sample2_Inner:System.NotImplementedException
                }
            }
        }

        /// <summary>
        /// 待たないパターン
        /// </summary>
        private void Sample3()
        {
            //待たないタスクの場合は普通にtry catchで囲んでも例外を取得できない
            //その場合は終わったタスク変数のExceptionプロパティを見て、何の例外が起きてタスクが終わったのかを調べる

            try
            {
                var t = Task.Run(() =>
                {
                    throw new NotImplementedException();
                });

                //ContinueWith:ターゲットタスク(ここではt)が終了した時に、継続して実行されるタスク(compt)を作成する
                //継続タスク内で、Exceptionプロパティの中身を確認している
                t.ContinueWith((compt) =>
                {
                    WriteLine("Sample3_Outer:" + compt.Exception.GetType()); //Sample3_Outer:System.AggregateException

                    if (compt.Exception is AggregateException age)
                        WriteLine("Sample3_Inner:" + age.InnerException.GetType()); //Sample3_Inner:System.NotImplementedException
                });
            }
            catch(Exception ex)
            {
                WriteLine("Sample3:" + ex.GetType()); //ここには来れない(例外をその場で処理していないため)
            }
        }

        /// <summary>
        /// 複数のTaskをTask.WhenAll()で待った時に、各タスクで起きた例外をまとめて取得
        /// </summary>
        private async void Sample4()
        {
            //Task.WhenAll()をtry catchでキャッチした例外は、複数例外がまとめられたAggregateExceptionではなく
            //各タスクで起きた例外のうち１つだけが入ったものになっている
            //起きた例外全部を拾おうとすると、Task.WhenAll()のタスクを受けたローカル変数の中のExceptionプロパティを見る必要がある
            //(それがAggregateExceptionとなっている)

            var t1 = Task.Run(() => { throw new NotImplementedException(); });
            var t2 = Task.Run(() => { throw new ArgumentException(); });
            var t3 = Task.Run(() => { throw new InvalidOperationException(); });
            var all = Task.WhenAll(t1, t2, t3);

            try
            {
                //WhenAll()のタスクのローカル変数を作って、それをtry catchする
                await all;
            }
            catch(Exception ex)
            {
                WriteLine("Sample4_Outer:" + ex.GetType()); //Sample4_Outer:System.NotImplementedException

                //exには例外が1つしか入っていないので、
                //WhenAllタスクのローカル変数のExceptionプロパティを見て
                //全ての例外をLINQで取り出す
                if (all.Exception is AggregateException age)
                    age.InnerExceptions.ToList().ForEach((ages) => WriteLine("Sample4_Inner:" + ages.GetType()));
                    //Sample4_Inner:System.NotImplementedException
                    //Sample4_Inner:System.ArgumentException
                    //Sample4_Inner:System.InvalidOperationException
            }
        }
    }
}
