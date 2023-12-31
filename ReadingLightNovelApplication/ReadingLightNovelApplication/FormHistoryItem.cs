﻿
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadingLightNovelApplication
{
    public partial class FormHistoryItem : Form
    {
        SupportMethod SupportMethod = new SupportMethod();
        string matp;
        public FormHistoryItem(string matacpham)
        {
            InitializeComponent();
            matp = matacpham;
        }

        private void FormHistoryItem_Load(object sender, EventArgs e)
        {
            DataTable dt = SupportMethod.DataReader("select top 1 *\r\nfrom TacPham " +
                "\r\ninner join Volume on Volume.MaTacPham = TacPham.MaTacPham" +
                "\r\ninner join Chapter on Chapter.MaVolume = Volume.MaVolume" +
                "\r\ninner join LichSu on LichSu.MaChapter = Chapter.MaChapter" +
                "\r\nwhere TacPham.MaTacPham = N'" + matp + "'" +
                "\r\norder by LichSu.ThoiGian desc");
            try
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(Application.StartupPath + "\\Asset\\DataLightNovel\\"
                    + dt.Rows[0]["MaTacPham"].ToString() + "\\" + dt.Rows[0]["Anh"].ToString());
                pbImg.Image = image;
            }
            catch {
                Image image = Image.FromFile(Application.StartupPath + "\\Asset\\DataLightNovel\\noLoadUser.png"
                        );
                pbImg.Image = image;
            }
            lblTenTruyen.Text = dt.Rows[0]["TenTacPham"].ToString();
            lblChapter.Text = dt.Rows[0]["TenChapter"].ToString();
            lblVol.Text = dt.Rows[0]["TenVolume"].ToString();
            lblLoai.Text = dt.Rows[0]["LoaiTruyen"].ToString();
        }

        private void lblTenTruyen_Click(object sender, EventArgs e)
        {
            LayoutLogged lg = SupportMethod.getFormParent(this, "LayoutLogged") as LayoutLogged;
            Panel panel1 = SupportMethod.getPanel(lg, "panelNoiDung");
            foreach (Control c in panel1.Controls)
            {
                c.Dispose();
            }
            SupportMethod.openChildFormDockFill(lg.getActiveForm(), new FormContent(matp), panel1);
        }

        private void lblChapter_Click(object sender, EventArgs e)
        {
            DataTable dt2 = SupportMethod.DataReader("select top 1 Volume.TenVolume, Chapter.TenChapter, Chapter.MaChapter, Chapter.ThoiGianDang" +
                "\r\nfrom TacPham \r\ninner join Volume on Volume.MaTacPham = TacPham.MaTacPham" +
                "\r\ninner join Chapter on Chapter.MaVolume = Volume.MaVolume" +
                "\r\nwhere TacPham.MaTacPham = '" + matp + "'" +
                "\r\norder by Chapter.ThoiGianDang desc");
            FormMain formMain = SupportMethod.getFormMain(this) as FormMain;
            Panel panel1 = SupportMethod.getPanel(formMain, "panelMain");
            foreach (Control c in panel1.Controls)
            {
                c.Dispose();
            }
            SupportMethod.openChildFormDockFill(formMain.getActiveForm(), new FormMainReading(dt2.Rows[0]["MaChapter"].ToString()), panel1);
        }

        private void pbImg_Click(object sender, EventArgs e)
        {
            LayoutLogged lg = SupportMethod.getFormParent(this, "LayoutLogged") as LayoutLogged;
            Panel panel1 = SupportMethod.getPanel(lg, "panelNoiDung");
            foreach (Control c in panel1.Controls)
            {
                c.Dispose();
            }
            SupportMethod.openChildFormDockFill(lg.getActiveForm(), new FormContent(matp), panel1);
        }

        /*private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa truyện này khỏi lịch sử không ? Nếu  có  ấn  nút  Yes, không  thì  ấn  nút  Hủy", "Xóa  lịch sử", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SupportMethod.DataChange("DELETE FROM LichSu\r\nWHERE MaChapter IN (" +
                "\r\n    SELECT Chapter.MaChapter\r\n    FROM TacPham" +
                "\r\n    INNER JOIN Volume ON Volume.MaTacPham = TacPham.MaTacPham" +
                "\r\n    INNER JOIN Chapter ON Chapter.MaVolume = Volume.MaVolume" +
                "\r\n    WHERE TacPham.MaTacPham = N'" + matp + "' and LichSu.TenDangNhap = N'HiuTao'\r\n);");
                LayoutLogged lg = SupportMethod.getFormParent(this, "LayoutLogged") as LayoutLogged;
                Panel panel1 = SupportMethod.getPanel(lg, "panelNoiDung");
                SupportMethod.openChildFormDockFill(lg.getActiveForm(), new FormLichSu(), panel1);
            }
        }*/

        private void lblChapter_Click_1(object sender, EventArgs e)
        {
            DataTable dt2 = SupportMethod.DataReader("select top 1 Volume.TenVolume, Chapter.TenChapter, Chapter.MaChapter, Chapter.ThoiGianDang" +
                "\r\nfrom TacPham \r\ninner join Volume on Volume.MaTacPham = TacPham.MaTacPham" +
                "\r\ninner join Chapter on Chapter.MaVolume = Volume.MaVolume" +
                "\r\nwhere TacPham.MaTacPham = '" + matp + "'" +
                "\r\norder by Chapter.ThoiGianDang desc");
            FormMain formMain = SupportMethod.getFormMain(this) as FormMain;
            Panel panel1 = SupportMethod.getPanel(formMain, "panelMain");
            foreach (Control c in panel1.Controls)
            {
                c.Dispose();
            }
            SupportMethod.openChildFormDockFill(formMain.getActiveForm(), new FormMainReading(dt2.Rows[0]["MaChapter"].ToString()), panel1);
        }
    }
}
