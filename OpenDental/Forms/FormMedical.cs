using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormMedical : System.Windows.Forms.Form{
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private OpenDental.UI.Button butAdd;
		private OpenDental.ODtextBox textMedical;
		private OpenDental.ODtextBox textService;
		private OpenDental.ODtextBox textMedicalComp;
		private OpenDental.ODtextBox textMedUrgNote;
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butAddDisease;// Required designer variable.
		private Patient PatCur;
		private OpenDental.UI.ODGrid gridMeds;
		private OpenDental.UI.ODGrid gridDiseases;
		private CheckBox checkPremed;
		private List<Disease> DiseaseList;
		private UI.Button butIcd9;
		private CheckBox checkDiscontinued;
		private ODGrid gridAllergies;
		private UI.Button butAddAllergy;
		private PatientNote PatientNoteCur;
		private CheckBox checkShowInactiveAllergies;
		private List<Allergy> allergyList; 

		///<summary></summary>
		public FormMedical(PatientNote patientNoteCur,Patient patCur){
			InitializeComponent();// Required for Windows Form Designer support
			PatCur=patCur;
			PatientNoteCur=patientNoteCur;
			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMedical));
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textMedUrgNote = new OpenDental.ODtextBox();
			this.textService = new OpenDental.ODtextBox();
			this.textMedical = new OpenDental.ODtextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.butAdd = new OpenDental.UI.Button();
			this.textMedicalComp = new OpenDental.ODtextBox();
			this.butAddDisease = new OpenDental.UI.Button();
			this.gridMeds = new OpenDental.UI.ODGrid();
			this.gridDiseases = new OpenDental.UI.ODGrid();
			this.checkPremed = new System.Windows.Forms.CheckBox();
			this.butIcd9 = new OpenDental.UI.Button();
			this.checkDiscontinued = new System.Windows.Forms.CheckBox();
			this.gridAllergies = new OpenDental.UI.ODGrid();
			this.butAddAllergy = new OpenDental.UI.Button();
			this.checkShowInactiveAllergies = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(786,650);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,25);
			this.butOK.TabIndex = 0;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(879,650);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,25);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textMedUrgNote
			// 
			this.textMedUrgNote.AcceptsReturn = true;
			this.textMedUrgNote.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
			this.textMedUrgNote.ForeColor = System.Drawing.Color.Red;
			this.textMedUrgNote.Location = new System.Drawing.Point(115,447);
			this.textMedUrgNote.Multiline = true;
			this.textMedUrgNote.Name = "textMedUrgNote";
			this.textMedUrgNote.QuickPasteType = OpenDentBusiness.QuickPasteType.MedicalUrgent;
			this.textMedUrgNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textMedUrgNote.Size = new System.Drawing.Size(252,33);
			this.textMedUrgNote.TabIndex = 53;
			// 
			// textService
			// 
			this.textService.AcceptsReturn = true;
			this.textService.Location = new System.Drawing.Point(115,558);
			this.textService.Multiline = true;
			this.textService.Name = "textService";
			this.textService.QuickPasteType = OpenDentBusiness.QuickPasteType.ServiceNotes;
			this.textService.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textService.Size = new System.Drawing.Size(252,83);
			this.textService.TabIndex = 52;
			// 
			// textMedical
			// 
			this.textMedical.AcceptsReturn = true;
			this.textMedical.Location = new System.Drawing.Point(115,482);
			this.textMedical.Multiline = true;
			this.textMedical.Name = "textMedical";
			this.textMedical.QuickPasteType = OpenDentBusiness.QuickPasteType.MedicalSummary;
			this.textMedical.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textMedical.Size = new System.Drawing.Size(252,74);
			this.textMedical.TabIndex = 51;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(6,559);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(106,16);
			this.label3.TabIndex = 50;
			this.label3.Text = "Service Notes";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6,445);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(106,21);
			this.label2.TabIndex = 49;
			this.label2.Text = "Med Urgent";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(6,483);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(106,17);
			this.label4.TabIndex = 47;
			this.label4.Text = "Medical Summary";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(371,242);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(421,18);
			this.label6.TabIndex = 6;
			this.label6.Text = "Medical History - Complete and Detailed (does not show in chart)";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,1);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(374,1);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(123,23);
			this.butAdd.TabIndex = 51;
			this.butAdd.Text = "&Add Medication";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// textMedicalComp
			// 
			this.textMedicalComp.AcceptsReturn = true;
			this.textMedicalComp.Location = new System.Drawing.Point(374,262);
			this.textMedicalComp.Multiline = true;
			this.textMedicalComp.Name = "textMedicalComp";
			this.textMedicalComp.QuickPasteType = OpenDentBusiness.QuickPasteType.MedicalHistory;
			this.textMedicalComp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textMedicalComp.Size = new System.Drawing.Size(585,379);
			this.textMedicalComp.TabIndex = 54;
			// 
			// butAddDisease
			// 
			this.butAddDisease.AdjustImageLocation = new System.Drawing.Point(0,1);
			this.butAddDisease.Autosize = true;
			this.butAddDisease.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddDisease.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddDisease.CornerRadius = 4F;
			this.butAddDisease.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddDisease.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddDisease.Location = new System.Drawing.Point(5,1);
			this.butAddDisease.Name = "butAddDisease";
			this.butAddDisease.Size = new System.Drawing.Size(98,23);
			this.butAddDisease.TabIndex = 58;
			this.butAddDisease.Text = "Add Problem";
			this.butAddDisease.Click += new System.EventHandler(this.butAddProblem_Click);
			// 
			// gridMeds
			// 
			this.gridMeds.HScrollVisible = false;
			this.gridMeds.Location = new System.Drawing.Point(374,26);
			this.gridMeds.Name = "gridMeds";
			this.gridMeds.ScrollValue = 0;
			this.gridMeds.Size = new System.Drawing.Size(585,216);
			this.gridMeds.TabIndex = 59;
			this.gridMeds.Title = "Medications";
			this.gridMeds.TranslationName = "TableMedications";
			this.gridMeds.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMeds_CellDoubleClick);
			// 
			// gridDiseases
			// 
			this.gridDiseases.HScrollVisible = false;
			this.gridDiseases.Location = new System.Drawing.Point(4,26);
			this.gridDiseases.Name = "gridDiseases";
			this.gridDiseases.ScrollValue = 0;
			this.gridDiseases.Size = new System.Drawing.Size(363,216);
			this.gridDiseases.TabIndex = 60;
			this.gridDiseases.Title = "Problems";
			this.gridDiseases.TranslationName = "TableDiseases";
			this.gridDiseases.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridDiseases_CellDoubleClick);
			// 
			// checkPremed
			// 
			this.checkPremed.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkPremed.Location = new System.Drawing.Point(6,407);
			this.checkPremed.Name = "checkPremed";
			this.checkPremed.Size = new System.Drawing.Size(123,35);
			this.checkPremed.TabIndex = 61;
			this.checkPremed.Text = "Premedicate (PAC or other)";
			this.checkPremed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkPremed.UseVisualStyleBackColor = true;
			// 
			// butIcd9
			// 
			this.butIcd9.AdjustImageLocation = new System.Drawing.Point(0,1);
			this.butIcd9.Autosize = true;
			this.butIcd9.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butIcd9.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butIcd9.CornerRadius = 4F;
			this.butIcd9.Image = global::OpenDental.Properties.Resources.Add;
			this.butIcd9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butIcd9.Location = new System.Drawing.Point(129,1);
			this.butIcd9.Name = "butIcd9";
			this.butIcd9.Size = new System.Drawing.Size(98,23);
			this.butIcd9.TabIndex = 62;
			this.butIcd9.Text = "Add ICD9";
			this.butIcd9.Click += new System.EventHandler(this.butIcd9_Click);
			// 
			// checkDiscontinued
			// 
			this.checkDiscontinued.Location = new System.Drawing.Point(514,2);
			this.checkDiscontinued.Name = "checkDiscontinued";
			this.checkDiscontinued.Size = new System.Drawing.Size(201,23);
			this.checkDiscontinued.TabIndex = 61;
			this.checkDiscontinued.Tag = "";
			this.checkDiscontinued.Text = "Show Discontinued Medications";
			this.checkDiscontinued.UseVisualStyleBackColor = true;
			this.checkDiscontinued.MouseUp += new System.Windows.Forms.MouseEventHandler(this.checkShowDiscontinuedMeds_MouseUp);
			// 
			// gridAllergies
			// 
			this.gridAllergies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridAllergies.HScrollVisible = false;
			this.gridAllergies.Location = new System.Drawing.Point(4,278);
			this.gridAllergies.Name = "gridAllergies";
			this.gridAllergies.ScrollValue = 0;
			this.gridAllergies.Size = new System.Drawing.Size(363,123);
			this.gridAllergies.TabIndex = 63;
			this.gridAllergies.Title = "Allergies";
			this.gridAllergies.TranslationName = "TableDiseases";
			this.gridAllergies.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridAllergies_CellDoubleClick);
			// 
			// butAddAllergy
			// 
			this.butAddAllergy.AdjustImageLocation = new System.Drawing.Point(0,1);
			this.butAddAllergy.Autosize = true;
			this.butAddAllergy.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddAllergy.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddAllergy.CornerRadius = 4F;
			this.butAddAllergy.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddAllergy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddAllergy.Location = new System.Drawing.Point(5,249);
			this.butAddAllergy.Name = "butAddAllergy";
			this.butAddAllergy.Size = new System.Drawing.Size(98,23);
			this.butAddAllergy.TabIndex = 64;
			this.butAddAllergy.Text = "Add Allergy";
			this.butAddAllergy.Click += new System.EventHandler(this.butAddAllergy_Click);
			// 
			// checkShowInactiveAllergies
			// 
			this.checkShowInactiveAllergies.Location = new System.Drawing.Point(129,250);
			this.checkShowInactiveAllergies.Name = "checkShowInactiveAllergies";
			this.checkShowInactiveAllergies.Size = new System.Drawing.Size(201,23);
			this.checkShowInactiveAllergies.TabIndex = 65;
			this.checkShowInactiveAllergies.Tag = "";
			this.checkShowInactiveAllergies.Text = "Show Inactive Allergies";
			this.checkShowInactiveAllergies.UseVisualStyleBackColor = true;
			this.checkShowInactiveAllergies.CheckedChanged += new System.EventHandler(this.checkShowInactiveAllergies_CheckedChanged);
			// 
			// FormMedical
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(964,683);
			this.Controls.Add(this.checkShowInactiveAllergies);
			this.Controls.Add(this.butAddAllergy);
			this.Controls.Add(this.gridAllergies);
			this.Controls.Add(this.butIcd9);
			this.Controls.Add(this.checkDiscontinued);
			this.Controls.Add(this.checkPremed);
			this.Controls.Add(this.gridDiseases);
			this.Controls.Add(this.gridMeds);
			this.Controls.Add(this.butAddDisease);
			this.Controls.Add(this.textMedUrgNote);
			this.Controls.Add(this.textService);
			this.Controls.Add(this.textMedicalComp);
			this.Controls.Add(this.textMedical);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMedical";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Medical";
			this.Load += new System.EventHandler(this.FormMedical_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormMedical_Load(object sender, System.EventArgs e){
			checkPremed.Checked=PatCur.Premed;
			textMedUrgNote.Text=PatCur.MedUrgNote;
			textMedical.Text=PatientNoteCur.Medical;
			textMedicalComp.Text=PatientNoteCur.MedicalComp;
			textService.Text=PatientNoteCur.Service;
			FillMeds();
			FillProblems();
			FillAllergies();
		}

		private void FillMeds(){
			Medications.Refresh();
			MedicationPats.Refresh(PatCur.PatNum);
			gridMeds.BeginUpdate();
			gridMeds.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableMedications","Medication"),120);
			gridMeds.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableMedications","Notes"),205);
			gridMeds.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableMedications","Notes for Patient"),205);
			gridMeds.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableMedications","Disc"),10,HorizontalAlignment.Center);
			gridMeds.Columns.Add(col);
			gridMeds.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<MedicationPats.List.Length;i++){
				if(!checkDiscontinued.Checked && MedicationPats.List[i].IsDiscontinued) {
					continue;
				}
				row=new ODGridRow();
				Medication generic=Medications.GetGeneric(MedicationPats.List[i].MedicationNum);
				string medName=Medications.GetMedication(MedicationPats.List[i].MedicationNum).MedName;
				if(generic.MedicationNum!=MedicationPats.List[i].MedicationNum) {//not generic
					medName+=" ("+generic.MedName+")";
				}
				row.Cells.Add(medName);
				row.Cells.Add(Medications.GetGeneric(MedicationPats.List[i].MedicationNum).Notes);
				row.Cells.Add(MedicationPats.List[i].PatNote);
				row.Cells.Add(MedicationPats.List[i].IsDiscontinued?"X":"");
				gridMeds.Rows.Add(row);
			}
			gridMeds.EndUpdate();
		}

		private void gridMeds_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormMedPat FormMP=new FormMedPat();
			FormMP.MedicationPatCur=MedicationPats.List[e.Row];
			FormMP.ShowDialog();
			FillMeds();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			//select medication from list.  Additional meds can be added to the list from within that dlg
			FormMedications FormM=new FormMedications();
			FormM.SelectMode=true;
			FormM.ShowDialog();
			if(FormM.DialogResult!=DialogResult.OK){
				return;
			}
			MedicationPat MedicationPatCur=new MedicationPat();
			MedicationPatCur.PatNum=PatCur.PatNum;
			MedicationPatCur.MedicationNum=FormM.MedicationNum;
			FormMedPat FormMP=new FormMedPat();
			FormMP.MedicationPatCur=MedicationPatCur;
			FormMP.IsNew=true;
			FormMP.ShowDialog();
			if(FormMP.DialogResult!=DialogResult.OK){
				return;
			}
			FillMeds();
		}

		private void FillProblems(){
			DiseaseList=Diseases.Refresh(PatCur.PatNum);
			gridDiseases.BeginUpdate();
			gridDiseases.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableDiseases","Name"),140);//total is about 325
			gridDiseases.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableDiseases","Patient Note"),145);
			gridDiseases.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableDisease","Status"),40);
			gridDiseases.Columns.Add(col);
			gridDiseases.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<DiseaseList.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(DiseaseDefs.GetName(DiseaseList[i].DiseaseDefNum));
				row.Cells.Add(DiseaseList[i].PatNote);
				row.Cells.Add(DiseaseList[i].ProbStatus.ToString());
				gridDiseases.Rows.Add(row);
			}
			gridDiseases.EndUpdate();
		}

		private void FillAllergies() {
			allergyList=Allergies.GetAll(PatCur.PatNum,checkShowInactiveAllergies.Checked);
			gridAllergies.BeginUpdate();
			gridAllergies.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableAllergies","Allergy"),100);
			gridAllergies.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableAllergies","Reaction"),195);
			gridAllergies.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableAllergies","Active"),60,HorizontalAlignment.Center);
			gridAllergies.Columns.Add(col);
			gridAllergies.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<allergyList.Count;i++){
				row=new ODGridRow();
				AllergyDef allergyDef=AllergyDefs.GetOne(allergyList[i].AllergyDefNum);
				row.Cells.Add(allergyDef.Description);
				row.Cells.Add(allergyList[i].Reaction);
				if(allergyList[i].StatusIsActive) {
					row.Cells.Add("X");
				}
				else {
					row.Cells.Add("");
				}
				gridAllergies.Rows.Add(row);
			}
			gridAllergies.EndUpdate();
		}

		private void butAddProblem_Click(object sender,EventArgs e) {
			Disease disease=new Disease();
			disease.PatNum=PatCur.PatNum;
			//todo: Use FormDiseaseDefs.IsSelectionMode
			FormDiseaseDefs formDD=new FormDiseaseDefs();
			formDD.IsSelectionMode=true;
			formDD.ShowDialog();
			if(formDD.DialogResult!=DialogResult.OK) {
				return;
			}
			disease.DiseaseDefNum=formDD.SelectedDiseaseDefNum;
			Diseases.Insert(disease);
			FillProblems();
		}

		private void butIcd9_Click(object sender,EventArgs e) {
			Disease disease=new Disease();
			disease.PatNum=PatCur.PatNum;
			//todo: Use FormDiseaseDefs.IsSelectionMode
			FormIcd9s formI=new FormIcd9s();
			formI.IsSelectionMode=true;
			formI.ShowDialog();
			if(formI.DialogResult!=DialogResult.OK) {
				return;
			}
			disease.DiseaseDefNum=formI.SelectedIcd9Num;
			Diseases.Insert(disease);
			FillProblems();
		}

		private void gridAllergies_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormAllergyEdit FAE=new FormAllergyEdit(PatCur);
			FAE.AllergyCur=allergyList[gridAllergies.GetSelectedIndex()];
			FAE.ShowDialog();
			FillAllergies();
		}

		private void butAddAllergy_Click(object sender,EventArgs e) {
			FormAllergyEdit FAE=new FormAllergyEdit(PatCur);
			FAE.AllergyCur=new Allergy();
			FAE.AllergyCur.IsNew=true;
			FAE.ShowDialog();
			FillAllergies();
		}
		
		private void checkShowInactiveAllergies_CheckedChanged(object sender,EventArgs e) {
			FillAllergies();
		}

		private void gridDiseases_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormDiseaseEdit FormD=new FormDiseaseEdit(DiseaseList[e.Row]);
			FormD.ShowDialog();
			FillProblems();
		}

		/*
		private void butQuestions_Click(object sender,EventArgs e) {
			FormQuestionnaire FormQ=new FormQuestionnaire(PatCur.PatNum);
			FormQ.ShowDialog();
			if(Questions.PatHasQuest(PatCur.PatNum)) {
				butQuestions.Text=Lan.g(this,"Edit Questionnaire");
			}
			else {
				butQuestions.Text=Lan.g(this,"New Questionnaire");
			}
		}*/

		private void butOK_Click(object sender, System.EventArgs e) {
			Patient PatOld=PatCur.Copy();
			PatCur.Premed=checkPremed.Checked;
			PatCur.MedUrgNote=textMedUrgNote.Text;
			Patients.Update(PatCur,PatOld);
			PatientNoteCur.Medical=textMedical.Text;
			PatientNoteCur.Service=textService.Text;
			PatientNoteCur.MedicalComp=textMedicalComp.Text;
			PatientNotes.Update(PatientNoteCur, PatCur.Guarantor);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void checkShowDiscontinuedMeds_MouseUp(object sender,MouseEventArgs e) {
			FillMeds();
		}



		

	

		

		

		

	

		

	}
}
