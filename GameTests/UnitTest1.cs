using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Session2;

namespace GameTests
{
    public class Tests
    {
        private Game game;

        [SetUp]
        public void SetUp()
        {
            game = new Game(new HashSet<Point>
            {
                new Point(0, 0), new Point(1, 0), new Point(2, 0)
                //  100       000
                //  100   -> 1110  
                //  100       000
            });
        }
        
        [Test]
        public void Test1()
        {
            new Game(new HashSet<Point>());
        }

        [Test]
        public void Test2()
        {
            game.Field.Count.Should().Be(3);
        }

        [Test]
        public void Test3()
        {
            game.GetAliveNeighbours(new Point(1, 0)).Count().Should().Be(2);
        }

        [Test]
        public void Test4()
        {
            Game.GetNeighbours(new Point(0, 0)).Should().BeEquivalentTo(new List<Point>
            {
                new Point(-1, -1), new Point(0, -1), new Point(1, -1),
                new Point(-1, 0), new Point(0, 0), new Point(1, 0),
                new Point(-1, 1), new Point(0, 1), new Point(1, 1),
            });
        }

        [Test]
        public void Test5()
        {
            game.GetNextPointState(new Point(0, 0)).Should().BeFalse();
            game.GetNextPointState(new Point(1, 0)).Should().BeTrue();
            game.GetNextPointState(new Point(2, 0)).Should().BeFalse();
            game.GetNextPointState(new Point(1, 1)).Should().BeTrue();
            game.GetNextPointState(new Point(-1, 1)).Should().BeFalse();
        }

        [Test]
        public void Test6()
        {
            game.MakeTurn().Field.Should().BeEquivalentTo(new HashSet<Point>
            {
                new Point(1, -1), new Point(1, 0), new Point(1, 1)
            });
        }
    }
}