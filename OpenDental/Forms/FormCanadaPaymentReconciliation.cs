using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.Eclaims;

namespace OpenDental {
	public partial class FormCanadaPaymentReconciliation:Form {

		List<Carrier> carriers=new List<Carrier>();

		public FormCanadaPaymentReconciliation() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormCanadaPaymentReconciliation_Load(object sender,EventArgs e) {
			for(int i=0;i<CanadianNetworks.Listt.Count;i++) {
				listNetworks.Items.Add(CanadianNetworks.Listt[i].Abbrev+" - "+CanadianNetworks.Listt[i].Descript);
			}
			for(int i=0;i<Carriers.Listt.Length;i++) {
				if(Carriers.Listt[i].CDAnetVersion!="02" &&//This transaction does not exist in version 02.
					(Carriers.Listt[i].CanadianSupportedTypes&CanSupTransTypes.RequestForPaymentReconciliation_06)==CanSupTransTypes.RequestForPaymentReconciliation_06) {
					carriers.Add(Carriers.Listt[i]);
					listCarriers.Items.Add(Carriers.Listt[i].CarrierName);
				}
			}
			long defaultProvNum=PrefC.GetLong(PrefName.PracticeDefaultProv);
			for(int i=0;i<ProviderC.List.Length;i++) {
				listBillingProvider.Items.Add(ProviderC.List[i].Abbr);
				listTreatingProvider.Items.Add(ProviderC.List[i].Abbr);
				if(ProviderC.List[i].ProvNum==defaultProvNum) {
					listBillingProvider.SelectedIndex=i;
					listTreatingProvider.SelectedIndex=i;
				}
			}
			textDateReconciliation.Text=DateTime.Today.ToShortDateString();
		}

		private void listCarriers_Click(object sender,EventArgs e) {
			listNetworks.SelectedIndex=-1;
		}

		private void listNetwork_Click(object sender,EventArgs e) {
			listCarriers.SelectedIndex=-1;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(listCarriers.SelectedIndex<0 && listNetworks.SelectedIndex<0) {
				MsgBox.Show(this,"You must first choose one carrier or one network.");
				return;
			}
			if(listBillingProvider.SelectedIndex<0) {
				MsgBox.Show(this,"You must first choose a billing provider.");
				return;
			}
			if(listTreatingProvider.SelectedIndex<0) {
				MsgBox.Show(this,"You must first choose a treating provider.");
				return;
			}
			DateTime reconciliationDate;
			try {
				reconciliationDate=DateTime.Parse(textDateReconciliation.Text).Date;
			}
			catch {
				MsgBox.Show(this,"Reconciliation date invalid.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			try {
				if(listCarriers.SelectedIndex>=0) {
					CanadianOutput.GetPaymentReconciliations(carriers[listCarriers.SelectedIndex],null,ProviderC.List[listTreatingProvider.SelectedIndex],
						ProviderC.List[listBillingProvider.SelectedIndex],reconciliationDate);
				}
				else {
					CanadianOutput.GetPaymentReconciliations(null,CanadianNetworks.Listt[listNetworks.SelectedIndex],ProviderC.List[listTreatingProvider.SelectedIndex],
						ProviderC.List[listBillingProvider.SelectedIndex],reconciliationDate);
				}
				Cursor=Cursors.Default;
				MsgBox.Show(this,"Done.");
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(Lan.g(this,"Request failed: ")+ex.Message);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}