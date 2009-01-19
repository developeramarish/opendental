using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;

namespace OpenDentBusiness {
	public class AutoNoteControls {
		/// <summary>A list of all the Prompts</summary>
		public static List<AutoNoteControl> Listt;

		/// <summary>A list with all the control settings</summary>
		public static void Refresh() {
			string command = "SELECT * FROM autonotecontrol ORDER BY Descript";
			Listt=RefreshAndFill(command);
		}

		private static List<AutoNoteControl> RefreshAndFill(string command) {
			DataTable table = General.GetTable(command);
			List<AutoNoteControl> retVal=new List<AutoNoteControl>();
			AutoNoteControl noteCont;
			for (int i=0;i<table.Rows.Count;i++){
				noteCont = new AutoNoteControl();
				noteCont.AutoNoteControlNum = PIn.PInt(table.Rows[i][0].ToString());
				noteCont.Descript = PIn.PString(table.Rows[i]["Descript"].ToString());
				noteCont.ControlType = PIn.PString(table.Rows[i]["ControlType"].ToString());
				noteCont.ControlLabel =PIn.PString(table.Rows[i]["ControlLabel"].ToString());
				noteCont.ControlOptions = PIn.PString(table.Rows[i]["ControlOptions"].ToString());
				retVal.Add(noteCont);
			}
			return retVal;
		}

		public static void Insert(AutoNoteControl autonotecontrol) {
			string command = "INSERT INTO autonotecontrol (Descript,ControlType,ControlLabel,ControlOptions)"
			+"VALUES ("			
			+"'"+POut.PString(autonotecontrol.Descript)+"', " 
			+"'"+POut.PString(autonotecontrol.ControlType)+"', "
			+"'"+POut.PString(autonotecontrol.ControlLabel)+"' ,"			
			+"'"+POut.PString(autonotecontrol.ControlOptions)+"')";
			General.NonQ(command);
		}


		public static void Update(AutoNoteControl autonotecontrol) {
			string command="UPDATE autonotecontrol SET "
				+"ControlType = '"+POut.PString(autonotecontrol.ControlType)+"', "
				+"Descript = '"+POut.PString(autonotecontrol.Descript)+"', "
				+"ControlLabel = '"+POut.PString(autonotecontrol.ControlLabel)+"', "
				+"ControlOptions = '"+POut.PString(autonotecontrol.ControlOptions)+"' "
				+"WHERE AutoNoteControlNum = '"+POut.PInt(autonotecontrol.AutoNoteControlNum)+"'";
			General.NonQ(command);
		}

		public static void Delete(int autoNoteControlNum) {
			//no validation for now.
			string command="DELETE FROM autonotecontrol WHERE AutoNoteControlNum="+POut.PInt(autoNoteControlNum);
			General.NonQ(command);
		}

		/*
		public static List<AutoNoteControl> GetForKeys(List<int> keys) {
			List<AutoNoteControl> retVal=new List<AutoNoteControl>();
			for(int k=0;k<keys.Count;k++) {
				for(int i=0;i<Listt.Count;i++) {
					if(Listt[i].AutoNoteControlNum==keys[k]) {
						retVal.Add(Listt[i]);
						break;
					}
					//if it can't find the controlNum, it fails silently.
				}
			}
			return retVal;
		}*/

		/*
		/// <summary>Returns all the control info about the selected control</summary>
		public static void RefreshControl(string ControlNumToShow) {
			string command = "SELECT * FROM autonotecontrol "
				+"WHERE AutoNoteControlNum = '"+ControlNumToShow+"'";
			DataTable table = General.GetTable(command);
			Listt=new List<AutoNoteControl>();
			//List = new AutoNote[table.Rows.Count];
			AutoNoteControl noteCont;
			noteCont = new AutoNoteControl();
			for (int i = 0; i < table.Rows.Count; i++) {
				noteCont.AutoNoteControlNum = PIn.PInt(table.Rows[i][0].ToString());
				noteCont.Descript = PIn.PString(table.Rows[i]["Descript"].ToString());
				noteCont.ControlType = PIn.PString(table.Rows[i]["ControlType"].ToString());
				noteCont.ControlLabel = PIn.PString(table.Rows[i]["ControlLabel"].ToString());
				noteCont.PrefaceText = PIn.PString(table.Rows[i]["PrefaceText"].ToString());
				noteCont.MultiLineText = PIn.PString(table.Rows[i]["MultiLineText"].ToString());
				noteCont.ControlOptions = PIn.PString(table.Rows[i]["ControlOptions"].ToString());
				Listt.Add(noteCont);
			}
		}*/

		/*
		///<summary>Converts the Num of the control to it's Name.
		public static List<AutoNoteControl> ControlNumToName(string controlNum) {
			string command="SELECT Descript FROM autonotecontrol "
			+"WHERE AutoNoteControlNum = "+"'"+controlNum+"'";
			DataTable table=General.GetTable(command);
			Listt=new List<AutoNoteControl>();
			AutoNoteControl note;
			note = new AutoNoteControl();
			for (int i = 0; i < table.Rows.Count; i++) {
				note.Descript=PIn.PString(table.Rows[0]["Descript"].ToString());
				Listt.Add(note);
			}
			return Listt;
		}*/

		/*
		/// <summary>Checks to see if the Control Name Already Exists.  If you are editing a control you would specify the original name. This name will be ignored in the search. If else set to NULL</summary>
		public static bool ControlNameUsed(string ControlName, string OriginalControlName) {
			string command="SELECT Descript FROM autonotecontrol WHERE "
			+"Descript = '"+POut.PString(ControlName)+"' AND Descript != '"+POut.PString(OriginalControlName)+"'";
			DataTable table=General.GetTable(command);
			bool isUsed=false;
			if (table.Rows.Count!=0) {//found duplicate control name				
				isUsed=true;
			}
			return isUsed;
		}*/
	}
}

