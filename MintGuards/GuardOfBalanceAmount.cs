namespace MintProgram.MintGuards;

public class GuardOfBalanceAmount : GuardOf<int>
{
    public GuardOfBalanceAmount(int balance)
    {
        Value = balance;
    }

    protected override int Value { get; }

    protected override int Guard(int value)
    {
        if (value < 0)
        {
            throw new GuardException(new GuardError("Negative Balance", $"Negative Balance of {value}"));
        }
        return value;
    }
}