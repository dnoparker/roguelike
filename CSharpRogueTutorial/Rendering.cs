using BearLib;
using Main;

namespace rougeLike
{
    class Rendering
    {
        private static void DrawMap()
        {
            for (int x = 0; x < Constants.mapWidth; x++)
            {
                for (int y = 0; y < Constants.mapHeight; y++)
                {
                    if (Rogue.GameWorld.Map.tiles[x, y].blocked)
                    {
                        Terminal.PutExt(x, y, 0, 0, '#');
                        // If floor above/below

                        if (y != 0)
                        {
                            if (!Rogue.GameWorld.Map.tiles[x, y - 1].blocked)
                            {
                                Terminal.PutExt(x, y, 0, 0, '─');
                                goto done;
                            }
                        }

                        if (y != Constants.mapHeight - 1)
                        {
                            if (!Rogue.GameWorld.Map.tiles[x, y + 1].blocked)
                            {
                                Terminal.PutExt(x, y, 0, 0, '─');
                                goto done;
                            }
                        }

                        if (x != 0)
                        {
                            if (!Rogue.GameWorld.Map.tiles[x - 1, y].blocked)
                            {
                                Terminal.PutExt(x, y, 0, 0, '│');
                                goto done;
                            }
                        }

                        if (x != Constants.mapWidth - 1)
                        {
                            if (!Rogue.GameWorld.Map.tiles[x + 1, y].blocked)
                            {
                                Terminal.PutExt(x, y, 0, 0, '│');
                                goto done;
                            }
                        }
                    done:
                        // Check for blank tiles up and down 
                        if (y != 0 && y != Constants.mapHeight - 1)
                        {
                            if(!Rogue.GameWorld.Map.tiles[x, y + 1].blocked && !Rogue.GameWorld.Map.tiles[x, y - 1].blocked)
                            {
                                Terminal.PutExt(x, y, 0, 0, ' ');
                                Rogue.GameWorld.Map.tiles[x, y].blocked = false;
                            }
                        }

                        // same for x
                        if (x != 0 && x != Constants.mapWidth - 1)
                        {
                            if (!Rogue.GameWorld.Map.tiles[x + 1, y].blocked && !Rogue.GameWorld.Map.tiles[x - 1, y].blocked)
                            {
                                Terminal.PutExt(x, y, 0, 0, ' ');
                                Rogue.GameWorld.Map.tiles[x, y].blocked = false;
                            }
                        }


                    }
                    else
                    {
                        Terminal.PutExt(x, y, 0, 0, ' ');
                    }

                    if(Rogue.GameWorld.Map.tiles[x, y].corridor)
                    {
                        Terminal.PutExt(x, y, 0, 0, ' ');
                    }
                }
            }
        }

        private static void DrawObjects()
        {
            foreach (GameObject obj in Rogue.GameWorld.Objects)
            {
                obj.Draw();
            }
        }

        public static void RenderAll()
        {
            Terminal.Clear();

            DrawMap();

            DrawObjects();

            Terminal.Refresh();
        }
    }
}
