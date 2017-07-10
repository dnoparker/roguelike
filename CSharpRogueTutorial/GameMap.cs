using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    class GameMap
    {
        public Tile[,] tiles;
        public bool[,] simpleAStarMap;


        public GameMap(Tile[,] Tiles)
        {
            tiles = Tiles;
            simpleAStarMap = new bool[Constants.mapWidth, Constants.mapHeight];
            for (int x = 0; x < Constants.mapWidth; x++)
            {
                for (int y = 0; y < Constants.mapHeight; y++)
                {
                    if (tiles[x, y].blocked)
                    {
                        simpleAStarMap[x, y] = false;
                        continue;
                    }
                    else
                    {
                        simpleAStarMap[x, y] = true;
                    }
                }
            }
        }
    }
}
