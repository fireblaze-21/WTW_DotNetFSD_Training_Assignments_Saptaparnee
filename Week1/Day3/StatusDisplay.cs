
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Day3
{
    class StatusDisplay
    {
        static void Main()
        {
            int userScore;
            string userName;
            AccessScore obj = new AccessScore();
            Console.WriteLine("Enter User Name: ");
            userName= Console.ReadLine();
            Console.WriteLine("Enter User Score: ");
            userScore= int.Parse(Console.ReadLine());
            Console.WriteLine("Is User Admin (Enter true or false) :");
            string input1 = Console.ReadLine();
            bool admin = bool.Parse(input1);
            Console.WriteLine("Has User Special Permission (Enter true or false) :");
            string input2 = Console.ReadLine();
            bool permission = bool.Parse(input2);
            obj.UserName = userName;
            obj.UserLevelScore = userScore;
            obj.IsAdmin = admin;
            obj.HasSpecialPermission = permission;

            if (userScore < 1 || userScore > 25)
            {
                Console.WriteLine("Invalid User Score");
                Console.ReadLine();
                return;
            }

            int calAccessScore = obj.CalculateAccessScore();
            obj.DisplayAccessControlStatus(calAccessScore);


            Console.ReadLine();
        }
    }

}
