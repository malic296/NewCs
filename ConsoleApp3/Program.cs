using System;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using HtmlAgilityPack;
using WebScrapper.Models;

namespace WebScrapper
{
    public class Program
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task Main()
        {
            try
            {
                String url = "https://fnbr.co/shop";
                HttpResponseMessage res = await httpClient.GetAsync(url);

                if (res.IsSuccessStatusCode)
                {
                    String resData = await res.Content.ReadAsStringAsync();
                    HtmlDocument document = new HtmlDocument();
                    document.LoadHtml(resData);

                    HtmlNodeCollection priceNodes = document.DocumentNode.SelectNodes("//div[@class='content-box']");
                    List<FortniteItem> fortniteItems = new List<FortniteItem>();
                    
                    Console.WriteLine($"Todays date: {DateTime.Today.ToString("dd/MM/yyyy")}");
                    

                    if (priceNodes != null)
                    {
                        Console.WriteLine($"Number of Items: {priceNodes.Count}");
                        foreach (HtmlNode priceNode in priceNodes)
                        {
                            try
                            {
                                string itemName = priceNode.ChildNodes[1].InnerText;
                                int itemPrice = int.Parse(priceNode.ChildNodes[3].InnerText.Replace(",", "").Trim());
                                fortniteItems.Add(new FortniteItem(){ItemName = itemName, ItemPrice = itemPrice, RecordCreateDate = DateTime.Today});

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Parsing failed for {priceNode}. Error: {e}");
                                throw;
                            }
                            
                        }

                        foreach (var item in fortniteItems)
                        {
                            Console.WriteLine($"{item.ItemName} - {item.ItemPrice}V");
                        }

                        Console.WriteLine($"Total price in V-bucks: {fortniteItems.Sum(item => item.ItemPrice)}");
                        
                    }
                }
                else
                {
                    Console.WriteLine("Error from API");  
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

    }
}