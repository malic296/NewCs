using System.Runtime.Serialization;

namespace WebScrapper.Model;

[DataContract]
public class Currency
{
    [DataMember] public int PricingID { get; set; }
    [DataMember] public int priceForPackage { get; set; }
    [DataMember] public int VPerPackage { get; set; }
    
    [DataMember] public int differenceV { get; set; }
    [DataMember] public int differenceP { get; set; }
    
    [DataMember] public bool overTheLimit { get; set; }
}