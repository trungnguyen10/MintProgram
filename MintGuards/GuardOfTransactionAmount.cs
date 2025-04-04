using System;

namespace MintProgram.MintGuards;

public class GuardOfTransactionAmount<TCurrency> : GuardOf<int> where TCurrency : Currency
{
    private readonly Mint<TCurrency>.Purse _purse;

    public static Func<Mint<TCurrency>.Purse, GuardOfTransactionAmount<TCurrency>> Create(int amount)
    {
        return (purse) => new(amount, purse);
    }

    public GuardOfTransactionAmount(int transactionAmount, Mint<TCurrency>.Purse purse)
    {
        Value = transactionAmount;
        _purse = purse;
    }

    protected override int Value { get; }

    protected override int Guard(int value)
    {
        if (value < 0)
        {
            throw new GuardException(new GuardError("Negative Transaction Amount", $"Negative Transaction Amount of {value}"));
        }
        if (value > _purse.Balance)
        {
            throw new GuardException(new GuardError("Transaction Amount Too Large", $"Transaction Amount of {value} is larger than the balance of source purse {_purse.Balance}"));
        }
        return value;
    }
}