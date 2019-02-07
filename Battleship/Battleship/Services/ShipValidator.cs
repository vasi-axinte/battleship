using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Model;

namespace Battleship.Services
{
    public class ShipValidator
    {
        public bool IsShipValidForList(Ship ship, List<Ship> ships)
        {
            foreach (var shipToBeCheckedAgainst in ships)
            {
                if (AreIntersecting(ship, shipToBeCheckedAgainst)) return false;
                if (AreOverlaping(ship, shipToBeCheckedAgainst)) return false;
            }
            return true;
        }

        private bool AreOverlaping(Ship ship1, Ship ship2)
        {
            if (ship1.HeadPoint.X == ship1.TailPoint.X && ship2.HeadPoint.X == ship2.TailPoint.X &&
                ship1.HeadPoint.X == ship2.HeadPoint.X) return true;

            if (ship1.HeadPoint.Y == ship1.TailPoint.Y && ship2.HeadPoint.Y == ship2.TailPoint.Y &&
                ship1.HeadPoint.Y == ship2.HeadPoint.Y) return true;

            return false;
        }

        private bool AreIntersecting(Ship ship1, Ship ship2)
        {
            if (AreParallel(ship1, ship2)) return false;
            if (ship1.HeadPoint.X == ship1.TailPoint.X)
            {
                if (ship1.HeadPoint.Y <= ship2.HeadPoint.Y && ship2.HeadPoint.Y <= ship1.TailPoint.Y &&
                    ship2.TailPoint.X >= ship1.HeadPoint.X)
                {
                    return true;
                }
            }
            else
            {
                if (ship1.HeadPoint.X <= ship2.HeadPoint.X && ship2.HeadPoint.X <= ship1.TailPoint.X &&
                    ship2.TailPoint.Y >= ship1.HeadPoint.X)
                {
                    return true;
                }
            }
            return false;
        }

        private bool AreParallel(Ship ship1, Ship ship2)
        {
            if (ship1.HeadPoint.X == ship1.TailPoint.X && ship2.HeadPoint.X == ship2.TailPoint.X) return true;
            if (ship1.HeadPoint.Y == ship1.TailPoint.Y && ship2.HeadPoint.Y == ship2.TailPoint.Y) return true;
            return false;
        }
    }
}
