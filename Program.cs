// See https://aka.ms/new-console-template for more information
using MintProgram;
using MintProgram.MintGuards;

var purse1 = Mint<USDollar>.MakePurse(new GuardOfBalanceAmount(100));
var purse2 = purse1.Sprout();
var purse3 = purse1.Sprout();
var purse4 = purse1.Sprout();
var purse5 = purse1.Sprout();
var purse6 = purse1.Sprout();
var purse7 = purse1.Sprout();
var purse8 = purse1.Sprout();
var purse9 = purse1.Sprout();
var purse10 = purse1.Sprout();

IEnumerable<Mint<USDollar>.Purse> purses = [purse1, purse2, purse3, purse4, purse5, purse6, purse7, purse8, purse9, purse10];

PrintPurses();

IEnumerable<Task> tasks = [
    new Task(() => Transfer(15,purse1,nameof(purse1),purse2, nameof(purse2))),
    new Task(() => Transfer(5,purse1,nameof(purse1),purse2, nameof(purse2))),
    new Task(() => Transfer(30,purse1,nameof(purse1),purse3, nameof(purse3))),
    new Task(() => Transfer(40,purse1,nameof(purse1),purse2, nameof(purse2))),
    new Task(() => Transfer(1,purse1,nameof(purse1),purse3, nameof(purse3))),
    new Task(() => Transfer(13,purse1,nameof(purse1),purse2, nameof(purse2))),
    new Task(() => Transfer(39,purse1,nameof(purse1),purse5, nameof(purse5))),
    new Task(() => Transfer(14,purse1,nameof(purse1),purse2, nameof(purse2))),
    new Task(() => Transfer(2,purse1,nameof(purse1),purse6, nameof(purse6))),
    new Task(() => Transfer(2,purse1,nameof(purse1),purse7, nameof(purse7))),
    new Task(() => Transfer(2,purse1,nameof(purse1),purse7, nameof(purse7))),
];

tasks = tasks.Select(t => {t.Start(); return t;});
await Task.WhenAll(tasks);

PrintPurses();

static void Transfer(int amount, Mint<USDollar>.Purse src, string srcName, Mint<USDollar>.Purse des, string desName)
{
    try
    {
        System.Console.WriteLine($"Transfer {amount} from {srcName} to {desName}");
        des.Deposit(amount, des);
    }
    catch (Exception ex)
    {
        System.Console.WriteLine(ex.Message);
    }
}

void PrintPurses()
{
    System.Console.WriteLine($"{nameof(purse1)} has balance {purse1.Balance}");
    System.Console.WriteLine($"{nameof(purse2)} has balance {purse2.Balance}");
    System.Console.WriteLine($"{nameof(purse3)} has balance {purse3.Balance}");
    System.Console.WriteLine($"{nameof(purse4)} has balance {purse4.Balance}");
    System.Console.WriteLine($"{nameof(purse5)} has balance {purse5.Balance}");
    System.Console.WriteLine($"{nameof(purse6)} has balance {purse6.Balance}");
    System.Console.WriteLine($"{nameof(purse7)} has balance {purse7.Balance}");
    System.Console.WriteLine($"{nameof(purse8)} has balance {purse8.Balance}");
    System.Console.WriteLine($"{nameof(purse9)} has balance {purse9.Balance}");
    System.Console.WriteLine($"{nameof(purse10)} has balance {purse10.Balance}");
    System.Console.WriteLine($"Total balance {purses.Sum(p => p.Balance)}");

}