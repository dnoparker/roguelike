using System;
using System.Collections.Generic;

namespace CSharpRogueTutorial
{
    [Serializable]
    class World
    {
        public GameMap Map;
        public GameObject Player;
        public GameObject debugPath;
        public List<GameObject> Objects;

        public World()
        {

        }
    }
}
