using CSharpRogueTutorial;
using System.Collections.Generic;
using BearLib;
using System;

namespace RogueTutorial
{
    class Rogue
    {
        public static World GameWorld;

        private static void Initialize()
        {
            Terminal.Open();
            Terminal.Set("window: size=" + Constants.ScreenWidth.ToString() + "x" + Constants.ScreenHeight.ToString() + "; font: VeraMono.ttf, size=12");
        }

        private static void NewGame()
        {
            GameWorld = new World();

            GameWorld.Objects = new List<GameObject>();

            GameWorld.Player = new GameObject('@', "red", 0, 0);
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
