// See https://aka.ms/new-console-template for more information
using MintProgram;
using MintProgram.MintGuards;

var purse1 = Mint<USDollar>.MakePurse(new GuardOfBalanceAmount(100));
var purse2 = purse1.Sprout();

System.Console.WriteLine($"{nameof(purse1)} has balance {purse1.Balance}");
System.Console.WriteLine($"{nameof(purse2)} has balance {purse2.Balance}");

var transferAmount = 45;
System.Console.WriteLine($"Transfer {transferAmount} from {nameof(purse1)} to {nameof(purse2)}");
purse2.Deposit(transferAmount, purse1);
System.Console.WriteLine($"{nameof(purse1)} has balance {purse1.Balance}");
System.Console.WriteLine($"{nameof(purse2)} has balance {purse2.Balance}");