using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace t
{
    /// <summary>
    /// 非同期・並列処理研究
    /// 参考:https://qiita.com/unsignedint/items/2bb663c8fb92ff0d2b5c
    /// </summary>
    class AsyncAwaitTest2
    {
        public void MainProgram()
        {
            Sample_PLINQ();
            Sample_Lock();
            list2.OrderBy(x => x); //リストのソート
            list2.ForEach(x => WriteLine(x));
        }

        List<int> list = new List<int> {1,2,3,4,5,6,7,8,9,10};
        List<int> list2 = new List<int>();

        /// <summary>
        /// PLINQのサンプル
        /// </summary>
        public void Sample_PLINQ()
        {
            //PLINQはLINQの並列処理版
            //.AsParallelを入れることで並列的なデータ取り出しが可能となる

            //この場合はライブラリ側でコレクションが精査され、
            //並列処理が安全、またはメリットがあるという結果になった場合において並列処理になる。
            //そうでなければ逐次的に実行される
            var data = list.AsParallel()
                           .Where(n => n % 2 == 0)
                           .Select(n => n);

            //どの程度の並列処理を行うかという設定は.WithDegreeParallelismで調整する
            //ネットワークリソースアクセスが絡む場合など、同時アクセスに制約がある場合に使用できる。
            //これが指定されない場合、コア数を元に自動的に設定される
            var data2 = list.AsParallel()
                            .WithDegreeOfParallelism(2)
                            .Where(n => n % 2 == 0)
                            .Select(n => n);

            //上記は順序が保証されていないので、順序が重要になってくる場合は.AsOrderedを使用する
            //使用しない場合に比べ、オーバーヘッド(余計な処理、負荷)がより高くなる
            var data3 = list.AsParallel()
                            .AsOrdered()
                            .Where(n => n % 2 == 0)
                            .Select(n => n);

            //データの取り出しの他、何らかの処理がしたい場合はForAllを使う
            //これを使用するとコレクションに対し、ラムダ式を指定し非同期的に処理を行うことができる
            //この実行時にAsOrderedを使うこともできるが並列処理が開始される順序は既知である場合においても
            //処理が実際に終了する時間は開きが出てくるため、必ずしも順序が保全されて出てくるとは限らない。
            //この場合は後ほどソートなどを行う必要が出てくる
            list.AsParallel()
                .ForAll(p =>
                {
                    p = p * 2;
                });

            //Parallel処理
            //PLINQはコレクションに対して行うことのできる処理だが、
            //汎用的に並列処理を行うための機構としてTask Parallel Libraryが用意されている
            //PLINQと似ているが、実際にはPLINQが同じ機構を利用しているため。
            Parallel.ForEach(list, p =>
             {
                 p = p * 2;
             });

            //PLINQには用意されていない機構として、Parallel.Forもある
            //やはり順番は保証されないので、何らかの形で調整する必要がある。
            Parallel.For(1, 100, index =>
              {
                  WriteLine($"{index}回目のループです。");
              });
        }

        /// <summary>
        /// 並列処理のロック
        /// </summary>
        public void Sample_Lock()
        {
            //並列的に実行しながら、他のコレクションに結果を追加していきたいシーンの場合

            //このままだと問題が発生する。
            //list.Add(p)のタイミングによっては正常にデータが追加されない恐れがある

            //list.AsParallel().ForAll(p =>
            //{
            //    p = p * 2;
            //    list2.Add(p);
            //});

            //このような場合、データをロックする必要がある

            list.AsParallel().ForAll(p =>
            {
                p = p * 2;

                lock (list2) //追加先のコレクションをロック
                {
                    list2.Add(p);
                }
            });
        }
    }
}