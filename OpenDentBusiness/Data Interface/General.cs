using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using OpenDentBusiness;

namespace OpenDentBusiness {
	public class General {
		///<summary>This method now also throws an exception instead of a messagebox if it fails.  So it's identical to GetTableEx.</summary>
		public static DataTable GetTable(string command) {
			DataTable retVal;
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("No longer allowed to send sql directly.  For user sql, use GetTableLow.  Othewise, rewrite the calling class to not use this query:\r\n"+command);
				//DtoGeneralGetTable dto=new DtoGeneralGetTable();
				//dto.Command=command;
				//retVal=RemotingClient.ProcessQuery(dto).Tables[0].Copy();
			}
			else{
				retVal=DataCore.GetTable(command).Tables[0].Copy();
			}
			retVal.TableName="";//this is needed for FormQuery dataGrid
			return retVal;
		}
		/*
		///<summary>Same as GetTable</summary>
		public static DataTable GetTableEx(string command) {
			DataTable retVal;
			if(RemotingClient.RemotingRole==RemotingRole.ClientTcp) {
DtoGeneralGetTable dto=new DtoGeneralGetTable();
				dto.Command=command;
				retVal=RemotingClient.ProcessQuery(dto).Tables[0].Copy();
			}
			else {
				retVal=GeneralB.GetTable(command).Tables[0].Copy();
			}
			retVal.TableName="";//this is needed for FormQuery dataGrid
			return retVal;
		}*/

		///<summary>This is used for queries written by the user.  If using the server component, it uses the user with lower privileges  to prevent injection attack.</summary>
		public static DataTable GetTableLow(string command) {
			DataTable retVal;
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				DtoGeneralGetTableLow dto=new DtoGeneralGetTableLow();
				dto.Command=command;
				retVal=RemotingClient.ProcessQuery(dto).Tables[0].Copy();
			}
			else {
				retVal=DataCore.GetTable(command).Tables[0].Copy();
			}
			retVal.TableName="";//this is needed for FormQuery dataGrid
			return retVal;
		}

		///<summary>This is for multiple queries all concatenated together with ;</summary>
		public static DataSet GetDataSet(string commands) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("No longer allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+commands);
				//DtoGeneralGetDataSet dto=new DtoGeneralGetDataSet();
				//dto.Commands=commands;
				//return RemotingClient.ProcessQuery(dto);
			}
			else {
				return DataCore.GetDataSet(commands);
			}
		}

		///<summary>This query is run with full privileges.  This is for commands generated by the main program, and the user will not have access for injection attacks.  Result is usually number of rows changed, or can be insert id if requested.</summary>
		public static int NonQ(string command, bool getInsertID) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("No longer allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
				//DtoGeneralNonQ dto=new DtoGeneralNonQ();
				//dto.Command=command;
				//dto.GetInsertID=getInsertID;
				//return RemotingClient.ProcessCommand(dto);
			}
			else {
				return DataCore.NonQ(command,getInsertID);
			}
		}

		public static int NonQ(string command) {
			return NonQ(command,false);
		}

		///<summary>We need to get away from this due to poor support from databases.  For now, each command will be sent entirely separately.  This never returns number of rows affected.</summary>
		public static int NonQ(string[] commands) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("No longer allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+commands[0]);
				//DtoGeneralNonQ dto=new DtoGeneralNonQ();
				//dto.Command=commands[i];
				//dto.GetInsertID=false;
				//RemotingClient.ProcessCommand(dto);
			}
			for(int i=0;i<commands.Length;i++) {
				DataCore.NonQ(commands[i],false);
			}
			return 0;
		}

		/*
		///<summary>Same as NonQ now.</summary>
		public static int NonQEx(string command,bool getInsertID) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientTcp) {
DtoGeneralNonQ dto=new DtoGeneralNonQ();
				dto.Command=command;
				dto.GetInsertID=getInsertID;
				return RemotingClient.ProcessCommand(dto);
			}
			else {
				return GeneralB.NonQ(command,getInsertID);
			}
		}

		public static int NonQEx(string command) {
			return NonQEx(command,false);
		}

		///<summary></summary>
		public static int NonQEx(string[] commands) {
			for(int i=0;i<commands.Length;i++) {
				if(RemotingClient.RemotingRole==RemotingRole.ClientTcp) {
					DtoGeneralNonQ dto=new DtoGeneralNonQ();
					dto.Command=commands[i];
					dto.GetInsertID=false;
					RemotingClient.ProcessCommand(dto);
				}
				else {
					GeneralB.NonQ(commands[i],false);
				}
			}
			return 0;
		}*/

		///<summary>Use this for count(*) queries.  They are always guaranteed to return one and only one value.  Not any faster, just handier.  Can also be used when retrieving prefs manually, since they will also return exactly one value.</summary>
		public static string GetCount(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("No longer allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
				//DtoGeneralGetTable dto=new DtoGeneralGetTable();
				//dto.Command=command;
				//return RemotingClient.ProcessQuery(dto).Tables[0].Rows[0][0].ToString();
			}
			else {
				return DataCore.GetTable(command).Tables[0].Rows[0][0].ToString();
			}
		}

		/*
		///<summary>Same as GetCount.</summary>
		public static string GetCountEx(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientTcp) {
				DtoGeneralGetTable dto=new DtoGeneralGetTable();
				dto.Command=command;
				return RemotingClient.ProcessQuery(dto).Tables[0].Rows[0][0].ToString();
			}
			else {
				return GeneralB.GetTable(command).Tables[0].Rows[0][0].ToString();
			}
		}*/
	}
}
