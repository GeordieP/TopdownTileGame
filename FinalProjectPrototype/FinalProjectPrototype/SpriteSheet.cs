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
    public class SpriteSheet {

        Texture2D mapTiles;
        GraphicsDevice graphicsDevice;

        public SpriteSheet(ContentManager Content, GraphicsDevice graphicsDevice) {
            this.graphicsDevice = graphicsDevice;
            mapTiles = Content.Load<Texture2D>("spritesheets/mapTiles");
        }

        public Texture2D GetTile(int x, int y, int w, int h) {
            RenderTarget2D newTexture = new RenderTarget2D(graphicsDevice, w, h);
            SpriteBatch spriteBatch = new SpriteBatch(graphicsDevice);

            // Render to the texture
            graphicsDevice.SetRenderTarget(newTexture);

            graphicsDevice.Clear(Color.White);

            // draw image to new texture
            spriteBatch.Begin();
            spriteBatch.Draw(mapTiles, Vector2.Zero, new Rectangle(x, y, w, h), Color.White);
            spriteBatch.End();

            // set render target back to screen
            graphicsDevice.SetRenderTarget(null);

            return newTexture;      // if any problems arise, try casting this as a Texture2D
        }
    }
}
