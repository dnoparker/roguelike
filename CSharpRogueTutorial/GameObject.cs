using BearLib;
using RogueTutorial;
using System;

namespace CSharpRogueTutorial
{
    [Serializable]
    class GameObject
    {
        public char tile;
        public string color;
        public int x;
        public int y;

        public GameObject(char Tile, string Color, int X, int Y)
        {
            x = X;
            y = Y;
            tile = Tile;
            color = Color;
        }

        internal void Draw()
        {
            Terminal.Color(Terminal.ColorFromName(color));
            Terminal.PutExt(x, y, 0, 0, tile);
            Terminal.Color(Terminal.ColorFromName("white"));
        }

        internal void Move(int dx, int dy)
        {
            if (!MapMethods.MapBlocked(x + dx, y + dy))
            {
                x += dx;
                y += dy;
            }
        }
    }
}
