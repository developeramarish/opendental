using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Fees {
		private static List<Fee> listt;

		///<summary>A list of all Fees.</summary>
		public static List<Fee> Listt{
			get {
				if(listt==null) {
					RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM fee";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Fee";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			listt=Crud.FeeCrud.TableToList(table);
			/*
			Dict=new Dictionary<FeeKey,Fee>();
			FeeKey key;
			for(int i=0;i<Listt.Count;i++) {
				key=new FeeKey();
				key.codeNum=Listt[i].CodeNum;
				key.feeSchedNum=Listt[i].FeeSched;
				if(Dict.ContainsKey(key)) {
					//Older versions used to delete duplicates here
				}
				else {
					Dict.Add(key,Listt[i]);
				}
			}*/
		}

		///<summary></summary>
		public static void Update(Fee fee){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),fee);
				return;
			}
			Crud.FeeCrud.Update(fee);
		}

		///<summary></summary>
		public static long Insert(Fee fee) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				fee.FeeNum=Meth.GetLong(MethodBase.GetCurrentMethod(),fee);
				return fee.FeeNum;
			}
			return Crud.FeeCrud.Insert(fee);
		}

		///<summary></summary>
		public static void Delete(Fee fee){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),fee);
				return;
			}
			string command="DELETE FROM fee WHERE FeeNum="+fee.FeeNum;
			Db.NonQ(command);
		}

		///<summary>Returns null if no fee exists for code/feeSched combo.</summary>
		public static Fee GetFee(long codeNum,long feeSchedNum) {
			//No need to check RemotingRole; no call to db.
			if(codeNum==0){
				return null;
			}
			if(feeSchedNum==0){
				return null;
			}
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].CodeNum==codeNum && Listt[i].FeeSched==feeSchedNum) {
					return Listt[i];
				}
			}
			return null;
		}

		///<summary>Returns an amount if a fee has been entered.  Otherwise returns -1.  Not usually used directly.</summary>
		public static double GetAmount(long codeNum,long feeSchedNum) {
			//No need to check RemotingRole; no call to db.
			if(codeNum==0){
				return -1;
			}
			if(feeSchedNum==0){
				return -1;
			}
			if(FeeScheds.GetIsHidden(feeSchedNum)){
				return -1;//you cannot obtain fees for hidden fee schedules
			}
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].CodeNum==codeNum && Listt[i].FeeSched==feeSchedNum) {
					return Listt[i].Amount;
				}
			}
			return -1;//code not found
		}

		///<summary>Almost the same as GetAmount.  But never returns -1;  Returns an amount if a fee has been entered.  Returns 0 if code can't be found.</summary>
		public static double GetAmount0(long codeNum,long feeSched) {
			//No need to check RemotingRole; no call to db.
			double retVal=GetAmount(codeNum,feeSched);
			if(retVal==-1){
				return 0;
			}
			return retVal;															 
		}

		///<summary>Gets the fee schedule from the priinsplan, the patient, or the provider in that order.  Either returns a fee schedule (fk to definition.DefNum) or 0.</summary>
		public static long GetFeeSched(Patient pat,List<InsPlan> PlanList,List<PatPlan> patPlans) {
			//No need to check RemotingRole; no call to db.
			//there's not really a good place to put this function, so it's here.
			long retVal=0;
			if(PatPlans.GetPlanNum(patPlans,1)!=0){
				InsPlan PlanCur=InsPlans.GetPlan(PatPlans.GetPlanNum(patPlans,1),PlanList);
				if(PlanCur==null){
					retVal=0;
				}
				else{
					retVal=PlanCur.FeeSched;
				}
			}
			if(retVal==0){
				if(pat.FeeSched!=0){
					retVal=pat.FeeSched;
				}
				else{
					if(pat.PriProv==0){
						retVal=ProviderC.List[0].FeeSched;
					}
					else{
            //MessageBox.Show(Providers.GetIndex(Patients.Cur.PriProv).ToString());   
						retVal=ProviderC.ListLong[Providers.GetIndexLong(pat.PriProv)].FeeSched;
					}
				}
			}
			return retVal;
		}

		///<summary>A simpler version of the same function above.  The required numbers can be obtained in a fairly simple query.  Might return a 0 if the primary provider does not have a fee schedule set.</summary>
		public static long GetFeeSched(long priPlanFeeSched,long patFeeSched,long patPriProvNum) {
			//No need to check RemotingRole; no call to db.
			if(priPlanFeeSched!=0){
				return priPlanFeeSched;
			}
			if(patFeeSched!=0){
				return patFeeSched;
			}
			return ProviderC.ListLong[Providers.GetIndexLong(patPriProvNum)].FeeSched;
		}

        ///<summary>Gets the fee schedule from the primary MEDICAL insurance plan, the patient, or the provider in that order.</summary>
		public static long GetMedFeeSched(Patient pat,List<InsPlan> PlanList,List<PatPlan> patPlans) {
			//No need to check RemotingRole; no call to db. ??
			long retVal = 0;
			if (PatPlans.GetPlanNum(patPlans, 1) != 0){
				//Pick the medinsplan with the ordinal closest to zero
				int planOrdinal=10; //This is a hack, but I doubt anyone would have more than 10 plans
				foreach(PatPlan plan in patPlans){
					if(plan.Ordinal<planOrdinal && InsPlans.GetPlan(plan.PlanNum,PlanList).IsMedical) {
						planOrdinal=plan.Ordinal;
					}
				}
				InsPlan PlanCur = InsPlans.GetPlan(PatPlans.GetPlanNum(patPlans, planOrdinal), PlanList);
				if (PlanCur == null){
					retVal = 0;
				} 
				else {
					retVal = PlanCur.FeeSched;
				}
			}
			if (retVal == 0){
				if (pat.FeeSched != 0){
					retVal = pat.FeeSched;
				} 
				else {
					if (pat.PriProv == 0){
						retVal = ProviderC.List[0].FeeSched;
					} 
					else {
						//MessageBox.Show(Providers.GetIndex(Patients.Cur.PriProv).ToString());   
						retVal = ProviderC.ListLong[Providers.GetIndexLong(pat.PriProv)].FeeSched;
					}
				}
			}
			return retVal;
		}

		///<summary>Clears all fees from one fee schedule.  Supply the FeeSchedNum of the feeSchedule.</summary>
		public static void ClearFeeSched(long schedNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),schedNum);
				return;
			}
			string command="DELETE FROM fee WHERE FeeSched="+schedNum;
			Db.NonQ(command);
		}

		///<summary>Copies any fee objects over to the new fee schedule.  Usually run ClearFeeSched first.  Be careful exactly which int's you supply.</summary>
		public static void CopyFees(long fromFeeSched,long toFeeSched) {
			//No need to check RemotingRole; no call to db.
			if(Listt==null) {
				RefreshCache();
			}
			Fee fee;
			for(int i=0;i<Listt.Count;i++){
				if(Listt[i].FeeSched!=fromFeeSched){
					continue;
				}
				fee=Listt[i].Copy();
				fee.FeeSched=toFeeSched;
				Fees.Insert(fee);
			}
		}

		///<summary>Increases the fee schedule by percent.  Round should be the number of decimal places, either 0,1,or 2.</summary>
		public static void Increase(long feeSched,int percent,int round) {
			//No need to check RemotingRole; no call to db.
			if(Listt==null) {
				RefreshCache();
			}
			Fee fee;
			double newVal;
			for(int i=0;i<Listt.Count;i++){
				if(Listt[i].FeeSched!=feeSched){
					continue;
				}
				if(Listt[i].Amount==0){
					continue;
				}
				fee=Listt[i].Copy();
				newVal=(double)fee.Amount*(1+(double)percent/100);
				fee.Amount=Math.Round(newVal,round);
				Fees.Update(fee);
			}
		}

		///<summary>schedI is the currently displayed index of the fee schedule to save to.  Empty fees never even make it this far and should be skipped earlier in the process.</summary>
		public static void Import(string codeText,double amt,long feeSchedNum) {
			//No need to check RemotingRole; no call to db.
			if(!ProcedureCodes.IsValidCode(codeText)){
				return;//skip for now. Possibly insert a code in a future version.
			}
			Fee fee=GetFee(ProcedureCodes.GetCodeNum(codeText),feeSchedNum);
			if(fee!=null){
				Delete(fee);
			}
			fee=new Fee();
			fee.Amount=amt;
			fee.FeeSched=feeSchedNum;
			fee.CodeNum=ProcedureCodes.GetCodeNum(codeText);
			Insert(fee);
		}

	}

	public struct FeeKey{
		public long codeNum;
		public long feeSchedNum;
	}

}