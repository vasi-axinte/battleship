namespace Battleship.Model
{
    public class ShipType
    {
        public ShipType(string name, string symbol, int size)
        {
            Name = name;
            Symbol = symbol;
            Size = size;
        }

        public string Name { get; }

        public string Symbol { get; }

        public int Size { get; }
    }
}