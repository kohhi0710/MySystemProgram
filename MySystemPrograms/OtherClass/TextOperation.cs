using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SystemModule;

namespace t
{
    /// <summary>
    /// テキストファイルIOクラス
    /// </summary>
    class TextOperation
    {
        /// <summary>
        /// テキストファイルを全行読み込んでひとつの変数にまとめる
        /// </summary>
        /// <param name="FilePath">テキストファイルパス</param>
        /// <returns></returns>
        public string GetText(string FilePath)
        {
            try
            {
                string rtnValue = "";

                System.IO.StreamReader sr = null;

                try
                {
                    sr = new System.IO.StreamReader(FilePath, System.Text.Encoding.Default);

                    while(true)
                    {
                        if (sr.Peek() != -1)
                            rtnValue = rtnValue + sr.ReadLine() + Environment.NewLine;
                        else
                            break;
                    }

                    sr.Close();
                    sr = null;
                }
                catch (Exception ex)
                {
                    if(sr != null)
                    {
                        sr.Close();
                        sr = null;
                    }
                }

                return rtnValue;
            }
            catch(Exception ex)
            {
                string ErrMsg = "テキストファイルの取得でエラーが発生しました。";
                MethodDefine MethodDefine = new MethodDefine("GetText", "0001");
                throw new ProgramErrorException(MethodDefine.GetMessageCode(),
                                              _SystemControl_Method._ProgramName,
                                              this.GetType().Name,
                                              MethodDefine.GetMethodName(),
                                              ErrMsg,
                                              ex.Message);
            }
        }

        /// <summary>
        /// テキストファイルを作成
        /// </summary>
        /// <param name="FilePath">保存先ファイルパス</param>
        /// <param name="Value">保存したいテキスト内容</param>
        public void CreateTextFile(string FilePath,string Value)
        {
            try
            {
                try
                {
                    if (System.IO.File.Exists(FilePath))
                        System.IO.File.Delete(FilePath);
                }
                catch(Exception ex)
                {
                    throw ex;
                }

                System.IO.FileStream fs = null;
                System.IO.StreamWriter sw = null;

                try
                {
                    fs = System.IO.File.OpenWrite(FilePath);
                    sw = new System.IO.StreamWriter(fs, System.Text.Encoding.GetEncoding("utf-8"));
                    sw.BaseStream.Seek(0, System.IO.SeekOrigin.End);

                    sw.WriteLine(Value);

                    sw.Flush();
                    sw.Close();
                    fs.Close();
                }
                catch(Exception ex)
                {
                    if (sw != null)
                        sw.Close();

                    if (fs != null)
                        fs.Close();
                }
            }
            catch (Exception ex)
            {
                string ErrMsg = "テキストファイルの出力でエラーが発生しました。";
                MethodDefine MethodDefine = new MethodDefine("CreateTextFile", "0002");
                throw new ProgramErrorException(MethodDefine.GetMessageCode(),
                                              _SystemControl_Method._ProgramName,
                                              this.GetType().Name,
                                              MethodDefine.GetMethodName(),
                                              ErrMsg,
                                              ex.Message);
            }
        }

        /// <summary>
        /// テキストファイルを読み込んで1行ずつリストへ格納する
        /// </summary>
        /// <param name="FilePath">テキストファイルパス</param>
        /// <returns></returns>
        public List<string> GetTextList(string FilePath)
        {
            try
            {
                List<string> rtnValue = new List<string>();

                System.IO.StreamReader sr = null;

                try
                {
                    sr = new System.IO.StreamReader(FilePath, System.Text.Encoding.Default);

                    while (true)
                    {
                        if (sr.Peek() != -1)
                            rtnValue.Add(rtnValue + sr.ReadLine() + Environment.NewLine);
                        else
                            break;
                    }

                    sr.Close();
                    sr = null;
                }
                catch (Exception ex)
                {
                    if (sr != null)
                    {
                        sr.Close();
                        sr = null;
                    }
                }

                return rtnValue;
            }
            catch (Exception ex)
            {
                string ErrMsg = "テキストファイルの取得でエラーが発生しました。";
                MethodDefine MethodDefine = new MethodDefine("GetTextList", "0001");
                _SystemControl_Method = new SystemControl_Method();
                throw new ProgramErrorException(MethodDefine.GetMessageCode(),
                                              _SystemControl_Method._ProgramName,
                                              this.GetType().Name,
                                              MethodDefine.GetMethodName(),
                                              ErrMsg,
                                              ex.Message);
            }
        }
    }
}