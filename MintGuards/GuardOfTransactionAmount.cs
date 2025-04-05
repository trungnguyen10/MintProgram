namespace MintProgram.MintGuards;

public class GuardOfTransactionAmount : GuardOf<int>
{
    private readonly int _sourceBalance;

    public static Func<int, GuardOfTransactionAmount> Create(int amount)
    {
        return (sourceBalance) => new(amount, sourceBalance);
    }

    private GuardOfTransactionAmount(int transactionAmount, int sourceBalance)
    {
        Value = transactionAmount;
        _sourceBalance = sourceBalance;
    }

    protected override int Value { get; }

    protected override int Guard(int value)
    {
        var errors = new List<GuardError>();
        if (value < 0)
        {
            errors.Add(new GuardError("Negative Transaction Amount", $"Negative Transaction Amount of {value}"));
        }
        if (value > _sourceBalance)
        {
            errors.Add(new GuardError("Transaction Amount Too Large", $"Transaction Amount of {value} is larger than the balance of source purse {_sourceBalance}"));
        }

        if (errors is [GuardError head, .. var tails])
        {
            throw new GuardException(head, tails);
        }

        return value;
    }
}