using Currency;

namespace Elixir
{
    public class Elixir : Currency.Currency
    {
        public Elixir(CurrencyType type) : base(type, 10) { }
    }
}