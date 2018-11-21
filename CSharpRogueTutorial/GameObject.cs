using BearLib;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Numerics;

namespace RogueLike
{
    [Serializable]
    class GameObject
    {
        public char tile;
        public string color;
        public Vector2 position;
        public Vector2 previousPosition;
        public List<Point> path = new List<Point>();

        public GameObject(char Tile, string Color, Vector2 Position)
        {
            position = Position;
            previousPosition = Position;
            tile = Tile;
            color = Color;
        }

        internal void Draw()
        {
            Terminal.Color(Terminal.ColorFromName(color));
            Terminal.PutExt((int)position.X, (int)position.Y, 0, 0, tile);
        }

       public virtual void Move(int dx, int dy)
        {
            previousPosition = new Vector2(position.X, position.Y);
            if (!MapGen.IsMapBlocked((int)position.X + dx, (int)position.Y + dy))
            {
                position.X += dx;
                position.Y += dy;
            }
        }

        public virtual void Update()
        {

        }

    }
}
