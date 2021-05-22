using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife_console
{
    class GameState
    {
        private readonly HashSet<(int, int)> livedCells;
        public Predicate<(int, int)> Field => pos => livedCells.Contains(pos);

        public GameState(HashSet<(int, int)> livedCells) => this.livedCells = livedCells;

        public IEnumerable<GameState> StartGame => Enumerable.Empty<GameState>().Append(NextTurn());

        public GameState NextTurn()
        {
            return new GameState(
                livedCells
                    .SelectMany(GetNeighbours)
                    .Where(pos =>
                        GetNeighbours(pos).Count(pos2 => Field(pos2)) == 3 ||
                        Field(pos) && GetNeighbours(pos).Count(pos2 => Field(pos2)) == 3)
                    .ToHashSet()
            );
        }

        public IEnumerable<(int, int)> GetNeighbours((int x, int y) pos) =>
            Enumerable.Range(pos.x - 1, 3).Zip(Enumerable.Range(pos.y - 1, 3), (x, y) => (x, y))
                .Where(p => !p.Equals(pos));
    }


    class Program
    {
        static void Main(string[] args)
        {
            var gameState = new GameState(new HashSet<(int, int)> {(0, 0), (0, 1), (0, 2), (1, 0), (2, 0), (3, 0)});
            while (true)
            {
                for (int y = 0; y < 40; y++)
                {
                    for (int x = 0; x < 40; x++)
                    {
                        Console.Write(gameState.Field((x, y)) ? "#" : ".");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("---------------------------end step-------------");

                Console.ReadKey();
                Console.WriteLine("----------------");
                gameState = gameState.NextTurn();
            }
        }
    }
}