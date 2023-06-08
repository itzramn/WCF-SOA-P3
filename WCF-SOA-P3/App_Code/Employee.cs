using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;


[DataContract]
public class Employee
{
    [DataMember]
    public int employeeId { get; set; }
    [DataMember]
    public int personId { get; set; }
    [DataMember]
    public int employeeNumber {get; set; }
    [DataMember]
    public DateTime entryDate { get; set; }
    [DataMember]
    public string name { get; set; }
    [DataMember]
    public string lastName { get; set; }
    [DataMember]
    public string curp { get; set; }
    [DataMember]
    public DateTime birthDate { get; set; }
    [DataMember]
    public string email { get; set; }
    [DataMember]
    public ICollection<EmployeeAsset> assets { get; set; }

}