using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tools;
using System.Numerics;
using System.Threading.Tasks;

namespace RogueLike
{
    class humanoid: GameObject
    {
        public humanoid(char Tile, string Color, Vector2 Position) : base(Tile, Color, Position)
        {

        }

        public void moveToPlayer()
        {

            if (path.Count < 0)
            {
                return;
            }
            Random rnd = new Random();
            int dice = rnd.Next(1, 11);

            if (dice == 6)
            {
                return;
            }
            // Store the prevous position and make it not blocked
            //Then block the current position. This stops the player from walking ontop of this gameobject

            previousPosition = position;
            Rogue.GameWorld.Map.tiles[(int)position.X, (int)position.Y].blocked = false;
            if(path.Count == 0)
            {
                return;
            }
            if(path.Count-2 < 0)
            {
                // Sometimes the path length is only 1 (right next to the player)
                position.X = path[0].X;
                position.Y = path[0].Y;
            } else
            {
                position.X = path[path.Count - 2].X;
                position.Y = path[path.Count - 2].Y;
            }
            Rogue.GameWorld.Map.tiles[(int)position.X, (int)position.Y].blocked = true;
        }

        // This is called every movement made
        public override void Update()
        {
            base.Update(); // Also call gameobjects update function
            findPath(Rogue.GameWorld.Player.position, position);
            moveToPlayer();
        }


        public void findPath(Vector2 startPosition, Vector2 endPosition)
        {
            path = aStar.findPath(startPosition, endPosition);
        }
    }
}
