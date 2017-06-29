using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    public class Circle
    {
        Texture2D texture;
        SpriteBatch spriteBatch;

        private Vector2 pos;
        private Vector2 vel;
        private Vector2 acc;
        const float max_vel = 10.0f;
        const float steer_force = 0.5f;
        const float approach_radius = 100.0f;

        public Circle()
        {
            pos = new Vector2(0.0f, 0.0f);
            vel = new Vector2(0, 0);
            acc = new Vector2(0, 0);
        }

        public void LoadContent(Texture2D text)
        {
            texture = text;
        }
        public void Check_Boundaries(int width, int height)
        {
            if ((pos.X <= 0))
                pos.X = 0;
            if ((pos.X + texture.Width) >= width)
                pos.X = width - texture.Width;
            if ((pos.Y <= 0))
                pos.Y = 0;
            if ((pos.Y + texture.Height) >= height)
                pos.Y = height - texture.Height;
        }

        public void Move()
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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pos);
        }
    }
}
