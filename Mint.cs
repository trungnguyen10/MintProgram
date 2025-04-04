using System;
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

        private void Decr(Func<Purse, GuardOfTransactionAmount<TCurrency>> amountGuardFunc)
        {
            lock (_balanceLock)
            {
                _balance -= amountGuardFunc(this).Guard();
            }
        }

        public void Deposit(int amount, Purse src)
        {
            lock (_balanceLock)
            {
                _balance += amount;
                src.Decr(GuardOfTransactionAmount<TCurrency>.Create(amount));
            }
        }
    }
}
