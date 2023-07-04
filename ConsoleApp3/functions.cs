using HtmlAgilityPack;
using System;
using System.Net.Http;
using System.Runtime.InteropServices.JavaScript;
using WebScrapper.Model;
using System.ComponentModel;


namespace WebScrapper;
                                                    
public class functions
{
    public static int cheapestWayToBuyVBucks(int goal)
    {
        /*  1000V -> 215CZK
        *  2800V -> 551CZK
        *  5000V -> 879CZK
        *  13500 -> 2159CZK
        */

        List<Currency> PricingOptions = new List<Currency>()
        {
            new() { PricingID = 1, priceForPackage = 215, VPerPackage = 1000, difference = 0},
            new() { PricingID = 2, priceForPackage = 551, VPerPackage = 2800, difference = 0 },
            new() { PricingID = 3, priceForPackage = 879, VPerPackage = 5000, difference = 0 },
            new() { PricingID = 4, priceForPackage = 2159, VPerPackage = 13500, difference = 0 }
        };
        
        //Still having problems when it comes to working with models
        //------------------------------------------------------------------------------------------------------
        List<int> IDs = new List<int>();
        foreach (Currency id in PricingOptions)
        {
            IDs.Add(id.PricingID);
        }
        
        int reps = IDs.Count;
        int starting_index = 0;
        foreach (Currency pack in PricingOptions)
        {
            int count = 0;
            int helpingNum = 0;
            while (count < reps)
            {
                //ID of combination
                String id = pack.PricingID.ToString() + ((pack.PricingID) + helpingNum).ToString();
                int finalID = int.Parse(id);
                
                //Price of combination
                List<Currency> prices =
                    PricingOptions.Where(option => option.PricingID == pack.PricingID || option.PricingID == (pack.PricingID + helpingNum)).ToList();

                int PriceForCombination = prices[0].priceForPackage + prices[1].priceForPackage;
                    
                //Vbucks within combination
                List<Currency> VBucks = 
                    PricingOptions.Where(option => option.PricingID == pack.PricingID || option.PricingID == (pack.PricingID + helpingNum)).ToList();

                int VBucksWithinCombination = VBucks[0].VPerPackage + VBucks[1].VPerPackage;

                PricingOptions.Add(new() { PricingID = finalID, priceForPackage = PriceForCombination, VPerPackage = VBucksWithinCombination, difference = 0 });
                helpingNum++;
                count++;
                prices.Clear();
                VBucks.Clear();
            }

            starting_index++;
            reps--;
        }
        //------------------------------------------------------------------------------------------------------

        int sum = 0;
        int sumPrize = 0;
        
        while (sum < goal)
        {

            foreach (Currency package in PricingOptions)
            {
                sum = sum + package.VPerPackage;
                package.difference = goal - sum;
                package.difference = Math.Abs(package.difference);
                sum = sum - package.VPerPackage;
            }

            int minDiff = PricingOptions.Min(option => option.difference);
            List<Currency> optionsWithMinDifference =
                PricingOptions.Where(option => option.difference == minDiff).ToList();

            foreach (Currency option in optionsWithMinDifference)
            {
                sum = sum + option.VPerPackage;
                sumPrize = sumPrize + option.priceForPackage;

                if (sum>=goal)
                {
                    break;
                }
            }
            
        }

        return sumPrize;


    }
}

/*
        int prize = 0;
        int goal_help = 0;
        int difference = 0;
        List<int> differences = new List<int>();

        Dictionary<int, int> comparison = new Dictionary<int, int>();
        comparison.Add(215, 1000);
        comparison.Add(551, 2800);
        comparison.Add(879, 5000);
        comparison.Add(2159, 13500);

        while (goal_help < goal)
        {
            goal_help = goal_help + comparison[2159];
            if (goal_help > goal)
            {
                difference = (goal_help - goal);
                differences.Add(difference);
            }
            else
            {
                differences.Add(0);
            }
            goal_help = goal_help - comparison[2159] + comparison[879];
            if (goal_help > goal)
            {
                difference = goal_help - goal;
                differences.Add(difference);
            }
            else
            {
                differences.Add(1);
            }
            goal_help = goal_help - comparison[879] + comparison[551];
            if (goal_help > goal)
            {
                difference = goal_help - goal;
                differences.Add(difference);
            }
            else
            {
                differences.Add(1);
            }
            goal_help = goal_help - comparison[551] + comparison[215];
            if (goal_help > goal)
            {
                difference = goal_help - goal;
                differences.Add(difference);
            }
            else
            {
                differences.Add(1);
            }
            goal_help = goal_help - comparison[215];


            int min = differences.Min();
            int index = differences.IndexOf(min);
            if (index == 0)
            {
                goal_help = goal_help + comparison[2159];
                prize = prize + 2159;
            }
            else if (index == 1)
            {
                goal_help = goal_help + comparison[879];
                prize = prize + 879;
                
            }
            else if (index == 2)
            {
                goal_help = goal_help + comparison[551];
                prize = prize + 551;
                
            }
            else
            {
                goal_help = goal_help + comparison[215];
                prize = prize + 215;
                
            }
            differences.Clear();

        }
        return prize;
        */
