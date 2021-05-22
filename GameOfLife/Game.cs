using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GameOfLife
{
    public class Game
    {
        public HashSet<Point> Field { get; private set; }

        public Game(HashSet<Point> field)
        {
            Field = field;
        }

        public IEnumerable<Point> GetAliveNeighbours(Point point)
        {
            return GetNeighbours(point)
                .Where(x => Field.Contains(x) && x != point);
        }

        public static IEnumerable<Point> GetNeighbours(Point point)
        {
            for (var dx = -1; dx <= 1; dx++)
            for (var dy = -1; dy <= 1; dy++)
                yield return new Point(point.X + dx, point.Y + dy);
        }

        public bool GetNextPointState(Point point)
        {
            var aliveNeighbours = GetAliveNeighbours(point).Count();
            var isAlive = Field.Contains(point);

            return isAlive && aliveNeighbours == 2 ||
                   aliveNeighbours == 3;
        }

        public Game Move()
        {
            Field = Field
                .SelectMany(GetNeighbours)
                .Where(GetNextPointState)
                .ToHashSet();
            return this;
        }
    }
}