using app.machine;
using app.symbols;
using app.user;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
.AddSingleton<IMachineService, MachineService>()
.AddSingleton<IUserService, UserService>()
.BuildServiceProvider();

Console.WriteLine("Welcome to Simple Slot Machine!");

List<SymbolModel> symbols = new()
{
    new SymbolModel("Apple", 0.4, 0.45, 'A'),
    new SymbolModel("Banana", 0.6, 0.35, 'B'),
    new SymbolModel("Pineapple", 0.8, 0.15, 'P'),
    new SymbolModel("Wildcard", 0, 0.05, '*'),
};
MachineModel machine = new(symbols);
UserModel user = new();

var machineService = serviceProvider.GetService<IMachineService>();
machineService!.Run(machine, user);

Console.WriteLine("Thank you for playing!");
