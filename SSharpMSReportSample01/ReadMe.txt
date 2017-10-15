MicroSoftReportのテストのため、以下３点実施

１．Microsoft rdlc パッケージのインストール
２．Microsoft SQLServerDataTool（SSDT)のインストール
３．NugetパッケージマネージャにてReportViewerインストール
　　WindowsFormで使うため以下実行
　　Install-Package Microsoft.ReportingServices.ReportViewerControl.WinForms

　　さらにツールボックスで右クリックしてアイテムの選択＞.Net Framework Component
　　より、「参照」ボタンをクリックして以下のDllを参照の対象に加える。
　　
　　フォルダ
　　{Solution Directory}\packages\Microsoft.ReportingServices.ReportViewerControl.Winforms.{version}\lib\net40
　　
　　ファイル名
　　Microsoft.ReportViewer.WinForms.dll

　　※ 今回はWebを対象としないがWebを対象とするときは
　　Install-Package Microsoft.ReportingServices.ReportViewerControl.WebForms
　　Microsoft.ReportViewer.WebForms.dll 

参考ページ
https://docs.microsoft.com/en-us/sql/reporting-services/application-integration/integrating-reporting-services-using-reportviewer-controls-get-started

　　