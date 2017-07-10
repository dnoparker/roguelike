using System;
using System.Collections.Generic;

namespace RogueLike
{
    [Serializable]
    class World
    {
        public GameMap Map;
        public GameObject Player;
        public Enemy Enemy;
        public List<GameObject> Objects;
        public BattleGrid battleGrid;

        public World()
        {

        }
    }
}
