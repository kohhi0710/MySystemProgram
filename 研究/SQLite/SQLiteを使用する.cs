using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Diagnostics;
using static System.Console;


namespace t
{
    /// <summary>
    /// SQLiteをC#で取り扱う
    /// 参考:http://rubbish.mods.jp/blog/2016/08/22/visual-studio%E3%81%A7c%E3%81%8B%E3%82%89sqlite%E3%82%92%E4%BD%BF%E3%81%86%E6%96%B9%E6%B3%95/
    /// </summary>
    class SQLiteConnection_Test
    {
        //データベースファイルのファイルパス
        public string _DBFilePath = System.AppDomain.CurrentDomain.BaseDirectory + "TestDataBase.db";
        //接続オブジェクト
        public SQLiteConnection Connect;
        //接続フラグ
        public bool OpenFlg = false;

        /// <summary>
        /// データベース作成
        /// </summary>
        public void CreateDataBase()
        {
            if (System.IO.File.Exists(_DBFilePath))
                return;
            
            //コネクションを開き、テーブル作成して閉じる
            //この時、ファイルパスにデータベースファイルが作成される
            using (Connect = new SQLiteConnection("Data Source=" + _DBFilePath))
            {
                Connect.Open();
                OpenFlg = true;

                using (var Command = Connect.CreateCommand())
                {
                    Command.CommandText = "create table " +
                                          "Sample(" +
                                                  "Id Integer " +
                                                  "PRIMARY KEY AUTOINCREMENT, " +
                                                  "Name TEXT, " +
                                                  "Age INTEGER)";
                    Command.ExecuteNonQuery();
                }

                Connect.Close();
                OpenFlg = false;
            }
        }

        /// <summary>
        /// データベース接続
        /// </summary>
        public void OpenDatabase()
        {
            Connect = new SQLiteConnection("Data Source=" + _DBFilePath);

            try
            {
                Connect.Open();
                OpenFlg = true;
            }
            catch(Exception ex)
            {
                WriteLine(ex.Message);
                OpenFlg = false;
            }
        }

        /// <summary>
        /// データベースにデータを追加
        /// </summary>
        public void AddData()
        {
            if (Connect == null)
                return;
            else if(OpenFlg == false)
                return;

            //トランザクション開始
            using (var Trans = Connect.BeginTransaction())
            {
                var Command = Connect.CreateCommand();

                //データ追加クエリ作成
                Command.CommandText = "insert into Sample (Name,Age) " +
                                                  "values (@Name, @Age)";
                //パラメータセット
                Command.Parameters.Add("Name", System.Data.DbType.String);　//Name = string型
                Command.Parameters.Add("Age", System.Data.DbType.Int64);    //Age = 数値型

                //データ追加
                Command.Parameters["Name"].Value = "佐藤";
                Command.Parameters["Age"].Value = "32";
                Command.ExecuteNonQuery();

                Command.Parameters["Name"].Value = "斎藤";
                Command.Parameters["Age"].Value = "50";
                Command.ExecuteNonQuery();

                //コミット
                Trans.Commit();
            }
        }

        /// <summary>
        /// データの取得
        /// </summary>
        public void GetData()
        {
            if (Connect == null)
                return;
            else if (OpenFlg == false)
                return;

            var Command = Connect.CreateCommand();
            Command.CommandText = "select * from Sample";

            using (var Reader = Command.ExecuteReader())
            {
                while(Reader.Read())
                {
                    WriteLine(Reader["Name"].ToString() + "," + Reader["Age"].ToString());
                }
            }
        }

        /// <summary>
        /// データベースを閉じる
        /// </summary>
        public void CloseDatabase()
        {
            if (Connect == null)
                return;
            else if (OpenFlg == false)
                return;

            Connect.Close();
            OpenFlg = false;
        }
    }
}
