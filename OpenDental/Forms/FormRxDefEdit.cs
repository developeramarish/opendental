using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class FormRxDefEdit : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.TextBox textDrug;
		private System.Windows.Forms.TextBox textNotes;
		private System.Windows.Forms.TextBox textRefills;
		private System.Windows.Forms.TextBox textDisp;
		private System.Windows.Forms.TextBox textSig;
		private System.ComponentModel.Container components = null;
		private Label label2;
		private ListBox listAlerts;
		private OpenDental.UI.Button butDelete;
		private Label label7;// Required designer variable.
		///<summary></summary>
		public bool IsNew;
		private RxDef RxDefCur;
		private CheckBox checkControlled;
		private UI.Button butAddAllergy;
		private Label labelRxNorm;
		private UI.Button butRxNormSelect;
		private TextBox textRxCui;
		private List<RxAlert> RxAlertList;

		///<summary>Must have already saved it to db so that we have a RxDefNum to work with.</summary>
		public FormRxDefEdit(RxDef rxDefCur){
			InitializeComponent();// Required for Windows Form Designer support
			Lan.F(this);
			RxDefCur=rxDefCur.Copy();
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRxDefEdit));
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textDrug = new System.Windows.Forms.TextBox();
			this.textNotes = new System.Windows.Forms.TextBox();
			this.textRefills = new System.Windows.Forms.TextBox();
			this.textDisp = new System.Windows.Forms.TextBox();
			this.textSig = new System.Windows.Forms.TextBox();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.listAlerts = new System.Windows.Forms.ListBox();
			this.butDelete = new OpenDental.UI.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.checkControlled = new System.Windows.Forms.CheckBox();
			this.butAddAllergy = new OpenDental.UI.Button();
			this.labelRxNorm = new System.Windows.Forms.Label();
			this.butRxNormSelect = new OpenDental.UI.Button();
			this.textRxCui = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(52, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 14);
			this.label1.TabIndex = 0;
			this.label1.Text = "Drug";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(20, 203);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(114, 37);
			this.label3.TabIndex = 2;
			this.label3.Text = "Notes (will not show on printout)";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(46, 150);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 14);
			this.label4.TabIndex = 3;
			this.label4.Text = "Refills";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(46, 124);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(88, 14);
			this.label5.TabIndex = 4;
			this.label5.Text = "Disp";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(56, 74);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(78, 14);
			this.label6.TabIndex = 5;
			this.label6.Text = "Sig";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDrug
			// 
			this.textDrug.Location = new System.Drawing.Point(134, 24);
			this.textDrug.Name = "textDrug";
			this.textDrug.Size = new System.Drawing.Size(254, 20);
			this.textDrug.TabIndex = 0;
			// 
			// textNotes
			// 
			this.textNotes.AcceptsReturn = true;
			this.textNotes.Location = new System.Drawing.Point(134, 199);
			this.textNotes.Multiline = true;
			this.textNotes.Name = "textNotes";
			this.textNotes.Size = new System.Drawing.Size(386, 92);
			this.textNotes.TabIndex = 4;
			// 
			// textRefills
			// 
			this.textRefills.Location = new System.Drawing.Point(134, 146);
			this.textRefills.Name = "textRefills";
			this.textRefills.Size = new System.Drawing.Size(114, 20);
			this.textRefills.TabIndex = 3;
			// 
			// textDisp
			// 
			this.textDisp.Location = new System.Drawing.Point(134, 120);
			this.textDisp.Name = "textDisp";
			this.textDisp.Size = new System.Drawing.Size(114, 20);
			this.textDisp.TabIndex = 2;
			// 
			// textSig
			// 
			this.textSig.AcceptsReturn = true;
			this.textSig.Location = new System.Drawing.Point(134, 70);
			this.textSig.Multiline = true;
			this.textSig.Name = "textSig";
			this.textSig.Size = new System.Drawing.Size(254, 44);
			this.textSig.TabIndex = 1;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(542, 420);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 5;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(542, 460);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 6;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(50, 305);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(84, 14);
			this.label2.TabIndex = 7;
			this.label2.Text = "Alerts";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listAlerts
			// 
			this.listAlerts.FormattingEnabled = true;
			this.listAlerts.Location = new System.Drawing.Point(134, 304);
			this.listAlerts.Name = "listAlerts";
			this.listAlerts.Size = new System.Drawing.Size(120, 95);
			this.listAlerts.TabIndex = 8;
			this.listAlerts.DoubleClick += new System.EventHandler(this.listAlerts_DoubleClick);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(36, 460);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(84, 24);
			this.butDelete.TabIndex = 16;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(132, 460);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(260, 26);
			this.label7.TabIndex = 17;
			this.label7.Text = "This does not damage any patient records";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// checkControlled
			// 
			this.checkControlled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkControlled.Location = new System.Drawing.Point(3, 47);
			this.checkControlled.Name = "checkControlled";
			this.checkControlled.Size = new System.Drawing.Size(145, 20);
			this.checkControlled.TabIndex = 19;
			this.checkControlled.Text = "Controlled Substance";
			this.checkControlled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkControlled.UseVisualStyleBackColor = true;
			// 
			// butAddAllergy
			// 
			this.butAddAllergy.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAddAllergy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAddAllergy.Autosize = true;
			this.butAddAllergy.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddAllergy.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddAllergy.CornerRadius = 4F;
			this.butAddAllergy.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddAllergy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddAllergy.Location = new System.Drawing.Point(260, 304);
			this.butAddAllergy.Name = "butAddAllergy";
			this.butAddAllergy.Size = new System.Drawing.Size(78, 24);
			this.butAddAllergy.TabIndex = 20;
			this.butAddAllergy.Text = "Add";
			this.butAddAllergy.Click += new System.EventHandler(this.butAddAllergy_Click);
			// 
			// labelRxNorm
			// 
			this.labelRxNorm.Location = new System.Drawing.Point(46, 176);
			this.labelRxNorm.Name = "labelRxNorm";
			this.labelRxNorm.Size = new System.Drawing.Size(88, 14);
			this.labelRxNorm.TabIndex = 21;
			this.labelRxNorm.Text = "RxNorm";
			this.labelRxNorm.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butRxNormSelect
			// 
			this.butRxNormSelect.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRxNormSelect.Autosize = true;
			this.butRxNormSelect.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRxNormSelect.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRxNormSelect.CornerRadius = 4F;
			this.butRxNormSelect.Location = new System.Drawing.Point(498, 172);
			this.butRxNormSelect.Name = "butRxNormSelect";
			this.butRxNormSelect.Size = new System.Drawing.Size(22, 22);
			this.butRxNormSelect.TabIndex = 260;
			this.butRxNormSelect.Text = "...";
			this.butRxNormSelect.Click += new System.EventHandler(this.butRxNormSelect_Click);
			// 
			// textRxCui
			// 
			this.textRxCui.Location = new System.Drawing.Point(134, 173);
			this.textRxCui.Name = "textRxCui";
			this.textRxCui.ReadOnly = true;
			this.textRxCui.Size = new System.Drawing.Size(358, 20);
			this.textRxCui.TabIndex = 261;
			// 
			// FormRxDefEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(634, 496);
			this.Controls.Add(this.textRxCui);
			this.Controls.Add(this.butRxNormSelect);
			this.Controls.Add(this.labelRxNorm);
			this.Controls.Add(this.butAddAllergy);
			this.Controls.Add(this.checkControlled);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.listAlerts);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.textSig);
			this.Controls.Add(this.textDisp);
			this.Controls.Add(this.textRefills);
			this.Controls.Add(this.textNotes);
			this.Controls.Add(this.textDrug);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRxDefEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Rx Template";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormRxDefEdit_FormClosing);
			this.Load += new System.EventHandler(this.FormRxDefEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormRxDefEdit_Load(object sender, System.EventArgs e) {
			textDrug.Text=RxDefCur.Drug;
			textSig.Text=RxDefCur.Sig;
			textDisp.Text=RxDefCur.Disp;
			textRefills.Text=RxDefCur.Refills;
			textNotes.Text=RxDefCur.Notes;
			checkControlled.Checked=RxDefCur.IsControlled;
			FillAlerts();
			FillRxCui();
		}

		private void FillRxCui() {
			if(RxDefCur.RxCui==0) {
				textRxCui.Text="";
			}
			else {
				textRxCui.Text=RxDefCur.RxCui.ToString()+" - "+RxNorms.GetDescByRxCui(RxDefCur.RxCui.ToString());
			}
		}

		private void FillAlerts(){
			RxAlertList=RxAlerts.Refresh(RxDefCur.RxDefNum);
			listAlerts.Items.Clear();
			for(int i=0;i<RxAlertList.Count;i++) {
				listAlerts.Items.Add(AllergyDefs.GetOne(RxAlertList[i].AllergyDefNum).Description);
			}
		}

		private void listAlerts_DoubleClick(object sender,EventArgs e) {
			if(listAlerts.SelectedIndex<0) {
				MsgBox.Show(this,"Select at least one Alert.");
				return;
			}
			FormRxAlertEdit FormRAE=new FormRxAlertEdit(RxAlertList[listAlerts.SelectedIndex],RxDefCur);
			FormRAE.ShowDialog();
			FillAlerts();
		}

		private void butAddAllergy_Click(object sender,EventArgs e) {
			FormAllergySetup FormAS=new FormAllergySetup();
			FormAS.IsSelectionMode=true;
			FormAS.ShowDialog();
			if(FormAS.DialogResult!=DialogResult.OK) {
				return;
			}
			RxAlert alert=new RxAlert();
			alert.AllergyDefNum=FormAS.SelectedAllergyDefNum;
			alert.RxDefNum=RxDefCur.RxDefNum;
			RxAlerts.Insert(alert);
			FillAlerts();
		}

		private void butRxNormSelect_Click(object sender,EventArgs e) {
			FormRxNorms FormRN=new FormRxNorms();
			FormRN.IsSelectionMode=true;
			FormRN.ShowDialog();
			if(FormRN.DialogResult!=DialogResult.OK) {
				return;
			}
			RxDefCur.RxCui=PIn.Long(FormRN.SelectedRxNorm.RxCui);
			FillRxCui();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,true,"Delete this prescription template?")){
				return;
			}
			RxDefs.Delete(RxDefCur);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			//RxCui is set when butRxNormSelect is clicked.
			RxDefCur.Drug=textDrug.Text;
			RxDefCur.Sig=textSig.Text;
			RxDefCur.Disp=textDisp.Text;
			RxDefCur.Refills=textRefills.Text;
			RxDefCur.Notes=textNotes.Text;
			RxDefCur.IsControlled=checkControlled.Checked;
			RxDefs.Update(RxDefCur);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormRxDefEdit_FormClosing(object sender,FormClosingEventArgs e) {
			if(DialogResult==DialogResult.OK){
				return;//close as normal
			}
			if(IsNew){
				RxDefs.Delete(RxDefCur);
			}
		}

		

		

		

		
	}
}
