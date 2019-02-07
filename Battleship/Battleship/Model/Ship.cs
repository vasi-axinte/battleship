namespace Battleship.Model
{
    public class Ship
    {
        public Point HeadPoint { get; }

        public Point TailPoint { get; }

        public ShipType Type { get; set; }

        public Ship(Point headPoint, Point tailPoint, ShipType shipType)
        {
            HeadPoint = headPoint;
            TailPoint = tailPoint;
            Type = shipType;
        }
    }
}