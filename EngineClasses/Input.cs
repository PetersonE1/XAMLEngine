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

        public static bool MouseHeldLeft = false;
        public static bool MouseClickedLeft = false;
        public static bool MouseReleasedLeft = false;
        public static bool MouseHeldRight = false;
        public static bool MouseClickedRight = false;
        public static bool MouseReleasedRight = false;

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
