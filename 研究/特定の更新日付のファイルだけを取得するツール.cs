using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace t
{
    /// <summary>
    /// 特定の更新日付のファイルだけを取得するツール
    /// 参考:https://qiita.com/koob/items/22db6cfd00ce881a5b3d
    /// CancelToken参考:https://qiita.com/TsuyoshiUshio@github/items/b2d23b37b410a2cfd330
    /// </summary>
    class GetDesignationDateFile
    {
        /// <summary>
        /// ディレクトリ構成を含め、再帰的に対象ファイルをコピーする
        /// </summary>
        /// <param name="originDirectory">取得元フォルダ</param>
        /// <param name="targetDirectory">保存先フォルダ</param>
        /// <param name="targetDateFrom">取得ファイル更新日付（From）</param>
        /// <param name="targetDateTo">取得ファイル更新日付（To）</param>
        /// <param name="ct">キャンセルトークン</param>
        public void CopyFileWithDirectory(string originDirectory,
                                          string targetDirectory,
                                          DateTime targetDateFrom,
                                          DateTime targetDateTo,
                                          CancellationToken ct)
        {
            //保存先のディレクトリ名の末尾に"\"をつける
            //ex: C:\Folder → C:\Folder\
            if (targetDirectory[targetDirectory.Length - 1] != Path.DirectorySeparatorChar)
                targetDirectory = targetDirectory + Path.DirectorySeparatorChar;

            try
            {
                //タスクのキャンセルがされていたら例外をスロー
                ct.ThrowIfCancellationRequested();

                //取得元ディレクトリ配下のファイルを検索し、条件に一致するファイルを取得する
                var Files = Directory.GetFiles(originDirectory);

                foreach(var item in Files)
                {
                    //処理中のディレクトリを画面に表示
                    //Invoke(new Action<string>(SetExecuteMsg), item);

                    if (!CheckTargetFile(item, targetDateFrom, targetDateTo))
                        continue;

                    //保存先のディレクトリがないときは作る(属性もコピー)
                    if(!Directory.Exists(targetDirectory))
                    {
                        Directory.CreateDirectory(targetDirectory);
                        File.SetAttributes(targetDirectory, File.GetAttributes(originDirectory));
                    }

                    File.Copy(item, targetDirectory + Path.GetFileName(item),true);
                }

                //取得元ディレクトリ配下のディレクトリについて、再帰的に呼び出す
                var dirs = Directory.GetDirectories(originDirectory);

                foreach(var item in dirs)
                {
                    if (!CheckTargetFolder(item))
                        continue;

                    CopyFileWithDirectory(item,
                                          targetDirectory + Path.GetFileName(item),
                                          targetDateFrom,
                                          targetDateTo,
                                          ct);
                }
            }
            catch(UnauthorizedAccessException ex)
            {
                var Msg = ex.Message.Split('\'');
                //Invoke(new Action<string>(SetErrMsg), "権限エラー：" + Msg[1]);
            }
            catch(OperationCanceledException)
            {
                //処理なし
                //処理キャンセルメッセージは呼び出し元でセットする
            }
            catch(Exception ex)
            {
                //Invoke(new Action<string>(SetErrMsg), ex.Message);
            }
        }

        /// <summary>
        /// ファイルの取得判定
        /// </summary>
        /// <param name="file"></param>
        /// <param name="targetDateFrom">取得ファイル更新日付（From）</param>
        /// <param name="targetDateTo">取得ファイル更新日付（To）</param>
        /// <returns></returns>        
        private static bool CheckTargetFile(string file,DateTime targetDateFrom,DateTime targetDateTo)
        {
            //存在チェック
            if (!File.Exists(file))
                return false;

            //属性チェック
            //非表示、システムファイルは除外
            FileAttributes attributes = File.GetAttributes(file);

            if ((attributes & FileAttributes.System) == FileAttributes.System
                || (attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                return false;

            //対象日付範囲チェック
            DateTime lastwriteDateTime = File.GetLastWriteTime(file);

            if (lastwriteDateTime.Date < targetDateFrom.Date || targetDateTo.Date < lastwriteDateTime.Date)
                return false;

            return true;
        }

        /// <summary>
        /// フォルダの対象判定
        /// </summary>
        /// <param name="dir">取得元フォルダ</param>
        /// <returns></returns>
        private static bool CheckTargetFolder(string dir)
        {
            var Array = dir.Split('\\');
            var strLastDir = Array[Array.Length - 1];

            //非表示、システムフォルダは除外
            FileAttributes attributes = File.GetAttributes(dir);

            if ((attributes & FileAttributes.System) == FileAttributes.System
                || (attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                return false;

            //特定フォルダ名は除外
            if (strLastDir == "bin"
                || strLastDir == "obj"
                || strLastDir == "Program Files"
                || strLastDir == "Program Files (x86)"
                || strLastDir == "Windows")
                return false;

            return true;
        }
    }
}
