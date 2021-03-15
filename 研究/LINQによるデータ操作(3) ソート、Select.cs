using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace t
{
    /// <summary>
    /// LINQによるデータ操作(3)
    /// 参考:https://qiita.com/Yt330110713/items/fd9892217e4786305c7c
    /// メインテーマ:ソート、Select
    /// </summary>
    class LINQPractice3
    {
        public void MainProgram()
        {
            Sample_Sort();
            WriteLine("------------");
            Sample_Select_Choice();
            WriteLine("------------");
            Sample_Select_Circ();
            WriteLine("------------");

            ReadKey();
        }

        /// <summary>
        /// ソート
        /// </summary>
        public void Sample_Sort()
        {
            //LINQのメソッド式を使えば配列やlistをソートすることができる
            
            //匿名メソッドで配列を作成
            var source = new[]
            {
                new{Id = 1,Name = "Taro",Age = 20,Sex = "M"},
                new{Id = 2,Name = "Jiro",Age = 20,Sex = "M"},
                new{Id = 3,Name = "Kuro",Age = 10,Sex = "M"},
                new{Id = 4,Name = "Haro",Age = 31,Sex = "W"},
            };

            //OrderBy
            var OrderedSource = source.OrderBy(x => x.Age); //Ageでソート

            foreach(var item in OrderedSource)
            {
                WriteLine(item);
                //{ Id = 3, Name = Kuro, Age = 10, Sex = M }
                //{ Id = 1, Name = Taro, Age = 20, Sex = M }
                //{ Id = 2, Name = Jiro, Age = 20, Sex = M }
                //{ Id = 4, Name = Haro, Age = 31, Sex = W }
            }

            WriteLine("------------");

            //OrderByDescending
            var OrderedSourceDis = source.OrderByDescending(x => x.Age); //Ageで降順ソート
            foreach (var item in OrderedSourceDis)
            {
                WriteLine(item);
                //{ Id = 4, Name = Haro, Age = 31, Sex = W }
                //{ Id = 1, Name = Taro, Age = 20, Sex = M }
                //{ Id = 2, Name = Jiro, Age = 20, Sex = M }
                //{ Id = 3, Name = Kuro, Age = 10, Sex = M }
            }
        }

        /// <summary>
        /// Select 選択
        /// </summary>
        public void Sample_Select_Choice()
        {
            //匿名メソッドで配列を作成
            var source = new[]
            {
                new{Id = 1,Name = "Taro",Age = 20,Sex = "M"},
                new{Id = 2,Name = "Jiro",Age = 20,Sex = "M"},
                new{Id = 3,Name = "Kuro",Age = 10,Sex = "M"},
                new{Id = 4,Name = "Haro",Age = 31,Sex = "W"},
            };

            //年齢でソートした配列
            var SelectedSource = source.Select(x => x.Age);
            //要素を全てカンマで連結させて出力
            WriteLine(string.Join(",", SelectedSource)); //20,20,10,31

            WriteLine("------------");

            //AgeとIdを抽出
            var SelectedSource2 = source.Select((x, y) => new { x.Age, x.Id });
            //要素を全てカンマで連結させて出力
            WriteLine(string.Join(",", SelectedSource2)); //{ Age = 20, Id = 1 },{ Age = 20, Id = 2 },{ Age = 10, Id = 3 },{ Age = 31, Id = 4 }
        }

        /// <summary>
        /// Select 演算
        /// </summary>
        public void Sample_Select_Circ()
        {
            var list = new List<int> { 1, 3, 5, 7, 9 };

            //全ての要素に3を加算
            var SelectedSource = list.Select(x => x + 3);
            //要素を全てカンマで連結させて出力
            WriteLine(string.Join(",", SelectedSource)); //4,6,8,10,12
        }
    }
}
