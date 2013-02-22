using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FinalProjectPrototype {
    public static class Util {

        // Tile Types
        public const byte TILE_GRASS = 0, TILE_DIRT = 1, TILE_STONE = 2, TILE_WATER = 3;

        // Keys
        public const byte KEY_IDLE = 0, KEY_UP = 1, KEY_RIGHT = 2, KEY_DOWN = 3, KEY_LEFT = 4;

        // Directions
        public const byte NONE = 0, UP = 1, RIGHT = 2, DOWN = 3, LEFT = 4;

    }
}
