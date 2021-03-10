using System;
using System.Collections.Generic;
using static SystemModule;

namespace t
{
    /// <summary>
    /// CSVバイト操作メソッドクラス
    /// </summary>
    class CSVByteOperation_Method:CSVByteOperation_Base
    {
        private string _FilePath;
        public List<List<byte>> _ByteList;

        //[使い方サンプル]-------------------------------------------------------------------------------------
        //  CSVByteOperation_Method csvm = new CSVByteOperation_Method(@"C:\Folder\aaaa.txt");
        //      
        //      for(int i = 0; i < csvm.GetDataCount(); i++)
        //      {
        //          Console.WriteLine(csvm.GetLineDataConvertString(i));
        //      }
        //-----------------------------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ　CSVデータのByte変換とライン区切り
        /// </summary>
        /// <param name="FilePath"></param>
        public CSVByteOperation_Method(string FilePath)
        {
            try
            {
                _FilePath = FilePath;

                GetCSV();
            }
            catch(ProgramErrorException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                string ErrMsg = "CSVの初期設定に失敗しました。";
                MethodDefine MethodDefine = new MethodDefine("Constructor", "0001");
                throw new ProgramErrorException(MethodDefine.GetMessageCode(),
                                              _SystemControl_Method._ProgramName,
                                              this.GetType().Name,
                                              MethodDefine.GetMethodName(),
                                              ErrMsg,
                                              ex.Message);
            }
        }

        /// <summary>
        /// CSVデータのByte変換とライン区切り
        /// </summary>
        public void GetCSV()
        {
            try
            {
                _ByteList = new List<List<byte>>();

                var BinaryData = GetBinaryData(_FilePath);
                _ByteList = DivideLine(BinaryData);
            }
            catch (ProgramErrorException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                string ErrMsg = "CSVの読み込みに失敗しました。";
                MethodDefine MethodDefine = new MethodDefine("GetCSV", "0002");
                throw new ProgramErrorException(MethodDefine.GetMessageCode(),
                                              _SystemControl_Method._ProgramName,
                                              this.GetType().Name,
                                              MethodDefine.GetMethodName(),
                                              ErrMsg,
                                              ex.Message);
            }
        }

        /// <summary>
        /// 読み込んだバイナリーデータリストの行数を取得
        /// </summary>
        /// <returns></returns>
        public int GetDataCount()
        {
            try
            {
                if (_ByteList == null)
                    return 0;

                return _ByteList.Count;
            }
            catch (ProgramErrorException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                string ErrMsg = "CSVデータのカウントに失敗しました。";
                MethodDefine MethodDefine = new MethodDefine("GetDataCount", "0008");
                throw new ProgramErrorException(MethodDefine.GetMessageCode(),
                                              _SystemControl_Method._ProgramName,
                                              this.GetType().Name,
                                              MethodDefine.GetMethodName(),
                                              ErrMsg,
                                              ex.Message);
            }
        }

        /// <summary>
        /// 読み込んだバイナリーデータリストのi番目のラインを取得
        /// </summary>
        /// <param name="index">取得したいインデックス</param>
        /// <returns></returns>
        public List<byte> GetLineData(int index)
        {
            try
            {
                if (_ByteList == null)
                    return null;

                if (index >= _ByteList.Count)
                    return null;

                return _ByteList[index];
            }
            catch (ProgramErrorException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                string ErrMsg = "CSVデータの取得に失敗しました。";
                MethodDefine MethodDefine = new MethodDefine("GetLineData", "0004");
                throw new ProgramErrorException(MethodDefine.GetMessageCode(),
                                              _SystemControl_Method._ProgramName,
                                              this.GetType().Name,
                                              MethodDefine.GetMethodName(),
                                              ErrMsg,
                                              ex.Message);
            }
        }

        /// <summary>
        /// 読み込んだバイナリーデータリストのi番目のラインを文字列で取得
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetLineDataConvertString(int index)
        {
            try
            {
                if (_ByteList == null)
                    return null;

                if (index >= _ByteList.Count)
                    return null;

                var ByteArray = new byte[_ByteList[index].Count];

                for(int i = 0; i < _ByteList[index].Count; i++)
                {
                    ByteArray[i] = _ByteList[index][i];
                }

                return System.Text.Encoding.Default.GetString(ByteArray);
            }
            catch (ProgramErrorException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                string ErrMsg = "CSVデータの取得に失敗しました。";
                MethodDefine MethodDefine = new MethodDefine("GetLineDataConvertString", "0004");
                throw new ProgramErrorException(MethodDefine.GetMessageCode(),
                                              _SystemControl_Method._ProgramName,
                                              this.GetType().Name,
                                              MethodDefine.GetMethodName(),
                                              ErrMsg,
                                              ex.Message);
            }
        }

        /// <summary>
        /// CSVデータからフィールドデータをピックアップし、リスト化する(使わない)
        /// </summary>
        /// <param name="TopPos"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public List<string> GetFieldCSV(int TopPos,int index)
        {
            try
            {
                List<string> rtnValue = new List<string>();
                var LineData = GetLineData(index);

                string Text = "";
                int Pos1 = TopPos;
                int idx = 0;

                while(true)
                {
                    int Pos2 = 0;

                    for (int i = Pos1; i < LineData.Count; i++)
                    {
                        Text = GetStringFromByte(LineData, i, 1);

                        if(Text == ",")
                        {
                            Pos2 = i - Pos1;
                            break;
                        }
                    }

                    if (Pos2 == 0 & Text == ",")
                        rtnValue.Add("");
                    else
                        rtnValue.Add(GetStringFromByte(LineData, Pos1, Pos2));

                    var q = @"""";
                    rtnValue[idx] = rtnValue[idx].Replace(q, "");
                    Pos1 += Pos2 + 1;
                    idx++;

                    if (Pos1 > LineData.Count)
                        break;
                }

                return rtnValue;
            }
            catch (ProgramErrorException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                string ErrMsg = "CSVデータの取得に失敗しました。";
                MethodDefine MethodDefine = new MethodDefine("GetFieldCSV", "0006");
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