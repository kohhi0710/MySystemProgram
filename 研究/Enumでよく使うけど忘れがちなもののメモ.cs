using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace t
{
    /// <summary>
    /// Enumでよく使うけど忘れがちなもののメモ
    /// 参考:https://qiita.com/sy102/items/fbd62c862c26d001cba1
    /// </summary>
    static class EnumMemo
    {
        /// <summary>
        /// enumの基本型
        /// </summary>
        public enum Seasons
        {
            Spring,
            Summer,
            Autumn,
            Winter
        };

        public static void Main_Enum()
        {
            //使い方サンプル
            var season = Seasons.Spring;
            var JapaneseName = GetJapaneseName(season);

            //定義されている値かどうかチェック
            WriteLine(Enum.IsDefined(typeof(Seasons), 0)); //0番(Spring)が定義されているか→True
            WriteLine(Enum.IsDefined(typeof(Seasons), 4)); //4番が定義されているか→False
            Console.WriteLine(Enum.IsDefined(typeof(Seasons), "Spring"));  //true
            Console.WriteLine(Enum.IsDefined(typeof(Seasons), "spring"));  //false(大文字・小文字区別される)

            //型変換
            //int→Enum
            WriteLine((Seasons)0); //Spring
            WriteLine((Seasons)4); //4(定義されていない場合は数字をそのまま返す、例外は発生しない)

            //Enum→int
            WriteLine((int)Seasons.Winter); //3

            //String→Enum
            if(Enum.TryParse<Seasons>("Summer",out var s))
            {
                WriteLine(s); //Summer
            }

            //数値の文字列も変換できる
            if(Enum.TryParse<Seasons>("1",out var s2))
            {
                WriteLine(s2); //Summer
            }

            //Enum→String
            WriteLine(Seasons.Winter.ToString()); //Winter
        }

        /// <summary>
        /// Enumにメソッドを実装(拡張メソッド)
        /// </summary>
        /// <param name="param">Seasons(Enum型)のパラメータ</param>
        /// <returns></returns>
        public static string GetJapaneseName(this Seasons param)
        {
            switch(param)
            {
                case Seasons.Spring:
                    return "春";
                case Seasons.Summer:
                    return "夏";
                case Seasons.Autumn:
                    return "秋";
                case Seasons.Winter:
                    return "冬";
            }

            return "";
        }

        public static T? ConvertToEnum<T>(object value) where T : struct
        {
            //TがEnumかどうかチェック
            //where T : Enum にすると、戻り値をNullableにできない
            if (!typeof(T).IsEnum) return null;

            string stringValue;
            switch (value)
            {
                case int intVal:
                    stringValue = intVal.ToString();
                    break;
                case string stringVal:
                    stringValue = stringVal;
                    break;
                default:
                    //int, string以外は処理対象外とする
                    return null;
            }

            //TryParseする前に、定義されている値かチェックする
            //数値の場合、定義されていない値でもTryParseが成功してしまうため
            if (!Enum.IsDefined(typeof(T), value)) return null;

            if (Enum.TryParse<T>(stringValue, out var result))
            {
                return result;
            }
            return null;
        }
    }
}
