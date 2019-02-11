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
        private UiHelper _uiHelper;
        private IGameService _gameService;

        private HitTypeEnum _lastHitResult;
        private bool _firstMove = true;
        private int _panelSize = 10;
        
        public UserInterface(IGameService gameService)
        {
            _gameService = gameService;
            _uiHelper = new UiHelper();
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
            PrintPanel(GetSymbolsForHitPanel);
            Console.WriteLine("Last hit: ");
        }

        private void DisplayShipPanel()
        {
            Console.WriteLine("Ship Panel");
            Console.WriteLine("---------------------------");
            PrintPanel(GetSymbolsForShipPanel);
            Console.WriteLine("---------------------------");
        }

        private void MakeMove()
        {
            Console.WriteLine("Insert your next hit`s coordinates:");

            var point = _uiHelper.GetHitPoint(_panelSize);
            if(point.X == -1 || point.Y == -1) return;
            _lastHitResult = _gameService.Hit(point);
        }

        private void PrintPanel(Func<int, int, string> getSymbol)
        {
            PrintPanelHeaderLine();
            for (var i = 0; i < _panelSize; i++)
            {
                Console.Write(UiConstants.Alphabet[i] + " ");
                for (var j = 0; j < _panelSize; j++)
                {
                    Console.Write(getSymbol(i, j) + " ");
                }
                Console.WriteLine();
            }
        }

        private void PrintPanelHeaderLine()
        {
            Console.Write("  ");
            for (var i = 0; i < _panelSize; i++)
            {
                Console.Write(i + 1 + " ");
            }
            Console.WriteLine();
        }

        private string GetSymbolsForShipPanel(int i, int j)
        {
            var ships = _gameService.GetShipList();
            return _uiHelper.GetSymbolAtPoint(i, j, ships);
        }

        private string GetSymbolsForHitPanel(int i, int j)
        {
            var points = _gameService.GetHitList();
            return _uiHelper.GetSymbolAtPoint(i, j, points);
        }
    }
}
