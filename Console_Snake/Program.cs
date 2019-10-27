using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace Console_Snake
{
    class Vector2f
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Vector2f(int _X, int _Y)
        {
            X = _X;
            Y = _Y;
        }
    }

    class Map
    {
        public string[,] MAP = new string[70, 25];

        public Map()
        {
            for (int i = 0; i <= MAP.GetLength(0) - 1; i++)
            {
                MAP[i, 0] = "#";
            }

            for (int j = 1; j <= MAP.GetLength(1) - 1; j++)
            {
                for (int k = 0; k <= MAP.GetLength(0) - 1; k++)
                {
                    if (k == 0 || k == MAP.GetLength(0) - 1)
                    {
                        MAP[k, j] = "#";
                    }
                    else
                    {
                        MAP[k, j] = " ";
                    }
                }
            }

            for (int x = 0; x <= MAP.GetLength(0) - 1; x++)
            {
                MAP[x, MAP.GetLength(1) - 1] = "#";
            }
        }
    }

    class Player : Map
    {
        protected List<Vector2f> POSITION = new List<Vector2f>();

        protected int[] DIRECTORY = new int[4]
        {
            0,  // left
            0,  // up
            0,  // right
            0   // down
        };

        public Player()
        {
            Random RAND = new Random();
            int X_Axis = RAND.Next(1, MAP.GetLength(0) - 2);
            int Y_Axis = RAND.Next(1, MAP.GetLength(1) - 2);

            POSITION.Add(new Vector2f(X_Axis, Y_Axis));
            POSITION.Add(new Vector2f(X_Axis + 1, Y_Axis));
            POSITION.Add(new Vector2f(X_Axis + 2, Y_Axis));
            POSITION.Add(new Vector2f(X_Axis + 3, Y_Axis));
            POSITION.Add(new Vector2f(X_Axis + 4, Y_Axis));
        }

        protected void drawPlayer()
        {
            for(int y = 0; y <= MAP.GetLength(1) - 1; y++)
            {
                for (int x = 0; x <= MAP.GetLength(0) - 1; x++)
                {
                    foreach(Vector2f item in POSITION)
                    {
                        if(item.X == x && item.Y == y)
                        {
                            Console.Write("O");
                        }
                    }
                    Console.Write(MAP[x, y]);
                }
                Console.Write("\n");
            }
        }

        private List<Vector2f> ReMapIndexes()
        {
            var T = POSITION[POSITION.Count - 1];

            if(POSITION.Count > 1)
            {
                for (int i = POSITION.Count - 1; i >= 1; i--)
                {
                    POSITION[i] = POSITION[i - 1];
                }
            }


            POSITION[0] = T;
            return POSITION;

        }


        protected void mLeft()
        {
            if(POSITION[0].X == 1)
            {
                POSITION[POSITION.Count - 1].X = MAP.GetLength(0) - 2;
                POSITION[POSITION.Count - 1].Y = POSITION[0].Y;
                ReMapIndexes();
            }
            else if(POSITION[0].X > 1)
            {
                POSITION[POSITION.Count - 1].X = POSITION[0].X - 1;
                POSITION[POSITION.Count - 1].Y = POSITION[0].Y;
                ReMapIndexes();
            }
        }

        protected void mUp()
        {
            if (POSITION[0].Y == 1)
            {
                POSITION[POSITION.Count - 1].X = POSITION[0].X;
                POSITION[POSITION.Count - 1].Y = MAP.GetLength(1) - 2;
                ReMapIndexes();
            }
            else if (POSITION[0].Y > 1)
            {
                POSITION[POSITION.Count - 1].X = POSITION[0].X;
                POSITION[POSITION.Count - 1].Y = POSITION[0].Y - 1;
                ReMapIndexes();
            }
        }

        protected void mRight()
        {
            if (POSITION[0].X == MAP.GetLength(0) - 2)
            {
                POSITION[POSITION.Count - 1].X = 1;
                POSITION[POSITION.Count - 1].Y = POSITION[0].Y;
                ReMapIndexes();
            }
            else if (POSITION[0].X < MAP.GetLength(0) - 2)
            {
                POSITION[POSITION.Count - 1].X = POSITION[0].X + 1;
                POSITION[POSITION.Count - 1].Y = POSITION[0].Y;
                ReMapIndexes();
            }
        }

        protected void mDown()
        {
            if (POSITION[0].Y == MAP.GetLength(1) - 2)
            {
                POSITION[POSITION.Count - 1].X = POSITION[0].X;
                POSITION[POSITION.Count - 1].Y = 1;
                ReMapIndexes();
            }
            else if (POSITION[0].Y < MAP.GetLength(1) - 2)
            {
                POSITION[POSITION.Count - 1].X = POSITION[0].X;
                POSITION[POSITION.Count - 1].Y = POSITION[0].Y + 1;
                ReMapIndexes();
            }
        }
    }

    class Game : Player
    {
        private readonly int _Time = 300;

        public Game()
        {
            while(true)
            {

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.LeftArrow)
                    {
                        DIRECTORY[0] = 1;
                        DIRECTORY[1] = 0;
                        DIRECTORY[2] = 0;
                        DIRECTORY[3] = 0;
                    }
                    else if(keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        DIRECTORY[0] = 0;
                        DIRECTORY[1] = 1;
                        DIRECTORY[2] = 0;
                        DIRECTORY[3] = 0;
                    }
                    else if (keyInfo.Key == ConsoleKey.RightArrow)
                    {
                        DIRECTORY[0] = 0;
                        DIRECTORY[1] = 0;
                        DIRECTORY[2] = 1;
                        DIRECTORY[3] = 0;
                    }
                    else if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        DIRECTORY[0] = 0;
                        DIRECTORY[1] = 0;
                        DIRECTORY[2] = 0;
                        DIRECTORY[3] = 1;
                    }
                }

                if(DIRECTORY[0] == 1)
                {
                    mLeft();
                }
                else if (DIRECTORY[1] == 1)
                {
                    mUp();
                }
                else if (DIRECTORY[2] == 1)
                {
                    mRight();
                }
                else if (DIRECTORY[3] == 1)
                {
                    mDown();
                }

                drawPlayer();

                Console.WriteLine($"Player X({POSITION[0].X}) Y({POSITION[0].Y})");


                Tick(ref _Time);
            }
        }

        private void Tick(ref int Time)
        {
            System.Threading.Thread.Sleep(Time);
            Console.Clear();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Game g = new Game();
        }
    }
}