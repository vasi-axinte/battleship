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
            Console.WriteLine("Hit Panel");
            PrintShipPanel(GetSymbolForHitPanel);
            Console.WriteLine("Last hit: ");
        }

        private void DisplayShipPanel()
        {
            Console.WriteLine("Ship Panel");
            Console.WriteLine("---------------------------");
            PrintShipPanel(GetSymbolForShipPanel);
            Console.WriteLine("---------------------------");

        }

        private void MakeMove()
        {
            Console.WriteLine("Insert your next hit`s coordinates:");

            var point = _uiService.GetHitPoint(_panelSize);
            if(point.X == -1 || point.Y == -1) return;
            _lastHitResult = _gameService.Hit(point);
        }

        private void PrintShipPanel(Func<int, int, string> panelPointSelector)
        {
            PrintFirstLine();
            for (var i = 0; i < _panelSize; i++)
            {
                Console.Write(alphabet[i]+" ");
                for (var j = 0; j < _panelSize; j++)
                {
                    Console.Write(panelPointSelector(i, j) + " ");
                }
                Console.WriteLine();
            }
        }

        private void PrintFirstLine()
        {
            Console.Write("  ");
            for (var i = 0; i < _panelSize; i++)
            {
                Console.Write(i + 1 + " ");
            }
            Console.WriteLine();
        }

        private string GetSymbolForShipPanel(int i, int j)
        {
            var ships = _gameService.GetEnemyShipList();

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
            return points.Any(point => i == point.X && j == point.Y) ? "X" : "~";
        }
    }
}
