using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


[ServiceContract]
public interface IService
{
    //   EMPLOYEES   //
    [OperationContract]
    string GetEmployees();

	[OperationContract]
    string CreateEmployee(string name, string lastName, string curp, DateTime birthDate, string email, int id, DateTime deliveryDate);
    [OperationContract]
    string DeleteEmployee(int employeeId);
	[OperationContract]
    string RemoveAssetFromEmployee(int employeeId, int assetId);
	string UpdateEmployee(int employeeId, string name, string lastName, string curp, DateTime birthDate, string email);


    //   EMAIL   //

    [OperationContract]
	string SendEmailReminders();

    //   AUTH   //

    [OperationContract]
	string ValidateEmployeeLogin(string email, string password);

	//   ASSETS   //
	[OperationContract]
	string GetAssets(bool? status);
    [OperationContract]
    string CreateAsset(string name, string description);
	[OperationContract]
	string DeleteAsset(int assetId);
	[OperationContract]
	string ReleaseAsset(int assetId);
}

[DataContract]
public class CompositeType
{
	bool boolValue = true;
	string stringValue = "Hello ";

	[DataMember]
	public bool BoolValue
	{
		get { return boolValue; }
		set { boolValue = value; }
	}

	[DataMember]
	public string StringValue
	{
		get { return stringValue; }
		set { stringValue = value; }
	}
}
