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

	[OperationContract]
	string GetDataEmployeeId(int employeeId);
	[OperationContract]
    void CreateEmployee(CreateEmployeeRequest request);
    [OperationContract]
    string DeleteEmployeeId(int employeeId);

	[OperationContract]
	string SendEmailReminders();

	[OperationContract]
	string ValidateEmployeeLogin(LoginRequest request);

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
