using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace t
{
    /// <summary>
    /// なんと便利なラムダ式
    /// 参考:https://qiita.com/menow2525/items/3d2483520b3d71842b10
    /// </summary>
    class LamdaMehodSample
    {
        /// <summary>
        /// 簡単な例
        /// </summary>
        public void EasySample()
        {
            string Value = "Hello World";

            //ふつうのforeach
            foreach (var item in Value.Split(' '))
                WriteLine(item);
                //Hello
                //World

            //ラムダ式
            Value.Split(' ').ToList().ForEach(v => WriteLine(v));
            //Hello
            //World
        }

        /// <summary>
        /// 応用例
        /// </summary>
        public void AppliedSample()
        {
            string Value = "0, 1, 2, 3, 4, 5, 6, 7, 8, 9";

            //ふつうのforeach
            var ValueArray = Value.Split(',');
            var Num_foreach = new int[ValueArray.Length];
            int index = 0;

            foreach(var item in ValueArray)
            {
                Num_foreach[index] = int.Parse(item);
                index++;
            }

            foreach(var item in Num_foreach)
            {
                WriteLine(item);
                //0
                //1
                //2
                //3
                //4
                //5
                //6
                //7
                //8
                //9
            }

            //ラムダ式
            var Num_lambda = Value.Split(',').Select(v => int.Parse(v)).ToArray();

            foreach (var item in Num_lambda)
            {
                WriteLine(item);
                //0
                //1
                //2
                //3
                //4
                //5
                //6
                //7
                //8
                //9
            }
        }
    }
}
