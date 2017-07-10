using BearLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    Terminal.PutExt(x , y, 500, 200, 'X');
                }
            }
        }
    }
}
