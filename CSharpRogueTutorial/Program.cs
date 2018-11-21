using System.Collections.Generic;
using BearLib;
using System;
using System.Numerics;
using System.Drawing;

namespace RogueLike
{
    class Rogue
    {
        public static World GameWorld;

        static void Main(string[] args)
        {
            Start();
            Update();
        }

        private static void Start()
        {
            GameWorld = new World();

            Terminal.Open();
            Terminal.Set("window: size=" + Constants.screenWidth.ToString() + "x" + Constants.screenHeight.ToString() + "; font: VeraMono.ttf, size=10");
            Terminal.BkColor(Color.FromArgb(255,40,40,60));
            Terminal.Color(Color.FromArgb(255, 240, 234, 214));
            SetUpGame();
            Terminal.Layer(0); // Set map to layer 0
            MapGen.DrawMap();
            Terminal.Layer(1); // Gamobjects to layer 1
        }

        private static void SetUpGame()
        {
            GameWorld.Objects = new List<GameObject>();
            GameWorld.Player = new GameObject('@', "yellow", new Vector2(0,0));
            GameWorld.Enemy = new Enemy('X', "red", new Vector2(0, 0));
            GameWorld.Objects.Add(GameWorld.Player);
            GameWorld.Objects.Add(GameWorld.Enemy);
            GameWorld.Map = MapGen.MakeMap();
        }

        private static void Update()
        {
            while (true)
            {
                RenderQueue();
                UpdateGameObjects();
                Controls.HandleKeys();
            }
        }

        public static void RenderQueue()
        {
            Terminal.ClearArea(0, 0, Constants.screenWidth, Constants.screenHeight);
            DrawGameObjects();
            Terminal.Refresh();
        }

        private static void DrawGameObjects()
        {
            foreach (GameObject obj in GameWorld.Objects)
            {
                obj.Draw();
            }
        }

        private static void UpdateGameObjects()
        {
            foreach (GameObject obj in GameWorld.Objects)
            {
                obj.Update();
            }
        }
    }
}
