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
            DisplayPanel(_uiService.GetPanelFromPointList(hitPoints, _panelSize));
            Console.WriteLine("Last hit: ");
        }

        private void DisplayShipPanel()
        {
            var ships = _gameService.GetEnemyShipList();
            var shipPanel = _uiService.GetPanelFromShipList(ships, _panelSize);
            Console.WriteLine("Ship Panel");
            DisplayPanel(shipPanel);
            Console.WriteLine("---------------------------");
        }

        private void MakeMove()
        {
            Console.WriteLine("Insert your next hit`s coordinates:");

            var point = _uiService.GetHitPoint(_panelSize);
            if(point.X == -1 || point.Y == -1) return;
            _lastHitResult = _gameService.GetHitResult(point);
        }

        private void DisplayPanel(char[,] panel)
        {
            for (var i = 0; i < panel.GetLength(0); i++)
            {
                for (var j = 0; j < panel.GetLength(1); j++)
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
