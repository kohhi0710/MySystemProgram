using System;
using static SystemModule;

namespace t
{
    /// <summary>
    /// データベース接続クラス(SQL Server)
    /// </summary>
    class SQLServer_Connection_Method : SQLServer_Connection_Base
    {
        public string _Query_SQL;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SQLServer_Connection_Method():base()
        {
            _Query_SQL = "";
        }

        /// <summary>
        /// データ読み込みSQLを実行する
        /// </summary>
        /// <param name="sql">クエリ文</param>
        public void GetSQL(string sql)
        {
            try
            {
                _Query_SQL = sql;
                _Log_Method.PutLog(3, "SQL:" + _Query_SQL, "", "");
                CommandExecuteQuery(_Query_SQL);
            }
            catch(Exception ex)
            {
                _Log_Method.PutLog(5, "SQL:" + "! SQL ERROR ! :" + _Query_SQL, ex.Message, "");
                throw ex;
            }
        }

        /// <summary>
        /// データ更新SQLを実行する
        /// </summary>
        /// <param name="sql">クエリ文</param>
        public void UpdateData(string sql)
        {
            try
            {
                _Query_SQL = sql;
                _Log_Method.PutLog(3, "SQL:" + _Query_SQL, "", "");
                CommandExecuteNonQuery(_Query_SQL);
            }
            catch(Exception ex)
            {
                _Log_Method.PutLog(5, "SQL:" + "! SQL ERROR ! :" + _Query_SQL, ex.Message, "");
                throw ex;
            }
        }

        /// <summary>
        /// データ更新SQLを実行する(更新件数を取得)
        /// </summary>
        /// <param name="sql">クエリ文</param>
        public int UpdateData_Count(string sql)
        {
            int Count = 0;

            try
            {
                _Query_SQL = sql;
                _Log_Method.PutLog(3, "SQL:" + _Query_SQL, "", "");
                Count = CommandExecuteNonQuery_Count(_Query_SQL);
            }
            catch (Exception ex)
            {
                _Log_Method.PutLog(5, "SQL:" + "! SQL ERROR ! :" + _Query_SQL, ex.Message, "");
                throw ex;
            }

            return Count;
        }

        /// <summary>
        /// データベース接続チェックを行う
        /// </summary>
        /// <param name="sql">クエリ文</param>
        /// <returns>接続判定True or False</returns>
        public bool DBAccessCheck(string sql)
        {
            bool Result;
            Result = true;

            //trueなら接続してfalseに変更
            if(Result == true)
            {
                Connect();
                Result = false;
            }

            bool GetFlg = false;

            try
            {
                CommandExecuteReader(_Query_SQL);

                if(_Reader != null)

                {
                    if (_Reader.Read())
                        GetFlg = true;
                }

                ReaderClose();
            }
            catch(Exception ex)
            {
                ReaderClose();
                throw ex;
            }

            if (Result)
                DisConnect();

            if (GetFlg == false)
                return false;

            return true;
        }
    }
}
