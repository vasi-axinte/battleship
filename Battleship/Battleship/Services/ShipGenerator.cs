using System;
using Battleship.Model;

namespace Battleship.Services
{
    public class ShipGenerator
    {
        readonly Random _random = new Random();

        public Ship GenerateShip(ShipType shipType, int panelSize)
        {
            if (panelSize < shipType.Size)
            {
                throw new ArgumentException("Panel size cannot be smaller than ship size");
            }
            var headPoint = GenerateHeadPoint(panelSize);
            var tailPoint = GenerateTailPoint(shipType, headPoint, panelSize);

            OrderShipPoints(headPoint, tailPoint);

            return new Ship(headPoint, tailPoint, shipType);
        }

        private void OrderShipPoints(Point headPoint, Point tailPoint)
        {
            if (tailPoint.X < headPoint.X)
            {
                var temp = tailPoint.X;
                tailPoint.X = headPoint.X;
                headPoint.X = temp;
            }

            if (tailPoint.Y < headPoint.Y)
            {
                var temp = tailPoint.Y;
                tailPoint.Y = headPoint.Y;
                headPoint.Y = temp;
            }
        }

        private Point GenerateHeadPoint(int panelSize)
        {
            var positionX = _random.Next(0, panelSize);
            var positionY = _random.Next(0, panelSize);

            var headPoint = new Point(positionX, positionY);
            return headPoint;
        }

        private Point GenerateTailPoint(ShipType shipType, Point headPoint, int panelSize)
        {
            var direction = _random.Next(0, 2);
            Point tailPoint;

            if (direction == 0)
            {
                tailPoint = headPoint.X + shipType.Size - 1 < panelSize ? new Point(headPoint.X + shipType.Size - 1, headPoint.Y) : new Point(headPoint.X - shipType.Size + 1, headPoint.Y);
            }
            else
            {
                tailPoint = headPoint.Y + shipType.Size - 1 < panelSize ? new Point(headPoint.X, headPoint.Y + shipType.Size - 1) : new Point(headPoint.X, headPoint.Y - shipType.Size + 1);
            }

            return tailPoint;
        }
    }
}
