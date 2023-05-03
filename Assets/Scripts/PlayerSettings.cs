using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public static class PlayerSettings
    {
        public static List<List<KeyCode>> Buttons = new List<List<KeyCode>>
        {
            new List<KeyCode>
            {
                KeyCode.W,
                KeyCode.A,
                KeyCode.D,
            },
            new List<KeyCode>
            {
                KeyCode.UpArrow,
                KeyCode.LeftArrow,
                KeyCode.RightArrow,
            },
            new List<KeyCode>
            {
                KeyCode.Y,
                KeyCode.G,
                KeyCode.J,
            },
        };
        public static List<Color> Colors = new List<Color>
        {
            Color.blue,
            Color.red,
            Color.yellow,
            Color.cyan
        };
    }
}
