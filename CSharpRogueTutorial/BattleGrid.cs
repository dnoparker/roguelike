using BearLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;

namespace RogueLike
{
    class BattleGrid
    {
        public static bool[,] battleTilesPath;
        public static battleTile[,] battleTiles;
        public static Vector2 playerPosition;
        public static Vector2 enemyPosition;
        public static Vector2 targetPosition;
        public static List<Point> path = new List<Point>();
        public enum arrowDirection { up, down, left, right, none };
        public static arrowDirection arrowDir = arrowDirection.none;
        public enum battlephase { choose, move, drawknock, knock }
        public static battlephase currentPhase = battlephase.choose;
        public static int state = 0;
        public static int knockPower = 2;
        public static int dice;
        public static Random rnd;

        public BattleGrid()
        {
            //drawMap();
            rnd = new Random();
        }

        public static void Update()
        {
            Terminal.ClearArea(0, 0, Constants.screenWidth, Constants.screenHeight);

            drawMap();
            placePlayer(playerPosition);
            placeEnemy(enemyPosition);
            if (state % 2 == 0)
            {
                playerTurn();
            }
            else
            {
                enemyTurn();
            }

            Terminal.Refresh();
        }

        public static void playerTurn()
        {
            switch (currentPhase)
            {
                case battlephase.choose:
                    drawAttackArrow(enemyPosition);
                    break;
                case battlephase.move:
                    drawPath(playerPosition, targetPosition);
                    break;
                case battlephase.drawknock:
                    knockPlayer();
                    currentPhase = battlephase.knock;
                    break;
                case battlephase.knock:
                    knockPlayer();
                    break;
                default:
                    break;
            }
        }

        public static void enemyTurn()
        {

        }

        public static void drawPath(Vector2 startPosition, Vector2 endPostion)
        {

            path = tools.aStar.findBattlePath(startPosition, endPostion);
            for (int i = 0; i < path.Count; i++)
            {
                Terminal.Color(Color.FromArgb(255, 0, 255, 40));
                Terminal.PutExt(path[i].X, path[i].Y, 500, 25, '+');
                Terminal.Color(Color.FromArgb(255, 240, 234, 214));
            }
        }

        public static void drawAttackArrow(Vector2 Position)
        {
            switch (arrowDir)
            {
                case arrowDirection.up:
                    Console.WriteLine("I am suppose to be UP");
                    Terminal.Color(Color.FromArgb(255, 0, 255, 40));
                    Terminal.PutExt((int)Position.X, (int)Position.Y - 1, 500, 25, '>');
                    targetPosition = new Vector2((int)Position.X, (int)Position.Y - 1);
                    Terminal.Color(Color.FromArgb(255, 240, 234, 214));
                    break;
                case arrowDirection.down:
                    Console.WriteLine("I am suppose to be down");
                    Terminal.Color(Color.FromArgb(255, 0, 255, 40));
                    Terminal.PutExt((int)Position.X, (int)Position.Y + 1, 500, 25, '>');
                    targetPosition = new Vector2((int)Position.X, (int)Position.Y + 1);
                    Terminal.Color(Color.FromArgb(255, 240, 234, 214));
                    break;
                case arrowDirection.left:
                    Console.WriteLine("I am suppose to be left");
                    Terminal.Color(Color.FromArgb(255, 0, 255, 40));
                    Terminal.PutExt((int)Position.X - 1, (int)Position.Y, 500, 25, '>');
                    targetPosition = new Vector2((int)Position.X - 1, (int)Position.Y);
                    Terminal.Color(Color.FromArgb(255, 240, 234, 214));
                    break;
                case arrowDirection.right:
                    Console.WriteLine("I am suppose to be right");
                    Terminal.Color(Color.FromArgb(255, 0, 255, 40));
                    Terminal.PutExt((int)Position.X + 1, (int)Position.Y, 500, 25, '>');
                    targetPosition = new Vector2((int)Position.X + 1, (int)Position.Y);
                    Terminal.Color(Color.FromArgb(255, 240, 234, 214));
                    break;
                default:
                    break;
            }

        }

        public static void Start(char playerCharacter, char enemyCharacter)
        {
            Rogue.battleMode = true;
            Terminal.Layer(2);
            drawMap();
            playerPosition = new Vector2(10, 3);
            enemyPosition = new Vector2(10, 7);
            placePlayer(playerPosition);
            placeEnemy(enemyPosition);
        }

        public static void placePlayer(Vector2 position)
        {
            battleTilesPath[(int)playerPosition.X, (int)playerPosition.Y] = true;
            Terminal.Color(Color.FromArgb(255, 255, 255, 40));
            Terminal.PutExt((int)position.X, (int)position.Y, 500, 25, 'P');
            Terminal.Color(Color.FromArgb(255, 240, 234, 214));
            battleTilesPath[(int)position.X, (int)position.Y] = false;
        }

        public static void placeEnemy(Vector2 position)
        {
            battleTilesPath[(int)enemyPosition.X, (int)enemyPosition.Y] = true;
            Terminal.Color(Color.FromArgb(255, 255, 255, 40));
            Terminal.PutExt((int)position.X, (int)position.Y, 500, 25, 'E');
            Terminal.Color(Color.FromArgb(255, 240, 234, 214));
            battleTilesPath[(int)position.X, (int)position.Y] = false;
        }

        public static bool HandleKeys()
        {
            int key = Terminal.Read();

            switch (currentPhase)
            {
                case battlephase.choose:
                    if (key == Terminal.TK_LEFT)
                    {
                        arrowDir = arrowDirection.left;
                    }
                    if (key == Terminal.TK_RIGHT)
                    {
                        arrowDir = arrowDirection.right;
                    }
                    if (key == Terminal.TK_UP)
                    {
                        arrowDir = arrowDirection.up;
                    }
                    if (key == Terminal.TK_DOWN)
                    {
                        arrowDir = arrowDirection.down;
                    }

                    if (key == Terminal.TK_ENTER)
                    {
                        if (arrowDir != arrowDirection.none)
                        {
                            currentPhase = battlephase.move;
                        }
                    }

                    break;
                case battlephase.move:
                    // MOVE
                    if (key == Terminal.TK_ENTER)
                    {
                        if (path.Count == 0)
                        {
                            currentPhase = battlephase.drawknock;
                        }
                        else
                        {
                            Vector2 vec = new Vector2(path[0].X, path[0].Y);
                            playerPosition = vec;
                        }

                    }
                    break;

                case battlephase.knock:
                    if (key == Terminal.TK_ENTER)
                    {
                        if (knockPower == 0)
                        {
                            state++;
                            knockPower = 2;
                            currentPhase = battlephase.choose;

                        }
                        else {
                            Vector2 vec = new Vector2(path[0].X, path[0].Y);
                            enemyPosition = vec;
                            knockPower--;
                        }
                    }

                    break;
                default:
                    break;
            }

            return false;
        }

        public static void drawMap()
        {
            battleTilesPath = new bool[21, 11];
            battleTiles = new battleTile[21, 11];
            for (int x = 0; x < 21; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    // This is the player area
                    Terminal.Color(Color.FromArgb(145, 243, 243, 247));
                    Terminal.PutExt(x, y, 500, 25, 'X');
                    Terminal.Color(Color.FromArgb(255, 240, 234, 214));
                    battleTilesPath[x, y] = true;
                    battleTiles[x, y] = new battleTile(false);

                    if (x == 1 || y == 1 || x == 19 || y == 9)
                    {
                        // This is the danger area
                        Terminal.Color(Color.FromArgb(255, 56, 243, 247));
                        Terminal.PutExt(x, y, 500, 25, 'X');
                        Terminal.Color(Color.FromArgb(255, 240, 234, 214));
                        battleTilesPath[x, y] = true;
                        battleTiles[x, y] = new battleTile(true);
                    }

                    if (x == 0 || y == 0 || x == 20 || y == 10)
                    {
                        // This is the outer area that is blocked
                        Terminal.Color(Color.FromArgb(255, 255, 50, 40));
                        Terminal.PutExt(x, y, 500, 25, 'X');
                        Terminal.Color(Color.FromArgb(255, 240, 234, 214));
                        battleTilesPath[x, y] = false;
                        battleTiles[x, y] = new battleTile(false);
                    }
                }
            }
        }

        public static void knockPlayer()
        {
            if (state % 2 == 0)
            {
                Vector2 knock = Vector2.Zero;
                //player turn
                switch (arrowDir)
                {
                    case arrowDirection.up:
                        knock = enemyPosition + (Vector2.UnitY * knockPower);
                        drawPath(enemyPosition, knock);
                        break;
                    case arrowDirection.down:
                        knock = enemyPosition - (Vector2.UnitY * knockPower);
                        drawPath(enemyPosition, knock);
                        break;
                    case arrowDirection.left:
                        knock = enemyPosition + (Vector2.UnitX * knockPower);
                        drawPath(enemyPosition, knock);
                        break;
                    case arrowDirection.right:
                        knock = enemyPosition - (Vector2.UnitX * knockPower);
                        drawPath(enemyPosition, knock);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Vector2 knock = Vector2.Zero;
                //player turn
                switch (arrowDir)
                {
                    case arrowDirection.up:
                        Console.WriteLine("UP");
                        knock = playerPosition + (Vector2.UnitY * knockPower);
                        drawPath(playerPosition, knock);
                        break;
                    case arrowDirection.down:
                        Console.WriteLine("DOWN");
                        knock = playerPosition - (Vector2.UnitY * knockPower);
                        drawPath(playerPosition, knock);
                        break;
                    case arrowDirection.left:
                        Console.WriteLine("LEFT");
                        knock = playerPosition + (Vector2.UnitX * knockPower);
                        drawPath(playerPosition, knock);
                        break;
                    case arrowDirection.right:
                        Console.WriteLine("RIGHT");
                        knock = playerPosition - (Vector2.UnitX * knockPower);
                        drawPath(playerPosition, knock);
                        break;
                    default:
                        break;
                }

            }
        }
    }

    class battleTile
    {
        public static bool isDanger = false;

        public battleTile(bool _isDanger)
        {
            isDanger = _isDanger;
        }
    }
}
