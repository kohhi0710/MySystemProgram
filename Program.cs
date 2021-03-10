using System;
using static SystemModule;
using static System.Console;
using static FunctionModule;
using System.Collections.Generic;

namespace t
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine(DateEdit_YYYYMM("202002", "/"));
            WriteLine(DateEdit_MD("20200201", 1));
            WriteLine(DateEdit_MD("20200201", 2));
            WriteLine(DateEdit_MD("20200201", 3));
            WriteLine(DateEdit_MD("20200201", 4));
            WriteLine(DateEdit_MD("20200201", 5));

            WriteLine(DateTime.Parse("9999/12/31 00:00:20"));

            var YYYYMM = "-1";
            if (int.Parse(YYYYMM) < 1 ^ int.Parse(YYYYMM) > 12)
                WriteLine("範囲外");

            WriteLine(ExtractOnlyNumber("1,000-1234 58"));
            WriteLine(ExtractOnlyNumber("-1,000"));
            WriteLine(ExtractOnlyNumber("-1.000"));
            WriteLine(FormatPrefixZero("12501111111111111111", 20));
            WriteLine(FormatTruncateDecimal("1.12345678","", 2));
            WriteLine(FormatTruncateDecimal("10000000.12345678", ",", 2));
            WriteLine(FormatTruncateDecimal_Money("1000000.25257174", ",", 5));
            WriteLine(CheckDigit("12222"));
            WriteLine(CheckDigit("1"));
            WriteLine(CheckDigit("1222a"));
            WriteLine(CheckDigit("1.2222"));
            WriteLine(RoundNumber(Convert.ToDecimal(10.5555),3,3));
            WriteLine(RoundNumber(Convert.ToDecimal(10.55555555555555), 12, 3));
            WriteLine(Factorial(20));
            WriteLine(RoundNumber(10.55555555555555, 12, 3));
            WriteLine(EscapeSingleQuotation("a'iueo'"));
            WriteLine(ConvertToUpperString("ァィゥェォぁぃぅ　ぇぉｧｨｩｪｫ",0));
            WriteLine(ConvertToUpperString("ァィゥェォぁぃぅ　ぇぉｧｨｩｪｫ", 1));

            WriteLine(SetPadRight("あいうえ", 2, "b"));
            WriteLine(SetPadRight("あいうえ",20,"ww"));
            WriteLine(SetPadRight("あいうえ", 1, "ww"));
            WriteLine(SetPadRight("あいうえ", 20, "pw"));

            WriteLine(SetPadLeft("あいうえおかきくけこ", 2, "b"));
            WriteLine(SetPadLeft("あいうえおかきくけこ", 1, "ww"));
            WriteLine(SetPadLeft("あいうえおかきくけこ", 5, "ww"));
            WriteLine(SetPadLeft("あいうえおかきくけこ", 6, "pw"));

            WriteLine(TimeMeasure_Start());
            WriteLine(TimeMeasure_End(TimeMeasure_Start()));

            _ProgramConfig = new ProgramConfig();
            WriteLine(_ProgramConfig._ServerName);
            WriteLine(_ProgramConfig._LoginUser);
            WriteLine(_ProgramConfig._LoginPassword);
            WriteLine(_ProgramConfig._DBName);

            //SystemControl
            SystemControl_Method sc = new SystemControl_Method();
            sc.ProgramStart();
            _Log_Method.PutLog(2, "テストログ", "aaaaa", "eeeee");
            sc.ProgramStart();

            //ExcelControl------------------------------------------------------
            var ExObj = new ExcelControl("Test1", "Sheet1");
            ExObj.ExcelOpen();
            ExObj._Excel.Worksheet("Sheet1").Cell("A1").Value = "あいうえお";
            WriteLine(ExObj._Excel.Worksheet("Sheet1").Cell("A1").Value);
            ExObj.ExcelWorkSave();
            ExObj.ExcelWorkCopyToSet();
            //------------------------------------------------------------------

            //ExcelControl------------------------------------------------------
            var ExObj2 = new ExcelControl("Test1", "");
            ExObj2.ExcelOpen();
            //ExObj2._Excel.Worksheet("Sheet1").Cell("A1").Value = "あいうえお";
            WriteLine(ExObj2._Excel.Worksheet("Sheet1").Cell("A1").Value);
            ExObj2.ExcelWorkSave();
            ExObj2.ExcelWorkCopyToSet();
            //------------------------------------------------------------------

            //AES
            //var aes = new AESSystem();
            //aes.MainProgram();

            //var p = new ExternalProcessOperation(@"C:\Program Files (x86)\Hidemaru\Hidemaru.exe","TestArg"); //秀丸ならファイル名がコマンドライン引数の値になる
            //p.Start();

            var qj = new QueryGenerator();
            qj._SELECT = new SELECT();
            qj._SELECT.AddQuery("test");
            qj._SELECT.AddQuery("Apple");
            qj._SELECT.AddQuery("BANANA");

            var qj2 = new QueryGenerator();
            qj2._SELECT = new SELECT("hogehoge");
            qj2._SELECT.AddQuery("test");
            qj2._SELECT.AddQuery("Apple");
            qj2._SELECT.AddQuery("BANANA");

            var qj3 = new QueryGenerator();
            var list = new List<string>() { "ebi", "tako", "ika" };
            qj3._SELECT = new SELECT(list);
            qj3._SELECT.AddQuery("test");
            qj3._SELECT.AddQuery("Apple");
            qj3._SELECT.AddQuery("BANANA");




            ReadKey();

            sc.ProgramEnd();
        }
    }

}
