using Main;
using tools;
using System;
using System.Collections.Generic;
using System.Linq;
using BearLib;

namespace CSharpRogueTutorial
{
    [Serializable]
    class Tile
    {
        public bool blocked;
        public bool explored;
        public bool visited;
        public bool corridor;

        public Tile(bool Blocked, bool _corridor)
        {
            _corridor = corridor;
            blocked = Blocked;
            explored = false;
            visited = false;
        }
    }

    [Serializable]
    class floorTile: IFloor
    {
        public enum type { concrete, soil}
        public type floorType;
        public Vector2 position;

        public floorTile (type _floorType, Vector2 _position)
        {
            position = _position;
            floorType = _floorType;
        }

        public void footprint()
        {
            Vector2 playerPos = new Vector2(Rogue.GameWorld.Player.previousPosition.x, Rogue.GameWorld.Player.previousPosition.y);
            if (tools.Vector2.vector2AreEqual(playerPos, position))
            {
                Terminal.Color(Terminal.ColorFromName("white"));
                Terminal.PutExt(position.x, position.y, 0, 0, 'R');
                Terminal.Color(Terminal.ColorFromName("white"));
            }
        }
    }

    class Room
    {
        public int startX;
        public int startY;
        public int endX;
        public int endY;

        public Room(int X, int Y, int Width, int Height)
        {
            startX = X;
            startY = Y;
            endX = X + Width;
            endY = Y + Height;
        }

        internal Vector2 Center()
        {
            int centerX = (startX + endX) / 2;
            int centerY = (startY + endY) / 2;

            return new Vector2(centerX, centerY);
        }

        internal bool Intersect(Room otherRoom)
        {
            return (startX <= otherRoom.endX && endX >= otherRoom.startX && startY <= otherRoom.endY && endY >= otherRoom.startY);
        }
    }

    class MapMethods
    {
        public static Random rand = new Random();          

        public static GameMap MakeMap()
        {
            Tile[,] tiles = BlankTiles(true, false);

            List<Room> roomList = new List<Room>();

            int roomIndex = 0;

            for (int i = 0; i < Constants.roomCount; i++)
            {
                int width = rand.Next(5, 10);
                int height = rand.Next(5, 10);

                int x = rand.Next(0, Constants.mapWidth - width - 1);
                int y = rand.Next(0, Constants.mapHeight - height - 1);

                Room newRoom = new Room(x, y, width, height);

                if (!Intersects(roomList, newRoom))
                {
                    CreateRoom(newRoom, ref tiles);

                    Vector2 newCenter = newRoom.Center();
                    

                    if (roomIndex == 0)
                    {
                        Rogue.GameWorld.Player.x = newCenter.x;
                        Rogue.GameWorld.Player.y = newCenter.y;
                    }
                    else
                    {
                        Vector2 previousCenter = roomList[roomIndex - 1].Center();

                        if (rand.Next(0, 2) == 0)
                        {
                            CreateHorizontalTunnel(previousCenter.x, newCenter.x, previousCenter.y, ref tiles);
                            CreateVerticalTunnel(previousCenter.y, newCenter.y, newCenter.x, ref tiles);
                        }
                        else
                        {
                            CreateVerticalTunnel(previousCenter.y, newCenter.y, previousCenter.x, ref tiles);
                            CreateHorizontalTunnel(previousCenter.x, newCenter.x, newCenter.y, ref tiles);
                        }
                    }
                    roomList.Add(newRoom);
                    roomIndex += 1;
                }
            }

            return new GameMap(tiles);
        }

        private static bool Intersects(List<Room> roomList, Room currentRoom)
        {
            foreach (Room otherRoom in roomList)
            {
                if (currentRoom.Intersect(otherRoom))
                {
                    return true;
                }
            }

            return false;
        }

        private static void CreateRoom(Room room, ref Tile[,] tiles)
        {
            for (int x = room.startX + 1; x < room.endX; x++)
            {
                for (int y = room.startY + 1; y < room.endY; y++)
                {
                    tiles[x, y].blocked = false;
                }
            }
        }

        private static int DistanceBetween(int x1, int y1, int x2, int y2)
        {
            return (int)(Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2)));
        }

        private static void CreateHorizontalTunnel(int x1, int x2, int y, ref Tile[,] tiles)
        {
            for (int x = Math.Min(x1, x2); x < Math.Max(x1, x2) + 2; x++)
            {
                // Add a second row. Makes the corridoors twice the width
                for (int i = 0; i < 2; i++)
                {
                    tiles[x + i, y].corridor = true;
                    tiles[x + i, y].blocked = false;
                }
            }
        }

        private static void CreateVerticalTunnel(int y1, int y2, int x, ref Tile[,] tiles)
        {
            for (int y = Math.Min(y1, y2); y < Math.Max(y1, y2) + 2; y++)
            {
                for (int i = 0; i < 2; i++)
                {
                    tiles[x+i, y].corridor = true;
                    tiles[x+i, y].blocked = false;
                }
            }
        }

        private static Tile[,] BlankTiles(bool blocked, bool corridor)
        {
            Tile[,] map = new Tile[Constants.mapWidth, Constants.mapHeight];

            for (int x = 0; x < Constants.mapWidth; x++)
            {
                for (int y = 0; y < Constants.mapHeight; y++)
                {
                    map[x, y] = new Tile(blocked, corridor);
                }
            }

            return map;
        }

        public static bool MapBlocked(int x, int y)
        {
            return Rogue.GameWorld.Map.tiles[x, y].blocked;
        }
    }
}
