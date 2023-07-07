using HtmlAgilityPack;
using System;
using System.Net.Http;
using System.Runtime.InteropServices.JavaScript;
using WebScrapper.Model;
using System.ComponentModel;
using System.Collections.Generic;


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
            new() { PricingID = 1, priceForPackage = 215, VPerPackage = 1000, differenceV = 0, differenceP = 0, overTheLimit = false},
            new() { PricingID = 2, priceForPackage = 551, VPerPackage = 2800, differenceV = 0, differenceP = 0, overTheLimit = false },
            new() { PricingID = 3, priceForPackage = 879, VPerPackage = 5000, differenceV = 0, differenceP = 0, overTheLimit = false },
            new() { PricingID = 4, priceForPackage = 2159, VPerPackage = 13500, differenceV = 0, differenceP = 0, overTheLimit = false }
        };

        //Creation of Dict to help with combining the values
        Dictionary<int, (int, int)> PricingDict = new Dictionary<int, (int, int)>();

        //Adding currencies into my dict
        int indexInDict = 0;
        foreach (Currency item in PricingOptions)
        {
            PricingDict.Add(indexInDict, (item.priceForPackage, item.VPerPackage));
            indexInDict++;
        }

        //Creating combinations and passing it as a Currency item into the Pricing options list
        int biggestKey = PricingDict.Keys.Max();
        foreach (KeyValuePair<int ,(int, int)> kvp in PricingDict)
        {
            int index = kvp.Key;
            int index_increment = kvp.Key;
            //making combinations        --------------------->                            //00
            while (index_increment < biggestKey)                                           //01
            {                                                                              //02
                (int price1, int VBucks1) = PricingDict[index];                            //03
                (int price2, int VBucks2) = PricingDict[index_increment];                  //11
                int addedPrice = price1 + price2;                                          //12
                int addedVBucks = VBucks1 + VBucks2;                                       //13...

                int largestID = PricingOptions.Max(currency => currency.PricingID);
                PricingOptions.Add(new() {PricingID = (largestID + 1), priceForPackage = addedPrice, VPerPackage = addedVBucks, differenceV = 0, differenceP = 0, overTheLimit = false});
                index_increment++;  
                
            }
            
        }

        //Tested if combinations are being created well
        /*
        string test = "";
        foreach (Currency item in PricingOptions)
        {
            test = test + $"ID: {item.PricingID}, price: {item.priceForPackage}, VBucks: {item.VPerPackage}, diffV: {item.differenceV}, diffP: {item.differenceP} \n";
        }
        
        return test;
        */      

        int sum = 0;
        int sumPrice = 0;
        bool limitDone = false;
        int vDiff;
        int finalAddPrice;
        int possibleAddID = 1;
        int possibleAddPrice;
        int finalAddVBucks;
        int finalAddID;
        int min;

        int t1 = 0;
        int t2 = 0;
        int t3 = 0;
        int t4 = 0;
        string tFinal = "";
        
        

        List<Currency> optionsWithMinDifferenceV = new List<Currency>();
        List<Currency> optionsWithMinDifferenceP = new List<Currency>();

        while (sum < goal)
        {

            foreach (Currency package in PricingOptions)
            {
                sum = sum + package.VPerPackage;
                vDiff = goal - sum;
                if (vDiff > 0)
                {
                    package.differenceV = vDiff;
                    package.overTheLimit = false;
                }
                else
                {
                    limitDone = true;
                    package.differenceP = package.priceForPackage;
                    package.overTheLimit = true;

                }
                sum = sum - package.VPerPackage;
                
            }

            if (limitDone == true)
            {
                int minDiffP = PricingOptions.Where(option => option.overTheLimit == true).Min(option => option.differenceP);
                optionsWithMinDifferenceP = PricingOptions.Where(option => option.differenceP == minDiffP && option.overTheLimit == true).ToList();
                
            }
            else
            {
                int minDiffV = PricingOptions.Min(option => option.differenceV);
                optionsWithMinDifferenceV = PricingOptions.Where(option => option.differenceV == minDiffV).ToList();
                
            }

            if (limitDone == false)
            {
                foreach (Currency option in optionsWithMinDifferenceV)
                {
                    sum = sum + option.VPerPackage;
                    sumPrice = sumPrice + option.priceForPackage;

                }
                
            }
            else
            {
                foreach (Currency option in optionsWithMinDifferenceP)
                {
                    min = PricingOptions.Min(option => option.priceForPackage);
                    
                    List<Currency> smallerPossibleAddInfo = new List<Currency>();
                    List<Currency> minimumInfo = new List<Currency>();

                    smallerPossibleAddInfo = PricingOptions.Where(opt => opt.PricingID == (option.PricingID - 1)).ToList();
                    minimumInfo = PricingOptions.Where(opt => opt.priceForPackage == min).ToList();

                    int smallerPrice = 0;
                    int smallerVBucks = 0;
                    int minimumPrice = 0;
                    int minimumVBucks = 0;
                    
                    foreach (Currency i in smallerPossibleAddInfo)
                    {
                        smallerPrice = i.priceForPackage;
                        smallerVBucks = i.VPerPackage;
                    }
                    
                    foreach (Currency i in minimumInfo)
                    {
                        minimumPrice = i.priceForPackage;
                        minimumVBucks = i.VPerPackage;
                    }

                    int priceChallenger = minimumPrice + smallerPrice;
                    int VBucksChallenger = minimumVBucks + smallerVBucks;

                    /*
                    t1 = priceChallenger;
                    t2 = option.priceForPackage;
                    t3 = sum + VBucksChallenger;
                    t4 = goal;

                    tFinal = tFinal + $"priceChallenger = {t1}, PriceOptional = {t2}, sum + VBucksChalleger = {t3}, goal = {goal}";
                    */
                    
                if (priceChallenger < option.priceForPackage && sum + VBucksChallenger >= goal)
                    {
                        finalAddPrice = priceChallenger;
                        finalAddVBucks = VBucksChallenger;
                    }
                    else
                    {
                        finalAddPrice = option.priceForPackage;
                        finalAddVBucks = option.VPerPackage;
                    }
                    
                    sum = sum + finalAddVBucks;
                    sumPrice = sumPrice + finalAddPrice;
                    break;
                    
                }
            }

        }

        //return tFinal;
        return sumPrice;

    }

}