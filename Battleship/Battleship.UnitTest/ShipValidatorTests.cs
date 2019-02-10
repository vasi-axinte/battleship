using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Model;
using Battleship.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Battleship.UnitTest
{
    [TestClass]
    public class ShipValidatorTests
    {
        [TestMethod]
        public void IsShipValid_WithHorizontalParallelShips_ReturnsTrue()
        {
            ShipValidator target = new ShipValidator();
            var shipType = new ShipType("Destroyer", "D", 4);
            var ship = new Ship(new Point(3, 2), new Point(3, 5), shipType);
            var shipToBeComparedWith = new Ship(new Point(5, 1), new Point(5, 4), shipType);
            var ships = new List<Ship> {shipToBeComparedWith};

            target.IsShipValidForList(ship, ships).Should().BeTrue();
        }

        [TestMethod]
        public void IsShipValid_WithVerticalParallelShips_ReturnsTrue()
        {
            ShipValidator target = new ShipValidator();
            var shipType = new ShipType("Destroyer", "D", 4);
            var ship = new Ship(new Point(2, 2), new Point(5, 2), shipType);
            var shipToBeComparedWith = new Ship(new Point(2, 3), new Point(5, 3), shipType);
            var ships = new List<Ship> { shipToBeComparedWith };

            target.IsShipValidForList(ship, ships).Should().BeTrue();
        }

        [TestMethod]
        public void IsShipValid_WithIntersectedShipsAtHead_ReturnsFalse()
        {
            ShipValidator target = new ShipValidator();
            var shipType = new ShipType("Destroyer", "D", 4);
            var ship = new Ship(new Point(3, 2), new Point(3, 5), shipType);
            var shipToBeComparedWith = new Ship(new Point(2, 2), new Point(5, 2), shipType);
            var ships = new List<Ship> { shipToBeComparedWith };

            target.IsShipValidForList(ship, ships).Should().BeFalse();
        }

        [TestMethod]
        public void IsShipValid_WithIntersectedShipsInTheMiddle_ReturnsFalse()
        {
            ShipValidator target = new ShipValidator();
            var shipType = new ShipType("Destroyer", "D", 4);
            var ship = new Ship(new Point(3, 2), new Point(3, 5), shipType);
            var shipToBeComparedWith = new Ship(new Point(0, 4), new Point(3, 4), shipType);
            var ships = new List<Ship> { shipToBeComparedWith };

            target.IsShipValidForList(ship, ships).Should().BeFalse();
        }

        [TestMethod]
        public void IsShipValid_WithOveralppingHorizontalShips_ReturnsFalse()
        {
            ShipValidator target = new ShipValidator();
            var shipType = new ShipType("Destroyer", "D", 4);
            var ship = new Ship(new Point(3, 2), new Point(3, 5), shipType);
            var shipToBeComparedWith = new Ship(new Point(3, 2), new Point(3, 5), shipType);
            var ships = new List<Ship> { shipToBeComparedWith };

            target.IsShipValidForList(ship, ships).Should().BeFalse();
        }

        [TestMethod]
        public void IsShipValid_WithOveralppingVerticalShips_ReturnsFalse()
        {
            ShipValidator target = new ShipValidator();
            var shipType = new ShipType("Destroyer", "D", 4);
            var ship = new Ship(new Point(2, 3), new Point(5, 3), shipType);
            var shipToBeComparedWith = new Ship(new Point(2, 3), new Point(5, 3), shipType);
            var ships = new List<Ship> { shipToBeComparedWith };

            target.IsShipValidForList(ship, ships).Should().BeFalse();
        }

        [TestMethod]
        public void IsShipValid_WithOveralppingHorizontaHavingDifferentSizeShips_ReturnsFalse()
        {
            ShipValidator target = new ShipValidator();
            var shipType = new ShipType("Destroyer", "D", 4);
            var shipType2 = new ShipType("Battleship", "B", 5);
            var ship = new Ship(new Point(3, 2), new Point(3, 5), shipType);
            var shipToBeComparedWith = new Ship(new Point(3, 1), new Point(3, 5), shipType2);
            var ships = new List<Ship> { shipToBeComparedWith };

            target.IsShipValidForList(ship, ships).Should().BeFalse();
        }
    }
}
