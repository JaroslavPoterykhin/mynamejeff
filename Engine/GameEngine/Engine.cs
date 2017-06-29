using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    public class Engine
    {
        int width;
        int height;

        Character player1;
        Character player2;

        private const float max_vel = 10.0f;
        private const float steer_force = 0.1f;
        private const float approach_radius = 100.0f;

        public Engine(int width, int height)
        {
            this.width = width;
            this.height = height;

            Random rand = new Random();

            player1 = new Character();
            player2 = new Character();
        }

        public void LoadContent(Texture2D text)
        {
            player1.LoadContent(text);
            player2.LoadContent(text);

            Random rand = new Random();
            player1.set_new_position(rand.Next(width), rand.Next(height));
            player2.set_new_position(rand.Next(width), rand.Next(height));
        }

        public void Update()
        {
            player1.check_screen_collision(width, height);
            player1.mouse_attach();
            player1.check_character_collision(player2);

            player2.check_screen_collision(width, height);
            player2.mouse_attach();
            player2.check_character_collision(player1);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            player1.render(spriteBatch);
            player2.render(spriteBatch);
        }
    }
}
