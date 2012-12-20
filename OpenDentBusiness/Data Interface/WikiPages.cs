using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{
	///<summary></summary>
	public class WikiPages{

		///<summary>Gets one WikiPage from the db.</summary>
		public static WikiPage GetMaster() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<WikiPage>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM wikipage WHERE PageTitle='_Master' and DateTimeSaved=(SELECT MAX(DateTimeSaved) FROM wikipage WHERE PageTitle='_Master');";
			return Crud.WikiPageCrud.SelectOne(command);
		}

		public static WikiPage GetByName(string PageName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<WikiPage>(MethodBase.GetCurrentMethod(),PageName);
			}
			string command="SELECT * FROM wikipage WHERE PageTitle='"+PageName+"' and DateTimeSaved=(SELECT MAX(DateTimeSaved) FROM wikipage WHERE PageTitle='"+PageName+"');";
			return Crud.WikiPageCrud.SelectOne(command);
		}

		public static string TranslateToXhtml(string wikiContent) {
			StringBuilder retVal = new StringBuilder();
			string baseLocalURL="wiki:";
			retVal.Append(wikiContent);
			retVal.Replace("&<","&lt;");
			retVal.Replace("&>","&gt;");
			retVal.Replace("<body>","<body><p>");
			retVal.Replace("</body>","</p></body>");
			//Replace internal Links
			MatchCollection matches = Regex.Matches(retVal.ToString(),"\\[\\[.{0,255}\\]\\]");
			foreach(Match link in matches) {
				retVal.Replace(link.Value,"<a href=\""+baseLocalURL+link.Value.Trim("[]".ToCharArray())+"\">"+link.Value.Trim("[]".ToCharArray())+"</a>");
			}
			retVal.Replace("\r\n\r\n","</p>\r\n<p>");
			retVal.Replace("<p></p>","<p>&nbsp;</p>");
			retVal.Replace("\r\n","<br />");
			retVal.Replace("     ","&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
			retVal.Replace("  ","&nbsp;&nbsp;");
			//TODO: Color tags
			//Imagae Tags
			return retVal.ToString();
		}
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<WikiPage> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<WikiPage>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM wikipage WHERE PatNum = "+POut.Long(patNum);
			return Crud.WikiPageCrud.SelectMany(command);
		}

		///<summary>Gets one WikiPage from the db.</summary>
		public static WikiPage GetOne(long wikiPageNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<WikiPage>(MethodBase.GetCurrentMethod(),wikiPageNum);
			}
			return Crud.WikiPageCrud.SelectOne(wikiPageNum);
		}

		///<summary></summary>
		public static long Insert(WikiPage wikiPage){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				wikiPage.WikiPageNum=Meth.GetLong(MethodBase.GetCurrentMethod(),wikiPage);
				return wikiPage.WikiPageNum;
			}
			return Crud.WikiPageCrud.Insert(wikiPage);
		}

		///<summary></summary>
		public static void Update(WikiPage wikiPage){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),wikiPage);
				return;
			}
			Crud.WikiPageCrud.Update(wikiPage);
		}

		///<summary></summary>
		public static void Delete(long wikiPageNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),wikiPageNum);
				return;
			}
			string command= "DELETE FROM wikipage WHERE WikiPageNum = "+POut.Long(wikiPageNum);
			Db.NonQ(command);
		}
		*/




	}
}