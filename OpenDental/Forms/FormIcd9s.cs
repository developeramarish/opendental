using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormIcd9s:Form {
		public bool IsSelectionMode;
		public long SelectedIcd9Num;

		public FormIcd9s() {
			InitializeComponent();
			Lan.F(this);
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormIcd9s_Load(object sender,EventArgs e) {
			if(IsSelectionMode) {
				butClose.Text=Lan.g(this,"Cancel");
			}
			else {
				butOK.Visible=false;
			}
			FillGrid();
		}

		private void FillGrid() {
			ICD9s.RefreshCache();
			listMain.Items.Clear();
			string s;
			for(int i=0;i<ICD9s.Listt.Count;i++) {
				s=ICD9s.Listt[i].Description;
				listMain.Items.Add(s);
			}
		}

		private void listMain_DoubleClick(object sender,System.EventArgs e) {
			if(listMain.SelectedIndex==-1) {
				return;
			}
			if(IsSelectionMode) {
				SelectedIcd9Num=ICD9s.Listt[listMain.SelectedIndex].ICD9Num;
				DialogResult=DialogResult.OK;
				return;
			}
			FormIcd9Edit FormI=new FormIcd9Edit(ICD9s.Listt[listMain.SelectedIndex]);
			FormI.ShowDialog();
			if(FormI.DialogResult!=DialogResult.OK)
				return;
			FillGrid();
		}

	}
}