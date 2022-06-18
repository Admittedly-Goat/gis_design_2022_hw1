namespace MyMapObjects
{
    public abstract class moSymbol
    {
        public abstract moSymbolTypeConstant SymbolType { get; }
        public abstract moSymbol Clone();

    }
}
