using System;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Linq;
using System.Collections.Generic;


namespace WebScrapper
{
    public class Program
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task Main()
        {
            String answer = "t";
            while (answer != "q")
            {

                Console.WriteLine("q - quit, t - shows recent item shop, a - shows statistics of all cosmetics, v - V-bucks pricing"); 
                answer = Console.ReadLine();
                
                
                if (answer == "t") //Todays item shop
                {
                    Console.WriteLine("--------------- TODAYS ITEM SHOP ---------------");
                    try
                    {
                        String url1 = "https://fnbr.co/shop";
                        HttpResponseMessage res1 = await httpClient.GetAsync(url1);

                        if (res1.IsSuccessStatusCode)
                        {
                            String res1Data = await res1.Content.ReadAsStringAsync();
                            HtmlDocument document1 = new HtmlDocument();
                            document1.LoadHtml(res1Data);

                            HtmlNodeCollection priceNodes = document1.DocumentNode.SelectNodes("//p[@class='item-price']");
                            HtmlNodeCollection rarityNodes = document1.DocumentNode.SelectNodes("//a[contains(@class, 'item-display')]");
                            
                            
                            //script for todays rarities
                            if (rarityNodes != null)
                            {
                                
                                String raritiesToday = "";
                                foreach (HtmlNode rarNode in rarityNodes)
                                {
                                    foreach (HtmlAttribute attribute in rarNode.Attributes)
                                    {
                                        if (attribute.Name == "class")
                                        {

                                            String[] helpful = attribute.Value.Split("rarity-");
                                            if (raritiesToday.Length < 1)
                                            {
                                                raritiesToday = raritiesToday + helpful[1];
                                            }
                                            else
                                            {
                                                raritiesToday = raritiesToday + "," + helpful[1];
                                            }
                                            
                                        }
                                    }
                                    
                                }
                               
                                String[] raritiesTodayArray = raritiesToday.Split(",");
                                Dictionary<String, int> raritiesTodayDictFinal = new Dictionary<string, int>();

                                foreach (String o in raritiesTodayArray)
                                {
                                    if (raritiesTodayDictFinal.ContainsKey(o))
                                    {
                                        raritiesTodayDictFinal[o] = raritiesTodayDictFinal[o] + 1;
                                    }
                                    else
                                    {
                                        raritiesTodayDictFinal[o] = 1;
                                    }
                                    
                                }

                                foreach (KeyValuePair<String, int> kvp in raritiesTodayDictFinal)
                                {
                                    String key = kvp.Key;
                                    int value = kvp.Value;
                                    
                                    Console.WriteLine($"Number of cosmetics in todays item shop... {key.ToUpper()} rarity: {value}");
                                    
                                }
                                
                                
                            }
                            
                            //Script for prices
                            if (priceNodes != null)
                            {
                                int itemCount = priceNodes.Count;
                                Console.WriteLine("");
                                Console.WriteLine("Overall todays item shop has: " + itemCount + " items");

                                
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

                                
                                int cheap = functions.cheapestWayToBuyVBucks(21301);
                                DateTime today = DateTime.Today;
                                string dateF = today.ToString("M/d/yyyy");
                                Console.WriteLine("Todays date: " + dateF);
                                Console.WriteLine("Amout of V-bucks to buy whole Item Shop: " + finalPrice);
                                Console.WriteLine("Cheapest way to buy that amount of V-bucks in CZK: " + cheap);
                                
                                
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("Error 1 from API");  
                        }

                        
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    
                }
                else if (answer == "a") //All skins
                {
                    Console.WriteLine("--------------- ALL TIME SKINS ---------------");
                    try
                    {
                        
                        String url2 = "https://www.fortniteskin.com/all";

                        HttpResponseMessage res2 = await httpClient.GetAsync(url2);

                        if (res2.IsSuccessStatusCode)
                        {
                            String res2Data = await res2.Content.ReadAsStringAsync();
                            HtmlDocument document2 = new HtmlDocument();
                            document2.LoadHtml(res2Data);

                            HtmlNodeCollection elements = document2.DocumentNode.SelectNodes("//li[contains(@class, 'items__list__item')]");
                            int fullCount = 0;
                            String rarities = "";
                            

                            if (elements != null)
                            {
                                foreach (HtmlNode element in elements)
                                {
                                    fullCount++;
                                    foreach (HtmlAttribute attribute in element.Attributes)
                                    {
                                        if (attribute.Name == "data-rarity")
                                        {
                                            if (rarities.Length > 0)
                                            {
                                                rarities = rarities + "," + attribute.Value;
                                            }
                                            else
                                            {
                                                rarities = rarities + attribute.Value;
                                            }
                                            
                                        }
                                    }
                                }

                                String[] rarityCount = rarities.Split(",");
                                Dictionary<String, int> rarityFinal = new Dictionary<string, int>();
                                
                                //This might look weird... I found a better approach, but I did not want to copy it
                                foreach (String o in rarityCount)
                                {
                                    if (rarityFinal.ContainsKey(o))
                                    {
                                        rarityFinal[o] = rarityFinal[o] + 1;
                                    }
                                    else
                                    {
                                        rarityFinal[o] = 1;
                                    }
                                }

                                foreach (KeyValuePair<String, int> kvp in rarityFinal)
                                {
                                    String key = kvp.Key;
                                    int value = kvp.Value;
                                    
                                    Console.WriteLine($"Number of cosmetics in {key.ToUpper()} fortnite rarity: {value}");
                                    
                                }
                                Console.WriteLine("");
                                Console.WriteLine("Full cosmetics count in fortnite: " + fullCount);


                            }
                        }

                        else
                        {
                            Console.WriteLine("Error 2 from API");
                        }
                        
                        
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                            
                }
                else if (answer == "v") //V-Bucks USD
                {
                    
                    
                    
                    
                    
                }
                else if (answer == "q")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("q - quit, t - shows recent item shop, a - shows statistics of all cosmetics, v - V-bucks pricing"); 
                    answer = Console.ReadLine();
                }
                        
                    
                        
            }

        }

    }
}