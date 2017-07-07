using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpRogueTutorial
{
    class GameMap
    {
        public Tile[,] tiles;

        public GameMap(Tile[,] Tiles)
        {
            tiles = Tiles;
        }
    }
}
