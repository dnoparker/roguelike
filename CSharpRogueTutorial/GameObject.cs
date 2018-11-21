using BearLib;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Numerics;
using Tools;

namespace RogueLike
{
    [Serializable]
     class GameObject
    {
        public char tile;
        public string color;
        public Vector2Int position;
        public Vector2Int previousPosition;
        public List<Point> path = new List<Point>();

        public GameObject(char Tile, string Color, Vector2Int Position)
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
        }

       public virtual void Move(int dx, int dy)
        {
            previousPosition = new Vector2Int(position.x, position.y);
            if (!MapGen.IsMapBlocked(position.x + dx, position.y + dy))
            {
                position.x += dx;
                position.y += dy;
            }
        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

    }
}
