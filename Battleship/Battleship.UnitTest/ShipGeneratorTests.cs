using System;
using Battleship.Model;
using Battleship.Services;
using FluentAssert;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Battleship.UnitTest
{
    [TestClass]
    public class ShipGeneratorTests
    {
        [TestMethod]
        public void GenerateShip_WithNormalSizePanel_ReturnsValidShip()
        {
            ShipGenerator generator = new ShipGenerator();

            var panelSize = 10;
            var ship = generator.GenerateShip(new ShipType("Battleship", 'B', 5), panelSize);

            ship.HeadPoint.X.ShouldBeLessThan(panelSize);
            ship.HeadPoint.Y.ShouldBeLessThan(panelSize);
            ship.TailPoint.Y.ShouldBeLessThan(panelSize);
            ship.TailPoint.Y.ShouldBeLessThan(panelSize);
        }
    }
}
