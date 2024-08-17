using app.symbols;

namespace app.machine;
public class MachineModel
{
    public MachineModel(List<SymbolModel> symbols)
    {
        Symbols = symbols;
    }
    public List<SymbolModel> Symbols { get; set; }
    public int Rows { get; set; }
}
