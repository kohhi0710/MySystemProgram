using System;
using System.Collections.Generic;
using System.Linq;
using static System.ValueTuple;
using System.Text;
using System.Threading.Tasks;

namespace t
{
    /// <summary>
    /// 匿名型とタプル型のちがい
    /// 参考：https://qiita.com/poka_try/items/27e5818517741e0a8a96
    /// NuGetからValueTupleパッケージをインストールする
    /// </summary>
    class tupple_test
    {
        public void Main_tup()
        {
            //匿名型
            List<string> list_ano = new List<string>() { "AAA", "BBB", "CCC", "DDD", "EEE" };
            var ano = list_ano.Select((str, index) => new { str, index });

            foreach(var item in list_ano)
            {
                Console.WriteLine(item);
            }

            //タプル
            List<string> list_tup = new List<string>() { "AAA", "BBB", "CCC", "DDD", "EEE" };
            var tup = list_tup.Select((str2, index2) => (str2, index2));
            (string s, int i) val = tup.FirstOrDefault();

            foreach (var item in list_tup)
            {
                Console.WriteLine(item);
            }
        }
    }
}
