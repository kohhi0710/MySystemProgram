/// <summary>
/// メソッド定義クラス　メッセージコード、メソッド名を保存する
/// </summary>
public class MethodDefine
{
    private string _MethodName;
    private string _MessageCode;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="MethodName">メソッド名</param>
    /// <param name="MessageCode">メッセージコード</param>
    public MethodDefine(string MethodName,string MessageCode)
    {
        _MethodName = MethodName;
        _MessageCode = MessageCode;
    }

    public string GetMethodName()
    {
        return _MethodName;
    }

    public string GetMessageCode()
    {
        return _MessageCode;
    }
}
