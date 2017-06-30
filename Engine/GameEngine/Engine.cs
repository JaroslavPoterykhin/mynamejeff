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
        bool collision = false;

        bool debug_info = true;
        Texture2D dot_texture;

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

        public void LoadContent(Texture2D text1, Texture2D text2, Texture2D text3)
        {

            player1.load_default_texture(text1);
            player1.load_collide_texture(text2);

            player2.load_default_texture(text1);
            player2.load_collide_texture(text2);

            Random rand = new Random();
            player1.set_new_position(rand.Next(width), rand.Next(height));
            player2.set_new_position(rand.Next(width), rand.Next(height));

            dot_texture = text3;
        }

        public void Update()
        {
            player1.check_screen_collision(width, height);
            player1.mouse_attach();
            player2.check_screen_collision(width, height);
            player2.mouse_attach();


            if (player1.check_character_collision(player2))
                if (collision == false)
                    collision = true;
                else
                    collision = false;
            player2.check_character_collision(player1);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!collision)
            {
                player1.render_default(spriteBatch);
                player2.render_default(spriteBatch);
            }
            else
            {
                player1.render_collision(spriteBatch);
                player2.render_collision(spriteBatch);
            }

            if (debug_info)
            {
                player1.render_mouse_thread(spriteBatch, dot_texture);
                player2.render_mouse_thread(spriteBatch, dot_texture);
            }
                
        }
    }
}
