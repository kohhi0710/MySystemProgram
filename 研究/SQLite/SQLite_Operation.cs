using System;
using System.Data.SQLite;

namespace t
{
    /// <summary>
    /// SQLite操作クラス
    /// </summary>
    class SQLite_Operation
    {
        #region "Field"
        /// <summary>
        /// データベースファイルパス
        /// </summary>
        private string _DBFilePath;
        /// <summary>
        /// DB接続オブジェクト
        /// </summary>
        private SQLiteConnection _Connect;
        /// <summary>
        /// データリーダー
        /// </summary>
        public SQLiteDataReader _Reader;
        /// <summary>
        /// トランザクション
        /// </summary>
        private SQLiteTransaction _Tran;
        #endregion

        #region "Constructor"
        /// <summary>
        /// コンストラクタ データベースファイルパスの定義
        /// </summary>
        /// <param name="DatabaseFilePath">データベースファイルパス</param>
        public SQLite_Operation(string DatabaseFilePath)
        {
            //ファイルパスをもとにデータベースファイルを作成する
            _DBFilePath = DatabaseFilePath;
            _Connect = new SQLiteConnection("Data Source=" + _DBFilePath);
        }
        #endregion

        #region "Method"

        /// <summary>
        /// データベースに接続する
        /// </summary>
        public void OpenDatabase()
        {
            try
            {
                if (_Connect == null)
                    throw new Exception("SQLite_Operation.OpenDatabase : データベースの接続に失敗しました。");

                _Connect = new SQLiteConnection("Data Source=" + _DBFilePath);

                try
                {
                    _Connect.Open();
                }
                catch (Exception ex)
                {
                    CloseDatabase();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// データベースとの接続を解除する
        /// </summary>
        public void CloseDatabase()
        {
            try
            {
                if (_Connect == null)
                    throw new Exception("SQLite_Operation.CloseDatabase : データベースの接続解除に失敗しました。");

                try
                {
                    _Connect.Close();
                }
                catch(Exception ex)
                {
                    _Connect.Close();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// テーブルを作成する
        /// </summary>
        /// <param name="TableName">テーブル名</param>
        /// <param name="TableParameterQuery">テーブルカラムパラメーターの設定クエリ　※カッコ内の値を入力</param>
        public void CreateDatabaseTable(string TableName,string TableParameterQuery)
        {
            try
            {
                if (_Connect == null)
                    throw new Exception("SQLite_Operation.CreateDatabaseTable : データベースのテーブル作成に失敗しました。");

                try
                {
                    using (var Command = _Connect.CreateCommand())
                    {
                        Command.CommandText = "create table " +
                                                TableName + "(" +
                                                TableParameterQuery + ")";
                        Command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    string ExMsg = "SQL logic error\r\ntable " + TableName + " already exists";
                    if (ex.Message != ExMsg)
                        throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// SELECT文を実行して_Readerに結果をセットする
        /// </summary>
        /// <param name="Query">SELECT文</param>
        public void SelectQueryExecution(string SELECTQuery)
        {
            try
            {
                if (_Connect == null)
                    throw new Exception("SQLite_Operation.SelectQueryExecution : データベースのクエリ実行に失敗しました。");

                try
                {
                    using (var Command = _Connect.CreateCommand())
                    {
                        Command.CommandText = SELECTQuery;
                        _Reader = Command.ExecuteReader();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// クエリを実行し、データベースを操作する(insert、update、deleteなど)
        /// </summary>
        /// <param name="DatabaseOperationQuery">データベース操作クエリ</param>
        public void DataOperationQueryExecution(string DatabaseOperationQuery)
        {
            try
            {
                if (_Connect == null)
                    throw new Exception("SQLite_Operation.DataOperationQueryExecution : データベースのクエリ実行に失敗しました。");

                try
                {
                    using (var Command = _Connect.CreateCommand())
                    {
                        Command.CommandText = DatabaseOperationQuery;
                        Command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// トランザクションスタート
        /// </summary>
        public void StartTransaction()
        {
            try
            {
                if (_Connect == null)
                    throw new Exception("SQLite_Operation.StartTransaction : トランザクションの開始に失敗しました。");

                try
                {
                    if(_Tran != null)
                    {
                        _Tran = null;
                        _Tran = _Connect.BeginTransaction();
                    }

                    _Tran = _Connect.BeginTransaction();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// トランザクションコミット
        /// </summary>
        public void CommitTransaction()
        {
            try
            {
                if (_Connect == null)
                    throw new Exception("SQLite_Operation.CommitTransaction : トランザクションのコミットに失敗しました。");

                try
                {
                    if(_Tran != null)
                    {
                        _Tran.Commit();
                    }
                    else
                    {
                        throw new Exception("SQLite_Operation.CommitTransaction : トランザクションがスタートされていません。");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// トランザクションロールバック
        /// </summary>
        public void RollBackTransaction()
        {
            try
            {
                if (_Connect == null)
                    throw new Exception("SQLite_Operation.RollBackTransaction : トランザクションのロールバックに失敗しました。");

                try
                {
                    if (_Tran != null)
                    {
                        _Tran.Rollback();
                    }
                    else
                    {
                        throw new Exception("SQLite_Operation.RollBackTransaction : トランザクションがスタートされていません。");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion
    }

    /// <summary>
    /// 使い方サンプル
    /// </summary>
    class SQLite_SampleProgram
    {
        public void MainProgram()
        {
            var sqlite = new SQLite_Operation(System.AppDomain.CurrentDomain.BaseDirectory + "TestDataBase.db");

            try
            {
                sqlite.OpenDatabase();
                sqlite.StartTransaction();

                sqlite.CreateDatabaseTable("Sample", "ID integer, Name string");
                sqlite.DataOperationQueryExecution("insert into Sample(ID,Name) values(1,'りんご') ");
                sqlite.DataOperationQueryExecution("insert into Sample(ID,Name) values(2,'ぶどう') ");
                sqlite.DataOperationQueryExecution("insert into Sample(ID,Name) values(3,'みかん') ");

                sqlite.SelectQueryExecution("select * from Sample");
                while (sqlite._Reader.Read())
                {
                    //どちらも同じ値を取得できる
                    Console.WriteLine(sqlite._Reader["ID"].ToString() + "," + sqlite._Reader["Name"].ToString());
                    Console.WriteLine(sqlite._Reader[0].ToString() + "," + sqlite._Reader[1].ToString());
                }

                sqlite.CommitTransaction();
                sqlite.CloseDatabase();
            }
            catch (Exception ex)
            {
                sqlite.RollBackTransaction();
                sqlite.CloseDatabase();
                Console.WriteLine(ex.Message);　//ログを出す(暫定的にコンソール出力)
            }
        }

        public void MainProgram2()
        {
            var sqlite = new SQLite_Operation(System.AppDomain.CurrentDomain.BaseDirectory + "TestDataBase.db");

            try
            {
                sqlite.OpenDatabase();

                sqlite.SelectQueryExecution("select * from Sample");
                while (sqlite._Reader.Read())
                {
                    Console.WriteLine(sqlite._Reader["ID"].ToString() + "," + sqlite._Reader["Name"].ToString());
                }

                sqlite.CloseDatabase();
            }
            catch (Exception ex)
            {
                sqlite.CloseDatabase();
                Console.WriteLine(ex.Message);　//ログを出す(暫定的にコンソール出力)
            }
        }
    }
}
