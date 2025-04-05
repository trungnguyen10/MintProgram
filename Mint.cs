using MintProgram.MintGuards;

namespace MintProgram;

public class Mint<TCurrency> where TCurrency : Currency
{
    public static Purse MakePurse(GuardOfBalanceAmount balance)
    {
        return new(balance);
    }

    public class Purse
    {
        private readonly Lock _balanceLock = new();
        private int _balance;

        public int Balance => _balance;

        internal Purse(GuardOfBalanceAmount balanceGuard)
        {
            _balance = balanceGuard.Guard();
        }

        public Purse Sprout()
        {
            return new(new GuardOfBalanceAmount(0));
        }

        private void Decr(Func<int, GuardOfTransactionAmount> amountGuardFunc)
        {
            lock (_balanceLock)
            {
                _balance -= amountGuardFunc(_balance).Guard();
            }
        }

        public void Deposit(int amount, Purse src)
        {
            lock (_balanceLock)
            {
                src.Decr(GuardOfTransactionAmount.Create(amount));
                _balance += amount;
            }
        }
    }
}
