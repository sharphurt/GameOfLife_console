using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Session2
{
    public class Game
    {
        public HashSet<Point> Field { get; set; }

        public Point LocationOffset;

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
            return isAlive && aliveNeighbours == 2 || aliveNeighbours == 3;
        }

        public Game MakeTurn()
        {
            Field = Field
                .SelectMany(GetNeighbours)
                .Where(GetNextPointState)
                .ToHashSet();
            return this;
        }

        public void SetOffset(Point offset)
        {
            Field = Field.Select(p => new Point(p.X + offset.X, p.Y + offset.Y)).ToHashSet();
        }
    }
}