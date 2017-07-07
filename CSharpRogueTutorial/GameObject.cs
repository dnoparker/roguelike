using BearLib;
using tools;
using Main;
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
        public Vector2 previousPosition;

        public GameObject(char Tile, string Color, int X, int Y)
        {
            x = X;
            y = Y;
            previousPosition = new Vector2(x, y);
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
            previousPosition = new Vector2(x, y);
            if (!MapMethods.MapBlocked(x + dx, y + dy))
            {
                for (int i = 0; i < Rogue.GameWorld.Map.floorTiles.Count; i++)
                {
                    if(Vector2.vector2AreEqual(Rogue.GameWorld.Map.floorTiles[i].position, new Vector2(x, y)))
                    {
                        Rogue.GameWorld.Map.floorTiles[i].footprint();
                    }
                }
                x += dx;
                y += dy;
            }
        }
    }
}
