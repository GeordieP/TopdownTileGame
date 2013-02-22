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

        // Game1 Specific Variables

        KeyboardState kbState;
        // 'center box' area for checking player position and scrolling map 
        Rectangle centerBox = new Rectangle((int)(GAMEWINDOW_WIDTH * 0.1), (int)(GAMEWINDOW_HEIGHT * 0.1), (int)(GAMEWINDOW_WIDTH - (GAMEWINDOW_WIDTH * 0.2)), (int)(GAMEWINDOW_HEIGHT - (GAMEWINDOW_HEIGHT * 0.2)));


        // Game Objects
        World map;
        Player player;
        public static SpriteSheet mapTileSheet;

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
            player = new Player(Content);
            mapTileSheet = new SpriteSheet(Content, GraphicsDevice);
            map = new World(Content);

            base.Initialize();
        }

        protected override void LoadContent() { spriteBatch = new SpriteBatch(GraphicsDevice); }

        protected override void UnloadContent() {}

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) this.Exit();

            kbState = Keyboard.GetState(PlayerIndex.One);
            #region;




            /* RAW MAP SCROLLING (DIRECT KEY INPUT)
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
                if (map.tileMap[0, 0].destination.X < 0) {
                    foreach (Tile t in map.tileMap) {
                        t.destination.X += 4;
                    }
                }
            }


            if (kbState.IsKeyDown(Keys.A)) {
                if (map.tileMap[map.tileMap.GetLength(0)-1, 0].destination.X + 64 > GraphicsDevice.Viewport.Width){
                    foreach (Tile t in map.tileMap) {
                        t.destination.X -= 4;
                    }
                }
            }
            */
            #endregion;
            #region;
            /* DIRECT KEY HANDLING

            if (kbState.GetPressedKeys().Length != 0) {
                if (kbState.IsKeyDown(Keys.W)) {
                    player.KeyPressed(Util.KEY_UP);
                } else if (kbState.IsKeyDown(Keys.S)) {
                    player.KeyPressed(Util.KEY_DOWN);
                }

                if (kbState.IsKeyDown(Keys.A)) {
                    player.KeyPressed(Util.KEY_LEFT);
                } else if (kbState.IsKeyDown(Keys.D)) {
                    player.KeyPressed(Util.KEY_RIGHT);
                }

            } else {
                player.KeyPressed(Util.KEY_IDLE);
            }
            */
            #endregion;

            // temporary key handling for player movement
            player.KeyPressed(kbState.GetPressedKeys());


            // player at edge of allowed area - scroll map
            // TODO: make map scrolling take into account movement direction
            // TODO: Allow direct communication between player and map
            if (kbState.GetPressedKeys().Length != 0) {

                // TODO: player goes out of bounds on right side because we're checking X rather than right (perhaps offset the centerBox?)
                if (!(centerBox.Contains(new Point((int)player.position.X, (int)player.position.Y))) && map.canMapScroll) {
                    if (player.position.X >= centerBox.Right) {    // right side
                        map.ScrollMap(Util.RIGHT);
                    } else if (player.position.X <= centerBox.Left) {   // left
                        map.ScrollMap(Util.LEFT);
                    }

                    if (player.position.Y + player.playerHeight >= centerBox.Bottom) {    // top
                        map.ScrollMap(Util.DOWN);
                    } else if (player.position.Y <= centerBox.Top) {    // bottom
                        map.ScrollMap(Util.UP);
                    }

                    player.isMapScrolling = true;
                }
            } else {
                // bringing the player back so they arent stuck in the "move map true" area

                if (player.position.X >= centerBox.Right) {    // right side
                    player.position.X -= 1;
                } else if (player.position.X <= centerBox.Left) {   // left
                    player.position.X += 1;
                }

                if (player.position.Y >= centerBox.Bottom) {    // top
                    player.position.Y -= 1;
                } else if (player.position.Y <= centerBox.Top) {    // bottom
                    player.position.Y += 1;
                }

                player.isMapScrolling = false;
            }

            player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            map.Draw(spriteBatch);
            player.Draw(spriteBatch);

            // draw debug rectangle for observing centerBox and map scrolling behaviour
            //spriteBatch.Draw(mapTileSheet.mapTiles, centerBox, new Rectangle((mapTileSheet.mapTiles.Width), (mapTileSheet.mapTiles.Height), 64, 64), Color.White);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
