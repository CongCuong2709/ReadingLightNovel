﻿using Guna.UI2.WinForms;
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
	public partial class FormTheoDoiNhieuItem : Form
	{
		private SupportMethod supportMethod = new SupportMethod();
		private string maTacPham;
		private int top;

		public FormTheoDoiNhieuItem(string maTacPham, int top)
		{
			InitializeComponent();
			this.maTacPham = maTacPham;
			this.top = top;
		}

		private void FormTheoDoiNhieuItem_Load(object sender, EventArgs e)
		{
			DataTable data = supportMethod.DataReader("select TacPham.TenTacPham, COUNT(*) as luotThich" +
				"\r\nfrom YeuThich inner join TacPham on TacPham.MaTacPham = YeuThich.MaTacPham" +
				"\r\nwhere TacPham.MaTacPham = N'" + maTacPham +"'" +
				"\r\ngroup by TacPham.MaTacPham, TacPham.TenTacPham");
			btnSo.Text = top.ToString();
			btnTenTruyen.Text = data.Rows[0]["TenTacPham"].ToString();
			btnLuotTheoDoi.Text = data.Rows[0]["luotthich"].ToString() + " theo dõi";

			btnTenTruyen.MouseEnter += mouseEnter;
			btnTenTruyen.MouseLeave += mouseLeave;
		}

		private void btnTenTruyen_Click(object sender, EventArgs e)
		{
			supportMethod.openChildFormFromForm("LayoutLogged", "panelNoiDung", new FormContent(maTacPham), this);

		}

		public void mouseEnter(object sender, EventArgs e)
		{
			Guna2Button btn = sender as Guna2Button;
			btn.ForeColor = Color.Green;
		}

		public void mouseLeave(object sender, EventArgs e)
		{
			Guna2Button btn = sender as Guna2Button;
			btn.ForeColor = Color.Black;
		}
	}
}
