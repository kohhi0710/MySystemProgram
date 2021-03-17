using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace t
{
    /// <summary>
    /// LINQによるデータ操作(1)
    /// 参考:https://qiita.com/Yt330110713/items/d9823e30caacbf536d2b
    /// メインテーマ:要素の抽出(単一)
    /// </summary>
    class LINQPractice1
    {
        public void MainProgram()
        {
            Query_Sample();
            WriteLine("------------");
            Method_Sample();
            WriteLine("------------");
            Sample_FirstorDefault();
            WriteLine("------------");
            Sample_First();
            WriteLine("------------");
            Sample_LastOrDefault();
            WriteLine("------------");
            Sample_SingleOrDefault();
            WriteLine("------------");

            ReadKey();
        }

        List<int> list = new List<int> { 1, 51, 64, 2, 23, 24 };

        /// <summary>
        /// クエリ構文
        /// </summary>
        public void Query_Sample()
        {
            //クエリ構文
            var query = from x              //from句はなんでもいい
                        in list　　　　　　 //抽出したいリスト
                        where x % 2 == 0    //抽出条件
                        orderby x　　　　　 //並べ替え
                        select x;　　　　　 //selectでしめる

            foreach(var item in query)
            {
                WriteLine(item);
            }

            //2
            //24
            //64
        }

        /// <summary>
        /// メソッド構文(推奨) 
        /// </summary>
        public void Method_Sample()
        {
            //処理内容は上のクエリ構文と同じ

            var Method = list
                         .Where(x => x % 2 == 0)
                         .OrderBy(x => x);

            foreach(var item in Method)
            {
                WriteLine(item);
            }

            //2
            //24
            //64
        }

        /// <summary>
        /// 要素の抽出　FirstOrDefault
        /// </summary>
        public void Sample_FirstorDefault()
        {
            //単一の要素を取得するメソッド
            //該当する要素のはじめ(First)を取得、
            //該当しない(Default)の場合はnullを返す

            var list_sample = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var query = list_sample.FirstOrDefault();
            WriteLine(query); //1

            //引数にラムダ式を指定することで条件を指定することも可能
            var query2 = list_sample.FirstOrDefault(x => x >= 5);
            WriteLine(query2); //5

            //条件に合致しない場合、規定値である0がかえってくる
            var query3 = list_sample.FirstOrDefault(x => x > 100);
            WriteLine(query3); //0

            //文字列に対してのクエリ、見つからない場合はnullがかえる
            var list_sample2 = new List<string> { "Osaka", "Hokkaido", "Tokyo", "Fukuoka" };
            var query4 = list_sample2.FirstOrDefault(x => x == "Kanagawa");
            WriteLine(query4); //null
        }

        /// <summary>
        /// 要素の抽出　First
        /// </summary>
        public void Sample_First()
        {
            //単一の要素を取得するメソッド
            //指定の要素が見つかればFirstOrDefaultと変わらないが、見つからない場合は例外がスローされる

            try
            {
                var list_sample = new List<string> { "Osaka", "Hokkaido", "Tokyo", "Fukuoka" };
                var query = list_sample.First(x => x == "Kanagawa"); //Kanagawaは存在しないので例外がスローされる
                WriteLine(query);
            }
            catch(Exception ex)
            {
                WriteLine(ex.Message); //シーケンスに一致する要素は含まれません。
            }
        }

        /// <summary>
        /// 要素の抽出　LastOrDefault
        /// </summary>
        public void Sample_LastOrDefault()
        {
            //該当する要素の最後を取得する

            var list_sample = new List<int> { 50, 60, 70, 80, 90 };
            WriteLine(list_sample.LastOrDefault(x => x > 70)); //90
        }

        /// <summary>
        /// 要素の抽出　SingleOrDefault
        /// </summary>
        public void Sample_SingleOrDefault()
        {
            //要素数が単一であった場合に、その要素をかえす
            //要素数が複数あった場合は例外がスローされ、ひとつもなかった場合は規定値0をかえす

            var list_sample = new List<int> { 50, 60, 70, 70, 80, 90 };

            //要素数が複数(例外スロー)
            try
            {
                WriteLine(list_sample.SingleOrDefault(x => x == 70)); //例外スロー
            }
            catch(Exception ex)
            {
                WriteLine(ex.Message); //シーケンスに複数の要素が含まれています。
            }
            //要素数が単一
            WriteLine(list_sample.SingleOrDefault(x => x == 80)); //80
            //合致なし(規定値0をかえす)
            WriteLine(list_sample.SingleOrDefault(x => x == 100)); //0
        }
    }
}
