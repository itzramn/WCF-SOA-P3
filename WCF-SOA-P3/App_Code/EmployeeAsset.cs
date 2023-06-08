using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

[DataContract]
public class EmployeeAsset
{
    [DataMember]
    public int id { get; set; }
    [DataMember]
    public string name { get; set; }
    [DataMember]
    public DateTime assignmentDate { get; set; }
    [DataMember]
    public DateTime deliveryDate { get; set; }
    [DataMember]
    public DateTime? releaseDate { get; set; }
}