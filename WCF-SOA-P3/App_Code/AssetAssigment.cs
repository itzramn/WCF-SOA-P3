using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

[DataContract]
public class AssetAssigment
{
    [DataMember]
    public int id { get; set; }
    [DataMember]
    public DateTime deliveryDate { get; set; }
}