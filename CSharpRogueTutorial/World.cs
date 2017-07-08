using System;
using System.Collections.Generic;

namespace CSharpRogueTutorial
{
    [Serializable]
    class World
    {
        public GameMap Map;
        public GameObject Player;
        public humanoid Enemy;
        public List<GameObject> Objects;

        public World()
        {

        }
    }
}
