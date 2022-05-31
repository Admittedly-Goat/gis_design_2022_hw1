using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMapObjects
{
    public abstract class moSymbol
    {
        public abstract moSymbolTypeConstant SymbolType { get; }
        public abstract moSymbol Clone();

    }
}
