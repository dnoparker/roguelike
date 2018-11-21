using System;
using Tools;
using System.Numerics;

namespace RogueLike
{
    class Enemy : GameObject
    {
        public Enemy(char Tile, string Color, Vector2 Position) : base(Tile, Color, Position)
        {

        }

        public void MoveToPlayer()
        {
            if (path.Count <= 0)
            {
                return;
            }

            // Store the prevous position and make it not blocked
            //Then block the current position. This stops the player from walking ontop of this gameobject

            previousPosition = position;
            Rogue.GameWorld.Map.tiles[(int)position.X, (int)position.Y].blocked = false;

            if (path.Count - 2 < 0)
            {
                // Sometimes the path length is only 1 (right next to the player)
                position.X = path[0].X;
                position.Y = path[0].Y;

            }
            else
            {
                position.X = path[path.Count - 2].X;
                position.Y = path[path.Count - 2].Y;
            }
            Rogue.GameWorld.Map.tiles[(int)position.X, (int)position.Y].blocked = true;
        }

        public override void Update()
        {
            base.Update();
            FindPath(Rogue.GameWorld.Player.position, position); //TODO: Needs FOV
            if (path.Count < 6)
            {
                MoveToPlayer();
            }
        }

        public void FindPath(Vector2 startPosition, Vector2 endPosition)
        {
            path = AStar.FindPath(startPosition, endPosition);
        }
    }
}
