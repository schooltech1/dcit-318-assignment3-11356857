
using System;

public sealed class SavingsAccount : Account
{
    public SavingsAccount(string accountNumber, decimal initialBalance)
        : base(accountNumber, initialBalance) { }

    public override void ApplyTransaction(Transaction transaction)
    {
        if (transaction.Amount > Balance)
        {
            Console.WriteLine("Insufficient funds.");
        }
        else
        {
            Balance -= transaction.Amount;
            Console.WriteLine($"Transaction successful. New balance: {Balance:C}");
        }
    }
}
