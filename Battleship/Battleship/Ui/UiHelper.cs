using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Model;

namespace Battleship.Ui
{
    public class UiHelper
    {
        public Point GetHitPoint(int panelSize)
        {
            string coordinatesAsString;
            do
            {
                coordinatesAsString = Console.ReadLine();

            } while (!CoordinatesAreValid(coordinatesAsString, panelSize));

            var x = UiConstants.Alphabet.IndexOf(coordinatesAsString[0].ToString().ToUpper());
            var y = Convert.ToInt32(coordinatesAsString.Substring(1)) - 1;

            return new Point(x, y);
        }

        private bool CoordinatesAreValid(string coordinatesAsString, int panelSize)
        {
            if (coordinatesAsString.Length < 2) return false;
            if (!UiConstants.Alphabet.Substring(0, panelSize).Contains(coordinatesAsString.ToUpper()[0])) return false;
            int number;
            if (!int.TryParse(coordinatesAsString.Substring(1), out number)) return false;
            if (number > panelSize) return false;

            return true;
        }

        public string GetSymbolAtPoint(int i, int j, List<Ship> ships)
        {
            foreach (var ship in ships)
            {
                if (i >= ship.HeadPoint.X && i <= ship.TailPoint.X && ship.HeadPoint.Y == j) return ship.Type.Symbol;

                if (i >= ship.TailPoint.X && i <= ship.HeadPoint.X && ship.HeadPoint.Y == j) return ship.Type.Symbol;

                if (j >= ship.HeadPoint.Y && j <= ship.TailPoint.Y && ship.HeadPoint.X == i) return ship.Type.Symbol;

                if (j >= ship.TailPoint.Y && j <= ship.HeadPoint.Y && ship.HeadPoint.X == i) return ship.Type.Symbol;
            }

            return UiConstants.EmptySymbol;
        }

        public string GetSymbolAtPoint(int i, int j, List<Point> points)
        {
            return points.Any(point => i == point.X && j == point.Y) ? UiConstants.HitSymbol : UiConstants.EmptySymbol;
        }
    }
}
