

using System;
using System.Collections.Generic;

public class FinanceApp
{
    private List<Transaction> _transactions = new List<Transaction>();
    private SavingsAccount _account;

    public void Run()
    {
        Console.WriteLine("Welcome to the Finance Management System!");

        // Get account details
        Console.Write("Enter your account number: ");
        string accountNumber = Console.ReadLine();

        Console.Write("Enter your starting balance: ");
        decimal initialBalance;
        while (!decimal.TryParse(Console.ReadLine(), out initialBalance) || initialBalance < 0)
        {
            Console.Write("Invalid amount. Enter a positive number: ");
        }

        _account = new SavingsAccount(accountNumber, initialBalance);

        // Main menu loop
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Add Transaction");
            Console.WriteLine("2. View Balance");
            Console.WriteLine("3. View Transactions");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option (1-4): ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    AddTransaction();
                    break;
                case "2":
                    Console.WriteLine($"\nCurrent Balance: {_account.Balance:C}");
                    break;
                case "3":
                    PrintTransactions();
                    break;
                case "4":
                    exit = true;
                    Console.WriteLine("Exiting the system. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter 1-4.");
                    break;
            }
        }
    }

    private void AddTransaction()
    {
        Console.WriteLine("\n--- Add New Transaction ---");

        Console.Write("Enter amount: ");
        decimal amount;
        while (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
        {
            Console.Write("Invalid amount. Enter a positive number: ");
        }

        Console.Write("Enter category (e.g., Groceries, Utilities): ");
        string category = Console.ReadLine();

        Console.WriteLine("Select Payment Method:");
        Console.WriteLine("1. Mobile Money");
        Console.WriteLine("2. Bank Transfer");
        Console.WriteLine("3. Crypto Wallet");

        ITransactionProcessor processor = null;
        while (processor == null)
        {
            Console.Write("Enter choice (1-3): ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    processor = new MobileMoneyProcessor();
                    break;
                case "2":
                    processor = new BankTransferProcessor();
                    break;
                case "3":
                    processor = new CryptoWalletProcessor();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select 1, 2, or 3.");
                    break;
            }
        }

        // Assign a transaction ID as count + 1
        int transactionId = _transactions.Count + 1;
        var transaction = new Transaction(transactionId, DateTime.Now, amount, category);

        processor.Process(transaction);
        _account.ApplyTransaction(transaction);
        _transactions.Add(transaction);
    }

    private void PrintTransactions()
    {
        Console.WriteLine("\n--- All Transactions ---");
        if (_transactions.Count == 0)
        {
            Console.WriteLine("No transactions found.");
            return;
        }

        foreach (var tx in _transactions)
        {
            Console.WriteLine($"#{tx.Id}: {tx.Category} - {tx.Amount:C} on {tx.Date}");
        }
    }
}


