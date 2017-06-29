using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    public class Character
    {
        Texture2D texture;
        private Vector2 pos;
        private Vector2 vel;
        private Vector2 acc;

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

        public void LoadContent(Texture2D text)
        {
            texture = text;
        }
        public void check_screen_collision(int width, int height)
        {
            if (pos.X <= 0 || (pos.X + texture.Width  >= width))
                vel.X *= -1;
            if (pos.Y <= 0 || (pos.Y + texture.Height >= height))
                vel.Y *= -1;
        }
        public void check_character_collision(Character player)
        {
            if ((pos.X + texture.Width) < player.Position.X) return;
            else if (pos.X > (player.Position.X + texture.Width)) return;
            else if ((pos.Y + texture.Height) < player.Position.Y) return;
            else if (pos.Y > (player.Position.Y + texture.Height)) return;

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
        }

        public void mouse_attach()
        {
            MouseState state = Mouse.GetState();
            Vector2 des = new Vector2(0, 0);
            des.X = state.X - (pos.X + (texture.Width/2));
            des.Y = state.Y - (pos.Y + (texture.Height/2));

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

        public void render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pos);
        }
    }
}
