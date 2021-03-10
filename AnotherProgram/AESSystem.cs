using System.IO;
using System.Security.Cryptography;
using System.Text;
using static System.Console;

namespace t
{
    class AESSystem
    {
        //↓128ビットで共有鍵と初期ベクターを生成したい時はこちらを使う。Encrypt()とDecrypt()メソッドの値も変更する。
        /*private const string AES_IV = @"11111111111111111";
        private const string AES_Key = @"1111111111111111";*/

        //256ビット共有鍵と初期ベクター
        private const string AES_IV = @"11111111111111111111111111111111";
        private const string AES_Key = @"11111111111111111111111111111111";


        public void MainProgram()
        {
            WriteLine("暗号化と復号化のどちらを行いますか？");
            WriteLine("暗号化はe、復号化はdを入力してください。");
            Write("→");
            string s = ReadLine();

            switch (s)
            {
                case "e":
                    EncryptKeyWord();
                    break;
                case "d":
                    DecryptAESWord();
                    break;
                default:
                    WriteLine("入力値に誤りがあります。もう一度入力してください。");
                    WriteLine("");
                    MainProgram();
                    break;
            }
        }

        private void EncryptKeyWord()
        {
            WriteLine("");
            WriteLine("暗号化したい文字列を入力して下さい。");
            Write("→");
            string input = ReadLine();
            WriteLine("");
            WriteLine("暗号化を実行します。");
            WriteLine("");
            System.Threading.Thread.Sleep(1000);
            string result = Encrypt(input, AES_IV, AES_Key);
            WriteLine("暗号化が完了しました。詳細は以下になります。");
            WriteLine("-------------------------------------------------");
            WriteLine("暗号化前文字列：" + input);
            WriteLine("暗号化後文字列：" + result);
            WriteLine("-------------------------------------------------");
            WriteLine("");
            WriteLine("プログラムをもう一度初めから実行しますか？続ける場合はy、終了する場合はnを入力して下さい。");
            Write("→");
            string s = ReadLine();
            EndPG(s);
        }

        private void DecryptAESWord()
        {
            WriteLine("");
            WriteLine("復号化したい文字列を入力して下さい。");
            Write("→");
            string input = ReadLine();
            WriteLine("");
            WriteLine("復号化を実行します。");
            WriteLine("");
            System.Threading.Thread.Sleep(1000);

            try
            {
                string result = Decrypt(input, AES_IV, AES_Key);
                WriteLine("復号化が完了しました。詳細は以下になります。");
                WriteLine("-------------------------------------------------");
                WriteLine("復号化前文字列：" + input);
                WriteLine("復号化後文字列：" + result);
                WriteLine("-------------------------------------------------");
                WriteLine("");
                WriteLine("プログラムをもう一度初めから実行しますか？続ける場合はy、終了する場合はnを入力して下さい。");
                Write("→");
                string s = ReadLine();
                EndPG(s);
            }
            catch
            {
                WriteLine("復号化が失敗しました。");
                WriteLine("プログラムをもう一度初めから実行しますか？続ける場合はy、終了する場合はnを入力して下さい。");
                Write("→");
                string s = ReadLine();
                EndPG(s);
            }
        }

        private void EndPG(string s)
        {
            switch (s)
            {
                case "y":
                    WriteLine("");
                    WriteLine("プログラムを再開します。");
                    WriteLine("");
                    MainProgram();
                    break;
                case "n":
                    WriteLine("");
                    WriteLine("プログラムを終了しています。");
                    WriteLine("");
                    System.Threading.Thread.Sleep(1000);
                    return;
                default:
                    WriteLine("入力値に誤りがあります。もう一度入力して下さい。");
                    Write("→");
                    string s2 = ReadLine();
                    EndPG(s2);
                    break;
            }
        }

        //------------------------------------------------------------------------------------------------
        //暗号化メソッド
        //------------------------------------------------------------------------------------------------
        /// <summary>
        /// 対称鍵暗号を使って文字列を暗号化する
        /// </summary>
        /// <param name="text">暗号化する文字列</param>
        /// <param name="iv">対称アルゴリズムの初期ベクター</param>
        /// <param name="key">対称アルゴリズムの共有鍵</param>
        /// <returns>暗号化された文字列</returns>
        private string Encrypt(string text, string iv, string key)
        {
            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                //共通鍵クラスの設定
                rijndael.BlockSize = 256;
                rijndael.KeySize = 256;
                rijndael.Mode = CipherMode.CBC;
                rijndael.Padding = PaddingMode.PKCS7;

                rijndael.IV = Encoding.UTF8.GetBytes(iv);
                rijndael.Key = Encoding.UTF8.GetBytes(key);

                //暗号化を実施するオブジェクトを生成
                ICryptoTransform encryptor = rijndael.CreateEncryptor(rijndael.Key, rijndael.IV);

                //バイト型の配列を設定
                byte[] encrypted;

                //メモリストリーム(メモリに読み書きする)クラスを設定
                using (MemoryStream mStream = new MemoryStream())
                {
                    //暗号化アルゴリズムのクラス(引数 = メモリストリーム、暗号化実施オブジェクト、モードを「書き込み」に設定)
                    using (CryptoStream ctStream = new CryptoStream(mStream, encryptor, CryptoStreamMode.Write))
                    {
                        //streamwriterでctStreamを読み込み
                        using (StreamWriter sw = new StreamWriter(ctStream))
                        {
                            //読み込んだ内容をもとにテキストを暗号化
                            sw.Write(text);
                        }

                        //配列に追加
                        encrypted = mStream.ToArray();

                    }
                }

                //値をリターン
                return (System.Convert.ToBase64String(encrypted));

            }
        }

        //------------------------------------------------------------------------------------------------
        //AES復号メソッド
        //------------------------------------------------------------------------------------------------
        /// <summary>
        /// 対称鍵暗号を使って暗号文を復号する
        /// </summary>
        /// <param name="cipher">暗号化された文字列</param>
        /// <param name="iv">対称アルゴリズムの初期ベクター</param>
        /// <param name="key">対称アルゴリズムの共有鍵</param>
        /// <returns>復号された文字列</returns>
        private string Decrypt(string cipher, string iv, string key)
        {
            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                //共通鍵セッティング
                rijndael.BlockSize = 256;
                rijndael.KeySize = 256;
                rijndael.Mode = CipherMode.CBC;
                rijndael.Padding = PaddingMode.PKCS7;

                rijndael.IV = Encoding.UTF8.GetBytes(iv);
                rijndael.Key = Encoding.UTF8.GetBytes(key);

                //暗号化用オブジェクト
                ICryptoTransform decryptor = rijndael.CreateDecryptor(rijndael.Key, rijndael.IV);

                //空の文字列生成
                string plain = string.Empty;

                //メモリストリームクラス　読み込んだ文字列をバイナリデータで読み込み
                using (MemoryStream mStream = new MemoryStream(System.Convert.FromBase64String(cipher)))
                {
                    //読み込みモードでメモリストリームのデータを解析
                    using (CryptoStream ctStream = new CryptoStream(mStream, decryptor, CryptoStreamMode.Read))
                    {
                        //StreamReaderで解析したデータを読み込み
                        using (StreamReader sr = new StreamReader(ctStream))
                        {
                            //空文字列に代入
                            plain = sr.ReadLine();

                        }
                    }
                }

                //値を返す
                return plain;

            }
        }
    }
}
