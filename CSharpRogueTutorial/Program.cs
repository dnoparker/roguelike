using System.Collections.Generic;
using BearLib;
using System;
using System.Numerics;
using System.Drawing;
using Tools;

namespace RogueLike
{
    class Rogue
    {
        public static World GameWorld;
        public static EventManager EventManager;

        static void Main(string[] args)
        {
            Start();
            Update();
        }

        private static void Start()
        {
            GameWorld = new World();
            EventManager = new EventManager();
            EventManager.AddListener<OnPlayerSpotted>(Spotted);
            SetUpTerminal();
            SetUpGame();
            DrawMap();
            StartGameObjects();
        }

        private static void Spotted(OnPlayerSpotted value)
        {
            Console.WriteLine(value.enemyName);
        }

        private static void SetUpTerminal()
        {
            Terminal.Open();
            Terminal.Set("window: size=" + Constants.screenWidth.ToString() + "x" + Constants.screenHeight.ToString() + "; font: VeraMono.ttf, size=10");
            Terminal.BkColor(Color.FromArgb(255, 40, 40, 60));
            Terminal.Color(Color.FromArgb(255, 240, 234, 214));
        }

        private static void SetUpGame()
        {
            GameWorld.Objects = new List<GameObject>();
            GameWorld.Player = new GameObject('@', "yellow", new Vector2Int(0, 0));
            GameWorld.Enemy = new Enemy('X', "red", new Vector2Int(0, 0));
            GameWorld.Objects.Add(GameWorld.Player);
            GameWorld.Objects.Add(GameWorld.Enemy);
            GameWorld.Map = MapGen.MakeMap();
        }

        private static void Update()
        {
            while (true)
            {
                Controls.HandleKeys();
                UpdateGameObjects();
                DrawGameObjects();
            }
        }

        public static void DrawGameObjects()
        {
            Terminal.Layer((int)Constants.TerminalLayers.gameobjects);
            Terminal.ClearArea(0, 0, Constants.screenWidth, Constants.screenHeight);
            foreach (GameObject obj in GameWorld.Objects)
            {
                obj.Draw();
            }
            Terminal.Refresh();
        }

        private static void DrawMap()
        {
            Terminal.Layer((int)Constants.TerminalLayers.map); // Set map to layer 0
            Terminal.ClearArea(0, 0, Constants.screenWidth, Constants.screenHeight);
            MapGen.DrawMap();
        }

        private static void StartGameObjects()
        {
            foreach (GameObject obj in GameWorld.Objects)
            {
                obj.Start();
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
