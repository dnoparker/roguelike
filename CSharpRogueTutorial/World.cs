using System;
using System.Collections.Generic;

namespace rougeLike
{
    [Serializable]
    class World
    {
        public GameMap Map;
        public GameObject Player;
        public List<GameObject> Objects;

        public World()
        {

        }
    }
}
