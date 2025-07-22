using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class CheckValidCoupon
    {
        static void Main()
        {
            // Step 1: Get the predefined set of valid coupon codes
            HashSet<string> validCoupons = GetValidCouponCodes();

            // Step 2: Prompt user for input
            string userCoupon = GetUserInput();

            // Step 3: Convert input to uppercase for consistent comparison
            userCoupon = userCoupon.ToUpper();

            // Step 4: Validate the coupon code
            bool isValid = IsValidCoupon(userCoupon, validCoupons);

            // Step 5: Display the result
            DisplayResult(isValid);
            Console.ReadLine();
        }

        // Method to return a HashSet of valid coupon codes (all uppercase)
        static HashSet<string> GetValidCouponCodes()
        {
            return new HashSet<string>
        {
            "SAVE10",
            "FREESHIP",
            "DISCOUNT20",
            "FLAT100"
        };
        }

        // Method to get user input from console
        static string GetUserInput()
        {
            Console.Write("Enter your coupon code: ");
            string input = Console.ReadLine()?.Trim() ?? "";
            return input;
        }

        // Method to check if the coupon code exists in the HashSet
        static bool IsValidCoupon(string coupon, HashSet<string> validCoupons)
        {
            return validCoupons.Contains(coupon);
        }

        // Method to display result based on validity
        static void DisplayResult(bool isValid)
        {
            if (isValid)
            {
                Console.WriteLine("Coupon is valid! Applying discount...");
            }
            else
            {
                Console.WriteLine("Invalid coupon code.");
            }
        }
    }

}


