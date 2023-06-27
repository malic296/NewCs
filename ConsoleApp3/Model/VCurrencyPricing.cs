using System.Runtime.Serialization;

namespace WebScrapper.Model;

[DataContract]
public class VCurrencyPricing
{
    [DataMember] public int PricingID { get; set; }
    [DataMember] public string Currency { get; set; }
    [DataMember] public double VInPackage { get; set; }
    [DataMember] public double PriceForPackage { get; set; }
    [DataMember] public double RatioPriceVsContent { get; set; }
    
}
[DataContract]
public class PackageOptions
{
    [DataMember] public VCurrencyPricing Pricing { get; set; }
    [DataMember] public int Volume { get; set; }
    
}