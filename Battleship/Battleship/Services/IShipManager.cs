using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Model;

namespace Battleship.Services
{
    public interface IShipManager
    {
        Dictionary<ShipType, int> GetEnemyShipsByType();
        List<Ship> GetEnemyShipList(Dictionary<ShipType, int> enemyShipsByType, int panelSize);
    }
}
