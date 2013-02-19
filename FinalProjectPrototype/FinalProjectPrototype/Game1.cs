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
    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Objects
        World map;
        public static SpriteSheet mapTileSheet;


        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 704;

        }

        protected override void Initialize() {

            mapTileSheet = new SpriteSheet(Content, GraphicsDevice);
            map = new World(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void UnloadContent() {
        }


        KeyboardState kbState;

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            kbState = Keyboard.GetState();


            // Y
            if (kbState.IsKeyDown(Keys.S)) {

                if (map.tileMap[0, 0].destination.Y < 0) {
                    foreach (Tile t in map.tileMap) {
                        t.destination.Y += 4;
                    }
                }
            }

            if (kbState.IsKeyDown(Keys.W)) {
                if (map.tileMap[0, map.tileMap.GetLength(1)-1].destination.Y + 64 > GraphicsDevice.Viewport.Height) {
                    foreach (Tile t in map.tileMap) {
                        t.destination.Y -= 4;
                    }
                }
            }


            // X
            if (kbState.IsKeyDown(Keys.D)) {
                foreach (Tile t in map.tileMap) {
                    t.destination.X += 4;
                }
            }


            if (kbState.IsKeyDown(Keys.A)) {
                foreach (Tile t in map.tileMap) {
                    t.destination.X -= 4;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            map.Draw(GraphicsDevice, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
