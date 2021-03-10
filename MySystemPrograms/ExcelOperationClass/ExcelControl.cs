using System;
using ClosedXML.Excel;
using static SystemModule;

namespace t
{
    /// <summary>
    /// Excel制御クラス
    /// </summary>
    class ExcelControl
    {
        //使い方サンプル---------------------------------------------------------
        //var ExObj = new ExcelControl("Test1", "Sheet1");
        //ExObj.ExcelOpen();
        //ExObj._Excel.Worksheet("Sheet1").Cell("A1").Value = "あいうえお";
        //WriteLine(ExObj._Excel.Worksheet("Sheet1").Cell("A1").Value);
        //ExObj.ExcelWorkSave();
        //ExObj.ExcelWorkCopyToSet();
        //-----------------------------------------------------------------------

        public XLWorkbook _Excel;

        /// <summary>
        /// Excelテンプレートのファイル名　テンプレート検索用
        /// </summary>
        public string _ExcelTemplateName;

        /// <summary>
        /// 編集用ファイルパス
        /// </summary>
        public string _ExcelWorkFilePath;

        /// <summary>
        /// 保存用ファイルパス
        /// </summary>
        public string _ExcelSetFilePath;

        public string _ExcelSheetName;
        public string _ExcelSheetNameTop;

        /// <summary>
        /// コンストラクタ エクセルシート名なしの時はシート1枚目がアクティブになる
        /// </summary>
        /// <param name="ExcelFormName">エクセルファイル名</param>
        /// <param name="ExcelSheetName">エクセルシート名</param>
        public ExcelControl(string ExcelFormName, string ExcelSheetName)
        {
            _ExcelTemplateName = ExcelFormName;
            _ExcelSheetName = ExcelSheetName;
            _ExcelSheetNameTop = ExcelSheetName;

            Clear();
        }

        /// <summary>
        /// Excel関連オブジェクトと変数を初期化
        /// </summary>
        public void Clear()
        {
            _Excel = null;
        }

        /// <summary>
        /// Excelファイルオープン
        /// </summary>
        public void ExcelOpen()
        {
            try
            {
                Clear();

                //Excelフォームをコピー
                ExcelInitCopy();

                //Excelフォームをロードする
                ExcelFormLoad();

                //シートを切り替える
                ActiveSheet(_ExcelSheetName);
            }
            catch (ProgramErrorException ex)
            {
                ExcelWorkSave();
                throw ex;
            }
            catch (Exception ex)
            {
                ExcelWorkSave();
                throw ex;
            }
        }

        /// <summary>
        /// シートを切り替える
        /// </summary>
        /// <param name="ExcelSheetName">シート名</param>
        public void ActiveSheet(string ExcelSheetName)
        {
            try
            {
                if(ExcelSheetName != "")
                {
                    _ExcelSheetName = ExcelSheetName;
                    var ExcelSheet = _Excel.Worksheet(_ExcelSheetName);
                    ExcelSheet.SetTabActive();
                    ExcelSheet.Range("A1").Select(); //A1を選択する

                }
                else
                {
                    ActiveSheetTop();
                }
            }
            catch (ProgramErrorException ex)
            {
                ExcelWorkSave();
                throw ex;
            }
            catch (Exception ex)
            {
                ExcelWorkSave();
                string ErrMsg = "Excelシートの取得に失敗しました。";
                MethodDefine MethodDefine = new MethodDefine("ActiveSheet", "0001");
                throw new ProgramErrorException(MethodDefine.GetMessageCode(),
                                              _SystemControl_Method._ProgramName,
                                              this.GetType().Name,
                                              MethodDefine.GetMethodName(),
                                              ErrMsg,
                                              ex.Message);
            }
        }

        /// <summary>
        /// シートのトップに切り替える
        /// </summary>
        public void ActiveSheetTop()
        {
            try
            {
                var ExcelSheet = _Excel.Worksheet(1);
                ExcelSheet.SetTabActive();
                ExcelSheet.Range("A1").Select();
            }
            catch (ProgramErrorException ex)
            {
                ExcelWorkSave();
                throw ex;
            }
            catch (Exception ex)
            {
                ExcelWorkSave();
                string ErrMsg = "Excelシートの取得に失敗しました。";
                MethodDefine MethodDefine = new MethodDefine("ActiveSheetTop", "0001");
                throw new ProgramErrorException(MethodDefine.GetMessageCode(),
                                              _SystemControl_Method._ProgramName,
                                              this.GetType().Name,
                                              MethodDefine.GetMethodName(),
                                              ErrMsg,
                                              ex.Message);
            }
        }

        /// <summary>
        /// テンプレートを編集用ファイルにコピーする
        /// </summary>
        public void ExcelInitCopy()
        {
            try
            {
                //テンプレートのファイルパスを取得
                string ExcelTemplateFilePath = _PathConfig.GetExcelTemplateFilePath(_ExcelTemplateName);
                //編集用のファイルパスを取得
                _ExcelWorkFilePath = _PathConfig.GetExcelWorkFilePath(_ExcelTemplateName);

                //編集用ファイル削除
                if (System.IO.File.Exists(_ExcelWorkFilePath))
                    System.IO.File.Delete(_ExcelWorkFilePath);

                //テンプレートを編集用ファイルパスへコピーする
                System.IO.File.Copy(ExcelTemplateFilePath, _ExcelWorkFilePath, true);

                int count = 0;
                while (System.IO.File.Exists(_ExcelWorkFilePath) == false)
                {
                    System.Threading.Thread.Sleep(200);
                    count += 1;

                    if (count > 100)
                    {
                        ExcelWorkSave();
                        string ErrMsg = "Excelフォームのコピーに失敗しました。";
                        MethodDefine MethodDefine = new MethodDefine("ExcelInitCopy", "0001");
                        throw new ProgramErrorException(MethodDefine.GetMessageCode(),
                                                      _SystemControl_Method._ProgramName,
                                                      this.GetType().Name,
                                                      MethodDefine.GetMethodName(),
                                                      ErrMsg,
                                                      "");
                    }
                }
            }
            catch (ProgramErrorException ex)
            {
                ExcelWorkSave();
                throw ex;
            }
            catch (Exception ex)
            {
                ExcelWorkSave();
                string ErrMsg = "Excelフォームのコピーに失敗しました。";
                MethodDefine MethodDefine = new MethodDefine("ExcelInitCopy", "0001");
                throw new ProgramErrorException(MethodDefine.GetMessageCode(),
                                              _SystemControl_Method._ProgramName,
                                              this.GetType().Name,
                                              MethodDefine.GetMethodName(),
                                              ErrMsg,
                                              ex.Message);
            }
        }

        /// <summary>
        /// 編集したExcelファイルを保存用フォルダにコピー
        /// </summary>
        public void ExcelWorkCopyToSet()
        {
            //保存用のファイルパスを取得
            _ExcelSetFilePath = _PathConfig.GetExcelSetFilePath(_ExcelTemplateName);

            try
            {
                //保存用パスのデータを削除
                if (System.IO.File.Exists(_ExcelSetFilePath))
                    System.IO.File.Delete(_ExcelSetFilePath);

                //保存用パスにテンプレートのデータをコピー
                System.IO.File.Copy(_ExcelWorkFilePath,_ExcelSetFilePath, true);
            }
            catch (ProgramErrorException ex)
            {
                ExcelWorkSave();
                throw ex;
            }
            catch (Exception ex)
            {
                ExcelWorkSave();
                string ErrMsg = "Excelのコピーに失敗しました。";
                MethodDefine MethodDefine = new MethodDefine("ExcelSave", "0001");
                throw new ProgramErrorException(MethodDefine.GetMessageCode(),
                                              _SystemControl_Method._ProgramName,
                                              this.GetType().Name,
                                              MethodDefine.GetMethodName(),
                                              ErrMsg,
                                              ex.Message);
            }
        }

        /// <summary>
        /// 編集用ファイルのロード
        /// </summary>
        public void ExcelFormLoad()
        {
            try
            {
                if(_ExcelSheetName != "" )
                {
                    _Excel = new XLWorkbook(_ExcelWorkFilePath);

                    var ExcelSheets = _Excel.Worksheets;
                    var ExcelSheet = ExcelSheets.Worksheet(_ExcelSheetName);

                    ExcelSheet.SetTabActive();
                }
                else
                {
                    _Excel = new XLWorkbook(_ExcelWorkFilePath);

                    var ExcelSheets = _Excel.Worksheets;
                    var ExcelSheet = ExcelSheets.Worksheet(1);

                    ExcelSheet.SetTabActive();
                }

            }
            catch (ProgramErrorException ex)
            {
                ExcelWorkSave();
                throw ex;
            }
            catch (Exception ex)
            {
                ExcelWorkSave();
                string ErrMsg = "Excelフォームのロードに失敗しました。";
                MethodDefine MethodDefine = new MethodDefine("ExcelFormLoad", "0001");
                throw new ProgramErrorException(MethodDefine.GetMessageCode(),
                                              _SystemControl_Method._ProgramName,
                                              this.GetType().Name,
                                              MethodDefine.GetMethodName(),
                                              ErrMsg,
                                              ex.Message);
            }
        }

        /// <summary>
        /// 編集用ファイルの保存と解放
        /// </summary>
        public void ExcelWorkSave()
        {
            if (_Excel == null)
                return;

            try
            {
                _Excel.SaveAs(_ExcelWorkFilePath);
                _Excel.Dispose();
            }
            catch (ProgramErrorException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                string ErrMsg = "ExcelのClose処理に失敗しました。";
                MethodDefine MethodDefine = new MethodDefine("ExcelClose", "0001");
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