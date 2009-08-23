using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary>Handles database commands related to the apptview table in the database.</summary>
	public class ApptViews{

		///<summary></summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string c="SELECT * FROM apptview ORDER BY itemorder";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),c);
			table.TableName="ApptView";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			ApptViewC.List=new ApptView[table.Rows.Count];
			for(int i=0;i<ApptViewC.List.Length;i++){
				ApptViewC.List[i]=new ApptView();
				ApptViewC.List[i].ApptViewNum = PIn.PInt   (table.Rows[i][0].ToString());
				ApptViewC.List[i].Description = PIn.PString(table.Rows[i][1].ToString());
				ApptViewC.List[i].ItemOrder   = PIn.PInt   (table.Rows[i][2].ToString());
				ApptViewC.List[i].RowsPerIncr = PIn.PInt   (table.Rows[i][3].ToString());
				ApptViewC.List[i].OnlyScheduledProvs = PIn.PBool  (table.Rows[i][4].ToString());	
			}
		}

		///<summary></summary>
		public static int Insert(ApptView Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.ApptViewNum=Meth.GetInt(MethodBase.GetCurrentMethod(),Cur);
				return Cur.ApptViewNum;
			}
			string command = "INSERT INTO apptview (Description,ItemOrder,RowsPerIncr,OnlyScheduledProvs) "
				+"VALUES ("
				+"'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PInt   (Cur.ItemOrder)+"', "
				+"'"+POut.PInt   (Cur.RowsPerIncr)+"', "
				+"'"+POut.PBool  (Cur.OnlyScheduledProvs)+"')";
			//MessageBox.Show(string command);
			Cur.ApptViewNum=Db.NonQ(command,true);
			return Cur.ApptViewNum;
		}

		///<summary></summary>
		public static void Update(ApptView Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command= "UPDATE apptview SET "
				+"Description='"   +POut.PString(Cur.Description)+"'"
				+",ItemOrder = '"  +POut.PInt   (Cur.ItemOrder)+"'"
				+",RowsPerIncr = '"+POut.PInt   (Cur.RowsPerIncr)+"'"
				+",OnlyScheduledProvs = '"+POut.PBool(Cur.OnlyScheduledProvs)+"'"
				+" WHERE ApptViewNum = '"+POut.PInt(Cur.ApptViewNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(ApptView Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command="DELETE from apptview WHERE ApptViewNum = '"
				+POut.PInt(Cur.ApptViewNum)+"'";
			Db.NonQ(command);
		}

	

	


	}

	


}









