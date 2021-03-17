using Spire.Xls;

namespace t
{
    /// <summary>
    /// HTML をExcelに変換
    /// 参考:https://qiita.com/iceblue/items/243aeba4c814adf82d70
    /// </summary>
    class ConvertToExcelFromHTML
    {
        public void Main()
        {
            Workbook workbook = new Workbook();
            //htmlファイルの読み込み、オブジェクト作成
            workbook.LoadFromHtml(@"C:\Folder\Sample.html");

            Worksheet sheet = workbook.Worksheets[0];
            sheet.AllocatedRange.AutoFitRows();

            //任意のファイルパス保存
            workbook.SaveToFile(@"C:\Folder\Result.xlsx", FileFormat.Version2016);
        }
    }
}
