using BearLib;
using tools;
using Main;
using System;

namespace rougeLike
{
    // Store everything that makes a gameobject what it is

    [Serializable]
    class GameObject
    {
        public char tile;
        public string color;
        public Vector2 position;

        public GameObject(char Tile, string Color, Vector2 Pos)
        {
            position = Pos;
            tile = Tile;
            color = Color;
        }

        internal void Draw()
        {
            Terminal.Color(Terminal.ColorFromName(color));
            Terminal.PutExt(position.x,position.y, 0, 0, tile);
            Terminal.Color(Terminal.ColorFromName("white"));
        }

        internal void Move(int x, int y)
        {
            if (!MapMethods.MapBlocked(position.x + x, position.y + y))
            {
                position.x += x;
                position.y += y;
            }
        }
    }
}
