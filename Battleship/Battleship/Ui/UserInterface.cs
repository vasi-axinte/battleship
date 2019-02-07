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
        private int _panelSize = 10;

        private UiService _uiService;
        private IGameService _gameService;
        private int _topCursorPosition = 25;

        public UserInterface(IGameService gameService)
        {
            _gameService = gameService;
            _uiService = new UiService();
        }

        public void Start()
        {
            Console.WriteLine("Initializing game...");
            _gameService.InitializeGame(_panelSize);
            var ships = _gameService.GetEnemyShipList();
            var shipPanel = _uiService.GetPanelFromShipList(ships, _panelSize);

            Console.WriteLine("Initial Panel");
            DisplayPanel(shipPanel);
            Console.WriteLine("---------------------------");

            while (!_gameService.GameOver())
            {
                MakeMove();
            }

            Console.WriteLine("Game is over.");
            DisplayPanel(shipPanel);
        }

        private void MakeMove()
        {
            Console.SetCursorPosition(0, 13);
            var hitPoints = _gameService.GetHitList();
            DisplayPanel(_uiService.GetPanelFromPointList(hitPoints, _panelSize));

            Console.WriteLine("Insert your next hit`s coordinates:");
            Console.SetCursorPosition(0, _topCursorPosition++);

            var point = _uiService.GetHitPoint(_panelSize);
            var hitResult = _gameService.GetHitResult(point);

            Console.SetCursorPosition(Console.CursorLeft + 3, Console.CursorTop - 1);
            Console.Write("->It`s a " + hitResult + "!");
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
