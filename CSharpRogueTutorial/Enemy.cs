using System;
using Tools;

namespace RogueLike
{
    class Enemy : GameObject
    {
        public Enemy(char Tile, string Color, Vector2Int Position) : base(Tile, Color, Position)
        {

        }

        public override void Start()
        {
            base.Start();
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
            Rogue.GameWorld.Map.tiles[position.x, position.y].blocked = false;

            if (path.Count - 2 < 0)
            {
                // Sometimes the path length is only 1 (right next to the player)
                position.x = path[0].X;
                position.y = path[0].Y;

            }
            else
            {
                position.x = path[path.Count - 2].X;
                position.y = path[path.Count - 2].Y;
            }
            Rogue.GameWorld.Map.tiles[(int)position.x, (int)position.y].blocked = true;
        }

        public override void Update()
        {
            base.Update();
            FindPath(Rogue.GameWorld.Player.position, position); //TODO: Needs FOV
            if (path.Count < 6)
            {
                MoveToPlayer();
                Rogue.EventManager.TriggerEvent(new OnPlayerSpotted("GOBLIN"));
            }
        }

        public void FindPath(Vector2Int startPosition, Vector2Int endPosition)
        {
            path = AStar.FindPath(startPosition, endPosition);
        }
    }
}
