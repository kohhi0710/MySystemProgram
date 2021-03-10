# MySystemPrograms
オリジナル業務システム
SystemControl_Methodクラスのインスタンスを生成することで関連プログラムが使用可能になる。

## SystemControlClass
・000_SystemControl_Base
・001_SystemControl_Method
システムの根幹になるクラス
インスタンスを生成することでConfigの読み込み、Logクラス・Pathクラスのインスタンス生成が実行され、MySystemProgramsフォルダ内の各種プログラムが使用可能になる

## LogClass
・000_Log_Base
・001_Log_Method
ログ関連クラス
ログメッセージの作成、ログの出力など

## ExceptionClass
・ProgramErrorException
Exceptionクラスを継承したエラークラス
エラーログ向けに調整している

## CSVByteOperationClass
・000_CSVByteOperation_Base
・001_CSVByteOperation_Method
CSVやテキストデータをByteに変換して扱うクラス

## DataBaseOperationClass
・000_SQLServer_Connection_Base
・001_SQLServer_Connection_Method
SQLServerとの接続、データのやりとりを扱うクラス

・【Sample】SQLOperation
使い方のサンプル

## ExcelOperationClass
・ExcelControl
Excelデータを扱うクラス

## OtherClass
・MethodDefine
エラー時に使用する。エラーが出たメソッドの情報を保存するクラス

・PathConfig
各種フォルダパス、ファイルパスを取得するメソッドを集めたクラス

・ProgramConfig
Config.xmlからデータを読み込んで情報を保存するクラス

・TextOperation
テキストデータを取り扱うクラス

# StaticClass
静的クラスを集めたフォルダ

## FunctionModule
便利な関数を集めたクラス

## SystemModule
インスタンス生成用の変数を集めたクラス

## QueryGenerator
データベースクエリを作成するクラス

# AnotherProgram
システムとは独立して単体で動作できるプログラム

## AESSystem
AESを使用した暗号・復号化を取り扱うクラス

## ExternalProcessOperation
外部プログラム(.exeなど)を起動するクラス

# Program
メイン処理
今のところはテスト駆動のみ

