using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class BankAccount
    {
        public string AccountHolder { get; }
        public decimal Balance { get; private set; }

        public BankAccount (string accountHolder , decimal balance)
        {
            if (balance < 0)
            {
                throw new ArgumentException("Initial Balance cannot be negative");
            }
            this.AccountHolder = accountHolder;
            this.Balance = balance;
        }
        public void Deposit(decimal amount)
        {
            if(amount<0)
            {
                throw new ArgumentException("Deposit amount cannot be negative");
            }
            Balance += amount;
        }
        public void Withdraw(decimal amount)
        {
            if(amount<0)
            {
                throw new ArgumentException("Withdrawal amount cannot be negative");

            }
            if(amount>Balance)
            {
                throw new InvalidOperationException("Insufficient Balance for Withdrawal");
            }
            Balance -= amount;
        }
    }
    internal class BankAccountMAnagement
    {
        static void Main()
        {
            try
            {
                BankAccount ba = new BankAccount("Saptaparnee", 7000);
                Console.WriteLine($"Account Holder Name : {ba.AccountHolder} , Initial Balance : {ba.Balance}");
                //Valid Deposit
                try
                {
                    Console.WriteLine(" Depositing INR 1000");
                    ba.Deposit(1000);
                    Console.WriteLine($"Available Balance ; {ba.Balance}");
                }
                catch(ArgumentException ex)
                {
                    Console.WriteLine($" Deposit Error : {ex.Message}");
                }
                Console.WriteLine("--------------------------------------");
                //InValid Deposit
                try
                {
                    Console.WriteLine(" Depositing -1000");
                    ba.Deposit(-1000);
                    Console.WriteLine($"Deposited  . Available Balance ; {ba.Balance}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($" Deposit Error : {ex.Message}");
                }
                Console.WriteLine("--------------------------------------");
                //Valid Withdrawal
                try
                {
                    Console.WriteLine(" Withdrawing INR 2000");
                    ba.Withdraw(2000);
                    Console.WriteLine($"Withdrawn INR 2000. Available Balance ; {ba.Balance}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($" Withdraw Error : {ex.Message}");
                }
                Console.WriteLine("--------------------------------------");
                //InValid Withdrawal
                try
                {
                    Console.WriteLine("Withdrawing -2000");
                    ba.Withdraw(-2000);
                    Console.WriteLine($"Withdrawn . Available Balance ; {ba.Balance}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($" Withdraw Error : {ex.Message}");
                }
                Console.WriteLine("--------------------------------------");
                //Insufficient Balance
                try
                {
                    Console.WriteLine("Withdrawing 10000");
                    ba.Withdraw(10000);
                    Console.WriteLine($"Withdrawn INR 10000 . Available Balance ; {ba.Balance}");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($" Withdraw Error : {ex.Message}");
                }
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine($" Account Creation error : {ex.Message}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($" Other errors : {ex.Message}");
            }
            finally
            {
                Console.WriteLine("-------------------Successful attempt----------------------------");
            }
            Console.ReadLine();
        }
    }
}
