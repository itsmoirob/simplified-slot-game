namespace app.symbols;
public class SymbolModel
{
    public string Name { get; set; }
    public double Coefficient { get; set; }
    public double Probabilty { get; set; }
    public char Label { get; set; }

    public SymbolModel(string name, double coefficient, double probabilty, char label)
    {
        Name = name;
        Coefficient = coefficient;
        Probabilty = probabilty;
        Label = label;
    }
}
