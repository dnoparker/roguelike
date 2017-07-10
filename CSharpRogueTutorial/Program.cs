﻿using System.Collections.Generic;
using BearLib;
using System;
using System.Numerics;

namespace RogueLike
{
    class Rogue
    {
        public static World GameWorld;

        private static void Start()
        {
            Terminal.Open();
            Terminal.Set("window: size=" + Constants.screenWidth.ToString() + "x" + Constants.screenHeight.ToString() + "; font: VeraMono.ttf, size=10");
            Terminal.BkColor(System.Drawing.Color.DarkOliveGreen);
            setUp();
            Terminal.Layer(0);
            MapGen.updateMap();
            Terminal.Layer(1);
        }

        private static void setUp()
        {
            GameWorld = new World();
            GameWorld.Objects = new List<GameObject>();
            GameWorld.Player = new GameObject('@', "yellow", new Vector2(0,0));
            GameWorld.Enemy = new humanoid('X', "red", new Vector2(0, 0));
            GameWorld.Objects.Add(GameWorld.Player);
            GameWorld.Objects.Add(GameWorld.Enemy);
            GameWorld.Map = MapGen.MakeMap();
        }

        private static void Update()
        {
            while (true)
            {
                renderQueue();
                Console.WriteLine();
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
            Terminal.ClearArea(0,0,Constants.screenWidth, Constants.screenHeight);
            updateObjects();
            Terminal.Refresh();
        }


    }
}
