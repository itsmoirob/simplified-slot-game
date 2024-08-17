using app.symbols;
using app.user;

namespace app.machine;
public interface IMachineService
{
    public void Run(MachineModel machine, UserModel user);
    public int GetStake(int curBalance);
    public List<SymbolModel> Spin(List<SymbolModel> symbols);
    public int CalculateWinnings(SymbolModel symbol, int numberOfWildCards, int stake);
    public void CheckLine(List<SymbolModel> line, out bool result, out SymbolModel winSymbol, out int numberOfWildcards);
}
