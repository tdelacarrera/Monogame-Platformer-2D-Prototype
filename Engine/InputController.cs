using Microsoft.Xna.Framework.Input;

namespace Monogame
{
    public static class InputController
    {
        private static KeyboardState _lastKeyboard;
        private static KeyboardState _currentKeyboard;
        public static bool Clicked { get; private set; }
        public static bool RightClicked { get; private set; }

        public static bool KeyPressed(Keys key)
        {
            return _currentKeyboard.IsKeyDown(key) && _lastKeyboard.IsKeyUp(key);
        }

        public static bool KeyDown(Keys key)
        {
            return _currentKeyboard.IsKeyDown(key);
        }

        public static void Update()
        {
            _lastKeyboard = _currentKeyboard;
            _currentKeyboard = Keyboard.GetState();
        }

    }
}
