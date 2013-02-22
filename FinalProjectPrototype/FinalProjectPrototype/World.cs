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

        short mapImageWidth, mapImageHeight;        // width and height of the map layout image
        private Texture2D mapLayoutImage;
        public Tile[,] tileMap;
        public Color[] pixelColor1d;     // 1 dimensional pixel colors
        public Color[,] pixelColor2d;    // 2 dimensional pixel colors

        public bool canMapScroll = true, isMapScrolling = false;

        // Constants
        const float SCROLL_SPEED = 4f;  // should be the same as player's walk speed (player's max velocity)
            // TODO: set scroll speed to player's max velocity once map and player can directly communicate

        public World(ContentManager content) {
            mapLayoutImage = content.Load<Texture2D>("images/map");

            // map image width and height - used to determine size of arrays
            mapImageWidth = (short)mapLayoutImage.Width;
            mapImageHeight = (short)mapLayoutImage.Height;

            tileMap = new Tile[mapImageWidth, mapImageHeight];          
            pixelColor1d = new Color[mapImageWidth*mapImageHeight];
            pixelColor2d = new Color[mapImageWidth, mapImageHeight];



            GetPixels();
            CreateMap();
        }

        public void GetPixels() {
            mapLayoutImage.GetData<Color>(pixelColor1d);

            int x = 0, y = 0;
            for (int i = 0; i < pixelColor1d.Length; i++) {
                pixelColor2d[x, y] = pixelColor1d[i];
                x++;
                if (x == pixelColor2d.GetLength(0)) {
                    x = 0;
                    y++;
                }
            }
        }

        public void CreateMap() {
            for (int i = 0; i < tileMap.GetLength(0); i++) {
                for (int j = 0; j < tileMap.GetLength(1); j++) {
                    tileMap[i, j] = new Tile(pixelColor2d[i, j], i, j);
                }
            }
            Console.WriteLine("Loading Map...");
        }

        public void ScrollMap(byte direction) {
            switch (direction) {
                case Util.UP:
                    if (tileMap[0, 0].destination.Y < 0) {
                        foreach (Tile t in tileMap) {
                            t.destination.Y += SCROLL_SPEED;
                        }
                    }
                    break;
                case Util.DOWN:
                    if (tileMap[0, tileMap.GetLength(1) - 1].destination.Y > Game1.GAMEWINDOW_HEIGHT) {
                        foreach (Tile t in tileMap) {
                            t.destination.Y -= SCROLL_SPEED;
                        }
                    }
                    break;
                case Util.LEFT:
                    if (tileMap[0, 0].destination.X < 0) {
                        foreach (Tile t in tileMap) {
                            t.destination.X += SCROLL_SPEED;
                        }
                    }
                    break;
                case Util.RIGHT:
                    if (tileMap[tileMap.GetLength(0) - 1, 0].destination.X > Game1.GAMEWINDOW_WIDTH) {
                        foreach (Tile t in tileMap) {
                            t.destination.X -= SCROLL_SPEED;
                        }
                    }
                    break;
            }
        }

        public void Update(GameTime gameTime) {
            
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (Tile t in tileMap) {
                spriteBatch.Draw(t.texture, t.destination, Color.White);
            }
        }
    }
}
