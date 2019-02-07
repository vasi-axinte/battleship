using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Model;
using Battleship.Services;
using FluentAssert;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Battleship.UnitTest
{
    [TestClass]
    public class GameServiceTests
    {
        [TestMethod]
        public void InitializeGame_WithOneShip_ReturnsItCorrectly()
        {
            int panelSize = 10;
            IShipManager shipManager = Substitute.For<IShipManager>();
            var destroyerType = new ShipType("Destroyer", 'D', 4);
            var shipsByType =  new Dictionary<ShipType, int> { { destroyerType, 1 } };
            var ship = new Ship(new Point(3, 2), new Point(3, 5), destroyerType);
            var shipList = new List<Ship> {ship};
            shipManager.GetEnemyShipsByType().Returns(shipsByType);
            shipManager.GetEnemyShipList(shipsByType, panelSize).Returns(shipList);
            GameService target = new GameService(shipManager);

            target.InitializeGame(panelSize);

            target.GetEnemyShipList().ShouldContainAll(shipList);
        }

        [TestMethod]
        public void GetHitResult_DoesNotHitShip_ReturnsMiss()
        {
            int panelSize = 10;
            IShipManager shipManager = Substitute.For<IShipManager>();
            var destroyerType = new ShipType("Destroyer", 'D', 4);
            var shipsByType = new Dictionary<ShipType, int> { { destroyerType, 1 } };
            var ship = new Ship(new Point(3, 2), new Point(3, 5), destroyerType);
            var shipList = new List<Ship> { ship };
            shipManager.GetEnemyShipsByType().Returns(shipsByType);
            shipManager.GetEnemyShipList(shipsByType, panelSize).Returns(shipList);
            GameService target = new GameService(shipManager);
            target.InitializeGame(panelSize);

            var result = target.GetHitResult(new Point(1, 1));

            result.ShouldBeEqualTo(HitTypeEnum.Miss);
        }

        [TestMethod]
        public void GetHitResult_HitsShip_ReturnsHit()
        {
            int panelSize = 10;
            IShipManager shipManager = Substitute.For<IShipManager>();
            var destroyerType = new ShipType("Destroyer", 'D', 4);
            var shipsByType = new Dictionary<ShipType, int> { { destroyerType, 1 } };
            var ship = new Ship(new Point(3, 2), new Point(3, 5), destroyerType);
            var shipList = new List<Ship> { ship };
            shipManager.GetEnemyShipsByType().Returns(shipsByType);
            shipManager.GetEnemyShipList(shipsByType, panelSize).Returns(shipList);
            GameService target = new GameService(shipManager);
            target.InitializeGame(panelSize);

            var result = target.GetHitResult(new Point(3, 2));

            result.ShouldBeEqualTo(HitTypeEnum.Hit);
        }

        [TestMethod]
        public void GetHitResult_HitsShipFourTimes_ReturnsSink()
        {
            int panelSize = 10;
            IShipManager shipManager = Substitute.For<IShipManager>();
            var destroyerType = new ShipType("Destroyer", 'D', 4);
            var shipsByType = new Dictionary<ShipType, int> { { destroyerType, 1 } };
            var ship = new Ship(new Point(3, 2), new Point(3, 5), destroyerType);
            var shipList = new List<Ship> { ship };
            shipManager.GetEnemyShipsByType().Returns(shipsByType);
            shipManager.GetEnemyShipList(shipsByType, panelSize).Returns(shipList);
            GameService target = new GameService(shipManager);
            target.InitializeGame(panelSize);

            target.GetHitResult(new Point(3, 2));
            target.GetHitResult(new Point(3, 3));
            target.GetHitResult(new Point(3, 4));
            var result = target.GetHitResult(new Point(3, 5));

            result.ShouldBeEqualTo(HitTypeEnum.Sink);
        }

        [TestMethod]
        public void GameOver_WithOneShipNotSinked_ReturnsFalse()
        {
            int panelSize = 10;
            IShipManager shipManager = Substitute.For<IShipManager>();
            var destroyerType = new ShipType("Destroyer", 'D', 4);
            var shipsByType = new Dictionary<ShipType, int> { { destroyerType, 1 } };
            var ship = new Ship(new Point(3, 2), new Point(3, 5), destroyerType);
            var shipList = new List<Ship> { ship };
            shipManager.GetEnemyShipsByType().Returns(shipsByType);
            shipManager.GetEnemyShipList(shipsByType, panelSize).Returns(shipList);
            GameService target = new GameService(shipManager);
            target.InitializeGame(panelSize);

            target.GetHitResult(new Point(3, 2));
            target.GetHitResult(new Point(3, 3));
            target.GetHitResult(new Point(3, 4));

            target.GameOver().ShouldBeFalse();
        }

        [TestMethod]
        public void GameOver_WithOneShipSinked_ReturnsTrue()
        {
            int panelSize = 10;
            IShipManager shipManager = Substitute.For<IShipManager>();
            var destroyerType = new ShipType("Destroyer", 'D', 4);
            var shipsByType = new Dictionary<ShipType, int> { { destroyerType, 1 } };
            var ship = new Ship(new Point(3, 2), new Point(3, 5), destroyerType);
            var shipList = new List<Ship> { ship };
            shipManager.GetEnemyShipsByType().Returns(shipsByType);
            shipManager.GetEnemyShipList(shipsByType, panelSize).Returns(shipList);
            GameService target = new GameService(shipManager);
            target.InitializeGame(panelSize);

            target.GetHitResult(new Point(3, 2));
            target.GetHitResult(new Point(3, 3));
            target.GetHitResult(new Point(3, 4));
            target.GetHitResult(new Point(3, 5));

            target.GameOver().ShouldBeTrue();
        }
    }
}
