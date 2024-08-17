using app.symbols;
using app.user;

namespace app.machine
{
    public class MachineService : IMachineService
    {
        private readonly IUserService _userService;

        public MachineService(IUserService userService)
        {
            _userService = userService;
        }

        public void Run(MachineModel machine, UserModel user)
        {
            // Get the users deposit
            _userService.Deposit(user);

            // Run the machine
            while (user.Balance > 0)
            {
                // get the stake
                int stake = GetStake(user.Balance);

                // simulate the spin four times to four different variables
                List<SymbolModel> line1 = Spin(machine.Symbols);
                List<SymbolModel> line2 = Spin(machine.Symbols);
                List<SymbolModel> line3 = Spin(machine.Symbols);
                List<SymbolModel> line4 = Spin(machine.Symbols);

                // display spin
                List<List<SymbolModel>> lines = new() { line1, line2, line3, line4 };
                DisplaySpin(lines);

                // check the result
                List<List<SymbolModel>> fullResults = new() { line1, line2, line3, line4 };
                int winningsSum = 0;
                fullResults.ForEach(r =>
                {
                    bool result;
                    SymbolModel winSymbol;
                    int numberOfWildcards;
                    CheckLine(r, out result, out winSymbol, out numberOfWildcards);

                    if (result)
                    {
                        int winnings = CalculateWinnings(winSymbol, numberOfWildcards, stake);
                        winningsSum += winnings;
                    }
                    
                });
                _userService.UpdateBalance(user, stake, winningsSum);
            }
        }

        public int GetStake(int curBalance)
        {
            while (true)
            {
                Console.WriteLine("Enter stake amount: ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int stake))
                {
                    if (stake > curBalance)
                    {
                        Console.WriteLine($"Stake amount is greater than current balance {curBalance:C}");
                    }
                    else
                    {
                        return stake;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }

        private void DisplaySpin(List<List<SymbolModel>> lines)
        {
            foreach (List<SymbolModel> line in lines)
            {
                Console.WriteLine($"{line[0].Label} {line[1].Label} {line[2].Label}");
            }
        }

        public void CheckLine(List<SymbolModel> line, out bool result, out SymbolModel winSymbol, out int numberOfWildcards)
        {
            // Check if all three symbols are the same
            if (line[0].Label == line[1].Label && line[1].Label == line[2].Label)
            {
                result = true;
                winSymbol = line[0];
                numberOfWildcards = 0;
                return;
            }

            // count the number of '*' symbols in the line
            List<SymbolModel> wildCheckLine = new();

            foreach (SymbolModel symbol in line)
            {
                if (symbol.Label != '*')
                {
                    wildCheckLine.Add(symbol);
                }
            }

            if (wildCheckLine.Count == 2)
            {
                if (wildCheckLine[0].Label == wildCheckLine[1].Label)
                {
                    result = true;
                    winSymbol = line[0];
                    numberOfWildcards = 2;
                    return;
                }
            }

            if (wildCheckLine.Count == 1)
            {

                result = true;
                winSymbol = wildCheckLine[0];
                numberOfWildcards = 2;
                return;

            }

            result = false;
            numberOfWildcards = 0;
            winSymbol = new SymbolModel("", 0, 0, ' ');
        }

        public int CalculateWinnings(SymbolModel symbol, int numberOfWildCards, int stake) {
            return (int)((symbol.Coefficient * (3 - numberOfWildCards)) * stake); 
        }

        public List<SymbolModel> Spin(List<SymbolModel> symbols)
        {
            // simulate the spin
            List<SymbolModel> line = new();
            Random random = new();

            for (int i = 0; i < 3; i++)
            {
                double totalWeight = symbols.Sum(item => item.Probabilty);
                double randomValue = random.NextDouble() * totalWeight;

                foreach (var symbol in symbols)
                {
                    if (randomValue < symbol.Probabilty)
                    {
                        line.Add(symbol);
                        break;
                    }
                    randomValue -= symbol.Probabilty;
                }
            }

            return line;
        }
    }
}
