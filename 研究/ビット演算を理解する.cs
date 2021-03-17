using System;
using System.Collections.Generic;
using static System.Console;
using System.Threading.Tasks;

namespace t
{
    /// <summary>
    /// ビット演算を理解する
    /// 参考:https://qiita.com/october/items/201f65c615a829a3e70d
    /// </summary>
    class Understand_BitCirc
    {
        public void Main()
        {
            //----------------------------------------------------------------------------------------
            //全て31を表す
            //----------------------------------------------------------------------------------------
            var dec = 31; //10進数
            var bin = 0B11111; //2進数(0Bか、0bを頭につける) 16 + 8 + 4 + 2 + 1 = 31
            var hex = 0X1F; //16進数(0Xか、0xを頭につける) 16 + 15 = 31

            //C#7以降では見やすいようにアンダーバーで区切ることができる
            //var bin2 = 0b_0011_1111_1001;
            //var bin3 = 0x_ff_2f_ff;

            //----------------------------------------------------------------------------------------
            //10進数を2進数で表示
            //----------------------------------------------------------------------------------------
            var list = new List<int>{ 1, 2, 3, 4, 5, 0b111, 0xFF };

            //Convert.ToStringの第2引数に2を指定すると、第1引数を2進数に変換する。
            //16を指定すると16進数に変換される。
            foreach (var item in list)
                WriteLine(Convert.ToString(item, 2).PadLeft(8, '0') + " " + item);
                //00000001 1
                //00000010 2
                //00000011 3
                //00000100 4
                //00000101 5
                //00000111 7
                //11111111 255

            foreach (var item in list)
                WriteLine(Convert.ToString(item, 16).PadLeft(8, '0') + " " + item);
                //00000001 1
                //00000002 2
                //00000003 3
                //00000004 4
                //00000005 5
                //00000007 7
                //000000ff 255

            //----------------------------------------------------------------------------------------
            //ビット演算子
            //----------------------------------------------------------------------------------------
            //「|」…片方だけでも1があれば回転して残像で両方1になる(or)
            //「&」…両方の穴に1が収まってはじめて1になる(and)

            int a = 0b0101;
            int b = 0b0110;

            Print(a & b);   //両方1なら1　　　　論理積　　　　0100
            Print(a | b);　 //どちらか1なら1　　論理和        0111
            Print(a ^ b);　 //片方だけ1なら1　　排他的論理和  0011
            Print(~a);　　　//0なら1、1なら0に　反転          11111111111111111111111111111010

            Print(a << 1);　//左にシフト                      1010
            Print(a >> 1);  //右にシフト                      0010

            //----------------------------------------------------------------------------------------
            //よく使う演算子のパターン
            //----------------------------------------------------------------------------------------
            //左からn番目にフラグを立てる 【i | (1 << n)】
            b = 1 << 2;　//0001→0100　10進数だと4を表す

            int i = 0b0001;
            i |= (1 << 2);　//0001と0100の論理和は0101　10進数だと5を表す

            //同じ値をXOR演算子　【^を使ってゼロクリアする】
            i = 0b0101;
            i ^= i; //0101と0101の排他的論理和は0000

            //左からn番目のフラグを消す　【i &= ~(1 << n)】
            i = 0b1111;
            i &= ~(1 << 2); //1111と0100の論理積(0100)を反転→1011

            //左からn番目のフラグが立っているか確認する　【i & (1 << n)】
            i = 0b0101;
            Convert.ToBoolean(i & (1 << 2)); //0101と0100の論理積は0100→0ではないのでtrue
            bool hoge = (i & (1 << 2)) != 1; //「0100は0001ではない」を満たすのでtrue

            //複数のフラグをまとめてマスクビットを作る
            a = (1 << 1);      //0010
            b = (1 << 2);      //0100
            var mask = a |= b; //0110 論理和

            int c = 0b1111;    //1111
            c &= ~mask;        //1111と「0110の反転(1001)」の論理積→1001

            //----------------------------------------------------------------------------------------
            //enumを使ってビット演算を見やすく
            //----------------------------------------------------------------------------------------
            //enumの前にFlagAttribute属性を指定することで、列挙型の名前でビット演算のフラグを管理できる。
            //Flagsを外すと数値に対してビット演算しない([Flags]をコメントにすると違いが分かる)
            for(int j = 0; j <= 8; j++)
            {
                WriteLine("{0} {1} {2}",
                          j,
                          Convert.ToString(j, 2).PadLeft(4, '0'),
                          (Collor)j);　//この行でフラグチェック、該当項目がない時は数値をそのまま返す
                //0 0000 0
                //1 0001 Red
                //2 0010 Green
                //3 0011 Red, Green
                //4 0100 Blue
                //5 0101 Red, Blue
                //6 0110 Green, Blue
                //7 0111 Red, Green, Blue
                //8 1000 8

                //※Flags属性なし↓　完全一致項目しか返してくれない
                //0 0000 0
                //1 0001 Red
                //2 0010 Green
                //3 0011 3
                //4 0100 Blue
                //5 0101 5
                //6 0110 6
                //7 0111 7
                //8 1000 8
            }

            //Taskを使ったフラグの具体例
            var task = new Task(() => Method_A(),
                                      TaskCreationOptions.LongRunning | TaskCreationOptions.PreferFairness);
            task.Start();

            CharacterManage();
        }

        /// <summary>
        /// 色のデータ列挙型
        /// </summary>
        [Flags]
        enum Collor : short
        {
            Red = 1 << 0,    //0001
            Green = 1 << 1,  //0010
            Blue = 1 << 2,   //0100
        };

        /// <summary>
        /// Status列挙型
        /// </summary>
        [Flags]
        public enum Status //10進数で書く場合は、2の倍数にする
        {
            /// <summary>
            /// 通常状態
            /// </summary>
            Normal = 0,     //通常状態　0000

            /// <summary>
            /// ねむり
            /// </summary>
            Sleep = 1,      //ねむり　　0001

            /// <summary>
            /// どく
            /// </summary>
            Poison = 2,     //どく　　　0010

            /// <summary>
            /// 攻撃UP
            /// </summary>
            AttackUp = 4,   //攻撃UP  　0100

            /// <summary>
            /// 防御UP
            /// </summary>
            DefUp = 8,      //防御UP  　1000
        };

        /// <summary>
        /// Status列挙型を使用したサンプルメソッド
        /// </summary>
        public void CharacterManage()
        {
            //キャラクター
            var chara = Status.Normal;

            //攻撃力アップ
            chara |= Status.AttackUp;
            Print(chara); //AttackUP

            //どくになる
            chara |= Status.Poison;
            Print(chara); //Poison, AttackUp

            //ねむりになる
            chara |= Status.Sleep;
            Print(chara); //Sleep, Poison, AttackUp

            //回復し、どくとねむりを解除　バフはそのまま
            Status Heal = Status.Sleep | Status.Poison; //チェック用の変数を作る
            chara &= ~Heal; //どくとねむり除去
            Print(chara); //AttackUp

            //攻撃・防御どちらか、もしくは両方アップしているか確認
            Status Up = Status.AttackUp | Status.DefUp; //チェック用の変数を作る
            bool b = (chara & Up) != Status.Normal; //通常状態と比較し、違っていればtrue

            //全ての効果を解除(いてつくはどう)
            chara ^= chara;
            Print(chara); //Normal
        }
        
        /// <summary>
        /// 状態確認メソッド
        /// </summary>
        /// <param name="i">数値型</param>
        public void Print(int i)
        {
            WriteLine(Convert.ToString(i, 2).PadLeft(4, '0'));
        }

        /// <summary>
        /// 状態確認メソッド
        /// </summary>
        /// <param name="s">列挙型Status</param>
        public void Print(Status s)
        {
            WriteLine("{0} {1}", Convert.ToString((int)s, 2).PadLeft(4, '0'), s);
        }

        /// <summary>
        ///Task用のテストメソッド
        /// </summary>
        public void Method_A()
        {
            //なんか処理
        }
    }
}
