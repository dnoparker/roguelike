using System.Collections.Generic;
using SimpleAStarExample;
using System.Drawing;
using RogueLike;
using System.Numerics;

namespace Tools
{

    public class AStar
    {
        public static List<Point> FindPath(Vector2Int startPostion,Vector2Int endPosition)
        {
            var startLocation = new Point(startPostion.x, startPostion.y);
            var endLocation = new Point(endPosition.x, endPosition.y);
            SearchParameters searchParameters = new SearchParameters(startLocation, endLocation, Rogue.GameWorld.Map.simpleAStarMap);
            PathFinder pathFinder = new PathFinder(searchParameters);
            List<Point> path = pathFinder.FindPath();
            return path;
        }
    }

    public class Vector2Int
    {
        public int x;
        public int y;

        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
