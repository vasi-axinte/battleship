using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Model;

namespace Battleship.Services
{
    public class GameService : IGameService
    {
        private int _panelSize;
        private Dictionary<ShipType, int> _enemyShipsByType;
        private Dictionary<Ship, int> _shipsWithHits;
        private List<Point> _hits;
        private HitTypeEnum _lastHitType;

        public GameService()
        {
            _hits = new List<Point>();
        }

        public void InitializeGame(int panelSize)
        {
            _panelSize = panelSize;
            CreateShipTypes();
            var enemyShips = GenerateEnemyShips();
            InitializeHitsPerShip(enemyShips);
        }

        private void InitializeHitsPerShip(List<Ship> enemyShips)
        {
            _shipsWithHits = new Dictionary<Ship, int>();
            foreach (var ship in enemyShips)
            {
                _shipsWithHits.Add(ship, 0);
            }
        }

        public bool GameOver()
        {
            return _shipsWithHits.All(hitPerShipPair => hitPerShipPair.Key.Type.Size == hitPerShipPair.Value);
        }

        public HitTypeEnum GetHitResult(Point point)
        {
            PerformHit(point);

            return _lastHitType;
        }

        public List<Ship> GetEnemyShipList()
        {
            return _shipsWithHits.Keys.ToList();
        }

        public List<Point> GetHitList()
        {
            return _hits;
        }

        private void PerformHit(Point point)
        {
            _lastHitType = HitTypeEnum.Miss;

            var ships = _shipsWithHits.Keys.ToList();
            foreach (var ship in ships)
            {
                if (!ShipIsHitInPoint(point, ship)) continue;

                if (_hits.Contains(point))
                {
                    _lastHitType = HitTypeEnum.Hit;
                    continue;
                }

                _lastHitType = _shipsWithHits[ship] + 1 == ship.Type.Size ? HitTypeEnum.Sink : HitTypeEnum.Hit;

                _shipsWithHits[ship] += 1;
            }
            _hits.Add(point);
        }

        private bool ShipIsHitInPoint(Point point, Ship ship)
        {
            return ship.HeadPoint.X <= point.X && point.X <= ship.TailPoint.X &&
                   ship.HeadPoint.Y <= point.Y && point.Y <= ship.TailPoint.Y;
        }

        private void CreateShipTypes()
        {
            var battleshipType = new ShipType("Battleship", 'B', 5);
            var destroyerType = new ShipType("Destroyer", 'D', 4);

            //_enemyShipsByType = new Dictionary<ShipType, int> {{battleshipType, 1}, {destroyerType, 2}};
            _enemyShipsByType = new Dictionary<ShipType, int> {{battleshipType, 1}};
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
                currentShip = shipGenerator.GenerateShip(shipType, _panelSize);
            } while (!shipValidator.IsShipValid(currentShip, ships));

            return currentShip;
        }
    }
}