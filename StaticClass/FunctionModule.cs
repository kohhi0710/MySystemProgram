using System;

public static class FunctionModule
{
    #region "日付時刻取得・変換・編集"

    /// <summary>
    /// 日付の取得(YYYYMMDD)
    /// </summary>
    /// <param name="SplitWord">区切り文字</param>
    /// <returns>YYYY/MM/DD(例)</returns>
    public static string GetSystemDate_YYYYMMDD(string SplitWord)
    {
        string Today = DateTime.Now.ToString("yyyyMMdd");

        return DateEdit_YYYYMMDD(Today, SplitWord);
    }

    /// <summary>
    /// 時刻の取得(HHmmss)
    /// </summary>
    /// <param name="SplitWord">区切り文字</param>
    /// <returns>HH:mm:ss(例)</returns>
    public static string GetSystemTime_HHmmss(string SplitWord)
    {
        string TimeNow = DateTime.Now.ToString("HHmmss");

        return TimeEdit_HHmmss(TimeNow, SplitWord);
    }

    /// <summary>
    /// 数字8桁→日付編集(区切り文字は引数で指定)
    /// </summary>
    /// <param name="Date">日付文字列</param>
    /// <param name="SplitWord">区切り文字</param>
    /// <returns>YYYY/MM/DD(例)</returns>
    public static string DateEdit_YYYYMMDD(string Date,string SplitWord)
    {
        if (Date == "")
            return "";

        Date = Date.Trim();

        if (Date.Length == 0)
            return "";

        string ymd = "";

        for(int i = 0; i < Date.Length; i++)
        {
            string w = Date.Substring(i, 1);
                
            double Isnumeric;
            if (double.TryParse(w, System.Globalization.NumberStyles.Any,null,out Isnumeric))
            {
                ymd += w;
            }
        }

        //区切り文字がない時は作成したymdをそのまま返す
        if (SplitWord == "")
            return ymd;

        //8文字でないときは引数のDateをそのまま返す
        if (ymd.Length != 8)
            return Date;

        //YYYYMMDDに区切り文字を挿入
        string YYYYMMDD = ymd.Substring(0, 4) + SplitWord +
                        ymd.Substring(4, 2) + SplitWord +
                        ymd.Substring(6, 2);

        return YYYYMMDD;
    }

    /// <summary>
    /// 数字6桁→日付編集(区切り文字は引数で指定)
    /// </summary>
    /// <param name="Date">日付文字列</param>
    /// <param name="SplitWord">区切り文字</param>
    /// <returns>YYYY/MM</returns>
    public static string DateEdit_YYYYMM(string Date,string SplitWord)
    {
        if (Date == "")
            return "";

        Date = Date.Trim();

        if (Date.Length == 0)
            return "";

        string ym = "";

        double Isnumeric;
        for(int i = 0; i < Date.Length; i++)
        {
            string w = Date.Substring(i, 1);

            if (double.TryParse(w, System.Globalization.NumberStyles.Any, null, out Isnumeric))
                ym += w;
        }

        if (SplitWord == "")
            return ym;

        if (ym.Length != 6)
            return Date;

        string YYYYMM = ym.Substring(0, 4) + SplitWord +
                        ym.Substring(4, 2);

        return YYYYMM;
    }

    /// <summary>
    /// 数字8桁→日付編集(月日を取得、引数でパターン指定)　
    /// </summary>
    /// <param name="Date">日付文字列</param>
    /// <param name="Pattern">パターン指定 1:M月D日 2:M/D 3:M月DD日 4:MM/DD</param>
    /// <returns></returns>
    public static string DateEdit_MD(string Date,int Pattern)
    {
        if (Date.Trim() == "")
            return "";

        if (Date.Length != 8)
            return "";

        string EditDate = "";

        try
        {
            int YYYY = int.Parse(Date.Substring(0, 4));
            int MM = int.Parse(Date.Substring(4, 2));
            int DD = int.Parse(Date.Substring(6, 2));

            switch(Pattern)
            {
                case 1: //M月D日
                    EditDate = MM.ToString("#0") + "月" + DD.ToString("#0") + "日";
                    break;

                case 2: //M/D
                    EditDate = MM.ToString("#0") + "/" + DD.ToString("#0");
                    break;

                case 3: //M月DD日
                    EditDate = MM.ToString("#0") + "月" + DD.ToString("00") + "日";
                    break;

                case 4: //MM/DD
                    EditDate = MM.ToString("00") + "/" + DD.ToString("00");
                    break;

                default:
                    EditDate = "";
                    break;
            }
        }
        catch(Exception ex)
        {
            return "";
        }

        return EditDate;
    }

    /// <summary>
    /// 数字6桁→時刻編集(区切り文字は引数で指定)
    /// </summary>
    /// <param name="Time">時刻文字列</param>
    /// <param name="SplitWord">区切り文字  HH:mm:ss(例)</param>
    /// <returns></returns>
    public static string TimeEdit_HHmmss(string Time,string SplitWord)
    {
        if (Time == "")
            return "";

        if (Time.Length != 6)
            return "";

        string HH = Time.Substring(0, 2);
        string MM = Time.Substring(2, 2);
        string SS = Time.Substring(4, 2);

        string HHMMSS = HH + SplitWord +
                        MM + SplitWord +
                        SS;

        return HHMMSS;
    }

    /// <summary>
    /// 文字列をDateTime型に変換(YYYYMMDD) 変換失敗時は「9999/12/31 0:00:00」を返す
    /// </summary>
    /// <param name="Date">日付文字列</param>
    /// <returns></returns>
    public static DateTime ConvertStringToDate_YYYYMMDD(string Date)
    {
        DateTime rtnValue;
        
        if (Date == "")
            return DateTime.Parse("9999/12/31");

        if (Date.Length == 0)
            return DateTime.Parse("9999/12/31");

        Date = DateEdit_YYYYMMDD(Date, "/");

        try
        {
            rtnValue = DateTime.Parse(Date);
        }
        catch(Exception ex)
        {
            return DateTime.Parse("9999/12/31");
        }

        return rtnValue;
    }

    /// <summary>
    /// 日付型を文字列型に変換(YYYYMMDD)
    /// </summary>
    /// <param name="Date">日付型</param>
    /// <returns></returns>
    public static string ConvertDateToString_YYYYMMDD(DateTime Date)
    {
        try
        {
            return Date.ToString("yyyyMMdd");
        }
        catch(Exception ex)
        {
            return "";
        }
    }

    /// <summary>
    /// 文字列(時刻)を日付型に変換(YYYY/MM/DD HH:mm:ss)　時刻だけ利用するので、日付は一律"0001/01/01"
    /// </summary>
    /// <param name="Time">HHmmss</param>
    /// <returns></returns>
    public static DateTime ConvertStringToTime(string Time)
    {
        if (Time == "")
            return DateTime.Parse("9999/12/31 00:00:00");

        if(Time.Trim() == "")
            return DateTime.Parse("9999/12/31 00:00:00");

        string HH = Time.Substring(0, 2);
        string MM = Time.Substring(2, 2);
        string SS = Time.Substring(4, 2);
        string TimeString = "0001/01/01 " + HH + ":" + MM + ":" + SS;

        try
        {
            return DateTime.Parse(TimeString);
        }
        catch(Exception ex)
        {
            return DateTime.Parse("9999/12/31 00:00:00");
        }
    }

    /// <summary>
    /// 前月を取得(取得できない場合は"")
    /// </summary>
    /// <param name="YYYYMM">年月</param>
    /// <returns></returns>
    public static string GetMonthLast_YYYYMM(string YYYYMM)
    {
        if (YYYYMM.Length != 6)
            return "";

        double Isnumeric;
        if (double.TryParse(YYYYMM, System.Globalization.NumberStyles.Any, null, out Isnumeric) == false)
            return "";

        if (YYYYMM == "000000")
            return "";

        if (int.Parse(YYYYMM.Substring(4, 2)) < 1 ^ int.Parse(YYYYMM.Substring(4, 2)) > 12)
            return "";

        try
        {
            var rtnValue = ConvertStringToDate_YYYYMMDD(YYYYMM + "01");
            rtnValue = rtnValue.AddMonths(-1);
            return rtnValue.ToString("yyyyMM");
        }
        catch(Exception ex)
        {
            return "";
        }
    }

    /// <summary>
    /// 次月を取得(取得できない場合は"")
    /// </summary>
    /// <param name="YYYYMM">年月</param>
    /// <returns></returns>
    public static string GetMonthNext_YYYYMM(string YYYYMM)
    {
        if (YYYYMM.Length != 6)
            return "";

        double Isnumeric;
        if (double.TryParse(YYYYMM, System.Globalization.NumberStyles.Any, null, out Isnumeric) == false)
            return "";

        if (YYYYMM == "000000")
            return "";

        if (int.Parse(YYYYMM.Substring(4, 2)) < 1 ^ int.Parse(YYYYMM.Substring(4, 2)) > 12)
            return "";

        try
        {
            var rtnValue = ConvertStringToDate_YYYYMMDD(YYYYMM + "01");
            rtnValue = rtnValue.AddMonths(1);
            return rtnValue.ToString("yyyyMM");
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    /// <summary>
    /// 前日を取得(取得できない場合は"")
    /// </summary>
    /// <param name="YYYYMMDD">年月日</param>
    /// <returns></returns>
    public static string GetDayYesterday_YYYYMMDD(string YYYYMMDD)
    {
        if (YYYYMMDD.Length != 8)
            return "";

        double Isnumeric;
        if (double.TryParse(YYYYMMDD, System.Globalization.NumberStyles.Any, null, out Isnumeric) == false)
            return "";

        if (YYYYMMDD == "00000000")
            return "";

        try
        {
            var rtnValue = ConvertStringToDate_YYYYMMDD(YYYYMMDD);
            rtnValue = rtnValue.AddDays(-1);
            return rtnValue.ToString("yyyyMMdd");
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    /// <summary>
    /// 翌日を取得(取得できない場合は"")
    /// </summary>
    /// <param name="YYYYMMDD">年月日</param>
    /// <returns></returns>
    public static string GetDayTommorow_YYYYMMDD(string YYYYMMDD)
    {
        if (YYYYMMDD.Length != 8)
            return "";

        double Isnumeric;
        if (double.TryParse(YYYYMMDD, System.Globalization.NumberStyles.Any, null, out Isnumeric) == false)
            return "";

        if (YYYYMMDD == "00000000")
            return "";

        try
        {
            var rtnValue = ConvertStringToDate_YYYYMMDD(YYYYMMDD);
            rtnValue = rtnValue.AddDays(1);
            return rtnValue.ToString("yyyyMMdd");
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    /// <summary>
    /// 開始月と終了月間の月数を求める 計算失敗は0を返す 引数6桁と8桁で処理方法が変わる
    /// </summary>
    /// <param name="Start_YYYYMM">開始年月(6桁or8桁)</param>
    /// <param name="End_YYYYMM">終了年月(6桁or8桁)</param>
    /// <returns></returns>
    public static int GetElapsedMonth(string Start_YYYYMMDD,string End_YYYYMMDD)
    {
        //引数6桁の場合は日数を考慮せず単純に月数の差だけを算出
        if (Start_YYYYMMDD.Length == 6 & End_YYYYMMDD.Length == 6)
        {
            DateTime Date_Start = ConvertStringToDate_YYYYMMDD(Start_YYYYMMDD + "01");
            DateTime Date_End = ConvertStringToDate_YYYYMMDD(End_YYYYMMDD + "01");

            if(Date_Start < Date_End)
                return (Date_End.Year - Date_Start.Year) * 12 + (Date_End.Month - Date_Start.Month);
            else
                return 0;
        }
        else if (Start_YYYYMMDD.Length == 8 & End_YYYYMMDD.Length == 8)　//引数8桁の場合は日数も考慮に入れて月数算出
        {
            DateTime Date_Start = ConvertStringToDate_YYYYMMDD(Start_YYYYMMDD);
            DateTime Date_End = ConvertStringToDate_YYYYMMDD(End_YYYYMMDD);
            int ElapsedMonths = 0;

            if (Date_Start < Date_End)
                ElapsedMonths = (Date_End.Year - Date_Start.Year) * 12 + (Date_End.Month - Date_Start.Month);
            else
                return 0;

            if (Date_Start.Day <= Date_End.Day)
                //Date_Start のDayがDate_EndのDay以上の場合は、その月を満了しているとみなす
                //ex:1/30→3/30以降の場合は満(3-1)ヶ月
                return ElapsedMonths;
            else if (Date_End.Day == DateTime.DaysInMonth(Date_End.Year, Date_End.Month) && Date_End.Day <= Date_Start.Day)
                //Date_Start のDayがDate_Endの表す月の末日以降の場合は、その月を満了しているとみなす
                //ex:1/30→2/28(平年2月末日) / 2/29(閏年2月末日)以降の場合は満(2-1)ヶ月
                return ElapsedMonths;
            else
                //それ以外の場合は、その月を満了していないとみなす
                //ex:1/30→3/29以前の場合は(3-1)ヶ月未満、よって満(3-1-1)ヶ月
                return ElapsedMonths - 1;
        }
        else
            return 0;
    }

    /// <summary>
    /// 開始月と終了月間の年数を求める 計算失敗は0を返す
    /// 経過月数÷12(端数切り捨て)したものを経過年数とする(満12ヶ月で満1年とする)
    /// </summary>
    /// <param name="Start_YYYYMM">開始年月(6桁or8桁)</param>
    /// <param name="End_YYYYMM">終了年月(6桁or8桁)</param>
    /// <returns></returns>
    public static int GetElapsedYear(string Start_YYYYMMDD, string End_YYYYMMDD)
    {
        return GetElapsedMonth(Start_YYYYMMDD, End_YYYYMMDD) / 12;
    }

    /// <summary>
    /// 〇ヶ月前、〇ヶ月後を取得(YYYYMMで取得)　取得失敗は""を返す
    /// </summary>
    /// <param name="YYYYMM">基準年月(6桁)</param>
    /// <param name="Interval">差分月数</param>
    /// <returns></returns>
    public static string GetMonthDifference_YYYYMM(string YYYYMM,int Interval)
    {
        if (YYYYMM.Length != 6)
            return "";

        double Isnumeric;
        if (double.TryParse(YYYYMM, System.Globalization.NumberStyles.Any, null, out Isnumeric) == false)
            return "";

        if (YYYYMM == "000000")
            return "";

        if (int.Parse(YYYYMM.Substring(4, 2)) < 1 ^ int.Parse(YYYYMM.Substring(4, 2)) > 12)
            return "";

        try
        {
            var rtnValue = ConvertStringToDate_YYYYMMDD(YYYYMM + "01");
            rtnValue = rtnValue.AddMonths(Interval);
            return rtnValue.ToString("yyyyMM");
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    #endregion

    #region "数字変換・編集　丸め　計算"

    /// <summary>
    /// 文字列の数字部分もしくは小数点・マイナス記号を抜き出し、順番に連結してかえす 数字がない場合は文字列"0"を返す
    /// ex:「1,000」→「1000」 「-1.0」→「-1.0」 「111-2222」→「111-2222」
    /// </summary>
    /// <param name="Value">文字列</param>
    /// <returns></returns>
    public static string ExtractOnlyNumber(string Value)
    {
        if (Value == "")
            return "0";

        if (Value.Trim() == "")
            return "0";

        string rtnValue = "";
        foreach(var item in Value)
        {
            double Isnumeric;

            //数字に変換可・小数点・マイナス記号はrtnValueに連結
            if (double.TryParse(Convert.ToString(item), System.Globalization.NumberStyles.Any, null, out Isnumeric) == true ^
                Convert.ToString(item) == "." ^
                Convert.ToString(item) == "-")
                rtnValue += Convert.ToString(item);
        }

        if (rtnValue.Trim() == "")
            return "0";

        return rtnValue;
    }

    /// <summary>
    /// 数字編集　指定の桁数になるまで前にゼロをつける Long型で変換しているので編集対象数値は18桁まで
    /// </summary>
    /// <param name="Value">編集対象数値(文字列)</param>
    /// <param name="Digit">指定桁数</param>
    /// <returns></returns>
    public static string FormatPrefixZero(string Value,int Digit)
    {
        double Isnumeric;
        if (double.TryParse(Value, System.Globalization.NumberStyles.Any, null, out Isnumeric) == true)
        {
            try
            {
                var Value_long = long.Parse(Value);
                var Format = "";

                for (int i = 0; i < Digit; i++)
                    Format += "0";

                return Value_long.ToString(Format);
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
        else
            return "0";
    }

    /// <summary>
    /// 数字編集　指定桁より大きい小数点以下を切り捨て　Long型で変換しているので編集対象数値は18桁まで
    /// </summary>
    /// <param name="Value">編集対象数値(文字列)</param>
    /// <param name="Comma">整数値にコンマをつける場合は","を指定</param>
    /// <param name="DecimalDigit">小数点以下桁数</param>
    /// <returns></returns>
    public static string FormatTruncateDecimal(string Value,string Comma,int DecimalDigit)
    {
        double Isnumeric;
        if (double.TryParse(Value, System.Globalization.NumberStyles.Any, null, out Isnumeric) == true)
        {
            try
            {
                string rtnValue = "";
                var Value_SplitDecimal = Value.Split(Convert.ToChar("."));

                //整数・小数・それ以上の何かがあれば変換不可
                if (Value_SplitDecimal.Length > 2)
                    return "0";

                long ValueFirst = 0;
                long ValueSecond = 0;

                switch(Value_SplitDecimal.Length)
                {
                    //小数点なし、整数
                    case 0:
                        break;
                    case 1:
                        ValueFirst = long.Parse(Value_SplitDecimal[0]);
                        ValueSecond = 0;
                        break;
                    case 2:
                        ValueFirst = long.Parse(Value_SplitDecimal[0]);
                        int ValueSecond_Length = Value_SplitDecimal[1].Length;

                        if(DecimalDigit > 0)
                        {
                            if (ValueSecond_Length == DecimalDigit ^
                                ValueSecond_Length == 0 ^
                                ValueSecond_Length > DecimalDigit)
                                Value_SplitDecimal[1] = Value_SplitDecimal[1].Substring(0, DecimalDigit);
                            else if (ValueSecond_Length < DecimalDigit)
                                Value_SplitDecimal[1] = Value_SplitDecimal[1].PadRight(DecimalDigit, Convert.ToChar("0"));
                            else
                                return "0";
                        }
                        ValueSecond = long.Parse(Value_SplitDecimal[1]);
                        break;
                }

                if(DecimalDigit > 0)
                {
                    string Format = "";

                    for (int i = 0; i < DecimalDigit; i++)
                        Format += "0";

                    if (Comma == ",")
                        rtnValue = ValueFirst.ToString("#,##0") + "." + ValueSecond.ToString(Format);
                    else
                        rtnValue = ValueFirst.ToString("###0") + "." + ValueSecond.ToString(Format);
                }
                else
                {
                    if (Comma == ",")
                        rtnValue = ValueFirst.ToString("#,##0");
                    else
                        rtnValue = ValueFirst.ToString("####0");
                }

                return rtnValue;
            }
            catch(Exception ex)
            {
                return "0";
            }
        }

        return "0";
    }

    /// <summary>
    /// 金額編集　指定桁より大きい小数点以下を切り捨て、結果に\マークをつける　Long型で変換しているので編集対象数値は18桁まで
    /// </summary>
    /// <param name="Value">編集対象数値(文字列)</param>
    /// <param name="Comma">整数値にコンマをつける場合は","を指定</param>
    /// <param name="DecimalDigit">小数点以下桁数</param>
    /// <returns></returns>
    public static string FormatTruncateDecimal_Money(string Value,string Comma,int DecimalDigit)
    {
        double Isnumeric;
        if (double.TryParse(Value, System.Globalization.NumberStyles.Any, null, out Isnumeric) == true)
        {
            try
            {
                string rtnValue = "";
                var Value_SplitDecimal = Value.Split(Convert.ToChar("."));

                //整数・小数・それ以上の何かがあれば変換不可
                if (Value_SplitDecimal.Length > 2)
                    return "0";

                long ValueFirst = 0;
                long ValueSecond = 0;

                switch (Value_SplitDecimal.Length)
                {
                    //小数点なし、整数
                    case 0:
                        break;
                    case 1:
                        ValueFirst = long.Parse(Value_SplitDecimal[0]);
                        ValueSecond = 0;
                        break;
                    case 2:
                        ValueFirst = long.Parse(Value_SplitDecimal[0]);
                        int ValueSecond_Length = Value_SplitDecimal[1].Length;

                        if (DecimalDigit > 0)
                        {
                            if (ValueSecond_Length == DecimalDigit ^
                                ValueSecond_Length == 0 ^
                                ValueSecond_Length > DecimalDigit)
                                Value_SplitDecimal[1] = Value_SplitDecimal[1].Substring(0, DecimalDigit);
                            else if (ValueSecond_Length < DecimalDigit)
                                Value_SplitDecimal[1] = Value_SplitDecimal[1].PadRight(DecimalDigit, Convert.ToChar("0"));
                            else
                                return "0";
                        }
                        ValueSecond = long.Parse(Value_SplitDecimal[1]);
                        break;
                }

                if (DecimalDigit > 0)
                {
                    string Format = "";

                    for (int i = 0; i < DecimalDigit; i++)
                        Format += "0";

                    if (Comma == ",")
                        rtnValue = @"\" + ValueFirst.ToString("#,##0") + "." + ValueSecond.ToString(Format);
                    else
                        rtnValue = @"\" + ValueFirst.ToString("###0") + "." + ValueSecond.ToString(Format);
                }
                else
                {
                    if (Comma == ",")
                        rtnValue = @"\" + ValueFirst.ToString("#,##0");
                    else
                        rtnValue = @"\" + ValueFirst.ToString("####0");
                }

                return rtnValue;
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        return "0";
    }

    /// <summary>
    /// 数字かどうかのチェック 0-9のみ
    /// </summary>
    /// <param name="Value">編集対象数値(文字列)</param>
    /// <returns></returns>
    public static bool CheckDigit(string Value)
    {
        if (Value.Length == 0)
            return false;

        foreach(var item in Value)
        {
            var item_String = Convert.ToString(item);

            if (System.Text.RegularExpressions.Regex.IsMatch(item_String, "[0-9]") == false)
                return false;
        }

        return true;
    }

    /// <summary>
    /// 半角かどうかのチェック
    /// </summary>
    /// <param name="Value">編集対象文字列</param>
    /// <returns></returns>
    public static bool CheckHankaku(string Value)
    {
        if (Value == "")
            return false;

        return System.Text.RegularExpressions.Regex.IsMatch(Value, "^[a-zA-Z0-9!-/:-@¥[-`{-~]+$");
    }

    /// <summary>
    /// 半角カタカナかどうかのチェック
    /// </summary>
    /// <param name="Value">編集対象文字列</param>
    public static bool CheckHankakuKatakana(string Value)
    {
        if (Value == "")
            return false;

        return System.Text.RegularExpressions.Regex.IsMatch(Value, "^[ｦ-ﾟ｡｢｣､･]+$");
    }

    /// <summary>
    /// 半角かどうかのチェック(カタカナ含む)
    /// </summary>
    /// <param name="Value">編集対象文字列</param>
    /// <returns></returns>
    public static bool CheckHankakuWithKatakana(string Value)
    {
        if (Value == "")
            return false;

        return System.Text.RegularExpressions.Regex.IsMatch(Value, "^[ｦ-ﾟ｡｢｣ ､･a-zA-Z0-9!-/:-@¥[-`{-~]+$");
    }

    /// <summary>
    /// 数値を丸め処理する
    /// </summary>
    /// <param name="Value">編集対象数値</param>
    /// <param name="RoundPatten">丸めパターン　0：整数　1～：小数点以下第〇位まで有効</param>
    /// <param name="RoundMethod">丸め方法　1：切り捨て　2：四捨五入　3：切り上げ</param>
    /// <returns></returns>
    /// <remarks>Decimal→Decimal</remarks>
    public static decimal RoundNumber(decimal Value, int RoundPatten,int RoundMethod)
    {
        decimal rtnValue = 0;

        //小数点以下第〇位まで有効
        if (RoundPatten > 0)
            rtnValue = Value * Convert.ToDecimal(Math.Pow(10, RoundPatten));
        else
            rtnValue = Value;

        switch(RoundMethod)
        {
            case 1:　//切り捨て
                rtnValue = Math.Truncate(rtnValue);
                break;
            case 2:　//切り上げ
                rtnValue = Math.Ceiling(rtnValue);
                break;
            case 3:  //四捨五入
                rtnValue = Math.Round(rtnValue, MidpointRounding.AwayFromZero);
                break;
            default:
                return 0;
        }

        //小数点以下第〇位まで有効
        if (RoundPatten > 0)
            rtnValue = rtnValue / Convert.ToDecimal(Math.Pow(10, RoundPatten));
        else
            return 0;

        return rtnValue;
    }

    /// <summary>
    /// 数値を丸め処理する
    /// </summary>
    /// <param name="Value">編集対象数値</param>
    /// <param name="RoundPatten">丸めパターン　0：整数　1～：小数点以下第〇位まで有効</param>
    /// <param name="RoundMethod">丸め方法　1：切り捨て　2：四捨五入　3：切り上げ</param>
    /// <returns></returns>
    /// <remarks>Double→Decimal</remarks>
    public static decimal RoundNumber(double Value, int RoundPatten, int RoundMethod)
    {
        double rtnValue = 0;

        //小数点以下第〇位まで有効
        if (RoundPatten > 0)
            rtnValue = Value * Math.Pow(10, RoundPatten);
        else
            rtnValue = Value;

        switch (RoundMethod)
        {
            case 1:　//切り捨て
                rtnValue = Math.Truncate(rtnValue);
                break;
            case 2:　//切り上げ
                rtnValue = Math.Ceiling(rtnValue);
                break;
            case 3:  //四捨五入
                rtnValue = Math.Round(rtnValue, MidpointRounding.AwayFromZero);
                break;
            default:
                return 0;
        }

        //小数点以下第〇位まで有効
        if (RoundPatten > 0)
            rtnValue = rtnValue / Math.Pow(10, RoundPatten);
        else
            return 0;

        return Convert.ToDecimal(rtnValue);
    }

    /// <summary>
    /// 数値を階乗する　オーバーフロー時はOverflowExceptionが発生
    /// 20くらいが限界
    /// </summary>
    /// <param name="Value">数値</param>
    /// <returns></returns>
    public static long Factorial(int Value) =>
        checked(Value == 0 ? 1L : Value * Factorial(Value - 1));

    #endregion

    #region "文字列編集"

    /// <summary>
    /// 文字列中ののシングルクォーテーションを2つ重ねてエスケープする SQLクエリ用
    /// </summary>
    /// <param name="Value">文字列</param>
    /// <returns></returns>
    public static string EscapeSingleQuotation(string Value)
    {
        if (Value == "")
            return "";

        try
        {
            //文字列中のシングルクォーテーションはクエリでそのまま使うとエラーを起こすので、2つ重ねてエスケープする
            return Value.Replace("'", "''");
        }
        catch(Exception ex)
        {
            return Value;
        }
    }

    /// <summary>
    /// 小文字を大文字に変換する 
    /// ex:「ｧ」→「ｱ」
    /// </summary>
    /// <param name="Value">文字列</param>
    /// <param name="Pattern">0:文字列中の空白文字トリムなし　1:文字列中の空白文字をトリムする</param>
    /// <returns></returns>
    public static string ConvertToUpperString(string Value,int Pattern)
    {
        string rtnValue = Value;

        if (rtnValue == "")
            return "";

        if (rtnValue.Trim() == "")
            return "";

        try
        {
            rtnValue = rtnValue.ToUpper();
            rtnValue = rtnValue.Replace("ｧ", "ｱ");
            rtnValue = rtnValue.Replace("ｨ", "ｲ");
            rtnValue = rtnValue.Replace("ｩ", "ｳ");
            rtnValue = rtnValue.Replace("ｪ", "ｴ");
            rtnValue = rtnValue.Replace("ｫ", "ｵ");
            rtnValue = rtnValue.Replace("ｯ", "ﾂ");
            rtnValue = rtnValue.Replace("ｬ", "ﾔ");
            rtnValue = rtnValue.Replace("ｭ", "ﾕ");
            rtnValue = rtnValue.Replace("ｮ", "ﾖ");
            rtnValue = rtnValue.Replace("ァ", "ア");
            rtnValue = rtnValue.Replace("ィ", "イ");
            rtnValue = rtnValue.Replace("ゥ", "ウ");
            rtnValue = rtnValue.Replace("ェ", "エ");
            rtnValue = rtnValue.Replace("ォ", "オ");
            rtnValue = rtnValue.Replace("ッ", "ツ");
            rtnValue = rtnValue.Replace("ャ", "ヤ");
            rtnValue = rtnValue.Replace("ュ", "ユ");
            rtnValue = rtnValue.Replace("ョ", "ヨ");
            rtnValue = rtnValue.Replace("ぁ", "あ");
            rtnValue = rtnValue.Replace("ぃ", "い");
            rtnValue = rtnValue.Replace("ぅ", "う");
            rtnValue = rtnValue.Replace("ぇ", "え");
            rtnValue = rtnValue.Replace("ぉ", "お");
            rtnValue = rtnValue.Replace("っ", "つ");
            rtnValue = rtnValue.Replace("ゃ", "や");
            rtnValue = rtnValue.Replace("ゅ", "ゆ");
            rtnValue = rtnValue.Replace("ょ", "よ");
            rtnValue = rtnValue.Replace("ゎ", "わ");

            if(Pattern == 1)
            {
                rtnValue = rtnValue.Replace(" ", ""); //空白つめる
                rtnValue = rtnValue.Replace("　", ""); //空白つめる
            }

            return rtnValue;
        }
        catch (Exception ex)
        {
            return Value;
        }
    }

    /// <summary>
    /// 指定された桁数になるまで右側に文字を埋める
    /// </summary>
    /// <param name="Value">文字列</param>
    /// <param name="Digit">指定桁数</param>
    /// <param name="SetWord">埋める文字　省略時は空白、2桁以上指定されている場合は先頭1文字目</param>
    /// <returns></returns>
    public static string SetPadRight(string Value, int Digit, string SetWord = "")
    {
        SetWord = SetWord.Trim();

        switch(SetWord.Length)
        {
            case 0:
                SetWord = " ";
                break;//省略時は" "
            case int n when n > 1:
                SetWord = SetWord.Substring(0,1);
                break;
        }

        try
        {
            Value = Value.Trim();

            //桁数オーバーしていたら右側からカット
            if (Value.Length > Digit)
            {
                Value = Value.Substring(0, Digit);
                return Value;
            }

            for (int i = Value.Length; i < Digit; i++)
                Value = Value + SetWord;

            return Value;
        }
        catch(Exception ex)
        {
            Value = "";
            for (int i = Value.Length; i < Digit; i++)
                Value = Value + SetWord;

            return Value;
        }
    }

    /// <summary>
    /// 指定された桁数になるまで左側に文字を埋める
    /// </summary>
    /// <param name="Value">文字列</param>
    /// <param name="Digit">指定桁数</param>
    /// <param name="SetWord">埋める文字　省略時は空白、2桁以上指定されている場合は先頭1文字目</param>
    /// <returns></returns>
    public static string SetPadLeft(string Value, int Digit, string SetWord = "")
    {
        SetWord = SetWord.Trim();

        switch (SetWord.Length)
        {
            case 0:
                SetWord = " ";
                break;//省略時は" "
            case int n when n > 1:
                SetWord = SetWord.Substring(0, 1);
                break;
        }

        try
        {
            Value = Value.Trim();

            //桁数オーバーしていたら左側からカット
            if (Value.Length > Digit)
            {
                Value = Value.Substring(Value.Length - Digit,Value.Length - (Value.Length - Digit));
                return Value;
            }

            for (int i = Value.Length; i < Digit; i++)
                Value = SetWord + Value;

            return Value;
        }
        catch (Exception ex)
        {
            Value = "";
            for (int i = Value.Length; i < Digit; i++)
                Value = SetWord + Value;

            return Value;
        }
    }

    #endregion

    #region "ファイル操作"

    /// <summary>
    /// ファイルを移動する
    /// </summary>
    /// <param name="InputPath">対象ファイルパス</param>
    /// <param name="OutputPath">移動先ファイルパス</param>
    public static void FileMove(string InputPath,string OutputPath)
    {
        bool RetryFlg = true;
        int RetryCount = 0;

        while (RetryFlg)
        {
            try
            {
                if (System.IO.File.Exists(OutputPath))
                    System.IO.File.Delete(OutputPath);

                System.IO.File.Move(InputPath, OutputPath);

                RetryFlg = false;
            }
            catch(Exception ex)
            {
                RetryCount++;

                if (RetryCount == 10)
                    throw ex;
            }
        }
    }

    /// <summary>
    /// ファイルをコピーする
    /// </summary>
    /// <param name="InputPath">対象ファイルパス</param>
    /// <param name="OutputPath">コピー先ファイルパス</param>
    public static void FileCopy(string InputPath, string OutputPath)
    {
        bool RetryFlg = true;
        int RetryCount = 0;

        while (RetryFlg)
        {
            try
            {
                if (System.IO.File.Exists(OutputPath))
                    System.IO.File.Delete(OutputPath);

                System.IO.File.Copy(InputPath, OutputPath);

                RetryFlg = false;
            }
            catch (Exception ex)
            {
                RetryCount++;

                if (RetryCount == 10)
                    throw ex;
            }
        }
    }

    /// <summary>
    /// ファイルを消去する
    /// </summary>
    /// <param name="FilePath">対象ファイルパス</param>
    public static void FileDelete(string FilePath)
    {
        bool RetryFlg = true;
        int RetryCount = 0;

        while (RetryFlg)
        {
            try
            {
                if (System.IO.File.Exists(FilePath))
                    System.IO.File.Delete(FilePath);

                RetryFlg = false;
            }
            catch (Exception ex)
            {
                RetryCount++;

                if (RetryCount == 10)
                    throw ex;
            }
        }
    }

    /// <summary>
    /// 指定フォルダ内のファイルを全て削除する
    /// </summary>
    /// <param name="FolderPath"></param>
    public static void ClearFileData(string FolderPath)
    {
        bool RetryFlg = true;
        int RetryCount = 0;

        while(true)
        {
            try
            {
                foreach (var item in System.IO.Directory.GetFiles(FolderPath))
                {
                    try
                    {
                        System.IO.File.Delete(item);
                    }
                    catch (Exception ex)
                    {
                        RetryCount++;

                        if (RetryCount == 10)
                            throw ex;
                    }
                }

                RetryFlg = false;
            }
            catch (Exception ex)
            {
                RetryCount++;

                if (RetryCount == 10)
                    throw ex;
            }
        }
    }


    #endregion

    #region "時間計測"

    /// <summary>
    /// 時刻計測スタート　現在時刻を取得
    /// </summary>
    /// <returns></returns>
    public static long TimeMeasure_Start()
    {
        return DateTime.Now.Ticks;
    }

    /// <summary>
    /// 時刻計測終了　スタート時刻と現在時刻の差(秒)を取得
    /// </summary>
    /// <param name="StartTime">スタート時刻</param>
    /// <returns></returns>
    public static long TimeMeasure_End(long StartTime)
    {
        var EndTime = DateTime.Now.Ticks;
        //ミリ秒の算出
        var MeasureTime = (EndTime - StartTime) / 10000;
        //秒の算出
        return MeasureTime / 1000;
    }

    #endregion

    
}
