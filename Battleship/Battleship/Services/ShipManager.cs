using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Model;

namespace Battleship.Services
{
    public class ShipManager : IShipManager
    {
        public Dictionary<ShipType, int> GetEnemyShipsByType()
        {
            var battleshipType = new ShipType("Battleship", 'B', 5);
            var destroyerType = new ShipType("Destroyer", 'D', 4);

            return new Dictionary<ShipType, int> { { battleshipType, 1 }, { destroyerType, 2 } };
        }

        public List<Ship> GetEnemyShipList(Dictionary<ShipType, int> enemyShipsByType, int panelSize)
        {
            var existingShips = new List<Ship>();

            foreach (var keyValuePair in enemyShipsByType)
            {
                GetAllShipsByType(keyValuePair.Key, keyValuePair.Value, existingShips, panelSize);
            }

            return existingShips;
        }

        private void GetAllShipsByType(ShipType shipType, int numberOfShipsByType, List<Ship> existingShips, int panelSize)
        {
            var shipGenerator = new ShipGenerator();

            for (var i = 0; i < numberOfShipsByType; i++)
            {
                var currentShip = GetNextValidShip(existingShips, shipGenerator, shipType, panelSize);
                existingShips.Add(currentShip);
            }
        }

        private Ship GetNextValidShip(List<Ship> ships, ShipGenerator shipGenerator, ShipType shipType, int panelSize)
        {
            var shipValidator = new ShipValidator();
            Ship currentShip;

            do
            {
                currentShip = shipGenerator.GenerateShip(shipType, panelSize);
            } while (!shipValidator.IsShipValidForList(currentShip, ships));

            return currentShip;
        }

    }
}
