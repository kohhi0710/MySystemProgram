using System;
using System.Collections.Generic;
using static CreateQueryClass_Constructor;

namespace t
{
    class QueryGenerator
    {
        public string _SetQuery { get; set; }

        //プロパティ　いらんかも
        public CreateQuery_SELECT _SELECT { get; set; }
        public CreateQuery_FROM _FROM { get; set; }
        public CreateQuery_INSERT _INSERT { get; set; }
        public CreateQuery_UPDATE _UPDATE { get; set; }
        public CreateQuery_WHERE _WHERE { get; set; }
        public CreateQuery_ORDER_BY _ORDER_BY { get; set; }
        public CreateQuery_LEFT_JOIN _LEFT_JOIN { get; set; }
        public CreateQuery_RIGHT_JOIN _RIGHT_JOIN { get; set; }
        public CreateQuery_Command _COMMAND { get; set; }

        public void CreateQuery(string Value)
        {
            _SetQuery = _SetQuery + " " + Environment.NewLine + Value;
        }
    }

    /// <summary>
    /// SELECT文の生成
    /// </summary>
    class CreateQuery_SELECT
    {
        public string _select = "select ";
        public string _Query { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="AddCommand">追加コマンド</param>
        public CreateQuery_SELECT(string AddCommand = "")
        {
            _Query = "";
            _select = init1(_select, AddCommand);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Value">初期値 csv形式の場合は区切って初期値に設定される</param>
        /// <param name="AddCommand">追加コマンド</param>
        public CreateQuery_SELECT(string Value, string AddCommand = "")
        {
            _Query = "";
            _Query = init2(_Query,_select,Value,AddCommand);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="List">初期値</param>
        /// <param name="AddCommand">追加コマンド</param>
        public CreateQuery_SELECT(List<string> List,string AddCommand = "")
        {
            _Query = "";
            _Query = init3(_Query, _select, List, AddCommand);
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

    /// <summary>
    /// FROM文の生成
    /// </summary>
    class CreateQuery_FROM
    {
        public string _from = "from ";
        public string _Query { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="AddCommand">追加コマンド</param>
        public CreateQuery_FROM(string AddCommand = "")
        {
            _Query = "";
            _from = init1(_from, AddCommand);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Value">初期値 csv形式の場合は区切って初期値に設定される</param>
        /// <param name="AddCommand">追加コマンド</param>
        public CreateQuery_FROM(string Value, string AddCommand = "")
        {
            _Query = "";
            _Query = init2(_Query, _from, Value, AddCommand);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="List">初期値</param>
        /// <param name="AddCommand">追加コマンド</param>
        public CreateQuery_FROM(List<string> List,string AddCommand = "")
        {
            _Query = "";
            _Query = init3(_Query, _from, List, AddCommand);
        }

        /// <summary>
        /// クエリに値を追加する
        /// </summary>
        /// <param name="Value"></param>
        public void AddQuery(string Value)
        {
            if (_Query == "")
                _Query = _from + Environment.NewLine + " " + Value + " ";
            else
                _Query = _Query + Environment.NewLine + "," + Value + " ";
        }
    }

    /// <summary>
    /// INSERT文の生成
    /// </summary>
    class CreateQuery_INSERT
    {
        public string _insert = "insert ";
        public string _Query { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="AddCommand">追加コマンド</param>
        public CreateQuery_INSERT(string AddCommand = "")
        {
            _Query = "";
            _insert = init1(_insert, AddCommand);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Value">初期値 csv形式の場合は区切って初期値に設定される</param>
        /// <param name="AddCommand">追加コマンド</param>
        public CreateQuery_INSERT(string Value,string AddCommand = "")
        {
            _Query = "";
            _Query = init2(_Query, _insert, Value, AddCommand);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="List">初期値</param>
        /// <param name="AddCommand">追加コマンド</param>
        public CreateQuery_INSERT(List<string> List,string AddCommand = "")
        {
            _Query = "";
            _Query = init3(_Query, _insert, List, AddCommand);
        }

        /// <summary>
        /// クエリに値を追加する
        /// </summary>
        /// <param name="Value"></param>
        public void AddQuery(string Value)
        {
            if (_Query == "")
                _Query = _insert + Environment.NewLine + " " + Value + " ";
            else
                _Query = _Query + Environment.NewLine + "," + Value + " ";
        }
    }

    /// <summary>
    /// UPDATE文の生成
    /// </summary>
    class CreateQuery_UPDATE
    {
        public string _update = "update ";
        public string _Query { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="AddCommand">追加コマンド</param>
        public CreateQuery_UPDATE(string AddCommand = "")
        {
            _Query = "";
            _update = init1(_update, AddCommand);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Value">初期値 csv形式の場合は区切って初期値に設定される</param>
        /// <param name="AddCommand">追加コマンド</param>
        public CreateQuery_UPDATE(string Value, string AddCommand = "")
        {
            _Query = "";
            _Query = init2(_Query, _update, Value, AddCommand);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="List">初期値</param>
        /// <param name="AddCommand">追加コマンド</param>
        public CreateQuery_UPDATE(List<string> List, string AddCommand = "")
        {
            _Query = "";
            _Query = init3(_Query, _update, List, AddCommand);
        }

        /// <summary>
        /// クエリに値を追加する
        /// </summary>
        /// <param name="Value"></param>
        public void AddQuery(string Value)
        {
            if (_Query == "")
                _Query = _update + Environment.NewLine + " " + Value + " ";
            else
                _Query = _Query + Environment.NewLine + "," + Value + " ";
        }
    }

    /// <summary>
    /// WHERE文の生成
    /// </summary>
    class CreateQuery_WHERE
    {
        public string _where = "where ";
        public string _Query { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="AddCommand">追加コマンド</param>
        public CreateQuery_WHERE(string AddCommand = "")
        {
            _Query = "";
            _where = init1(_where, AddCommand);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Value">初期値 csv形式の場合は区切って初期値に設定される</param>
        /// <param name="AddCommand">追加コマンド</param>
        public CreateQuery_WHERE(string Value, string AddCommand = "")
        {
            _Query = "";
            _Query = init2(_Query, _where, Value, AddCommand);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="List">初期値</param>
        /// <param name="AddCommand">追加コマンド</param>
        public CreateQuery_WHERE(List<string> List, string AddCommand = "")
        {
            _Query = "";
            _Query = init3(_Query, _where, List, AddCommand);
        }

        /// <summary>
        /// クエリに値を追加する
        /// </summary>
        /// <param name="Value"></param>
        public void AddQuery(string Value)
        {
            if (_Query == "")
                _Query = _where + Environment.NewLine + " " + Value + " ";
            else
                _Query = _Query + Environment.NewLine + "," + Value + " ";
        }
    }

    /// <summary>
    /// ORDER BY文の生成
    /// </summary>
    class CreateQuery_ORDER_BY
    {
        public string _order_by = "order by ";
        public string _Query { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="AddCommand">追加コマンド</param>
        public CreateQuery_ORDER_BY(string AddCommand = "")
        {
            _Query = "";
            _order_by = init1(_order_by, AddCommand);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Value">初期値 csv形式の場合は区切って初期値に設定される</param>
        /// <param name="AddCommand">追加コマンド</param>
        public CreateQuery_ORDER_BY(string Value, string AddCommand = "")
        {
            _Query = "";
            _Query = init2(_Query, _order_by, Value, AddCommand);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="List">初期値</param>
        /// <param name="AddCommand">追加コマンド</param>
        public CreateQuery_ORDER_BY(List<string> List, string AddCommand = "")
        {
            _Query = "";
            _Query = init3(_Query, _order_by, List, AddCommand);
        }

        /// <summary>
        /// クエリに値を追加する
        /// </summary>
        /// <param name="Value"></param>
        public void AddQuery(string Value)
        {
            if (_Query == "")
                _Query = _order_by + Environment.NewLine + " " + Value + " ";
            else
                _Query = _Query + Environment.NewLine + "," + Value + " ";
        }
    }

    /// <summary>
    /// LEFT JOIN文の生成
    /// </summary>
    class CreateQuery_LEFT_JOIN
    {
        public string _left_join = "left join ";
        public string _Query { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name = "AddCommand" > 追加コマンド </ param >
        public CreateQuery_LEFT_JOIN(string AddCommand = "")
        {
            _Query = "";
            _left_join = init1(_left_join, AddCommand);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name = "Value" > 初期値 csv形式の場合は区切って初期値に設定される</param>
        /// <param name = "AddCommand" > 追加コマンド </ param >
        public CreateQuery_LEFT_JOIN(string Value, string AddCommand = "")
        {
            _Query = "";
            _Query = init2(_Query, _left_join, Value, AddCommand);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name = "List" > 初期値 </ param >
        /// < param name="AddCommand">追加コマンド</param>
        public CreateQuery_LEFT_JOIN(List<string> List, string AddCommand = "")
        {
            _Query = "";
            _Query = init3(_Query, _left_join, List, AddCommand);
        }

        /// <summary>
        /// クエリに値を追加する
        /// </summary>
        /// <param name = "Value" ></ param >
        public void AddQuery(string Value)
        {
            if (_Query == "")
                _Query = _left_join + Environment.NewLine + " " + Value + " ";
            else
                _Query = _Query + Environment.NewLine + "," + Value + " ";
        }
    }

    /// <summary>
    /// RIGHT JOIN文の生成
    /// </summary>
    class CreateQuery_RIGHT_JOIN
    {
        public string _right_join = "right join ";
        public string _Query { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="AddCommand">追加コマンド</param>
        public CreateQuery_RIGHT_JOIN(string AddCommand = "")
        {
            _Query = "";
            _right_join = init1(_right_join, AddCommand);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Value">初期値 csv形式の場合は区切って初期値に設定される</param>
        /// <param name="AddCommand">追加コマンド</param>
        public CreateQuery_RIGHT_JOIN(string Value, string AddCommand = "")
        {
            _Query = "";
            _Query = init2(_Query, _right_join, Value, AddCommand);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="List">初期値</param>
        /// <param name="AddCommand">追加コマンド</param>
        public CreateQuery_RIGHT_JOIN(List<string> List, string AddCommand = "")
        {
            _Query = "";
            _Query = init3(_Query, _right_join, List, AddCommand);
        }

        /// <summary>
        /// クエリに値を追加する
        /// </summary>
        /// <param name="Value"></param>
        public void AddQuery(string Value)
        {
            if (_Query == "")
                _Query = _right_join + Environment.NewLine + " " + Value + " ";
            else
                _Query = _Query + Environment.NewLine + "," + Value + " ";
        }
    }

    /// <summary>
    /// 任意のコマンド文の生成
    /// </summary>
    class CreateQuery_Command
    {
        public string _MainCommand;
        public string _Query { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="MainCommand">コマンド指定</param>
        /// <param name="AddCommand">追加コマンド</param>
        public CreateQuery_Command(string MainCommand,string AddCommand = "")
        {
            _MainCommand = MainCommand + " ";
            _Query = "";
            _MainCommand = init1(MainCommand, AddCommand);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="MainCommand">コマンド指定</param>
        /// <param name="Value">初期値 csv形式の場合は区切って初期値に設定される</param>
        /// <param name="AddCommand">追加コマンド</param>
        public CreateQuery_Command(string MainCommand,string Value, string AddCommand = "")
        {
            _MainCommand = MainCommand + " ";
            _Query = "";
            _Query = init2(_Query, _MainCommand, Value, AddCommand);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="MainCommand">コマンド指定</param>
        /// <param name="List">初期値</param>
        /// <param name="AddCommand">追加コマンド</param>
        public CreateQuery_Command(string MainCommand,List<string> List, string AddCommand = "")
        {
            _MainCommand = MainCommand + " ";
            _Query = "";
            _Query = init3(_Query, _MainCommand, List, AddCommand);
        }

        /// <summary>
        /// クエリに値を追加する
        /// </summary>
        /// <param name="Value"></param>
        public void AddQuery(string Value)
        {
            if (_Query == "")
                _Query = _MainCommand + Environment.NewLine + " " + Value + " ";
            else
                _Query = _Query + Environment.NewLine + "," + Value + " ";
        }
    }
}

/// <summary>
/// コンストラクタ処理クラス
/// </summary>
public static class CreateQueryClass_Constructor
{
    public static string init1(string MainCommand, string AddCommand = "")
    {
        return MainCommand + AddCommand + " ";
    }

    public static string init2(string _Query, string MainCommand, string Value, string AddCommand = "")
    {
        var SplitValue = Value.Split(Convert.ToChar(","));

        if (SplitValue.Length == 0)
        {
            if (_Query == "")
                _Query = MainCommand + AddCommand + Environment.NewLine + " " + Value + " ";
            else
                _Query = _Query + Environment.NewLine + "," + Value + " ";
        }
        else
        {
            foreach (var item in SplitValue)
            {
                if (_Query == "")
                    _Query = MainCommand + AddCommand + Environment.NewLine + " " + item + " ";
                else
                    _Query = _Query + Environment.NewLine + "," + item + " ";
            }
        }

        return _Query;
    }

    public static string init3(string _Query, string MainCommand, List<string> List, string AddCommand = "")
    {
        foreach (var item in List)
        {
            if (_Query == "")
                return MainCommand + AddCommand + Environment.NewLine + " " + item + " ";
            else
                _Query = _Query + Environment.NewLine + "," + item + " ";
        }

        return _Query;
    }
}
