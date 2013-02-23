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

        // Debugging variables
        private bool showDebugRectangles = false;

        // Game1 Specific Variables
        KeyboardState newKBState, oldKBState;

        // Game Objects
        public World map;
        public Player player;
        public static SpriteSheet mapTileSheet;

        // 'center box' area for checking player position and scrolling map
        private static float centerBoxPercent = 0.4f; // ( * 100) percent of the screen on each side of the center box
        public static Rectangle centerBox = new Rectangle((int)(GAMEWINDOW_WIDTH * centerBoxPercent), (int)(GAMEWINDOW_HEIGHT * centerBoxPercent), (int)(GAMEWINDOW_WIDTH - (GAMEWINDOW_WIDTH * (centerBoxPercent * 2))), (int)(GAMEWINDOW_HEIGHT - (GAMEWINDOW_HEIGHT * (centerBoxPercent * 2))));

        // Constants
            // Use constants for game width and height so they can be easily accessed in other classes
        public const short GAMEWINDOW_WIDTH = 1270;
        public const short GAMEWINDOW_HEIGHT = 704;


        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = GAMEWINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = GAMEWINDOW_HEIGHT;

        }

        protected override void Initialize() {
            mapTileSheet = new SpriteSheet(Content, GraphicsDevice);
            map = new World(Content, player);
            player = new Player(Content, map);

            base.Initialize();
        }

        protected override void LoadContent() { spriteBatch = new SpriteBatch(GraphicsDevice); }
        protected override void UnloadContent() {}

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) this.Exit();

            newKBState = Keyboard.GetState(PlayerIndex.One);

            // MAP SCROLLING (manual)
            #region;

            if (newKBState.IsKeyDown(Keys.Down)) {
                map.ScrollMap(Util.DOWN);
            } else if (newKBState.IsKeyDown(Keys.Up)) {
                map.ScrollMap(Util.UP);
            }

            if (newKBState.IsKeyDown(Keys.Right)) {
                map.ScrollMap(Util.RIGHT);
            } else if (newKBState.IsKeyDown(Keys.Left)) {
                map.ScrollMap(Util.LEFT);
            }

            #endregion;

            // Player controls
            #region;

            //if (kbState.GetPressedKeys().Length != 0) {
            //    if (kbState.IsKeyDown(Keys.W)) {
            //        player.KeyPressed(Util.KEY_UP);
            //    }
                
            //    if (kbState.IsKeyDown(Keys.S)) {
            //        player.KeyPressed(Util.KEY_DOWN);
            //    }

            //    if (kbState.IsKeyDown(Keys.A)) {
            //        player.KeyPressed(Util.KEY_LEFT);
            //    }
                
            //    if (kbState.IsKeyDown(Keys.D)) {
            //        player.KeyPressed(Util.KEY_RIGHT);
            //    }

            //} else {
            //    player.KeyPressed(Util.KEY_IDLE);
            //}
            #endregion;

            if (newKBState.IsKeyDown(Keys.Space) && !oldKBState.IsKeyDown(Keys.Space)) {
                showDebugRectangles = !showDebugRectangles;
            }

            player.KeyPressed(newKBState.GetPressedKeys());
            player.Update(gameTime);


            oldKBState = newKBState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            map.Draw(spriteBatch);
            player.Draw(spriteBatch);

            // draw debug rectangle for observing centerBox and map scrolling behaviour
            if (showDebugRectangles) {
                spriteBatch.Draw(mapTileSheet.mapTiles, centerBox, new Rectangle((mapTileSheet.mapTiles.Width), (mapTileSheet.mapTiles.Height), 64, 64), Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
