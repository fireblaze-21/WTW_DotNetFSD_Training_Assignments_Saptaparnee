using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class BankAccount
    {
        public string AccountNumber { get; set; }
        public string AccountHolder { get; set; }
        public double Balance { get; set; }
       

        public BankAccount(string accountNumber, string accountHolder, double balance)
        {
            this.AccountNumber = accountNumber;
            this.AccountHolder = accountHolder;
            this.Balance = balance > 0 ? balance : 0; 
        }

        public void DisplayAccountInfo()
        {
            Console.WriteLine($"----------------------Account Details-----------------");
            Console.WriteLine($" Account Holder Name : {AccountHolder}");
            Console.WriteLine($" Account Number : {AccountNumber}");
            Console.WriteLine($" Balance INR : {Balance}");

        }
    }
    class BankAccountManagement
    {
        // Deposit Amount
        public static void Deposit(BankAccount bankAccount , double depositAmount)
        { 
            if(depositAmount<0)
            {
                Console.WriteLine($"Invalid Deposit Amount. ");
                return;
            }
            bankAccount.Balance += depositAmount;
        }

        // Withdraw Amount
        public static void Withdraw(BankAccount bankAccount , double withdrawAmount)
        {
            if(withdrawAmount<0)
            {
                Console.WriteLine($"Invalid Deposit Amount.");
                return;
            }
            if(bankAccount.Balance<withdrawAmount)
            {
                Console.WriteLine($" Withdrawal Amount Exceeds Current Balance. ");
                return;
            }
            bankAccount.Balance -= withdrawAmount;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount bankaccount = new BankAccount("12345", "Narasimha", 1000);
            
            bankaccount.DisplayAccountInfo();
            Console.WriteLine($"-------------------Deposit INR 500-----------------------");
            BankAccountManagement.Deposit(bankaccount, 500);

            bankaccount.DisplayAccountInfo();
            Console.WriteLine($"--------------------Withdraw INR 200---------------------");
            BankAccountManagement.Withdraw(bankaccount, 200);

            bankaccount.DisplayAccountInfo();
            Console.WriteLine($"--------------------Withdraw INR 2000---------------------");
            BankAccountManagement.Withdraw(bankaccount, 2000);

            bankaccount.DisplayAccountInfo();

            Console.ReadLine();
        }
    }
}
