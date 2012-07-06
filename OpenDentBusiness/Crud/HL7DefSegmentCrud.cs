//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	internal class HL7DefSegmentCrud {
		///<summary>Gets one HL7DefSegment object from the database using the primary key.  Returns null if not found.</summary>
		internal static HL7DefSegment SelectOne(long hL7DefSegmentNum){
			string command="SELECT * FROM hl7defsegment "
				+"WHERE HL7DefSegmentNum = "+POut.Long(hL7DefSegmentNum);
			List<HL7DefSegment> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one HL7DefSegment object from the database using a query.</summary>
		internal static HL7DefSegment SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<HL7DefSegment> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of HL7DefSegment objects from the database using a query.</summary>
		internal static List<HL7DefSegment> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<HL7DefSegment> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		internal static List<HL7DefSegment> TableToList(DataTable table){
			List<HL7DefSegment> retVal=new List<HL7DefSegment>();
			HL7DefSegment hL7DefSegment;
			for(int i=0;i<table.Rows.Count;i++) {
				hL7DefSegment=new HL7DefSegment();
				hL7DefSegment.HL7DefSegmentNum= PIn.Long  (table.Rows[i]["HL7DefSegmentNum"].ToString());
				hL7DefSegment.HL7DefMessageNum= PIn.Long  (table.Rows[i]["HL7DefMessageNum"].ToString());
				hL7DefSegment.ItemOrder       = PIn.Int   (table.Rows[i]["ItemOrder"].ToString());
				hL7DefSegment.CanRepeat       = PIn.Bool  (table.Rows[i]["CanRepeat"].ToString());
				hL7DefSegment.IsOptional      = PIn.Bool  (table.Rows[i]["IsOptional"].ToString());
				hL7DefSegment.Note            = PIn.String(table.Rows[i]["Note"].ToString());
				retVal.Add(hL7DefSegment);
			}
			return retVal;
		}

		///<summary>Inserts one HL7DefSegment into the database.  Returns the new priKey.</summary>
		internal static long Insert(HL7DefSegment hL7DefSegment){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				hL7DefSegment.HL7DefSegmentNum=DbHelper.GetNextOracleKey("hl7defsegment","HL7DefSegmentNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(hL7DefSegment,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							hL7DefSegment.HL7DefSegmentNum++;
							loopcount++;
						}
						else{
							throw ex;
						}
					}
				}
				throw new ApplicationException("Insert failed.  Could not generate primary key.");
			}
			else {
				return Insert(hL7DefSegment,false);
			}
		}

		///<summary>Inserts one HL7DefSegment into the database.  Provides option to use the existing priKey.</summary>
		internal static long Insert(HL7DefSegment hL7DefSegment,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				hL7DefSegment.HL7DefSegmentNum=ReplicationServers.GetKey("hl7defsegment","HL7DefSegmentNum");
			}
			string command="INSERT INTO hl7defsegment (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="HL7DefSegmentNum,";
			}
			command+="HL7DefMessageNum,ItemOrder,CanRepeat,IsOptional,Note) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(hL7DefSegment.HL7DefSegmentNum)+",";
			}
			command+=
				     POut.Long  (hL7DefSegment.HL7DefMessageNum)+","
				+    POut.Int   (hL7DefSegment.ItemOrder)+","
				+    POut.Bool  (hL7DefSegment.CanRepeat)+","
				+    POut.Bool  (hL7DefSegment.IsOptional)+","
				+DbHelper.ParamChar+"paramNote)";
			if(hL7DefSegment.Note==null) {
				hL7DefSegment.Note="";
			}
			OdSqlParameter paramNote=new OdSqlParameter("paramNote",OdDbType.Text,hL7DefSegment.Note);
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command,paramNote);
			}
			else {
				hL7DefSegment.HL7DefSegmentNum=Db.NonQ(command,true,paramNote);
			}
			return hL7DefSegment.HL7DefSegmentNum;
		}

		///<summary>Updates one HL7DefSegment in the database.</summary>
		internal static void Update(HL7DefSegment hL7DefSegment){
			string command="UPDATE hl7defsegment SET "
				+"HL7DefMessageNum=  "+POut.Long  (hL7DefSegment.HL7DefMessageNum)+", "
				+"ItemOrder       =  "+POut.Int   (hL7DefSegment.ItemOrder)+", "
				+"CanRepeat       =  "+POut.Bool  (hL7DefSegment.CanRepeat)+", "
				+"IsOptional      =  "+POut.Bool  (hL7DefSegment.IsOptional)+", "
				+"Note            =  "+DbHelper.ParamChar+"paramNote "
				+"WHERE HL7DefSegmentNum = "+POut.Long(hL7DefSegment.HL7DefSegmentNum);
			if(hL7DefSegment.Note==null) {
				hL7DefSegment.Note="";
			}
			OdSqlParameter paramNote=new OdSqlParameter("paramNote",OdDbType.Text,hL7DefSegment.Note);
			Db.NonQ(command,paramNote);
		}

		///<summary>Updates one HL7DefSegment in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.</summary>
		internal static void Update(HL7DefSegment hL7DefSegment,HL7DefSegment oldHL7DefSegment){
			string command="";
			if(hL7DefSegment.HL7DefMessageNum != oldHL7DefSegment.HL7DefMessageNum) {
				if(command!=""){ command+=",";}
				command+="HL7DefMessageNum = "+POut.Long(hL7DefSegment.HL7DefMessageNum)+"";
			}
			if(hL7DefSegment.ItemOrder != oldHL7DefSegment.ItemOrder) {
				if(command!=""){ command+=",";}
				command+="ItemOrder = "+POut.Int(hL7DefSegment.ItemOrder)+"";
			}
			if(hL7DefSegment.CanRepeat != oldHL7DefSegment.CanRepeat) {
				if(command!=""){ command+=",";}
				command+="CanRepeat = "+POut.Bool(hL7DefSegment.CanRepeat)+"";
			}
			if(hL7DefSegment.IsOptional != oldHL7DefSegment.IsOptional) {
				if(command!=""){ command+=",";}
				command+="IsOptional = "+POut.Bool(hL7DefSegment.IsOptional)+"";
			}
			if(hL7DefSegment.Note != oldHL7DefSegment.Note) {
				if(command!=""){ command+=",";}
				command+="Note = "+DbHelper.ParamChar+"paramNote";
			}
			if(command==""){
				return;
			}
			if(hL7DefSegment.Note==null) {
				hL7DefSegment.Note="";
			}
			OdSqlParameter paramNote=new OdSqlParameter("paramNote",OdDbType.Text,hL7DefSegment.Note);
			command="UPDATE hl7defsegment SET "+command
				+" WHERE HL7DefSegmentNum = "+POut.Long(hL7DefSegment.HL7DefSegmentNum);
			Db.NonQ(command,paramNote);
		}

		///<summary>Deletes one HL7DefSegment from the database.</summary>
		internal static void Delete(long hL7DefSegmentNum){
			string command="DELETE FROM hl7defsegment "
				+"WHERE HL7DefSegmentNum = "+POut.Long(hL7DefSegmentNum);
			Db.NonQ(command);
		}

	}
}