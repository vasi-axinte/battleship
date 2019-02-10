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
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private UiService _uiService;
        private IGameService _gameService;

        private HitTypeEnum _lastHitResult;
        private bool _firstMove = true;
        private int _panelSize = 10;
        
        public UserInterface(IGameService gameService)
        {
            _gameService = gameService;
            _uiService = new UiService();
        }

        public void Start()
        {
            Console.WriteLine("Initializing game...");
            _gameService.InitializeGame(_panelSize);

            while (!_gameService.GameOver())
            {
                Console.Clear();;
                DisplayShipPanel();
                DisplayHitPanel();
                DisplayLastHitResult();
                MakeMove();
                _firstMove = false;
            }

            Console.WriteLine("Game is over.");
        }

        private void DisplayLastHitResult()
        {
            if(!_firstMove) Console.WriteLine(_lastHitResult);
        }

        private void DisplayHitPanel()
        {
            var hitPoints = _gameService.GetHitList();
            Console.WriteLine("Hit Panel");
            //DisplayPanel(_uiService.GetPanelFromPointList(hitPoints, _panelSize));
            DisplayHitPanelTest();
            Console.WriteLine("Last hit: ");
        }

        private void DisplayShipPanel()
        {
            var ships = _gameService.GetEnemyShipList();
            var shipPanel = _uiService.GetPanelFromShipList(ships, _panelSize);
            Console.WriteLine("Ship Panel");
            //DisplayPanel(shipPanel);
            Console.WriteLine("---------------------------");

            Console.WriteLine("Ship Panel with no panel");
            DisplayShipPanelTest();
            Console.WriteLine("---------------------------");

        }

        private void MakeMove()
        {
            Console.WriteLine("Insert your next hit`s coordinates:");

            var point = _uiService.GetHitPoint(_panelSize);
            if(point.X == -1 || point.Y == -1) return;
            _lastHitResult = _gameService.Hit(point);
        }

        private void DisplayPanel(string[,] panel)
        {
            Console.WriteLine("   ");
            for (var i = 0; i < panel.GetLength(0); i++)
            {
                for (var j = 0; j < panel.GetLength(1); j++)
                {
                    Console.Write(panel[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        private void DisplayShipPanelTest()
        {
            for (var i = 0; i < _panelSize + 1; i++)
            {
                for (var j = 0; j < _panelSize + 1; j++)
                {
                    Console.Write(GetSymbolForShipPanel(i, j) + " ");
                }
                Console.WriteLine();
            }
        }

        private void DisplayHitPanelTest()
        {
            for (var i = 0; i < _panelSize + 1; i++)
            {
                for (var j = 0; j < _panelSize + 1; j++)
                {
                    Console.Write(GetSymbolForHitPanel(i, j) + " ");
                }
                Console.WriteLine();
            }
        }

        private string GetSymbolForShipPanel(int i, int j)
        {
            var ships = _gameService.GetEnemyShipList();

            if (i == 0 && j == 0)
            {
                return " ";
            }

            if (i == 0)
            {
                return j.ToString();
            }

            if (j == 0)
            {
                return alphabet[i-1].ToString();
            }

            foreach (var ship in ships)
            {
                if (i >= ship.HeadPoint.X && i <= ship.TailPoint.X && ship.HeadPoint.Y == j) return ship.Type.Symbol;

                if (i >= ship.TailPoint.X && i <= ship.HeadPoint.X && ship.HeadPoint.Y == j) return ship.Type.Symbol;

                if (j >= ship.HeadPoint.Y && j <= ship.TailPoint.Y && ship.HeadPoint.X == i) return ship.Type.Symbol;

                if (j >= ship.TailPoint.Y && j <= ship.HeadPoint.Y && ship.HeadPoint.X == i) return ship.Type.Symbol;
            }

            return "~";
        }

        private string GetSymbolForHitPanel(int i, int j)
        {
            var points = _gameService.GetHitList();

            if (i == 0 && j == 0)
            {
                return " ";
            }

            if (i == 0)
            {
                return j.ToString();
            }

            if (j == 0)
            {
                return alphabet[i - 1].ToString();
            }

            return points.Any(point => i == point.X && j == point.Y) ? "X" : "~";
        }
    }
}
