using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SystemModule;

namespace t
{
    /// <summary>
    /// システムコントロールクラス
    /// </summary>
    class SystemControl_Method : SystemControl_Base
    {
        //プロセス待ち合わせ時間
        protected int _ProcessSleepTime = 100;

        /// <summary>
        /// コンストラクタ 全てのアプリケーションの初期処理
        /// </summary>
        public SystemControl_Method()
        {
            try
            {
                try
                {
                    //プロジェクト名設定
                    _ProgramName = base._ProgramName;
                    //ルートパス設定
                    _RootPath = GetRootPathDefine();
                    //PathConfigクラスインスタンス生成とパス定義
                    _PathConfig = new PathConfig(_RootPath);
                }
                catch (ProgramErrorException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    MethodDefine MethodDefine = new MethodDefine("Constructor", "0001");
                    string ErrMsg = "システムエラーが発生しました。";
                    throw new ProgramErrorException(MethodDefine.GetMessageCode(), _ProgramName, this.GetType().Name,
                                                    MethodDefine.GetMethodName(), ErrMsg, ex.Message);
                }
            }
            catch (ProgramErrorException ex)
            {
                ExceptionEnd(ex);
            }
            catch (Exception ex)
            {
                ExceptionEnd(ex);
            }
        }

        /// <summary>
        /// プログラム開始処理
        /// </summary>
        public void ProgramStart()
        {
            try
            {
                try
                {
                    //ProgramConfigパス設定
                    SetConfig();

                    //ログクラスインスタンス生成と初期設定
                    InitLog();

                    if (_ProcessCheck)
                    {
                        if (SubProcessCheck())
                            throw new Exception("既に起動されています。");
                    }

                    PutLogProgramStart();

                    #region "不使用"
                    //SysTempFolderのファイル削除
                    //ClearFileData(_PathConfig.GetSystemTempFolderPath());

                    //TempFolderのファイル削除
                    //ClearFileData(_PathConfig.GetTempFolderPath());

                    //ExcelTempFolderのファイル削除
                    //ClearFileData(_PathConfig.GetExcelTempFolderPath());

                    //プロジェクト開始ログ出力
                    //PutLogProjectStart();

                    //データベースオープン
                    //DBOpen();
                    #endregion
                }
                catch (ProgramErrorException ex)
                {
                    ExceptionEnd(ex);
                }
                catch (Exception ex)
                {
                    MethodDefine MethodDefine = new MethodDefine("ProgramStart", "0001");
                    string ErrMsg = "システムエラーが発生しました。";
                    throw new ProgramErrorException(MethodDefine.GetMessageCode(), _ProgramName, this.GetType().Name,
                                                    MethodDefine.GetMethodName(), ErrMsg, ex.Message);
                }
            }
            catch (ProgramErrorException ex)
            {
                ExceptionEnd(ex);
            }
            catch (Exception ex)
            {
                ExceptionEnd(ex);
            }
        }

        /// <summary>
        /// プログラム終了処理
        /// </summary>
        public void ProgramEnd()
        {
            try
            {
                PutLogProgramEnd();

                System.Environment.Exit(0);
            }
            catch (ProgramErrorException ex)
            {
                ExceptionEnd(ex);
            }
            catch (Exception ex)
            {
                MethodDefine MethodDefine = new MethodDefine("ProgramEnd", "0001");
                string ErrMsg = "システムエラーが発生しました。";
                throw new ProgramErrorException(MethodDefine.GetMessageCode(), _ProgramName, this.GetType().Name,
                                                MethodDefine.GetMethodName(), ErrMsg, ex.Message);
            }
        }

        /// <summary>
        /// 異常終了処理
        /// </summary>
        /// <param name="ex">例外</param>
        public void ExceptionEnd(Exception ex)
        {
            try
            {
                //エラーログ出力
                PutLogException(ex);

                //終了
                System.Environment.Exit(0);
            }
            catch(ProgramErrorException ex2) //超例外処理
            {
                //エラーログ出力
                PutLogException(ex);

                string ErrMsg = "深刻なエラーが発生しました。プログラムを強制終了します。";
                _Log_Method.PutLog(6, ErrMsg, ex2.Message, "");
                Environment.FailFast(ErrMsg + ":" + ex2.Message);
            }
            catch (Exception ex2) //超例外処理
            {　
                //エラーログ出力
                PutLogException(ex);

                string ErrMsg = "深刻なエラーが発生しました。プログラムを強制終了します。";
                _Log_Method.PutLog(6, ErrMsg, ex2.Message, "");
                Environment.FailFast(ErrMsg + ":" + ex2.Message);
            }
        }
    }
}
