using System;
using System.Data.SqlClient;
using static SystemModule;

namespace t
{
    /// <summary>
    /// SQL Server接続スーパークラス
    /// </summary>
    class SQLServer_Connection_Base
    {
        private const int _MAXRetryCount = 10;  //再試行上限回数

        public SqlDataReader _Reader;          //Data Reader
        public SqlConnection _Connection;   //データベースコネクション
        public SqlTransaction _Transaction; //トランザクション定義
        public SqlCommand _Command;         //コマンド

        protected bool _ConnectFlg;         //接続情報フラグ　True:接続中  False:接続可

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SQLServer_Connection_Base()
        {
            _Connection = null;
            _Transaction = null;
            _ConnectFlg = false;
        }

        /// <summary>
        /// SQLServerと接続
        /// </summary>
        public void Connect()
        {
            if (_ConnectFlg == true)
                return;

            //リトライカウントと、リトライ判定フラグ
            int RetryCount = 0;
            bool RetryFlg = true;

            while(RetryFlg)
            {
                try
                {
                    //SQLコネクション用のパラメータを設定
                    string ConConfig = "Server=" + _ProgramConfig._ServerName + ";" 
                                     + " uid=" + _ProgramConfig._LoginUser + ";" 
                                     + " pwd=" + _ProgramConfig._LoginPassword + ";" 
                                     + " database=" + _ProgramConfig._DBName;

                    //コネクションオブジェクトの設定
                    _Command = new SqlCommand();
                    _Command.Connection = _Connection;

                    //コネクションの取得
                    _Connection = new SqlConnection(ConConfig);

                    //コネクション開始
                    _Connection.Open();

                    //フラグを接続中に設定
                    _ConnectFlg = true;
                    //リトライ不可に設定
                    RetryFlg = false;
                }
                catch(InvalidOperationException ex1)
                {
                    //無効な操作が発生したらリトライ
                    RetryCount = RetryCount + 1;

                    //リトライ上限なら例外スロー
                    if (RetryCount == _MAXRetryCount)
                    {
                        _Log_Method.PutLog(5, "SQL:" + "Connect エラー", ex1.Message, "");
                        throw ex1;
                    }

                    System.Threading.Thread.Sleep(1000);
                }
                catch(Exception ex9)
                {
                    ConClose();
                    _ConnectFlg = false;

                    //接続失敗のエラーログを出力
                    _Log_Method.PutLog(5, "SQL:" + "Connect エラー", ex9.Message, "");

                    throw ex9;
                }
            }
        }

        /// <summary>
        /// DBコネクションを開放する。接続情報フラグをFalseにする
        /// </summary>
        public void DisConnect()
        {
            //接続可の場合はretrun
            if (_ConnectFlg == false)
                return;

            //接続オブジェクトがnullの場合はreturn
            if (_Connection == null)
                return;

            ReaderClose();

            ConClose();
        }

        /// <summary>
        /// Readerオブジェクトを閉じ、nullにする
        /// </summary>
        public void ReaderClose()
        {
            if(_Reader != null)
            {
                _Reader.Close();
                _Reader = null;
            }
        }

        /// <summary>
        /// 接続終了処理
        /// </summary>
        public void ConClose()
        {
            //接続状態ならば、接続を終了する
            if (_Connection.State == System.Data.ConnectionState.Open)
            {
                _Connection.Close();
                _Connection = null;
            }
        }

        /// <summary>
        /// クエリを実行して結果をReaderオブジェクトに入れる。トランザクション対応版
        /// </summary>
        /// <param name="sql">クエリ文</param>
        public void CommandExecuteQuery(string sql)
        {
            try
            {
                //トランザクションオブジェクトがnullの時
                if (_Transaction == null)
                {
                    _Command = new SqlCommand(sql, _Connection);
                    _Reader = _Command.ExecuteReader();
                }
                else //トランザクション開始中はこっちの処理
                {
                    _Command = new SqlCommand(sql, _Connection,_Transaction);
                    _Reader = _Command.ExecuteReader();
                }
            }
            catch(Exception ex)
            {
                ReaderClose();
                throw ex;
            }
        }

        /// <summary>
        /// クエリを実行して影響を受ける行数を取得
        /// </summary>
        /// <param name="sql">クエリ文</param>
        public void CommandExecuteNonQuery(string sql)
        {
            try
            {
                _Command = new SqlCommand(sql, _Connection, _Transaction);

                //タイムアウトが発生する場合があるので、実行時間を1200秒(20分)まで待機
                _Command.CommandTimeout = 1200;

                _Command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw ex;

            }
        }

        /// <summary>
        /// クエリ文を実行して更新件数を取得する
        /// </summary>
        /// <param name="sql">クエリ文</param>
        /// <returns>更新件数</returns>
        public int CommandExecuteNonQuery_Count(string sql)
        {
            int Count = 0;

            try
            {
                _Command = new SqlCommand(sql, _Connection, _Transaction);

                //タイムアウトが発生する場合があるので、実行時間を1200秒(20分)まで待機
                _Command.CommandTimeout = 1200;

                Count = _Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Count;
        }

        /// <summary>
        /// クエリを実行して結果をReaderオブジェクトに入れる。
        /// </summary>
        /// <param name="sql">クエリ文</param>
        public void CommandExecuteReader(string sql)
        {
            try
            {
                _Command = new SqlCommand(sql, _Connection);
                _Reader = _Command.ExecuteReader();
            }
            catch (Exception ex)
            {
                ReaderClose();
                throw ex;
            }
        }

        /// <summary>
        /// トランザクションを開始する
        /// </summary>
        public void StartTransaction()
        {
            //トランザクション開始
            if (_Transaction == null)
                _Transaction = _Connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
        }

        /// <summary>
        /// トランザクションをコミットする
        /// </summary>
        public void CommitTransaction()
        {
            if(_Transaction != null)
            {
                _Transaction.Commit();
                _Transaction = null;
            }
        }

        /// <summary>
        /// トランザクションをロールバックする
        /// </summary>
        public void RollbackTransaction()
        {
            if (_Transaction != null)
            {
                _Transaction.Rollback();
                _Transaction = null;
            }
        }
    }
}