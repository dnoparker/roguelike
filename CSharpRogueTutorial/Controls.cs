using BearLib;
using RogueTutorial;
using System.Linq;

namespace CSharpRogueTutorial
{
    class Controls
    {
        static int[] LeftMovement = { Terminal.TK_LEFT, Terminal.TK_KP_4, Terminal.TK_H };
        static int[] RightMovement = { Terminal.TK_RIGHT, Terminal.TK_KP_6, Terminal.TK_L };
        static int[] UpMovement = { Terminal.TK_UP, Terminal.TK_KP_8, Terminal.TK_K };
        static int[] DownMovement = { Terminal.TK_DOWN, Terminal.TK_KP_2, Terminal.TK_J };

        public static int[] EscapeKeys = { Terminal.TK_ESCAPE };

        public static bool HandleKeys()
        {
            int key = Terminal.Read();

            if (LeftMovement.Contains(key))
            {
                Rogue.GameWorld.Player.Move(-1, 0);
            }
            else if (RightMovement.Contains(key))
            {
                Rogue.GameWorld.Player.Move(1, 0);
            }
            else if (UpMovement.Contains(key))
            {
                Rogue.GameWorld.Player.Move(0, -1);
            }
            else if (DownMovement.Contains(key))
            {
                Rogue.GameWorld.Player.Move(0, 1);
            }

            else if (EscapeKeys.Contains(key))
            {
                return true;
            }

            return false;
        }
    }
}
