using UnityEngine;　　　　　　　//UnityEngineも忘れずに。忘れると通常のUnityEngine名前空間が使えなくなる
using static UnityEngine.Mathf; //よく使う名前空間のものはstaticをつけて登録
using static UnityEngine.Input;

/// <summary>
/// Unity用staticの使い方まとめ
/// 参考:https://qiita.com/UbiquitousD/items/4571b512674e268b495a
/// </summary>
class UnityStaticPractice
{
    //Unityではシーンをまたぐとガベージコレクションで変数が破棄されてしまう
    //static事前にメモリを確保しておくことで、変数を保持することができる
    //ただし、使いすぎるとメモリを圧迫するので計画的に使用する
    public static int score = 0;

    public void MainProgram()
    {
        Clamp(0,0,0);
        GetAxis("Test");
    }
}

/// <summary>
/// ゲームコントローラークラス
/// </summary>
class GameController : MonoBehaviour
{
    private CharacterController cc;
    private CharacterController2 cc2;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        //cc2 = GetComponent<CharacterController2>(); //Static変数を持ったクラスなので、GetComponentは必要ない

        Processing();
    }

    private void Processing()
    {
        Debug.Log(cc.score); //0
        cc.ScoreUp();        //Score加算
        Debug.Log(cc.score); //100
    }

    //メンバー変数を参照しようとすると、
    //インスタンス参照でメンバーにアクセスできません。代わりに型名を使用してください
    //とエラーが出る。
    //staticでどこでも使えるようになったため、GetComponentする必要はなくなった。
    private void Processing2()
    {
        //Debug.Log(cc2.score); //0
        //cc2.ScoreUp();        //Score加算
        //Debug.Log(cc2.score); //100

        Debug.Log(CharacterController2.score);
        CharacterController2.ScoreUp();
        Debug.Log(CharacterController2.score);
    }
}

/// <summary>
/// キャラクタークラス
/// </summary>
class CharacterController :MonoBehaviour
{
    public int score = 0;

    public void ScoreUp()
    {
        score += 100;
    }
}

/// <summary>
/// キャラクタークラス2 static変数持ちバージョン
/// </summary>
class CharacterController2 : MonoBehaviour
{
    public static int score = 0;

    public static void ScoreUp()
    {
        score += 100;
    }
}