using System;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using HtmlAgilityPack;

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

                    HtmlNodeCollection priceNodes = document.DocumentNode.SelectNodes("//p[@class='item-price']");

                    if (priceNodes != null)
                    {
                        int itemCount = priceNodes.Count;
                        Console.WriteLine("Number of Items: " + itemCount);

                        
                        int finalPrice = 0;
                        int x;
                        foreach (HtmlNode priceNode in priceNodes)
                        {
                            string price = priceNode.InnerHtml;

                            string[] prices = price.Split("</noscript>");

                            bool help = false;
                            
                            foreach (var p in prices)
                            {
                                
                                if (help == true)
                                {

                                    x = int.Parse(p.Replace(",", "").Trim());
                                    finalPrice = finalPrice + x;
                                }
                                else
                                {
                                    
                                }

                                help = true;
                                
                            }
                            
                        }
                        Console.WriteLine("Price in V-bucks: " + finalPrice);
                        DateTime today = DateTime.Today;
                        string dateF = today.ToString("M/d/yyyy");
                        Console.WriteLine("Todays date: " + dateF);
                        
                        
                        
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
                throw;
            }
        }

    }
}