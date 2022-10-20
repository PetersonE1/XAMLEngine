using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace XAMLEngine
{
    public static class Input
    {
        public static List<Key> keysPressed;
        public static List<Key> keysPressedThisFrame;
        public static List<Key> keysReleasedThisFrame;

        public static bool MouseHeld = false;
        public static bool MouseClicked = false;
        public static bool MouseReleased = false;

        public static bool IsPressed(Key key)
        {
            if (keysPressed.Contains(key))
                return true;
            return false;
        }

        public static bool WasPressedThisFrame(Key key)
        {
            if (keysPressedThisFrame.Contains(key))
                return true;
            return false;
        }

        public static bool WasReleasedThisFrame(Key key)
        {
            if (keysReleasedThisFrame.Contains(key))
                return true;
            return false;
        }
    }
}
