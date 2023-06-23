using System.Runtime.Serialization;

namespace WebScrapper.Models;

[DataContract]
public class FortniteItem
{
    [DataMember] public string ItemName { get; set; }
    [DataMember] public int ItemPrice { get; set; }
    [DataMember] public DateTime RecordCreateDate { get; set; }

}