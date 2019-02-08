using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Model;

namespace Battleship.Ui
{
    public class UiService
    {
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public Point GetHitPoint(int panelSize)
        {
            string coordinatesAsString;
            do
            {
                coordinatesAsString = Console.ReadLine();

            } while (!CoordinatesAreValid(coordinatesAsString, panelSize));

            var x = alphabet.IndexOf(coordinatesAsString[0].ToString().ToUpper());
            var y = Convert.ToInt32(coordinatesAsString.Substring(1)) - 1;

            return new Point(x, y);
        }


        public char[,] GetPanelFromShipList(List<Ship> ships, int panelSize)
        {
            var panel = GenerateShipPanel(ships, panelSize);

            return panel;
        }

        public char[,] GetPanelFromPointList(List<Point> points, int panelSize)
        {
            var panel = GenerateEmptyPanel(panelSize);
            foreach (var point in points)
            {
                panel[point.X, point.Y] = 'X';
            }

            return panel;
        }

        private char[,] GenerateEmptyPanel(int panelSize)
        {
            var matrix = new char[panelSize, panelSize];
            for (var i = 0; i < panelSize; i++)
            {
                for (var j = 0; j < panelSize; j++)
                {
                    matrix[i, j] = '~';
                }
            }
            return matrix;
        }

        private bool CoordinatesAreValid(string coordinatesAsString, int panelSize)
        {
            if (coordinatesAsString.Length < 2) return false;
            if (!alphabet.Substring(0, panelSize).Contains(coordinatesAsString.ToUpper()[0])) return false;
            int number;
            if (!int.TryParse(coordinatesAsString.Substring(1), out number)) return false;
            if (number > panelSize) return false;

            return true;
        }


        private char[,] GenerateShipPanel(List<Ship> ships, int panelSize)
        {
            var panel = GenerateEmptyPanel(panelSize);
            foreach (var ship in ships)
            {
                for (var i = ship.HeadPoint.X; i <= ship.TailPoint.X; i++)
                {
                    panel[i, ship.HeadPoint.Y] = ship.Type.Symbol;
                }

                for (var i = ship.HeadPoint.Y; i <= ship.TailPoint.Y; i++)
                {
                    panel[ship.HeadPoint.X, i] = ship.Type.Symbol;
                }
            }

            return panel;
        }
    }
}
