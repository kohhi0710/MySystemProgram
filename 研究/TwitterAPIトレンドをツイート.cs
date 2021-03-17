using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;

namespace t
{
    /// <summary>
    /// TwitterAPIトレンドをツイート
    /// 参考:https://qiita.com/chiaki1220jp/items/62b415f7719e427c32c5
    /// </summary>
    class TrendTweet
    {
        public void MainProgram()
        {
            //API Key
            string APIKey = "<APIキー>";
            //API Secret Key
            string APISecretKey = "＜APIシークレットキー＞";
            //Access Token
            string AccssToken = "＜アクセストークン＞";
            //Access Token Secret
            string AccessTokenSecret = "＜アクセストークンシークレット＞";

            //認証情報
            var Tokens = CoreTweet.Tokens.Create(APIKey, APISecretKey, AccssToken, AccessTokenSecret);
            //大阪のトレンド情報取得
            var TrendsJson = Tokens.Trends.Place(15015370); //Osaka

            //JSONデータ取得
            string JsonData = TrendsJson.Json.ToString();
            //データ加工
            JsonData = "{ json_data: " + JsonData + "}";

            //URLデコードする
            string URLDec = System.Web.HttpUtility.UrlDecode(JsonData);
            //JSON文字列をデシリアライズ
            Root TrendData = JsonConvert.DeserializeObject<Root>(URLDec);

            //練習数
            int DataCount = 3;

            //ツイート
            Tokens.Statuses.Update(status => DataCount + "testTweet");
            //1秒待つ
            Thread.Sleep(1000);

            for(int i = 0; i < DataCount; i++)
            {
                //取得トレンド総数
                int TrendCnt = TrendData._Json_Data[0]._Trends.Count;

                //ランダム値
                Random Rnd = new Random();
                int TrendRdm = Rnd.Next(0, TrendCnt);

                //トレンド文字列取得
                string TrendText = TrendData._Json_Data[0]._Trends[TrendRdm]._Query;
                //#はいらないので置換
                TrendText = TrendText.Replace("#", "");

                //ツイート
                Tokens.Statuses.Update(status => "【" + (i + 1) + "】" + TrendText);
                //1秒待つ
                Thread.Sleep(1000);
            }

            //ツイート
            Tokens.Statuses.Update(status => "終了");
        }
    }

    public class Trend
    {
        public string _Name { get; set; }
        public string _URL { get; set; }
        public object _Promoted_Content { get; set; }
        public string _Query { get; set; }
        public int? _Tweet_volume { get; set; }
    }

    public class Location
    {
        public string _Name { get; set; }
        //WOEID:32ビットの一意の参照識別子であり、地球上のあらゆる機能を識別する
        public int _WOEID { get; set; }
    }

    public class JsonData
    {
        public List<Trend> _Trends { get; set; }
        public DateTime _As_Of { get; set; }
        public DateTime _Created_At { get; set; }
        public List<Location> _Locations { get; set; }
    }

    public class Root
    {
        public List<JsonData> _Json_Data { get; set; }
    }
}
