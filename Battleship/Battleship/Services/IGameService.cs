using System.Collections.Generic;
using Battleship.Model;

namespace Battleship.Services
{
    public interface IGameService
    {
        void InitializeGame(int panelSize);
        bool GameOver();
        HitTypeEnum GetHitResult(Point point);
        List<Ship> GetEnemyShipList();
        List<Point> GetHitList();
    }
}