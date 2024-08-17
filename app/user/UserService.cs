namespace app.user;
public class UserService : IUserService
{
    public void Deposit(UserModel user)
    {
        while (true)
        {
            Console.WriteLine("Please deposit money you would like to play with: ");
            string input = Console.ReadLine();
            if (input == "quit")
            {
                break;
            }

            if (int.TryParse(input, out int deposit))
            {
                user.Balance = deposit;
                return;
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
        }
    }

    public void UpdateBalance(UserModel user, int stake, int winnings)
    {
        Console.WriteLine($"You have won {winnings:C}");
        user.Balance = user.Balance - stake + winnings;
        Console.WriteLine($"New balance is {user.Balance:C}");
    }
}
