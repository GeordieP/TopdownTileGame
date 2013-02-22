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

        Texture2D texture;
        public Vector2 position = new Vector2(Game1.GAMEWINDOW_WIDTH / 2, Game1.GAMEWINDOW_HEIGHT / 2);
        private Vector2 Velocity = Vector2.Zero;
        private Vector2 InitialVelocity = Vector2.Zero;
        private Vector2 Acceleration = Vector2.Zero;
        private Vector2 Force = Vector2.Zero;

        public bool isMapScrolling = false; // if the map is scrolling, don't allow movement (player will get ahead)

        public short playerWidth, playerHeight;


        // Constants
        private const float mass = 1f;
        private const float PUSH = 0.5f;

        private const float MAX_FORCE = 2f;
        private const float MAX_VELOCITY = 4f;

        public Player() { }
        public Player(ContentManager content){
            texture = content.Load<Texture2D>("images/player");
            playerWidth = (short)texture.Width;
            playerHeight = (short)texture.Height;
        }

        public void KeyPressed(Keys[] pressedKeys) {
            if (pressedKeys.Length == 0 || isMapScrolling) {
                Velocity = Vector2.Zero;
                Force = Vector2.Zero;
            } else {
                if (pressedKeys.Contains(Keys.W)) {
                    if (Force.Y > (MAX_FORCE * -1)) {
                        Force.Y -= PUSH;
                    }
                } else if (pressedKeys.Contains(Keys.S)) {
                    if (Force.Y < MAX_FORCE) {
                        Force.Y += PUSH;
                    }
                } else if (pressedKeys.Contains(Keys.A)) {
                    if (Force.X > (MAX_FORCE * -1)) {
                        Force.X -= PUSH;
                    }
                } else if (pressedKeys.Contains(Keys.D)) {
                    if (Force.X < MAX_FORCE) {
                        Force.X += PUSH;
                    }
                }
            }
        }

        public void KeyPressed(byte key) {
            if (key != 0 && !isMapScrolling) {
                switch (key) {
                    case 1:
                        if (Force.Y > (MAX_FORCE * -1)) {
                            Force.Y -= PUSH;
                        }
                        break;
                    case 3:
                        if (Force.Y < MAX_FORCE) {
                            Force.Y += PUSH;
                        }
                        break;
                    case 4: // left
                        if (Force.X > (MAX_FORCE * -1)) {
                            Force.X -= PUSH;
                        }
                        break;
                    case 2: // right
                        if (Force.X < MAX_FORCE) {
                            Force.X += PUSH;
                        }
                        break;
                }
            } else {
                // No keys pressed - stop the movement dead for now
                Force = Vector2.Zero;
                Velocity = Vector2.Zero;
            }
        }

        //public void KeyPressed(byte key) {
        //    if (key != Util.KEY_IDLE) {
        //        switch (key) {
        //            case Util.KEY_UP:
        //                if (Force.Y > (MAX_FORCE.Y * -1)) {
        //                    Force.Y -= PUSH;
        //                }
        //                break;
        //            case Util.KEY_DOWN:
        //                if (Force.Y < MAX_FORCE.Y) {
        //                    Force.Y += PUSH;
        //                }
        //                break;
        //            case Util.KEY_LEFT:
        //                if (Force.X > (MAX_FORCE.X * -1)) {
        //                    Force.X -= PUSH;
        //                }
        //                break;
        //            case Util.KEY_RIGHT:
        //                if (Force.X < MAX_FORCE.X) {
        //                    Force.X += PUSH;
        //                }
        //                break;
        //        }
        //    } else {
        //        // No keys pressed - stop the movement dead for now
        //        Force = Vector2.Zero;
        //        Velocity = Vector2.Zero;
        //    }
        //}

        public void Update(GameTime gameTime) {
            Acceleration = Force / mass;
            if (Velocity.X < MAX_VELOCITY && Velocity.X > -MAX_VELOCITY) Velocity.X += Acceleration.X;
            if (Velocity.Y < MAX_VELOCITY && Velocity.Y > -MAX_VELOCITY) Velocity.Y += Acceleration.Y;

            position += Velocity;

        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
