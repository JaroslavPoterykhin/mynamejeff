using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    public class Engine
    {
        Circle[] circle;
        int circle_amount;

        private const float max_vel = 10.0f;
        private const float steer_force = 0.5f;
        private const float approach_radius = 100.0f;

        public Engine(int amount)
        {
            circle_amount = amount;
            circle = new Circle[circle_amount];
            for (int i = 0; i < circle_amount; i++)
            {
                circle[i] = new Circle();
            }

        }

        public void LoadContent(Texture2D text)
        {
            for (int i = 0; i < circle_amount; ++i)
            {
                circle[i].LoadContent(text);
            }
        }

        public void Update()
        {

            for (int i = 0; i < circle_amount; ++i)
            {
                circle[i].Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < circle_amount; ++i)
            {
                circle[i].Draw(spriteBatch);
            }
        }
    }
}
