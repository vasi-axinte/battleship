using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Ui;

namespace Battleship
{
    public class Game
    {
        private const int PanelSize = 10;
        private Dictionary<ShipType, int> _enemyShipsByType;
        private UserInterface ui;

        public Game()
        {
            ui = new UserInterface();

            ui.DisplayInfo("Creating ships...");
            CreateShipTypes();

            ui.DisplayInfo("Generating enemy ships...");
            var enemyShips = GenerateEnemyShips();

            ui.DisplayInfo("Generating panel...");
            var emptyPanel = GeneratePanel(PanelSize);

            var panel = PlaceEnemyShips(emptyPanel, enemyShips);

            ui.DisplayPanel(panel, PanelSize);
        }

        private void CreateShipTypes()
        {
            var battleshipType = new ShipType("Battleship", 'B', 5);
            var destroyerType = new ShipType("Destroyer", 'D', 4);

            _enemyShipsByType = new Dictionary<ShipType, int> {{battleshipType, 1}, {destroyerType, 2}};
        }

        private List<Ship> GenerateEnemyShips()
        {
            var existingShips = new List<Ship>();

            foreach (var keyValuePair in _enemyShipsByType)
            {
                GenerateAllTheShipsByType(keyValuePair.Key, keyValuePair.Value, existingShips);
            }

            return existingShips;
        }

        private void GenerateAllTheShipsByType(ShipType shipType, int numberOfShipsByType, List<Ship> existingShips)
        {
            var shipGenerator = new ShipGenerator();

            for (var i = 0; i < numberOfShipsByType; i++)
            {
                var currentShip = GetNextValidShip(existingShips, shipGenerator, shipType);
                existingShips.Add(currentShip);
            }
        }

        private Ship GetNextValidShip(List<Ship> ships, ShipGenerator shipGenerator, ShipType shipType)
        {
            var shipValidator = new ShipValidator();
            Ship currentShip;

            do
            {
                currentShip = shipGenerator.GenerateShip(shipType, PanelSize);
            } while (!shipValidator.IsShipValid(currentShip, ships));

            return currentShip;
        }

        private char[,] PlaceEnemyShips(char[,] panel, List<Ship> enemyShips)
        {
            foreach (var enemyShip in enemyShips)
            {
                panel[enemyShip.HeadPoint.X, enemyShip.HeadPoint.Y] = enemyShip.Type.Symbol;
                panel[enemyShip.TailPoint.X, enemyShip.TailPoint.Y] = enemyShip.Type.Symbol;
                if (enemyShip.HeadPoint.X - enemyShip.TailPoint.X != 0)
                {
                    for (var i = enemyShip.HeadPoint.X; i <= enemyShip.TailPoint.X; i++)
                    {
                        panel[i, enemyShip.HeadPoint.Y] = enemyShip.Type.Symbol;
                    }
                }

                if (enemyShip.HeadPoint.Y - enemyShip.TailPoint.Y != 0)
                {
                    for (var i = enemyShip.HeadPoint.Y; i <= enemyShip.TailPoint.Y; i++)
                    {
                        panel[enemyShip.HeadPoint.X, i] = enemyShip.Type.Symbol;
                    }
                }
            }

            return panel;
        }

        private char[,] GeneratePanel(int panelSize)
        {
            var matrix = new char[panelSize,panelSize];
            for (var i = 0; i < panelSize; i++)
            {
                for (var j = 0; j < panelSize; j++)
                {
                    matrix[i, j] = '~';
                }
            }
            return matrix;
        }

        public void Start()
        {
            //InitializeGame();
            //DisplayPanels();
        }
    }
}
