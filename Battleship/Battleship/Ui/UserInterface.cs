using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Model;
using Battleship.Services;

namespace Battleship.Ui
{
    public class UserInterface
    {
        string alphabet = "ABCDEFGHIJ";
        private IGameService _gameService;
        private int _panelSize = 10;

        public UserInterface(IGameService gameService)
        {
            _gameService = gameService;
        }

        public void Start()
        {
            Console.WriteLine("Initializing game...");
            _gameService.InitializeGame(_panelSize);
            var initialPanel = GetPanelFromShipList();
            Console.WriteLine("Initial Panel");
            DisplayPanel(initialPanel);
            Console.WriteLine("---------------------------");
            while (!_gameService.GameOver())
            {
                var point = GetHitPoint();
                var hitResult = _gameService.GetHitResult(point);
                Console.WriteLine("It`s a " + hitResult + "!");
            }

            Console.WriteLine("Game is over.");
            DisplayPanel(initialPanel);
        }

        private char[,] GetPanelFromShipList()
        {
            var ships = _gameService.GetEnemyShipList();
            var panel = GeneratePanel(ships);

            return panel;
        }

        private char[,] GeneratePanel(List<Ship> ships)
        {
            var panel = GenerateEmptyPanel(_panelSize);
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

        private Point GetHitPoint()
        {
            Console.WriteLine("Insert your next hit`s coordinates:");
            string coordinatesAsString;
            do
            {
                coordinatesAsString = Console.ReadLine();

            } while (CoordinatesAreValid(coordinatesAsString));

            var x = alphabet.IndexOf(coordinatesAsString[0].ToString().ToUpper());
            var y = Convert.ToInt32(coordinatesAsString.Substring(1)) - 1;

            return new Point(x, y);
        }

        private bool CoordinatesAreValid(string coordinatesAsString)
        {
            if (coordinatesAsString.Length != 2) return false;
            if (!alphabet.Contains(coordinatesAsString.ToUpper()[0])) return false;
            int number;
            if (int.TryParse(coordinatesAsString.Substring(1), out number)) return false;

            return true;
        }

        public void DisplayPanel(char[,] panel)
        {
            for (int i = 0; i < panel.GetLength(0); i++)
            {
                for (int j = 0; j < panel.GetLength(1); j++)
                {
                    Console.Write(panel[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public void DisplayInfo(string info)
        {
            Console.WriteLine(info);
        }
    }
}
