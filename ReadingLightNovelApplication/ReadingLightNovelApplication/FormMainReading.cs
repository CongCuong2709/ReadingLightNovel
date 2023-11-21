﻿using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ReadingLightNovelApplication
{
    public partial class FormMainReading : Form
    {
        SupportMethod dataload = new SupportMethod();
        string ma, ngay;

        public FormMainReading(string machapter)
        {
            InitializeComponent();
            ma = machapter;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            FormMain formMain = dataload.getFormMain(this) as FormMain;
            Panel panel1 = dataload.getPanel(formMain, "panelMain");
            LayoutLogged lg = new LayoutLogged();
            dataload.openChildFormDockFill(formMain.getactive(), lg, panel1);
            Panel panel2 = dataload.getPanel(lg, "panelNoiDung");
            foreach (Control c in panel2.Controls)
            {
                c.Dispose();
            }
            DataTable dt = dataload.DataReader("select TacPham.MaTacPham" +
                "\r\nfrom TacPham \r\ninner join Volume on Volume.MaTacPham = TacPham.MaTacPham" +
                "\r\ninner join Chapter on Chapter.MaVolume = Volume.MaVolume" +
                "\r\nwhere Chapter.MaChapter = '" + ma + "'");
            dataload.openChildFormDockFill(lg.getActiveForm(), new FormContent(dt.Rows[0][0].ToString()), panel2);
        }

        private void FormMainReading_Load(object sender, EventArgs e)
        {
            DataTable dt = dataload.DataReader("select *" +
                " \r\nfrom Chapter inner join Volume on Volume.MaVolume = Chapter.MaVolume" +
                "\r\ninner join TacPham on TacPham.MaTacPham = Volume.MaTacPham" +
                "\r\nwhere Chapter.MaChapter = '" + ma + "'");
            btnVolName.Text = dt.Rows[0]["TenVolume"].ToString();
            btnChapterName.Text = dt.Rows[0]["TenChapter"].ToString();

            DataTable dt1 = dataload.DataReader("select count(BinhLuan.MaBinhLuan) " +
                "\r\nfrom Chapter inner join Volume on Volume.MaVolume = Chapter.MaVolume" +
                "\r\ninner join BinhLuan on BinhLuan.MaChapter = Chapter.MaChapter" +
                "\r\nwhere Chapter.MaChapter = '" + ma + "'");

            DataTable dt2 = dataload.DataReader("DECLARE @StartDate DATE = '" + dt.Rows[0]["ThoiGianDang"].ToString() + "'" +
                "\r\nDECLARE @EndDate DATE = GETDATE();  -- Ngày hiện tại" +
                "\r\n\r\nDECLARE @DateDiff INT = DATEDIFF(DAY, @StartDate, @EndDate);" +
                "\r\n\r\nSELECT\r\n    CASE\r\n        " +
                "WHEN @DateDiff < 30 THEN  CAST(@DateDiff AS NVARCHAR(10)) +  N' Ngày' \r\n        " +
                "WHEN @DateDiff < 365 THEN + CAST(@DateDiff / 30 AS NVARCHAR(10)) +  N' Tháng' \r\n        " +
                "ELSE CAST(@DateDiff / 365 AS NVARCHAR(10)) +  N' Năm' \r\n    END AS Result;");

            int t = dataload.CountWordsFromFile(Application.StartupPath + "\\Asset\\DataLightNovel\\"
                        + dt.Rows[0]["MaTacPham"].ToString() + "\\" + dt.Rows[0]["TenVolume"].ToString() + "\\" + dt.Rows[0]["Nguon"].ToString());

            btnDetail.Text = dt1.Rows[0][0].ToString() + " Bình luận - " + t.ToString() + " Từ - " + dt2.Rows[0][0].ToString();
            dt2.Dispose();
            dt1.Dispose();
            string urlContent = Application.StartupPath + "\\Asset\\DataLightNovel\\"
                        + dt.Rows[0]["MaTacPham"].ToString() + "\\" + dt.Rows[0]["TenVolume"].ToString() + "\\" + dt.Rows[0]["Nguon"].ToString();
            lbContent.Text = dataload.loadContent(urlContent);

            dataload.AddChildFormDockTop(new FormCommentArea(ma), this.panelCmt);

        }

        


        

        

        
    }
}
