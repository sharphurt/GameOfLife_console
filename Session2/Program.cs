using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace Session2
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(new HashSet<Point>
            {
                new Point(1, 1), new Point(2, 1), new Point(3, 1)
            });
            var size = 20;
            while (true)
            {
                for (var x = 0; x < size; x++)
                for (var y = 0; y < size; y++)
                {
                    Console.CursorLeft = x;
                    Console.CursorTop = y;

                    var symbol = game.Field.Contains(new Point(x, y)) ? "#" : "_";
                    Console.Write(symbol);
                }

                game.MakeTurn();
                Thread.Sleep(50);
            }
        }
    }
}