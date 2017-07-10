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
        public static bool[,] battleTiles;
        public static Vector2 playerPosition;
        public static Vector2 enemyPosition;
        public static Vector2 targetPosition;
        public enum arrowDirection { up,down,left,right};
        public static arrowDirection arrowDir = arrowDirection.up;
        public enum battlephase { choose, execute }
        public static battlephase currentPhase = battlephase.choose;
        public static int state = 0;

        public BattleGrid()
        {
            //drawMap();
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
            } else
            {
                enemyTurn();
            }

            Terminal.Refresh();
        }

        public static void playerTurn()
        {
            Console.WriteLine("Player Choosing Attack Direction");
            targetPosition = enemyPosition;
            Console.WriteLine(enemyPosition);
            switch (currentPhase)
            {
                case battlephase.choose:
                    drawAttackArrow(enemyPosition);
                    break;
                case battlephase.execute:
                    break;
                default:
                    break;
            }
        }

        public static void enemyTurn()
        {
            Console.WriteLine("Enemy Turn");
        }

        public static void drawAttackArrow(Vector2 targetPosition)
        {
            Console.WriteLine("Drawing arrow!");

            switch (arrowDir)
            {
                case arrowDirection.up:
                    Terminal.Color(Color.FromArgb(255, 0, 255, 40));
                    Terminal.PutExt((int)targetPosition.X, (int)targetPosition.Y - 1, 500, 25, '>');
                    Terminal.Color(Color.FromArgb(255, 240, 234, 214));
                    break;
                case arrowDirection.down:
                    Terminal.Color(Color.FromArgb(255, 0, 255, 40));
                    Terminal.PutExt((int)targetPosition.X, (int)targetPosition.Y + 1, 500, 25, '>');
                    Terminal.Color(Color.FromArgb(255, 240, 234, 214));
                    break;
                case arrowDirection.left:
                    Terminal.Color(Color.FromArgb(255, 0, 255, 40));
                    Terminal.PutExt((int)targetPosition.X -1, (int)targetPosition.Y, 500, 25, '>');
                    Terminal.Color(Color.FromArgb(255, 240, 234, 214));
                    break;
                case arrowDirection.right:
                    Terminal.Color(Color.FromArgb(255, 0, 255, 40));
                    Terminal.PutExt((int)targetPosition.X +1, (int)targetPosition.Y, 500, 25, '>');
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
            playerPosition = new Vector2(10, 2);
            enemyPosition = new Vector2(10, 8);
            placePlayer(playerPosition);
            placeEnemy(enemyPosition);
            //drawMap();

        }

        public static void placePlayer (Vector2 position)
        {
            Terminal.Color(Color.FromArgb(255, 255, 255, 40));
            Terminal.PutExt((int)position.X, (int)position.Y, 500, 25, 'P');
            Terminal.Color(Color.FromArgb(255, 240, 234, 214));
        }

        public static void placeEnemy(Vector2 position)
        {
            Terminal.Color(Color.FromArgb(255, 255, 255, 40));
            Terminal.PutExt((int)position.X, (int)position.Y, 500, 25, 'E');
            Terminal.Color(Color.FromArgb(255, 240, 234, 214));
        }

        public static bool HandleKeys()
        {
            int key = Terminal.Read();

            // WAIT
            if (key == Terminal.TK_ENTER)
            {
                state++;
            }

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

                    break;
                case battlephase.execute:
                    break;
                default:
                    break;
            }

            //drawAttackArrow(targetPosition);

            return false;
        }

        public static void drawMap()
        {
            battleTiles = new bool[21, 11];
            for (int x = 0; x < 21; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    // This is the player area
                    Terminal.Color(Color.FromArgb(145, 243, 243, 247));
                    Terminal.PutExt(x, y, 500, 25, 'X');
                    Terminal.Color(Color.FromArgb(255, 240, 234, 214));
                    battleTiles[x, y] = false;

                    if (x == 1 || y == 1 || x == 19 || y == 9)
                    {
                        // This is the danger area
                        Terminal.Color(Color.FromArgb(255, 56, 243, 247));
                        Terminal.PutExt(x, y, 500, 25, 'X');
                        Terminal.Color(Color.FromArgb(255, 240, 234, 214));
                        battleTiles[x, y] = true;
                    }

                    if (x == 0 || y == 0 || x == 20 || y == 10)
                    {
                        // This is the outer area that is blocked
                        Terminal.Color(Color.FromArgb(255, 255, 50, 40));
                        Terminal.PutExt(x, y, 500, 25, 'X');
                        Terminal.Color(Color.FromArgb(255, 240, 234, 214));
                        battleTiles[x, y] = false;
                    }
                }
            }
        }


    }
}
