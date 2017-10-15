using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using CSharpMSReportSample01.Properties;

namespace CSharpMSReportSample01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            List<ReportParameter> reportParams = new List<ReportParameter>();
            reportParams.Add(new ReportParameter("ReportParameter1", "ReportParameter1 - TEST"));
            reportParams.Add(new ReportParameter("ReportParameter2", "ReportParameter2 - TEST"));
            this.reportViewer1.LocalReport.SetParameters(reportParams);

            //// Load Data to dt
            //var dt = new DataSet1.CustomersDataTable();
            //var da = new DataSet1TableAdapters.CustomersTableAdapter();
            //da.Fill(dt);

            // 定義したデータセットに値を設定する。
            DataTable memberDataTable = new ReportDataSet.MemberDataTable(); // データセットからテーブルを取得
            DataRow memberDataRow = memberDataTable.NewRow();
            memberDataRow["MemberId"] = 1;
            memberDataRow["Name"] = "習志野太郎";
            memberDataRow["Address"] = "千葉県習志野市";
            memberDataRow["Age"] = 34;

            DataRow memberDataRow2 = memberDataTable.NewRow();
            memberDataRow2["MemberId"] = 2;
            memberDataRow2["Name"] = "習志野太郎2 - 2件テスト";
            memberDataRow2["Address"] = "千葉県習志野市2 - 2件テスト";
            memberDataRow2["Age"] = 44;

            memberDataTable.Rows.Add(memberDataRow);
            memberDataTable.Rows.Add(memberDataRow2);

            DataTable accountDataTable = new ReportDataSet.AccountDataTable(); // データセットからテーブルを取得
            DataRow accountDataRow = accountDataTable.NewRow();
            accountDataRow["AccountId"] = 12;
            accountDataRow["MemberId"] = 1;
            accountDataRow["SystemId"] = 120;
            accountDataRow["UserAccount"] = "UserAccountTest";
            accountDataRow["Password"] = "UserPasswordTest";

            accountDataTable.Rows.Add(accountDataRow);

            //// Set Report Properties
            //var rds = new ReportDataSource("CustomersTable", (DataTable)dt);
            //this.reportViewer1.LocalReport.DataSources.Add(rds);
            //this.reportViewer1.LocalReport.ReportPath
            //  = @"C:\test\Report1.rdlc";

            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Member", memberDataTable));
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Account", accountDataTable));

            //// Refresh
            this.reportViewer1.RefreshReport();
        }
    }
}
