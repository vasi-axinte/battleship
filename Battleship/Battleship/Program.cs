using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Services;
using Battleship.Ui;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            IGameService gameService = new GameService();
            UserInterface ui = new UserInterface(gameService);

            ui.Start();
            Console.ReadLine();
        }
    }
}
