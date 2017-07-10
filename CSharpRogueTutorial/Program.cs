using CSharpRogueTutorial;
using System.Collections.Generic;
using BearLib;
using System;
using System.Numerics;

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

            GameWorld.Player = new GameObject('@', "yellow", new Vector2(0,0));
            GameWorld.Enemy = new humanoid('X', "red", new Vector2(0, 0));
            GameWorld.Objects.Add(GameWorld.Player);
            GameWorld.Objects.Add(GameWorld.Enemy);

            GameWorld.Map = MapMethods.MakeMap();
        }

        private static void MainLoop()
        {
            while (true)
            {
                Rendering.UpdateAndRender();

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
