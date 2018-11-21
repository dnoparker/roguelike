using System.Collections.Generic;
using SimpleAStarExample;
using System.Drawing;
using RogueLike;
using System.Numerics;

namespace Tools
{

    public class AStar
    {
        public static List<Point> FindPath(Vector2 startPostion,Vector2 endPosition)
        {
            var startLocation = new Point((int)startPostion.X, (int)startPostion.Y);
            var endLocation = new Point((int)endPosition.X, (int)endPosition.Y);
            SearchParameters searchParameters = new SearchParameters(startLocation, endLocation, Rogue.GameWorld.Map.simpleAStarMap);
            PathFinder pathFinder = new PathFinder(searchParameters);
            List<Point> path = pathFinder.FindPath();
            return path;
        }
    }
}
