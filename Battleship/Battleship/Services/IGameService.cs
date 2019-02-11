using System.Collections.Generic;
using Battleship.Model;

namespace Battleship.Services
{
    public interface IGameService
    {
        void InitializeGame(int panelSize);
        bool GameOver();
        HitTypeEnum Hit(Point point);
        List<Ship> GetShipList();
        List<Point> GetHitList();
    }
}