using System;
using System.Collections.Generic;
using SimpleAStarExample;
using System.Drawing;
using Main;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tools
{
    public class Vector2
    {
        public int x;
        public int y;

        public Vector2(int X, int Y)
        {
            x = X;
            y = Y;
        }

        public static bool vector2AreEqual(Vector2 p1, Vector2 p2)
        {
            if (p1.x == p2.x && p1.y == p2.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class aStar
    {
        public static List<Point> findPath()
        {
            var startLocation = new Point(Rogue.GameWorld.Player.position.x, Rogue.GameWorld.Player.position.y);
            var endLocation = new Point(Rogue.GameWorld.debugPath.position.x, Rogue.GameWorld.debugPath.position.y);
            SearchParameters searchParameters = new SearchParameters(startLocation, endLocation, Rogue.GameWorld.Map.simpleAStarMap);
            PathFinder pathFinder = new PathFinder(searchParameters);
            List<Point> path = pathFinder.FindPath();
            return path;
        }
    }
}
