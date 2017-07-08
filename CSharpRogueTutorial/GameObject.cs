using BearLib;
using tools;
using Main;
using System;
using System.Drawing;
using System.Collections.Generic;

namespace CSharpRogueTutorial
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
            Terminal.PutExt(position.x, position.y, 0, 0, tile);
            Terminal.Color(Terminal.ColorFromName("white"));
        }

       public virtual void Move(int dx, int dy)
        {
            previousPosition = new Vector2(position.x, position.y);
            if (!MapMethods.MapBlocked(position.x + dx, position.y + dy))
            {
                position.x += dx;
                position.y += dy;
            }
        }

        public virtual void Update()
        {
            Draw();
        }

    }
}
