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
    public class Tile{

        public Texture2D texture;
        public Vector2 destination;

        public Tile(byte type, int i, int j) {
            switch (type) {
                case Util.TILE_GRASS:
                    texture = Game1.mapTileSheet.GetTile(0, 0, 64, 64);
                    destination = new Vector2(i * 64, j * 64);
                break;
                case Util.TILE_DIRT:
                texture = Game1.mapTileSheet.GetTile(64, 0, 64, 64);
                destination = new Vector2(i * 64, j * 64);
                break;
            }
        }

    }
}
