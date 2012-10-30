using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

namespace OpenDentalWebService {
	///<summary>Provides a base class for DTO classes.  A DTO class is a simple data storage object.</summary>
	public abstract class DataTransferObject {
		///<summary></summary>
		public Credentials Credentials;
		///<summary>This is the name of the method that we need to call.  "Class.Method" format.  Not used with GetTableLow.</summary>
		public string MethodName;
		///<summary>This is a list of parameters that we are passing.  They can be various kinds of objects.</summary>
		public List<DtoObject> Params;
		///<summary>This is a list of parameter types that we are passing.  This array must directly match the count of Params.</summary>
		public List<String> ParamTypes;
		///<summary>Used to quickly tell the type of DataTransferObject.</summary>
		public string Type;

		public string Serialize() {
			StringBuilder strBuild=new StringBuilder();
			//TODO: Write serialize code.
			return strBuild.ToString();
		}

		public static DataTransferObject Deserialize(string data) {
			DataTransferObject dto=null;
			//XmlReader is said to be the quickest way to read XML as opposed to LINQ to XML or other methods of reading xml files.
			using(XmlReader reader=XmlReader.Create(new StringReader(data))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						#region DtoType
						case "DtoGetTable":
						case "DtoGetTableLow":
						case "DtoGetDS":
						case "DtoGetLong":
						case "DtoGetInt":
						case "DtoGetVoid":
						case "DtoGetObject":
						case "DtoGetString":
						case "DtoGetBool":
							dto=GetDtoForType(reader.Name);//Set dto to the correct type.
							break;
						#endregion
						#region Credentials
						case "Credentials":
							if(dto==null) {//This should never happen. 
								//TODO: Throw an exception for unknown dto type.
							}
							//Create a new Credentials object.
							Credentials creds=new Credentials();
							//Read and set the UserName and Password elements.
							reader.ReadToFollowing("UserName");
							creds.Username=reader.Value;							
							reader.ReadToFollowing("Password");
							creds.Password=reader.Value;
							dto.Credentials=creds;
							break;
						#endregion
						#region MethodName
						case "MethodName":
							if(dto==null) {//This should never happen. 
								//TODO: Throw an exception for unknown dto type.
							}
							dto.MethodName=reader.Value;
							break;
						#endregion
						#region Params
						case "Params":
							if(dto==null) {//This should never happen. 
								//TODO: Throw an exception for unknown dto type.
							}
							if(!reader.IsEmptyElement) {//Parameters are present.
								SetParamsAndParamTypes(reader,dto);
							}
							break;
						#endregion
					}
				}
			}
			return dto;
		}

		///<summary>Returns the correct DataTransferObject for the type of object passed in.</summary>
		private static DataTransferObject GetDtoForType(string dtoType) {
			DataTransferObject dto=null;
			switch(dtoType) {
				case "DtoGetDS":
					dto=new DtoGetDS();
					break;
				case "DtoGetTable":
					dto=new DtoGetTable();
					break;
				case "DtoGetTableLow":
					dto=new DtoGetTableLow();
					break;
				case "DtoGetLong":
					dto=new DtoGetLong();
					break;
				case "DtoGetInt":
					dto=new DtoGetInt();
					break;
				case "DtoGetVoid":
					dto=new DtoGetVoid();
					break;
				case "DtoGetObject":
					dto=new DtoGetObject();
					break;
				case "DtoGetString":
					dto=new DtoGetString();
					break;
				case "DtoGetBool":
					dto=new DtoGetBool();
					break;
				case "DtoException":
					dto=new DtoException();
					break;
			}
			return dto;
		}

		///<summary>Correctly sets Params AND ParamTypes on the dto object based on the navigator passed in.</summary>
		private static void SetParamsAndParamTypes(XmlReader reader,DataTransferObject dto) {
			//The Params node is going to be a list of DtoObjects. Each has </TypeName> and </Obj>.  TypeName will give us the type and Obj will contain the serialized object.
			//Loop through the nodes and call a function that will call each corresponding S class to deserialize the desired object correctly.
			while(reader.Read()) {//Just read till the end of the xml stream because parameters are the last thing in any dto object anyway.
				//Only detect start elements.
				if(!reader.IsStartElement()) {
					continue;
				}
				if(reader.Name=="TypeName") {
					DtoObject dtoObj=new DtoObject();
					string typeName=reader.Value;
					dtoObj.TypeName=typeName;
					dto.ParamTypes.Add(typeName);
					reader.Read();//Moves down to <Obj>.
					string xml="";//TODO: Write code to strip out JUST the "object" xml and then call CallClassDeserializer which can deserialize any of our objects.
					object obj=DtoMethods.CallClassDeserializer(dtoObj.TypeName,xml);
					dto.Params.Add(dtoObj);
				}
			}
		}


	}

	///<summary>The username and password are internal to OD.  They are not the MySQL username and password.</summary>
	public class Credentials {
		public string Username;
		///<summary>If using Ecw, then the password is actually just a hash because we don't know the real password.</summary>
		public string Password;
	}

	///<summary></summary>
	public class DtoGetDS:DataTransferObject {

	}

	///<summary></summary>
	public class DtoGetTable:DataTransferObject {

	}

	///<summary></summary>
	public class DtoGetTableLow:DataTransferObject {

	}

	///<summary>Gets a long.</summary>
	public class DtoGetLong:DataTransferObject {

	}

	///<summary>Gets an int.</summary>
	public class DtoGetInt:DataTransferObject {

	}

	///<summary>Used when the return type is void.  It will still return 0 to ack.</summary>
	public class DtoGetVoid:DataTransferObject {

	}

	///<summary>Gets an object which must be serializable.  Calling code will convert object to specific type.</summary>
	public class DtoGetObject:DataTransferObject {
		///<summary>This is the "FullName" string representation of the type of object that we expect back as a result.  Examples: System.Int32, OpenDentBusiness.Patient, OpenDentBusiness.Patient[], List&lt;OpenDentBusiness.Patient&gt;.  DataTable and DataSet not allowed.</summary>
		public string ObjectType;
	}

	///<summary>Gets a simple string.</summary>
	public class DtoGetString:DataTransferObject {

	}

	///<summary>Gets a bool.</summary>
	public class DtoGetBool:DataTransferObject {

	}

	///<summary>OpenDentBusiness and all the DA classes are designed to throw an exception if something goes wrong.  If using OpenDentBusiness through the remote server, then the server catches the exception and passes it back to the main program using this DTO.  The client then turns it back into an exception so that it behaves just as if OpenDentBusiness was getting called locally.</summary>
	public class DtoException:DataTransferObject {
		public string Message;
	}





}
