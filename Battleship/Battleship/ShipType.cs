namespace Battleship
{
    public class ShipType
    {
        public ShipType(string name, char symbol, int size)
        {
            Name = name;
            Symbol = symbol;
            Size = size;
        }

        public string Name { get; }

        public char Symbol { get; }

        public int Size { get; }
    }
}