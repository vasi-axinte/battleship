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
        Dictionary<ShipType, int> GetShipsByType();
        List<Ship> GetShipList(Dictionary<ShipType, int> enemyShipsByType, int panelSize);
    }
}
