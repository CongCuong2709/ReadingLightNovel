﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadingLightNovelApplication
{
    internal class SupportMethod
    {
		string strConnect = "Data Source=DESKTOP-EAAUDNC\\USERNAME;" +
			"Initial Catalog=ReadingLightNovel;Integrated Security=True";
		SqlConnection sqlConnect = null;

		//Connect to database
		private void OpenConnect()
		{
			sqlConnect = new SqlConnection(strConnect);
			if (sqlConnect.State != ConnectionState.Open)
				sqlConnect.Open();
		}

		//Close database
		private void CloseConnect()
		{
			if (sqlConnect.State != ConnectionState.Closed)
				sqlConnect.Close();
			sqlConnect.Dispose(); // giải phóng bộ nhớ cho database
		}

		// Get result 's Query 
		public DataTable DataReader(string selectCommandText)
		{
			DataTable dtBang = new DataTable();
			OpenConnect();
			SqlDataAdapter sqlDataAdapte = new SqlDataAdapter(selectCommandText, sqlConnect);
			sqlDataAdapte.Fill(dtBang);
			CloseConnect();
			return dtBang;
		}

		public List<object> GetValueOfOneField(string selectCommandText)
		{
			List<object> list = new List<object>();
			DataTable dataTable = DataReader(selectCommandText);
			if(dataTable.Columns.Count >= 2)
			{
				return list;
			}
			foreach(var row in dataTable.Rows)
			{
				list.Add(row);
			}
			return list;
		}

		//Charge data of database by SQL query command
		public void DataChanger(string commandText)
		{
			OpenConnect();
			SqlCommand sqlcommand = new SqlCommand(commandText);
			sqlcommand.Connection = sqlConnect;
			sqlcommand.CommandText = commandText;
			sqlcommand.ExecuteNonQuery(); // để thực thi câu lệnh SQL không trả về kết quả
										  // dữ liệu (ví dụ: INSERT, UPDATE, DELETE).
			CloseConnect();
		}

		//func Load form into panel
		public void openChildForm(Form activeForm, Form formName, Panel panelName)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = formName;
            formName.TopLevel = false;
            formName.FormBorderStyle = FormBorderStyle.None;
            formName.Dock = DockStyle.Top;
            panelName.Controls.Add(formName);
            panelName.Tag = formName;
            formName.BringToFront();
            formName.Show();
        }

    }
}
