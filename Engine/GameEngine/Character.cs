using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    public class Character
    {
        Texture2D texture1;
        Texture2D texture2;
        private Vector2 pos;
        private Vector2 vel;
        private Vector2 acc;
        float angle = 0;

        public Vector2 Position     { get { return pos; } }
        public Vector2 Velocity     { get { return vel; } }
        public Vector2 Acceleration { get { return acc; } }

        const float max_vel = 10.0f;
        const float steer_force = 0.1f;
        const float approach_radius = 100.0f;

        public Character()
        {
            pos = new Vector2(0.0f, 0.0f);
            vel = new Vector2(0, 0);
            acc = new Vector2(0, 0);
        }

        public void set_new_position(float x, float y)
        {
            pos.X = x;
            pos.Y = y;
        }

        public void load_default_texture(Texture2D text)
        {
            texture1 = text;
        }
        public void load_collide_texture(Texture2D text)
        {
            texture2 = text;
        }
        public void check_screen_collision(int width, int height)
        {
            //TODO: This is ugly looking stuff I TELL U WHAT
            if (pos.X <= 0 || (pos.X + texture1.Width  >= width))
                vel.X *= -1;
            if (pos.Y <= 0 || (pos.Y + texture1.Height >= height))
                vel.Y *= -1;
        }
        public bool check_character_collision(Character player)
        {
            if ((pos.X + texture1.Width) < player.Position.X) return false;
            else if (pos.X > (player.Position.X + texture1.Width)) return false;
            else if ((pos.Y + texture1.Height) < player.Position.Y) return false;
            else if (pos.Y > (player.Position.Y + texture1.Height)) return false;

            Vector2 jump_vec;
            jump_vec.X = pos.X - player.Position.X;
            jump_vec.Y = pos.Y - player.Position.Y;

            if (jump_vec.Length() > max_vel)
            {
                jump_vec.Normalize();
                Vector2.Multiply(ref jump_vec, max_vel, out jump_vec);
            }

            acc = jump_vec;
            Vector2.Add(ref vel, ref acc, out vel);
            Vector2.Add(ref pos, ref vel, out pos);

            return true;
        }

        public void mouse_attach()
        {
            MouseState state = Mouse.GetState();
            Vector2 des = new Vector2(0, 0);
            des.X = state.X - (pos.X + (texture1.Width/2));
            des.Y = state.Y - (pos.Y + (texture1.Height/2));
            angle = (float)Math.Atan2(des.Y, des.X);

            float distance = des.Length();
            des.Normalize();
            float ratio = 0;
            if(distance < approach_radius)
            {
                ratio = distance / approach_radius;
                if (ratio > 0.01)
                {
                    des.X *= distance / approach_radius * max_vel;
                    des.Y *= distance / approach_radius * max_vel;
                }
                else
                {
                    des.X = 0;
                    des.Y = 0;
                }
            }
            else
            {
                des.X *= max_vel;
                des.Y *= max_vel;
            }

            acc.X = des.X - vel.X;
            acc.Y = des.Y - vel.Y;
            if(acc.Length() > steer_force)
            {
                acc.Normalize();
                Vector2.Multiply(ref acc, steer_force, out acc);
            }

            Vector2.Add(ref vel, ref acc, out vel);
            Vector2.Add(ref pos, ref vel, out pos);
        }

        public void render_default(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture1, pos);
        }
        public void render_collision(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2, pos);
        }

        public void render_mouse_thread(SpriteBatch spriteBatch, Texture2D text)
        {
            MouseState state = Mouse.GetState();

            Vector2 source = new Vector2();
            source.X = (pos.X + texture1.Width / 2);
            source.Y = (pos.Y + texture1.Height / 2);

            Vector2 target = new Vector2();
            target.X = state.X - source.X;
            target.Y = state.Y - source.Y;

            render_line(spriteBatch, text, source, target);
        }

        private void render_line(SpriteBatch spriteBatch, Texture2D text, 
                                          Vector2 source, Vector2 target)
        {
            var length = target.Length();
            var line_angle = (float)Math.Atan2(target.Y, target.X);
            var origin = new Vector2(0.0f, 0.0f);
            var scale = new Vector2(length, 2f);

            spriteBatch.Draw(text, source, null, Color.Red, line_angle, origin, scale,
                SpriteEffects.None, 0);
        }
    }
}
