using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Signals {
		///<summary>Gets all Signals and Acks Since a given DateTime.  If it can't connect to the database, then it no longer throws an error, but instead returns a list of length 0.  Remeber that the supplied dateTime is server time.  This has to be accounted for.</summary>
		public static List<Signal> RefreshTimed(DateTime sinceDateT) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Signal>>(MethodBase.GetCurrentMethod(),sinceDateT);
			}
			string command="SELECT * FROM signal "
				+"WHERE SigDateTime>"+POut.PDateT(sinceDateT)+" "
				+"OR AckTime>"+POut.PDateT(sinceDateT)+" "
				+"ORDER BY SigDateTime";
			//note: this might return an occasional row that has both times newer.
			List<Signal> sigList=new List<Signal>();
			try {
				sigList=RefreshAndFill(Db.GetTable(command));
			} 
			catch {
				//we don't want an error message to show, because that can cause a cascade of a large number of error messages.
			}
			SigElement[] sigElementsAll=SigElements.GetElements(sigList);
			for(int i=0;i<sigList.Count;i++) {
				sigList[i].ElementList=SigElements.GetForSig(sigElementsAll,sigList[i].SignalNum);
			}
			return sigList;
		}

		///<summary>This excludes all Invalids.  It is only concerned with text and button messages.  It includes all messages, whether acked or not.  It's up to the UI to filter out acked if necessary.  Also includes all unacked messages regardless of date.</summary>
		public static List<Signal> RefreshFullText(DateTime sinceDateT) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Signal>>(MethodBase.GetCurrentMethod(),sinceDateT);
			}
			string command="SELECT * FROM signal "
				+"WHERE (SigDateTime>"+POut.PDateT(sinceDateT)+" "
				+"OR AckTime>"+POut.PDateT(sinceDateT)+" "
				+"OR AckTime<'1880-01-01') "//always include all unacked.
				+"AND SigType="+POut.PInt((int)SignalType.Button)
				+" ORDER BY SigDateTime";
			//note: this might return an occasional row that has both times newer.
			List<Signal> sigList=new List<Signal>();
			try {
				sigList=RefreshAndFill(Db.GetTable(command));
			} 
			catch {
				//we don't want an error message to show, because that can cause a cascade of a large number of error messages.
			}
			SigElement[] sigElementsAll=SigElements.GetElements(sigList);
			for(int i=0;i<sigList.Count;i++) {
				sigList[i].ElementList=SigElements.GetForSig(sigElementsAll,sigList[i].SignalNum);
			}
			//ArrayList retVal=new ArrayList(sigList);
			return sigList;//retVal;
		}

		///<summary>Only used when starting up to get the current button state.  Only gets unacked messages.  There may well be extra and useless messages included.  But only the lights will be used anyway, so it doesn't matter.</summary>
		public static List <Signal> RefreshCurrentButState() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Signal>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM signal "
				+"WHERE SigType=0 "//buttons only
				+"AND AckTime<'1880-01-01' "
				+"ORDER BY SigDateTime";
			List <Signal> sigList=new List<Signal> ();
			try {
				sigList=RefreshAndFill(Db.GetTable(command));
			} 
			catch {
				//we don't want an error message to show, because that can cause a cascade of a large number of error messages.
			}
			SigElement[] sigElementsAll=SigElements.GetElements(sigList);
			for(int i=0;i<sigList.Count;i++) {
				sigList[i].ElementList=SigElements.GetForSig(sigElementsAll,sigList[i].SignalNum);
			}
			return sigList;
		}

		private static List<Signal> RefreshAndFill(DataTable table) {
			//No need to check RemotingRole; no call to db.
			List<Signal> retVal=new List<Signal>();
			Signal sig;
			for(int i=0;i<table.Rows.Count;i++) {
				sig=new Signal();
				sig.SignalNum  = PIn.PInt(table.Rows[i][0].ToString());
				sig.FromUser   = PIn.PString(table.Rows[i][1].ToString());
				sig.ITypes     = PIn.PString(table.Rows[i][2].ToString());
				sig.DateViewing= PIn.PDate(table.Rows[i][3].ToString());
				sig.SigType    = (SignalType)PIn.PInt(table.Rows[i][4].ToString());
				sig.SigText    = PIn.PString(table.Rows[i][5].ToString());
				sig.SigDateTime= PIn.PDateT(table.Rows[i][6].ToString());
				sig.ToUser     = PIn.PString(table.Rows[i][7].ToString());
				sig.AckTime    = PIn.PDateT(table.Rows[i][8].ToString());
				sig.TaskNum    = PIn.PInt(table.Rows[i][9].ToString());
				retVal.Add(sig);
			}
			retVal.Sort();
			return retVal;
		}
	
		///<summary></summary>
		public static void Update(Signal sig){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),sig);
				return;
			}
			string command= "UPDATE signal SET " 
				+"FromUser = '"    +POut.PString(sig.FromUser)+"'"
				+",ITypes = '"     +POut.PString(sig.ITypes)+"'"
				+",DateViewing = " +POut.PDate  (sig.DateViewing)
				+",SigType = '"    +POut.PInt   ((int)sig.SigType)+"'"
				+",SigText = '"    +POut.PString(sig.SigText)+"'"
				//+",SigDateTime = '"+POut.PDateT (SigDateTime)+"'"//we don't want to ever update this
				+",ToUser = '"     +POut.PString(sig.ToUser)+"'"
				+",AckTime = "     +POut.PDateT (sig.AckTime)
				+",TaskNum = '"    +POut.PInt   (sig.TaskNum)+"'"
				+" WHERE SignalNum = '"+POut.PInt(sig.SignalNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static int Insert(Signal sig){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				sig.SignalNum=Meth.GetInt(MethodBase.GetCurrentMethod(),sig);
				return sig.SignalNum;
			}
			//we need to explicitly get the server time in advance rather than using NOW(),
			//because we need to update the signal object soon after creation.
			//DateTime now=ClockEvents.GetServerTime();
			sig.SigDateTime=MiscData.GetNowDateTime();
			if(PrefC.RandomKeys){
				sig.SignalNum=MiscData.GetKey("signal","SignalNum");
			}
			string command= "INSERT INTO signal (";
			if(PrefC.RandomKeys){
				command+="SignalNum,";
			}
			command+="FromUser,ITypes,DateViewing,SigType,SigText,SigDateTime,ToUser,AckTime,TaskNum"
				+") VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.PInt(sig.SignalNum)+"', ";
			}
			command+=
				 "'"+POut.PString(sig.FromUser)+"', "
				+"'"+POut.PString(sig.ITypes)+"', "
				+POut.PDate  (sig.DateViewing)+", "
				+"'"+POut.PInt   ((int)sig.SigType)+"', "
				+"'"+POut.PString(sig.SigText)+"', "
				+POut.PDateT(sig.SigDateTime)+", "
				+"'"+POut.PString(sig.ToUser)+"', "
				+POut.PDateT (sig.AckTime)+", "
				+"'"+POut.PInt(sig.TaskNum)+"')";
 			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				sig.SignalNum=Db.NonQ(command,true);
			}
			return sig.SignalNum;
		}

		//<summary>There's no such thing as deleting a signal</summary>
		/*public void Delete(){
			string command= "DELETE from Signal WHERE SignalNum = '"
				+POut.PInt(SignalNum)+"'";
			DataConnection dcon=new DataConnection();
 			Db.NonQ(command);
		}*/

		///<summary>After a refresh, this is used to determine whether the Appt Module needs to be refreshed.  Must supply the current date showing as well as the recently retrieved signal list.</summary>
		public static bool ApptNeedsRefresh(List <Signal> signalList,DateTime dateTimeShowing){
			//No need to check RemotingRole; no call to db.
			List<string> iTypeList;
			for(int i=0;i<signalList.Count;i++){
				iTypeList=new List<string>(signalList[i].ITypes.Split(','));
				if(iTypeList.Contains(((int)InvalidType.Date).ToString()) && signalList[i].DateViewing.Date==dateTimeShowing){
					return true;
				}
			}
			return false;
		}

		///<summary>After a refresh, this is used to determine whether the Current user has received any new tasks through subscription.  Must supply the current usernum as well as the recently retrieved signal list.  The signal list will include any task changes including status changes and deletions.  This will be called twice, once with isPopup=true and once with isPopup=false.</summary>
		public static List<Task> GetNewTaskPopupsThisUser(List <Signal> signalList,int userNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Task>>(MethodBase.GetCurrentMethod(),signalList,userNum);
			}
			List<Signal> sigListFiltered=new List<Signal>();
			for(int i=0;i<signalList.Count;i++){
				if(signalList[i].ITypes==((int)InvalidType.TaskPopup).ToString()){
					sigListFiltered.Add(signalList[i]);
				}
			}
			if(sigListFiltered.Count==0){//no task popup signals
				return new List<Task>();
			}
			string command="SELECT task.* FROM taskancestor,task,tasklist,tasksubscription "
				+"WHERE taskancestor.TaskListNum=tasklist.TaskListNum "
				+"AND task.TaskNum=taskancestor.TaskNum "
				+"AND tasksubscription.TaskListNum=tasklist.TaskListNum "
				+"AND tasksubscription.UserNum="+POut.PInt(userNum)
				+" AND (";
			for(int i=0;i<sigListFiltered.Count;i++){
				if(i>0){
					command+=" OR ";
				}
				command+="task.TaskNum= "+POut.PInt(sigListFiltered[i].TaskNum);
			}
			command+=")";
			return Tasks.RefreshAndFill(Db.GetTable(command));
		}

		///<summary>After a refresh, this is used to get a list containing all flags of types that need to be refreshed.   Types of Date and Task are not included.</summary>
		public static List<int> GetInvalidTypes(List <Signal> signalList){
			//No need to check RemotingRole; no call to db.
			List<int> retVal=new List<int>();
			string[] strArray;
			for(int i=0;i<signalList.Count;i++){
				if(signalList[i].SigType!=SignalType.Invalid){
					continue;
				}
				if(signalList[i].ITypes==((int)InvalidType.Date).ToString()){
					continue;
				}
				if(signalList[i].ITypes==((int)InvalidType.Task).ToString()){
					continue;
				}
				if(signalList[i].ITypes==((int)InvalidType.TaskPopup).ToString()){
					continue;
				}
				strArray=signalList[i].ITypes.Split(',');
				for(int t=0;t<strArray.Length;t++){
					if(!retVal.Contains(PIn.PInt(strArray[t]))){
						retVal.Add(PIn.PInt(strArray[t]));
					}
				}
			}
			return retVal;
		}


		///<summary>After a refresh, this gets a list of only the button signals.</summary>
		public static List <Signal> GetButtonSigs(List <Signal> signalList){
			//No need to check RemotingRole; no call to db.
			List <Signal> list=new List <Signal> ();
			for(int i=0;i<signalList.Count;i++){
				if(signalList[i].SigType!=SignalType.Button){
					continue;
				}
				list.Add(signalList[i]);
			}
			return list;
		}

		///<summary>When user clicks on a colored light, they intend to ack it to turn it off.  This acks all signals with the specified index.  This is in case multiple signals have been created from different workstations.  This acks them all in one shot.  Must specify a time because you only want to ack signals earlier than the last time this workstation was refreshed.  A newer signal would not get acked.
		///If this seems slow, then I will need to check to make sure all these tables are properly indexed.</summary>
		public static void AckButton(int buttonIndex,DateTime time){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),buttonIndex,time);
				return;
			}
			//FIXME:UPDATE-MULTIPLE-TABLES
			/*string command= "UPDATE signal,sigelement,sigelementdef "
				+"SET signal.AckTime = ";
				if(FormChooseDatabase.DBtype==DatabaseType.Oracle) {
					command+="(SELECT CURRENT_TIMESTAMP FROM DUAL)";
				}else{//Assume MySQL
					command+="NOW()";
				}
				command+=" "
				+"WHERE signal.AckTime < '1880-01-01' "
				+"AND SigDateTime <= '"+POut.PDateT(time)+"' "
				+"AND signal.SignalNum=sigelement.SignalNum "
				+"AND sigelement.SigElementDefNum=sigelementdef.SigElementDefNum "
				+"AND sigelementdef.LightRow="+POut.PInt(buttonIndex);
			Db.NonQ(command);*/
			//Rewritten so that the SQL is compatible with both Oracle and MySQL.
			string command= "SELECT signal.SignalNum FROM signal,sigelement,sigelementdef "
				+"WHERE signal.AckTime < '1880-01-01' "
				+"AND SigDateTime <= "+POut.PDateT(time)+" "
				+"AND signal.SignalNum=sigelement.SignalNum "
				+"AND sigelement.SigElementDefNum=sigelementdef.SigElementDefNum "
				+"AND sigelementdef.LightRow="+POut.PInt(buttonIndex);
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0){
				return;
			}
			command="UPDATE signal SET AckTime = ";
			if(DataConnection.DBtype==DatabaseType.Oracle){
				command+=POut.PDateT(MiscData.GetNowDateTime());
			}else {//Assume MySQL
				command+="NOW()";
			}
			command+=" WHERE ";
			for(int i=0;i<table.Rows.Count;i++){
				command+="SignalNum="+table.Rows[i][0].ToString();
				if(i<table.Rows.Count-1){
					command+=" OR ";
				}
			}
			Db.NonQ(command);
		}

		/// <summary>Won't work with InvalidType.Date, InvalidType.Task, or InvalidType.TaskPopup  yet.</summary>
		public static void SetInvalid(params InvalidType[] itypes) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),itypes);
				return;
			}
			string itypeString="";
			for(int i=0;i<itypes.Length;i++) {
				if(i>0) {
					itypeString+=",";
				}
				itypeString+=((int)itypes[i]).ToString();
			}
			Signal sig=new Signal();
			sig.ITypes=itypeString;
			sig.DateViewing=DateTime.MinValue;
			sig.SigType=SignalType.Invalid;
			sig.TaskNum=0;
			Insert(sig);
		}
	}

	

	


}




















