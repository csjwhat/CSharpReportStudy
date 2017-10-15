using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Data;

namespace CSharpMSReportSample02
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputReport();
        }

        public static void OutputReport()
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;

            // レポートの作成
            using (LocalReport localReport = new ReportViewer().LocalReport)
            {
                // レポート定義を設定
                localReport.ReportPath = @"TestReport.rdlc";

                // パラメータの設定
                List<ReportParameter> reportParams = new List<ReportParameter>();
                reportParams.Add(new ReportParameter("ReportParameter1", "ReportParameter1 - TEST"));
                reportParams.Add(new ReportParameter("ReportParameter2", "ReportParameter2 - TEST"));
                localReport.SetParameters(reportParams);

                // 定義した(.Net)データセットに値を設定する。
                DataTable member2DataTable = new ReportSample02DataSet.Member2DataTable(); // データセットからテーブルを取得
                DataRow memberDataRow = member2DataTable.NewRow();
                memberDataRow["Member2Id"] = 1;
                memberDataRow["Name"] = "習志野太郎";
                memberDataRow["Address"] = "千葉県習志野市";
                memberDataRow["Age"] = 34;

                DataRow memberDataRow2 = member2DataTable.NewRow();
                memberDataRow2["Member2Id"] = 2;
                memberDataRow2["Name"] = "習志野太郎2 - 2件テスト";
                memberDataRow2["Address"] = "千葉県習志野市2 - 2件テスト";
                memberDataRow2["Age"] = 44;

                member2DataTable.Rows.Add(memberDataRow);
                member2DataTable.Rows.Add(memberDataRow2);

                DataTable accountDataTable = new ReportSample02DataSet.AccountDataTable(); // データセットからテーブルを取得
                DataRow accountDataRow = accountDataTable.NewRow();
                accountDataRow["AccountId"] = 12;
                accountDataRow["Member2Id"] = 1;
                accountDataRow["SystemId"] = 120;
                accountDataRow["UserAccount"] = "UserAccountTest";
                accountDataRow["Password"] = "UserPasswordTest";

                accountDataTable.Rows.Add(accountDataRow);

                //// Set Report Properties
                //var rds = new ReportDataSource("CustomersTable", (DataTable)dt);
                //this.reportViewer1.LocalReport.DataSources.Add(rds);
                //this.reportViewer1.LocalReport.ReportPath
                //  = @"C:\test\Report1.rdlc";

                // (.Net)データセットに設定した値が、レポートのデータソースとして使われるよう
                // 設定する。
                localReport.DataSources.Add(new ReportDataSource("Member2", member2DataTable));
                localReport.DataSources.Add(new ReportDataSource("Account2", accountDataTable));

                // Byteの配列（PDF）への変換
                byte[] bytes = localReport.Render(
                    "PDF", null, out mimeType, out encoding, out filenameExtension,
                    out streamids, out warnings);

                // ファイル名・ファイルパスの決定
                string tempPath = @"C:\Users\Tetsutaro Yamada\Desktop";
                string saveAs = string.Format("{0}.pdf", Path.Combine(tempPath, "myfilename"));

                int idx = 0;
                while (File.Exists(saveAs))
                {
                    idx++;
                    saveAs = string.Format("{0}.{1}.pdf", Path.Combine(tempPath, "myfilename"), idx);
                }


                // レポートの出力
                using (FileStream fs = new FileStream(saveAs, FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                }
                localReport.Dispose();
            }
        }


    }
}
