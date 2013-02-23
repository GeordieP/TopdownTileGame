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
    public class Player {
        World map;      // allow direct communication between player and map

        Texture2D texture;
        public Vector2 position = new Vector2(Game1.GAMEWINDOW_WIDTH / 2, Game1.GAMEWINDOW_HEIGHT / 2);
        private Vector2 Velocity = Vector2.Zero;

        Rectangle playerRectangle;
        Rectangle centerBox = Game1.centerBox;

        public bool isMapScrolling = false; // if the map is scrolling, don't allow movement (player will get ahead)
        private bool nonePressed = true, upPressed = false, leftPressed = false, downPressed = false, rightPressed = false;
        private List<byte> deniedKeys = new List<byte>();

        public short playerWidth, playerHeight;

        // Constants
        private const float PUSH = 4f;
        private const float MAX_VELOCITY = 4f;

        public Player() { }
        public Player(ContentManager content, World map){
            this.map = map;
            texture = content.Load<Texture2D>("images/player");
            playerWidth = (short)texture.Width;
            playerHeight = (short)texture.Height;

            playerRectangle = new Rectangle((int)position.X, (int)position.Y, playerWidth, playerHeight);
        }

        public void KeyPressed(Keys[] pressedKeys) {
            if (pressedKeys.Length == 0) {
                nonePressed = true;
                upPressed = false;
                leftPressed = false;
                downPressed = false;
                rightPressed = false;
            } else {
                nonePressed = false;

                if (pressedKeys.Contains(Keys.W)) {
                    upPressed = true;
                } else {
                    upPressed = false;
                }

                if (pressedKeys.Contains(Keys.A)) {
                    leftPressed = true;
                } else {
                    leftPressed = false;
                }

                if (pressedKeys.Contains(Keys.S)) {
                    downPressed = true;
                } else {
                    downPressed = false;
                }

                if (pressedKeys.Contains(Keys.D)) {
                    rightPressed = true;
                } else {
                    rightPressed = false;
                }
            }
        }

        public void Update(GameTime gameTime) {
            if (nonePressed) {
                Velocity = Vector2.Zero;
            } else {
                if (upPressed && !deniedKeys.Contains(Util.UP) && position.Y > 0) {
                    Velocity.Y = -PUSH;
                } else if (downPressed && !deniedKeys.Contains(Util.DOWN) && (position.Y + playerHeight) < Game1.GAMEWINDOW_HEIGHT) {
                    Velocity.Y = PUSH;
                } else {
                    Velocity.Y = 0f;
                }

                if (leftPressed && !deniedKeys.Contains(Util.LEFT) && position.X > 0) {
                    Velocity.X = -PUSH;
                } else if (rightPressed && !deniedKeys.Contains(Util.RIGHT) && (position.X + playerWidth) < Game1.GAMEWINDOW_WIDTH) {
                    Velocity.X = PUSH;
                } else {
                    Velocity.X = 0f;
                }
            }

            position += Velocity;

            // scroll map
            #region;
            playerRectangle.X = (int)position.X;
            playerRectangle.Y = (int)position.Y;

            if (!centerBox.Contains(playerRectangle)) {
                if ((position.X + playerWidth) >= centerBox.Right && rightPressed) {
                    map.ScrollMap(Util.RIGHT);

                    if (map.canMapScroll) { // as long as the map can scroll (not at edge of tilemap)
                        deniedKeys.Add(Util.RIGHT);
                    } else {
                        for (int i = 0; i < deniedKeys.Count(); i++) {
                            if (deniedKeys[i] == Util.RIGHT) {
                                deniedKeys.RemoveAt(i);
                                break;
                            }
                        }
                    }
                } else if (position.X <= centerBox.Left && leftPressed){
                    map.ScrollMap(Util.LEFT);
                    if (map.canMapScroll) {
                        deniedKeys.Add(Util.LEFT);
                    } else {
                        for (int i = 0; i < deniedKeys.Count(); i++) {
                            if (deniedKeys[i] == Util.LEFT) {
                                deniedKeys.RemoveAt(i);
                                break;
                            }
                        }
                    }
                }   

                if ((position.Y + playerHeight) >= centerBox.Bottom && downPressed){
                    map.ScrollMap(Util.DOWN);

                    if (map.canMapScroll) {
                        deniedKeys.Add(Util.DOWN);
                    } else {
                        for (int i = 0; i < deniedKeys.Count(); i++) {
                            if (deniedKeys[i] == Util.DOWN) {
                                deniedKeys.RemoveAt(i);
                                break;
                            }
                        }
                    }
                } else if (position.Y <= centerBox.Top && upPressed){
                    map.ScrollMap(Util.UP);

                    if (map.canMapScroll) {
                        deniedKeys.Add(Util.UP);
                    } else {
                        for (int i = 0; i < deniedKeys.Count(); i++) {
                            if (deniedKeys[i] == Util.UP) {
                                deniedKeys.RemoveAt(i);
                                break;
                            }
                        }
                    }
                }

                isMapScrolling = true;
            } else {
                isMapScrolling = false;
                deniedKeys.Clear();     // might be unecessary
            }

            #endregion;
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
