using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tools;
using Main;
using System.Threading.Tasks;

namespace CSharpRogueTutorial
{
    class humanoid: GameObject
    {
        public humanoid(char Tile, string Color, Vector2 Position) : base(Tile, Color, Position)
        {

        }

        public void moveToPath()
        {

            if (path.Count < 0)
            {
                return;
            }

            // Store the prevous position and make it not blocked
            //Then block the current position. This stops the player from walking ontop of this gameobject

            previousPosition = position;
            Rogue.GameWorld.Map.tiles[position.x, position.y].blocked = false;
            position.x = path[path.Count -2].X;
            position.y = path[path.Count -2].Y;
            Rogue.GameWorld.Map.tiles[position.x, position.y].blocked = true;
        }

        // This is called every movement made
        public override void Update()
        {
            base.Update(); // Also call gameobjects update function

            // thinking space.. should I chase the player?

            base.updatePath(); // get a path to player (need to make this explicit)
            moveToPath(); // Path towards player
        }
    }
}
