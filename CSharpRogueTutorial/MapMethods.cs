using RogueTutorial;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpRogueTutorial
{
    [Serializable]
    class Tile
    {
        public bool blocked;
        public bool explored;
        public bool visited;

        public Tile(bool Blocked)
        {
            blocked = Blocked;
            explored = false;
            visited = false;
        }
    }

    class Coordinate
    {
        public int x;
        public int y;

        public Coordinate(int X, int Y)
        {
            x = X;
            y = Y;
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

        internal Coordinate Center()
        {
            int centerX = (startX + endX) / 2;
            int centerY = (startY + endY) / 2;

            return new Coordinate(centerX, centerY);
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
            Tile[,] tiles = BlankTiles(true);

            List<Room> roomList = new List<Room>();

            int roomCount = 0;

            for (int i = 0; i < 20; i++)
            {
                int width = rand.Next(5, 10);
                int height = rand.Next(5, 10);

                int x = rand.Next(0, Constants.MapWidth - width - 1);
                int y = rand.Next(0, Constants.MapHeight - height - 1);

                Room newRoom = new Room(x, y, width, height);

                if (!Intersects(roomList, newRoom))
                {
                    CreateRoom(newRoom, ref tiles);

                    Coordinate newCenter = newRoom.Center();
                    

                    if (roomCount == 0)
                    {
                        Rogue.GameWorld.Player.x = newCenter.x;
                        Rogue.GameWorld.Player.y = newCenter.y;
                    }
                    else
                    {
                        Coordinate previousCenter = roomList[roomCount - 1].Center();

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
                    roomCount += 1;
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
            for (int x = Math.Min(x1, x2); x < Math.Max(x1, x2) + 1; x++)
            {
                tiles[x, y].blocked = false;
            }
        }

        private static void CreateVerticalTunnel(int y1, int y2, int x, ref Tile[,] tiles)
        {
            for (int y = Math.Min(y1, y2); y < Math.Max(y1, y2) + 1; y++)
            {
                tiles[x, y].blocked = false;
            }
        }

        private static Tile[,] BlankTiles(bool Blocked)
        {
            Tile[,] map = new Tile[Constants.MapWidth, Constants.MapHeight];

            for (int x = 0; x < Constants.MapWidth; x++)
            {
                for (int y = 0; y < Constants.MapHeight; y++)
                {
                    map[x, y] = new Tile(Blocked);
                }
            }

            return map;
        }

        public static GameMap MakeMaze()
        {
            Tile[,] tiles = BlankTiles(true);

            for (int x = 0; x < Constants.MapWidth - 1; x++)
            {
                for (int y = 0; y < Constants.MapHeight - 1; y++)
                {
                    if (x % 2 != 0 && y % 2 != 0)
                    {
                        tiles[x, y] = new Tile(false);
                    }
                }
            }

            CarveMaze(1, 1, ref tiles);

            Rogue.GameWorld.Player.x = 1;
            Rogue.GameWorld.Player.y = 1;

            return new GameMap(tiles);
        }

        public static void CarveMaze(int startx, int starty, ref Tile[,] tiles)
        {
            tiles[startx, starty].visited = true;

            foreach (Coordinate tile in GetMazeNeighbours(startx, starty))
            {
                if (tiles[tile.x, tile.y].visited == false)
                {
                    tiles[(tile.x + startx) / 2, (tile.y + starty) / 2].blocked = false;

                    CarveMaze(tile.x, tile.y, ref tiles);
                }
            }
        }

        public static List<Coordinate> GetMazeNeighbours(int x, int y)
        {
            List<Coordinate> neighbours = new List<Coordinate>();

            if (x - 2 >= 0) neighbours.Add(new Coordinate(x - 2, y));
            if (x + 2 <= Constants.MapWidth - 2) neighbours.Add(new Coordinate(x + 2, y));
            if (y - 2 >= 0) neighbours.Add(new Coordinate(x, y - 2));
            if (y + 2 <= Constants.MapHeight - 2) neighbours.Add(new Coordinate(x, y + 2));

            return neighbours.OrderBy(a => rand.Next()).ToList();
        }

        public static bool MapBlocked(int x, int y)
        {
            return Rogue.GameWorld.Map.tiles[x, y].blocked;
        }
    }
}
