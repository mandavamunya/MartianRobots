using System;
using System.Collections.Generic;
using System.IO;

namespace MartianRobots
{
    public enum Orientation { N, E, S, W }

    public struct Position
    {
        public int X;
        public int Y;
        public Orientation Orientation;

        public Position(int x, int y, Orientation orientation)
        {
            X = x;
            Y = y;
            Orientation = orientation;
        }

        public override string ToString()
        {
            return $"{X} {Y} {Orientation}";
        }
    }

    public class Robot
    {
        public Position Position;
        public bool IsLost = false;

        private static readonly Dictionary<Orientation, (int dx, int dy)> MoveForward = new()
        {
            { Orientation.N, (0, 1) },
            { Orientation.E, (1, 0) },
            { Orientation.S, (0, -1) },
            { Orientation.W, (-1, 0) },
        };

        public Robot(Position position)
        {
            Position = position;
        }

        public void Execute(string instructions, int maxX, int maxY, HashSet<(int, int, Orientation)> scent)
        {
            foreach (char command in instructions)
            {
                if (IsLost) break;

                switch (command)
                {
                    case 'L':
                        Position.Orientation = (Orientation)(((int)Position.Orientation + 3) % 4);
                        break;
                    case 'R':
                        Position.Orientation = (Orientation)(((int)Position.Orientation + 1) % 4);
                        break;
                    case 'F':
                        var move = MoveForward[Position.Orientation];
                        int newX = Position.X + move.dx;
                        int newY = Position.Y + move.dy;

                        if (newX < 0 || newY < 0 || newX > maxX || newY > maxY)
                        {
                            if (!scent.Contains((Position.X, Position.Y, Position.Orientation)))
                            {
                                scent.Add((Position.X, Position.Y, Position.Orientation));
                                IsLost = true;
                            }
                        }
                        else
                        {
                            Position.X = newX;
                            Position.Y = newY;
                        }
                        break;
                }
            }
        }

        public string GetOutput()
        {
            return $"{Position}{(IsLost ? " LOST" : "")}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("input.txt");
            var gridSize = lines[0].Split(' ');
            int maxX = int.Parse(gridSize[0]);
            int maxY = int.Parse(gridSize[1]);

            var scent = new HashSet<(int, int, Orientation)>();
            for (int i = 1; i < lines.Length; i += 2)
            {
                var positionParts = lines[i].Split(' ');
                int x = int.Parse(positionParts[0]);
                int y = int.Parse(positionParts[1]);
                Orientation orientation = Enum.Parse<Orientation>(positionParts[2]);

                var robot = new Robot(new Position(x, y, orientation));
                string instructions = lines[i + 1];

                robot.Execute(instructions, maxX, maxY, scent);
                Console.WriteLine(robot.GetOutput());
            }
        }
    }
}