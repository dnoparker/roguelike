﻿using System;
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
            Rogue.GameWorld.Map.tiles[position.x, position.y].blocked = false;
            if(path.Count-2 < 0)
            {
                // Sometimes the path length is only 1 (right next to the player)
                position.x = path[0].X;
                position.y = path[0].Y;
            } else
            {
                position.x = path[path.Count - 2].X;
                position.y = path[path.Count - 2].Y;
            }
            Rogue.GameWorld.Map.tiles[position.x, position.y].blocked = true;
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
