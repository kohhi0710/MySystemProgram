using System;
using System.Collections.Generic;

namespace t
{
    /// <summary>
    /// CSVバイト操作スーパークラス
    /// </summary>
    class CSVByteOperation_Base
    {
        /// <summary>
        /// バイト配列から位置、長さでバイト配列を取得する
        /// </summary>
        /// <param name="InputData">Byteデータ配列</param>
        /// <param name="StartPoint">開始ポイント</param>
        /// <param name="Length">取得したい長さ</param>
        /// <returns></returns>
        protected List<byte> GetByteFromByte(List<byte> InputData,int StartPoint,int Length)
        {
            List<byte> rtnValue;

            if(Length == 0)
                Length = InputData.Count - StartPoint;

            if (Length < 1)
            {
                rtnValue = new List<byte>();
                return rtnValue;
            }

            rtnValue = new List<byte>();

            for(int i = 0; i < Length; i++)
            {
                if (StartPoint + i < InputData.Count)
                    rtnValue.Add(InputData[StartPoint + i]);
            }

            return rtnValue;
        }

        /// <summary>
        /// バイト配列から位置、長さで文字列を取得する(GetFieldCSV用)
        /// </summary>
        /// <param name="InputData">Byteデータ配列</param>
        /// <param name="StartPoint">開始ポイント</param>
        /// <param name="Length">取得したい長さ</param>
        /// <returns>List型</returns>
        protected string GetStringFromByte(List<byte> InputData, int StartPoint, int Length)
        {
            string rtnValue = "";
            List<byte> list = new List<byte>();

            if (Length <= StartPoint)
                return rtnValue;

            if (Length == 0)
                Length = InputData.Count - StartPoint;

            for (int i = 0; i < Length; i++)
            {
                if (StartPoint + i < InputData.Count)
                    list.Add(InputData[StartPoint + i]);
            }

            try
            {
                byte Byte00 = 0;
                byte Byte32 = 32;

                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] == Byte00)
                        list[i] = Byte32;
                    else
                        list[i] = InputData[StartPoint + i];
                }

                byte[] ForConvertion = new byte[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    ForConvertion[i] = list[i];
                }

                rtnValue = System.Text.Encoding.Default.GetString(ForConvertion);
            }
            catch (Exception ex)
            {
                return "";
            }

            return rtnValue;
        }

        /// <summary>
        /// テキストデータを読み込んでバイト配列を取得する
        /// </summary>
        /// <param name="FilePath">テキストデータのパス</param>
        /// <returns>List型</returns>
        protected List<byte> GetBinaryData(string FilePath)
        {
            List<byte> rtnValue = new List<byte>();

            System.IO.FileStream fs = null;
            System.IO.BinaryReader br = null;

            try
            {
                fs = new System.IO.FileStream(FilePath, System.IO.FileMode.Open);
                br = new System.IO.BinaryReader(fs);

                int Length = (int)fs.Length;

                var ForConvertion = br.ReadBytes(Length);
                fs.Close();

                foreach(var item in ForConvertion)
                {
                    rtnValue.Add(item);
                }
            }
            catch(Exception ex)
            {
                if (br != null)
                    br.Close();

                if (fs != null)
                    fs.Close();

                throw ex;
            }

            return rtnValue;
        }

        /// <summary>
        /// 改行を区切りとして、バイトデータを1行ずつまとめてリストに取得する
        /// </summary>
        /// <param name="InputData"></param>
        /// <returns></returns>
        protected List<List<byte>> DivideLine(List<byte> InputData)
        {
            List<List<byte>> rtnValue = new List<List<byte>>();

            int StartPoint = 0;
            int EndPoint = 0;
            int Length = 0;

            while(EndPoint >= 0)
            {
                EndPoint = SearchCRLF(InputData,StartPoint);

                if (EndPoint < 0)
                {
                    Length = InputData.Count - StartPoint;

                    if (Length <= 0)
                        break;
                }
                else
                    Length = EndPoint - StartPoint; //CRLFぶん減らす

                var LineData = GetByteFromByte(InputData, StartPoint, Length);

                rtnValue.Add(LineData);

                StartPoint = EndPoint + 1;
            }

            return rtnValue;
        }

        /// <summary>
        /// 読み込みデータからCRLFの位置を取得
        /// </summary>
        /// <param name="InputData">バイト配列</param>
        /// <param name="StartPoint">開始ポイント</param>
        /// <returns></returns>
        protected int SearchCRLF(List<byte> InputData,int StartPoint)
        {
            int EndPoint = -1;

            for(int i = StartPoint; i < InputData.Count -1; i++)
            {
                if (InputData[i] == 0xD & InputData[i + 1] == 0xA)
                {
                    EndPoint = i + 1;
                    break;
                }
            }

            return EndPoint;
        }
    }
}
