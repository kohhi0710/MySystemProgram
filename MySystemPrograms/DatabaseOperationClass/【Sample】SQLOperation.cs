using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t
{
    /// <summary>
    /// SQLServerにクエリを送ってデータを取得するサンプルプログラム
    /// </summary>
    class SQLOperation
    {
        /// <summary>
        /// データベースからデータを取得　サンプル
        /// </summary>
        /// <param name="SQLQuery">クエリ</param>
        public void MainProgram(string SQLQuery)
        {
            SQLServer_Connection_Method sqlm = new SQLServer_Connection_Method();
            SystemModule._Log_Method = new Log_Method();
            SystemModule._ProgramConfig = new ProgramConfig();
            
            try
            {
                sqlm.Connect();
                sqlm.CommandExecuteReader(SQLQuery);
                var data = sqlm._Reader;

                while(data.Read())
                {
                    var value = data["CTLKEY"];
                    Console.WriteLine(value);

                    value = data["CTLVAL"];
                    Console.WriteLine(value);

                    value = data["SETUME"];
                    Console.WriteLine(value);

                    value = data["INSDAT"];
                    Console.WriteLine(value);

                    value = data["UPDDAT"];
                    Console.WriteLine(value);

                    Console.WriteLine("--------------------------");
                }


                sqlm.DisConnect();

                sqlm = new SQLServer_Connection_Method();

            }
            catch(System.Data.SqlClient.SqlException ex)
            {
                var ErrMsg = new StringBuilder();

                for(int i = 0; i < ex.Errors.Count; i++)
                {
                    ErrMsg.Append(Environment.NewLine +
                                  "Index #" + i.ToString() + Environment.NewLine +
                                  "Message: (" + System.Reflection.MethodBase.GetCurrentMethod().Name + ")" + ex.Errors[i].Message + Environment.NewLine +
                                  "LineNumber: (" + ex.Errors[i].LineNumber + ")" + Environment.NewLine +
                                  "Source: " + ex.Errors[i].Source + Environment.NewLine +
                                  "Procedure: " + ex.Errors[i].Procedure + Environment.NewLine);
                }

                SystemModule._Log_Method.PutLog(5,System.Reflection.MethodBase.GetCurrentMethod().Name + ":" + SQLQuery, "", "");
                sqlm.DisConnect();

                throw ex;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
