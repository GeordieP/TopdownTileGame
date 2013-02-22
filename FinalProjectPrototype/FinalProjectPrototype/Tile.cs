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

        public byte type;
        public Texture2D texture;
        public Vector2 destination;

        public Color GRASS_GREEN = new Color(57, 124, 48, 255);
        public Color DIRT_BROWN = new Color(150, 64, 0, 255);
        private Color STONE_GRAY = new Color(105, 105, 105, 255);
        private Color WATER_BLUE = new Color(0, 0, 255, 255);

        public Tile(Color pixelColor, int i, int j) {
            if (pixelColor == GRASS_GREEN) {
                type = Util.TILE_GRASS;
            } else if (pixelColor == DIRT_BROWN) {
                type = Util.TILE_DIRT;
            } else if (pixelColor == STONE_GRAY) {
                type = Util.TILE_STONE;
            } else if (pixelColor == WATER_BLUE) {
                type = Util.TILE_WATER;
            }

            MakeTile(i, j);
        }

        public void MakeTile(int i, int j) {
            switch (type) {
                case Util.TILE_GRASS:
                    texture = Game1.mapTileSheet.GetTile(0 * 64, 0 * 64, 64, 64);
                    break;
                case Util.TILE_DIRT:
                    texture = Game1.mapTileSheet.GetTile(1 * 64, 0 * 64, 64, 64);
                    break;
                case Util.TILE_STONE:
                    texture = Game1.mapTileSheet.GetTile(2 * 64, 0 * 64, 64, 64);
                    break;
                case Util.TILE_WATER:
                    texture = Game1.mapTileSheet.GetTile(3 * 64, 0 * 64, 64, 64);
                    break;

            }

            destination = new Vector2(i * 64, j * 64);

        }

    }
}
