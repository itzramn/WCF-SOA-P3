using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

[DataContract]
public class UpdateEmployeeRequest
{
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
}