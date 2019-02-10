using System;
using Battleship.Model;
using Battleship.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute.ExceptionExtensions;

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

            ship.HeadPoint.X.Should().BeLessThan(panelSize);
            ship.HeadPoint.Y.Should().BeLessThan(panelSize);
            ship.TailPoint.Y.Should().BeLessThan(panelSize);
            ship.TailPoint.Y.Should().BeLessThan(panelSize);
        }

        [TestMethod]
        public void GenerateShip_WithSmallerPanelThanShip_ThrowsInvalidArgumentException()
        {
            ShipGenerator generator = new ShipGenerator();
            var panelSize = 3;

            Action a = () => generator.GenerateShip(new ShipType("Battleship", 'B', 5), panelSize);

            a.Should().Throw<ArgumentException>();
        }
    }
}
