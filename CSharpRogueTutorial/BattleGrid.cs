using BearLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RogueLike
{
    class BattleGrid
    {
        public Tile[,] battleTiles;

        public BattleGrid()
        {
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    Terminal.Color(Color.FromArgb(145, 243, 243, 247));
                    Terminal.PutExt(x , y, 500, 25, 'X');
                    Terminal.Color(Color.FromArgb(255, 240, 234, 214));

                    if( x == 0 || y == 0 || x == 19 || y == 9)
                    {
                        Terminal.Color(Color.FromArgb(255, 56, 243, 247));
                        Terminal.PutExt(x, y, 500, 25, 'X');
                        Terminal.Color(Color.FromArgb(255, 240, 234, 214));
                    }
                }
            }
        }
    }
}
