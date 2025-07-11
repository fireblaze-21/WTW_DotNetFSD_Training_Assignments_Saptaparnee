using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day3
{
    internal class AccessScore
    {
        public int UserLevelScore;
        public bool IsAdmin;
        public bool HasSpecialPermission;
        public string UserName;

        public int CalculateAccessScore()
        {
            int accessScore = 0;

            if (IsAdmin == true)
            {
                accessScore += 50;
            }
            if (HasSpecialPermission == true)
            {
                accessScore += 25;
            }
            accessScore += UserLevelScore;
            return accessScore;
        }

        public void DisplayAccessControlStatus(int accessScore)
        {
            Console.WriteLine($" User Name :  {UserName}");
            Console.WriteLine($" User Level Score :  {UserLevelScore} ");
            Console.WriteLine($" User Access Score :  {accessScore} ");
            if (accessScore < 25)
            {
                Console.WriteLine($" Access Control Status : Restricted Access");
            }
            else if (accessScore >= 25 && accessScore < 50)
            {
                Console.WriteLine($"  Access Control Status : Standard Access");
            }
            else if (accessScore >= 50 && accessScore < 75)
            {
                Console.WriteLine($" Access Control Status :  Admin Access");
            }
            else
            {
                Console.WriteLine($"  Access Control Status : Full Access");
            }
            
        }
    
    }
}