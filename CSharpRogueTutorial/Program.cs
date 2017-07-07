using rougeLike;
using System.Collections.Generic;
using BearLib;
using System;

namespace Main
{
    class Rogue
    {
        public static World GameWorld;

        private static void Initialize()
        {
            Terminal.Open();
            Terminal.Set("window: size=" + Constants.screenWidth.ToString() + "x" + Constants.screenHeight.ToString() + "; font: VeraMono.ttf, size=10");
        }

        private static void NewGame()
        {
            GameWorld = new World();

            GameWorld.Objects = new List<GameObject>();

            GameWorld.Player = new GameObject('@', "yellow", 0, 0);
            GameWorld.Objects.Add(GameWorld.Player);

            GameWorld.Map = MapMethods.MakeMap();
        }

        private static void MainLoop()
        {
            while (true)
            {
                Rendering.RenderAll();

                bool exit = Controls.HandleKeys();

                if (exit)
                {
                    break;
                }
            }

            Terminal.Close();
        }

        static void Main(string[] args)
        {
            Initialize();
            NewGame();
            MainLoop();
        }
    }
}
