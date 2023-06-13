using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

[DataContract]
public class CreateAssetRequest
{
    [DataMember]
    public string name { get; set; }
    [DataMember]
    public string description { get; set; }
}