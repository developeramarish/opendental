﻿using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace OpenDentBusiness.HL7 {
	public class DFT {
		private MessageHL7 msg;
		private SegmentHL7 seg;

		///<summary>The constructor has all the info necessary to create the Message object.</summary>
		public DFT(Appointment apt,Patient pat) {
			msg=new MessageHL7(MessageType.DFT);
			MSH();
			EVN();
			PID(pat);
			PV1(apt);
			FT1(apt);
			DG1();
		}

		///<summary>The constructor has all the info necessary to create the Message object.</summary>
		public DFT(Appointment apt,Patient pat,string pdfDataBase64){
			msg=new MessageHL7(MessageType.DFT);
			MSH();
			EVN();
			PID(pat);
			PV1(apt);
			FT1(apt);
			DG1();
			ZX1(pdfDataBase64);
		}

		/////<summary>The constructor has all the info necessary to create the Message object.</summary>
		//public DFT(Patient pat,List<Procedure> procs,string pdfDataBase64){
		//  msg=new MessageHL7(MessageType.DFT);
		//  MSH();
		//  EVN();
		//  PID(pat);
		//  long provNum=0;
		//  //Try to get a provider from one of the treatment planned procedures.
		//  for(int i=0;i<procs.Count;i++){
		//    if(procs[i].ProvNum!=0){
		//      provNum=procs[i].ProvNum;
		//      break;
		//    }
		//  }
		//  //If no treatment planned procedures are marked with a provider, then use a default provider.
		//  if(provNum==0){
		//    if(pat.PriProv==0){
		//      provNum=PrefC.GetInt(PrefName.PracticeDefaultProv);
		//    }
		//    else{
		//      provNum=pat.PriProv;
		//    }
		//  }
		//  Provider prov=Providers.GetProv(provNum);
		//  PV1(prov);
		//  FT1(procs);
		//  DG1();
		//  ZX1(pdfDataBase64);
		//}

		///<summary>Message Header Segment</summary>
		private void MSH(){
			seg=new SegmentHL7(@"MSH|^~\&|OD||ECW||"+DateTime.Now.ToString("yyyyMMddHHmmss")+"||DFT^P03||P|2.3");
			msg.Segments.Add(seg);
		}

		///<summary>Event type segment.</summary>
		private void EVN(){
			seg=new SegmentHL7("EVN|P03|"+DateTime.Now.ToString("yyyyMMddHHmmss")+"|");
			msg.Segments.Add(seg);
		}

		///<summary>Patient identification.</summary>
		private void PID(Patient pat){
			seg=new SegmentHL7(SegmentName.PID);
			seg.SetField(0,"PID");
			seg.SetField(1,"1");
			seg.SetField(2,pat.ChartNumber);//Account number.  eCW requires this to be the same # as came in on PID.4.
			seg.SetField(3,pat.PatNum.ToString());//??what is this MRN?
			seg.SetField(5,pat.LName,pat.FName,pat.MiddleI);
			//we assume that dob is always valid because eCW should always pass us a dob.
			seg.SetField(7,pat.Birthdate.ToString("yyyyMMdd"));
			seg.SetField(8,ConvertGender(pat.Gender));
			seg.SetField(10,ConvertRace(pat.Race));
			seg.SetField(11,pat.Address,pat.Address2,pat.City,pat.State,pat.Zip);
			seg.SetField(13,ConvertPhone(pat.HmPhone));
			seg.SetField(14,ConvertPhone(pat.WkPhone));
			seg.SetField(16,ConvertMaritalStatus(pat.Position));
			seg.SetField(19,pat.SSN);
			msg.Segments.Add(seg);
		}

		/////<summary>Patient visit.</summary>
		//private void PV1(Provider prov){
		//  seg=new SegmentHL7(SegmentName.PV1);
		//  seg.SetField(0,"PV1");
		//  seg.SetField(7,prov.Abbr,prov.LName,prov.FName,prov.MI);
		//  msg.Segments.Add(seg);
		//}

		///<summary>Patient visit.</summary>
		private void PV1(Appointment apt){
			seg=new SegmentHL7(SegmentName.PV1);
			seg.SetField(0,"PV1");
			Provider prov=Providers.GetProv(apt.ProvNum);
			seg.SetField(7,prov.Abbr,prov.LName,prov.FName,prov.MI);
			seg.SetField(19,apt.AptNum.ToString());
			msg.Segments.Add(seg);
		}

		///<summary>Financial transaction segment.</summary>
		private void FT1(Appointment apt){
			List<Procedure> procs=Procedures.GetProcsForSingle(apt.AptNum,false);
			ProcedureCode procCode;
			for(int i=0;i<procs.Count;i++) {
				seg=new SegmentHL7(SegmentName.FT1);
				seg.SetField(0,"FT1");
				seg.SetField(1,(i+1).ToString());
				seg.SetField(4,procs[i].ProcDate.ToString("yyyyMMddHHmmss"));
				seg.SetField(5,procs[i].ProcDate.ToString("yyyyMMddHHmmss"));
				seg.SetField(6,"CG");
				seg.SetField(10,"1.0");
				seg.SetField(16,"");//location code and description???
				seg.SetField(19,procs[i].DiagnosticCode);
				Provider prov=Providers.GetProv(procs[i].ProvNum);
				seg.SetField(20,prov.Abbr,prov.LName,prov.FName,prov.MI);//performed by provider.
				seg.SetField(21,prov.Abbr,prov.LName,prov.FName,prov.MI);//ordering provider.
				seg.SetField(22,procs[i].ProcFee.ToString("F2"));
				procCode=ProcedureCodes.GetProcCode(procs[i].CodeNum);
				seg.SetField(25,procCode.ProcCode);
				if(procCode.TreatArea==TreatmentArea.ToothRange){
					seg.SetField(26,procs[i].ToothRange,"");
				}
				else if(procCode.TreatArea==TreatmentArea.Surf){//probably not necessary
					seg.SetField(26,Tooth.ToInternat(procs[i].ToothNum),Tooth.SurfTidyForClaims(procs[i].Surf,procs[i].ToothNum));
				}
				else{
					seg.SetField(26,Tooth.ToInternat(procs[i].ToothNum),procs[i].Surf);
				}
				msg.Segments.Add(seg);
			}
		}

		/////<summary>Financial transaction segment.</summary>
		//private void FT1(TreatPlan treatplan,Patient pat,Provider prov){
		//  ProcTP[] allPatTpProcs=ProcTPs.Refresh(pat.PatNum);
		//  ProcTP[] procsForTp=ProcTPs.GetListForTP(treatplan.TreatPlanNum,allPatTpProcs);
		//  for(int i=0;i<procsForTp.Length;i++) {
		//    seg=new SegmentHL7(SegmentName.FT1);
		//    seg.SetField(0,"FT1");
		//    seg.SetField(1,(i+1).ToString());
		//    seg.SetField(4,treatplan.DateTP.ToString("yyyyMMddHHmmss"));
		//    seg.SetField(5,treatplan.DateTP.ToString("yyyyMMddHHmmss"));
		//    seg.SetField(6,"CG");
		//    seg.SetField(10,"1.0");
		//    seg.SetField(16,"");//location code and description???
		//    seg.SetField(20,prov.Abbr,prov.LName,prov.FName,prov.MI);//performed by provider.
		//    seg.SetField(21,prov.Abbr,prov.LName,prov.FName,prov.MI);//ordering provider.
		//    seg.SetField(22,procsForTp[i].FeeAmt.ToString("F2"));
		//    seg.SetField(25,procsForTp[i].ProcCode);
		//    seg.SetField(26,Tooth.ToInternat(procsForTp[i].ToothNumTP),procsForTp[i].Surf);
		//    msg.Segments.Add(seg);
		//  }
		//}

		///<summary>Diagnosis segment. Optional.</summary>
		private void DG1(){
			//DG1 optional, so we'll skip for now---------------------------------
			//seg=new SegmentHL7(SegmentName.DG1);
			//msg.Segments.Add(seg);
		}

		///<summary>PDF data segment.</summary>
		private void ZX1(string pdfDataBase64){
			seg=new SegmentHL7(SegmentName.ZX1);
			seg.SetField(0,"ZX1");
			seg.SetField(1,"6");
			seg.SetField(2,"PDF");
			seg.SetField(3,"PATHOLOGY^Pathology Report^L");
			seg.SetField(4,"");
			seg.SetField(5,pdfDataBase64);
			msg.Segments.Add(seg);
		}

		public string GenerateMessage() {
			return msg.ToString();
		}

		private string ConvertGender(PatientGender gender){
			if(gender==PatientGender.Female) {
				return "F";
			}
			if(gender==PatientGender.Male) {
				return "M";
			}
			return "U";
		}

		private string ConvertRace(PatientRace race) {
			switch(race) {
				case PatientRace.AmericanIndian:
					return "American Indian Or Alaska Native";
				case PatientRace.Asian:
					return "Asian";
				case PatientRace.HawaiiOrPacIsland:
					return "Native Hawaiian or Other Pacific";
				case PatientRace.AfricanAmerican:
					return "Black or African American";
				case PatientRace.White:
					return "White";
				case PatientRace.HispanicLatino:
					return "Hispanic";
				case PatientRace.Other:
					return "Other Race";
				default:
					return "Other Race";
			}
		}

		private string ConvertPhone(string phone) {
			string retVal="";
			for(int i=0;i<phone.Length;i++){
				if(Char.IsNumber(phone,i)){
					if(retVal=="" && phone.Substring(i,1)=="1"){
						continue;//skip leading 1.
					}
					retVal+=phone.Substring(i,1);
				}
				if(retVal.Length==10){
					return retVal;
				}
			}
			//never made it to 10
			return "";
		}

		private string ConvertMaritalStatus(PatientPosition patpos) {
			switch(patpos){
				case PatientPosition.Single:
					return "Single";
				case PatientPosition.Married:
					return "Married";
				case PatientPosition.Divorced:
					return "Divorced";
				case PatientPosition.Widowed:
					return "Widowed";
				case PatientPosition.Child:
					return "Single";
				default:
					return "Single";
			}
		}






	}
}
