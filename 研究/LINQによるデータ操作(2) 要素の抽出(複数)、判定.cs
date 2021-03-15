using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace t
{
    /// <summary>
    /// LINQによるデータ操作(2)
    /// 参考:https://qiita.com/Yt330110713/items/ff6face91aef6b400e08
    /// メインテーマ:要素の抽出(複数)、判定
    /// </summary>
    class LINQPractice2
    {
        public void MainProgram()
        {
            Sample_Where();
            WriteLine("------------");
            Sample_Distinct();
            WriteLine("------------");
            Sample_Skip();
            WriteLine("------------");
            Sample_Take();
            WriteLine("------------");
            Sample_All();
            WriteLine("------------");
            Sample_Contains();
            WriteLine("------------");

            ReadKey();
        }

        /// <summary>
        /// 要素の抽出(複数)　Where
        /// </summary>
        public void Sample_Where()
        {
            //合致する条件のものを複数かえす
            //最頻出

            var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var tmp = list.Where(x => x >= 5);
            
            foreach(var item in tmp)
            {
                WriteLine(item);
            }

            //6,7,8,9,10
        }

        /// <summary>
        /// 要素の抽出(複数)　Distinct
        /// </summary>
        public void Sample_Distinct()
        {
            //一意のデータを抽出する
            //重複データを削除した結果をかえす

            var list = new List<int> { 1, 2, 3, 3, 3, 4, 5, 6, 6, 6, 6, 7, 8, 9, 10 };
            var tmp = list.Distinct().ToList();

            foreach (var item in tmp)
            {
                WriteLine(item);
            }

            //1,2,3,4,5,6,7,8,9,10
        }

        /// <summary>
        /// 要素の抽出(複数)　Skip
        /// </summary>
        public void Sample_Skip()
        {
            //リストの先頭から指定した数だけスキップした要素の配列をかえす

            var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var tmp = list.Skip(3);

            foreach (var item in tmp)
            {
                WriteLine(item);
            }

            //4,5,6,7,8,9,10
        }

        /// <summary>
        /// 要素の抽出(複数)　Take
        /// </summary>
        public void Sample_Take()
        {
            //リストの先頭から指定した数だけの要素を取り出す

            var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var tmp = list.Take(3);

            foreach (var item in tmp)
            {
                WriteLine(item);
            }

            //1,2,3
        }

        /// <summary>
        /// 要素の判定　All
        /// </summary>
        public void Sample_All()
        {
            //要素のすべてが指定した条件を満たしているかを確認

            var SameList = new List<int> { 1,1,1,1,1 };
            var NotSameList = new List<int> { 1, 1, 2, 3, 4 };

            WriteLine(SameList.All(x => x == 1)); //全て1なのでtrue
            WriteLine(NotSameList.All(x => x == 1)); //1ではない要素があるのでfalse
        }

        /// <summary>
        /// 要素の判定　Any
        /// </summary>
        public void Sample_Any()
        {
            //要素のいずれかが指定した条件を満たしているかを確認

            var SameList = new List<int> { 1, 1, 1, 1, 1 };
            var NotSameList = new List<int> { 1, 1, 2, 3, 4 };

            WriteLine(SameList.Any(x => x >= 4)); //4以上の要素はないのでfalse
            WriteLine(NotSameList.Any(x => x >= 4)); //要素のうち、4が条件を満たすのでtrue
        }

        /// <summary>
        /// 要素の判定　Contains
        /// </summary>
        public void Sample_Contains()
        {
            //要素のいずれかが指定した要素を含んでいるかを確認
            //Anyと違い、ラムダ式を使わず文字や数字を直接指定する

            var SameList = new List<int> { 1, 1, 1, 1, 1 };
            var NotSameList = new List<int> { 1, 1, 2, 3, 4 };

            WriteLine(SameList.Contains(2)); //false
            WriteLine(NotSameList.Contains(2)); //true
        }

    }
}
