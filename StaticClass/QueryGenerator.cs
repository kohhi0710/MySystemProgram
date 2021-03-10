using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t
{
    class QueryGenerator
    {
        public string _Query { get; set; }

        public SELECT _SELECT { get; set; }
        public FROM _FROM { get; set; }

        public QueryGenerator()
        {

        }

    }

    /// <summary>
    /// SELECT文の生成
    /// </summary>
    class SELECT
    {
        public const string _select = "select ";
        public string _Query { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SELECT()
        {
            _Query = "";
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Value">初期値</param>
        public SELECT(string Value)
        {
            _Query = "";

            if (_Query == "")
                _Query = _select + Environment.NewLine + " " + Value + " ";
            else
                _Query = _Query + Environment.NewLine + "," + Value + " ";
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="List">初期値</param>
        public SELECT(List<string> List)
        {
            _Query = "";

            foreach(var item in List)
            {
                if (_Query == "")
                    _Query = _select + Environment.NewLine + " " + item + " ";
                else
                    _Query = _Query + Environment.NewLine + "," + item + " ";
            }
        }


        /// <summary>
        /// クエリに値を追加する
        /// </summary>
        /// <param name="Value"></param>
        public void AddQuery(string Value)
        {
            if (_Query == "")
                _Query = _select + Environment.NewLine + " " + Value + " ";
            else
                _Query = _Query + Environment.NewLine + "," + Value + " ";
        }
    }

    class FROM
    {

    }


}
