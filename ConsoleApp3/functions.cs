namespace WebScrapper;
                                                    
public class functions
{
    public static int howMuch(int goal)
    {
        /*  1000V -> 215CZK
        *  2800V -> 551CZK
        *  5000V -> 879CZK
        *  13500 -> 2159CZK
        */
        
        //(nevidím smysl psát poznámky pro tebe anglicky)
        // pravděpodobně by jsi měl lepší a jednodušší způsob jak to udělat, ale je to funkce, která by měla fungovat, aspoň co jsem testoval, takže účel to splnilo
        
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

    }
    public static float toUSD(int czk, float trans)
    {
        float usd = czk * trans;
        return usd;
    }
}