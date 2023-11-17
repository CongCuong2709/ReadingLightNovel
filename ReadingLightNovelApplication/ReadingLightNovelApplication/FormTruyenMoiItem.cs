﻿using System;
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
	public partial class FormTruyenMoiItem : Form
	{

		private string maTacPham;
		private SupportMethod supportMethod = new SupportMethod();
		public FormTruyenMoiItem(string maTacPham)
		{
			InitializeComponent();
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			this.BackColor = Color.Transparent;
			this.maTacPham = maTacPham;
		}

		private void FormTruyenMoiItem_Load(object sender, EventArgs e)
		{
			DataTable data = supportMethod.DataReader("select TacPham.MaTacPham, TacPham.TenTacPham, TacPham.TomTat, TacPham.Anh" +
				"\r\nfrom TacPham" +
				"\r\nwhere TacPham.MaTacPham = N'"+ maTacPham + "'");

			if (File.Exists(Application.StartupPath + "\\Asset\\DataLightNovel\\"
				+ data.Rows[0]["MaTacPham"].ToString() + "\\" + data.Rows[0]["Anh"].ToString()))
			{
				panelAnh.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Asset\\DataLightNovel\\"
				+ data.Rows[0]["MaTacPham"].ToString() + "\\" + data.Rows[0]["Anh"].ToString());
			}
			else
			{
				panelAnh.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Asset\\DataLightNovel\\"
				+ data.Rows[0]["MaTacPham"].ToString() + "\\NoUser.jpg");
			}
			btnTenTruyen.Text = data.Rows[0]["TenTacPham"].ToString();
			btnTomTat.Text = data.Rows[0]["TomTat"].ToString();

		}
	}
}
