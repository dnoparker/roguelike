using System;
using System.Collections.Generic;
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

}
