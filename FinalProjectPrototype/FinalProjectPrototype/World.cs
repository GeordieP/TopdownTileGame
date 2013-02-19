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
    public class World {

        int width, height;
        public Tile[,] tileMap;

        public World(int width, int height) {
            this.width = width;
            this.height = height;

            tileMap = new Tile[(width / 64)+1, (height / 64)+1];
            CreateMap();
        }

        public void CreateMap() {
            for (int i = 0; i < tileMap.GetLength(0); i++) {
                for (int j = 0; j < tileMap.GetLength(1); j++) {
                    tileMap[i, j] = new Tile(Util.TILE_GRASS, i, j);
                }
            }
        }

        public void Update(GameTime gameTime) {
            
        }

        public void Draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch) {

            foreach (Tile t in tileMap) {
                spriteBatch.Draw(t.texture, t.destination, Color.White);
            }
        }
    }
}
