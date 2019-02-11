﻿using System;
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

        private bool CoordinatesAreValid(string coordinatesAsString, int panelSize)
        {
            if (coordinatesAsString.Length < 2) return false;
            if (!alphabet.Substring(0, panelSize).Contains(coordinatesAsString.ToUpper()[0])) return false;
            int number;
            if (!int.TryParse(coordinatesAsString.Substring(1), out number)) return false;
            if (number > panelSize) return false;

            return true;
        }
    }
}
