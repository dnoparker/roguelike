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
        private static bool battleMode = false;

        private static void Start()
        {
            GameWorld = new World();

            Terminal.Open();
            Terminal.Set("window: size=" + Constants.screenWidth.ToString() + "x" + Constants.screenHeight.ToString() + "; font: VeraMono.ttf, size=10");
            Terminal.BkColor(Color.FromArgb(255,40,40,60));
            Terminal.Color(Color.FromArgb(255, 240, 234, 214));
            setUpGame();
            Terminal.Layer(0); // Set map to layer 0
            MapGen.updateMap();
            Terminal.Layer(2);
            GameWorld.battleGrid = new BattleGrid();
            Terminal.Layer(1); // Gamobjects to layer 1

        }

        private static void setUpGame()
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
                renderQueue();
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
            Start();
            Update();
        }

        private static void updateObjects()
        {
            foreach (GameObject obj in Rogue.GameWorld.Objects)
            {
                obj.Update();
            }
        }

        public static void renderQueue()
        {
            if (battleMode)
            {
                Terminal.Layer(2);
                Terminal.ClearArea(0, 0, Constants.screenWidth, Constants.screenHeight);
                Terminal.Refresh();
                return;
            }
            Terminal.ClearArea(0,0,Constants.screenWidth, Constants.screenHeight);
            updateObjects();
            Terminal.Refresh();
        }


    }
}
