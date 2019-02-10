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
        private Dictionary<ShipType, int> _enemyShipsByType;
        private Dictionary<Ship, int> _shipsWithHits;
        private List<Point> _hits;
        private HitTypeEnum _lastHitType;
        private IShipManager _shipManager;

        public GameService(IShipManager shipManager)
        {
            _hits = new List<Point>();
            _shipManager = shipManager;
        }

        public void InitializeGame(int panelSize)
        {
            _enemyShipsByType = _shipManager.GetEnemyShipsByType();
            var enemyShips = _shipManager.GetEnemyShipList(_enemyShipsByType, panelSize);
            InitializeHitsPerShip(enemyShips);
        }

        public bool GameOver()
        {
            return _shipsWithHits.All(hitPerShipPair => hitPerShipPair.Key.Type.Size == hitPerShipPair.Value);
        }

        public HitTypeEnum Hit(Point point)
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

        private void InitializeHitsPerShip(List<Ship> enemyShips)
        {
            _shipsWithHits = new Dictionary<Ship, int>();
            foreach (var ship in enemyShips)
            {
                _shipsWithHits.Add(ship, 0);
            }
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
    }
}