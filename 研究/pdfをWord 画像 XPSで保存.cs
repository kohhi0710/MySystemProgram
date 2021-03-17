using Spire.Pdf;

namespace t
{
    /// <summary>
    /// PDFをWord、Image、XPSで保存
    /// 参考:https://qiita.com/iceblue/items/609ce93f082633ab2d6d
    /// </summary>
    class ConvertAnyFromPDF
    {
        public void Main()
        {
            ConvertToWordFromPDF();
            ConvertToImageFromPDF();
            ConvertToXPSFromPDF();
        }

        /// <summary>
        /// PDFをWordで保存
        /// </summary>
        public void ConvertToWordFromPDF()
        {
            PdfDocument doc = new PdfDocument();
            doc.LoadFromFile(@"C:\Folder\Sample.pdf");

            doc.SaveToFile(@"C:\Folder\Result.doc", FileFormat.DOC);
        }

        /// <summary>
        /// PDFを画像で保存
        /// </summary>
        public void ConvertToImageFromPDF()
        {
            PdfDocument doc = new PdfDocument();
            doc.LoadFromFile(@"C:\Folder\Sample.pdf");

            //PDFのページをループ
            for (int i = 0; i < doc.Pages.Count; i++)
            {
                //PDFをbmpで保存
                System.Drawing.Image bmp = doc.SaveAsImage(i);

                //bitをpngで保存
                string FileName = string.Format("Page-{0}.png", i + 1);
                bmp.Save(FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        /// <summary>
        /// PDFをXPSで保存
        /// </summary>
        public void ConvertToXPSFromPDF()
        {
            PdfDocument doc = new PdfDocument();
            doc.LoadFromFile(@"C:\Folder\Sample.pdf");

            doc.SaveToFile(@"C:\Folder\Result.xps", FileFormat.XPS);
        }

    }
}
