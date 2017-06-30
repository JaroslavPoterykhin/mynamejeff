using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    public class Engine
    {
	public static int window_width;
	public static int window_heigth;

	ContentManager media;
	Player_Manager pl_manager;

        public Engine(int pl_amount, int width, int height)
        {
	    pl_manager = new Player_Manager(pl_amount);
	    window_width = width;
	    window_heigth = height;
        }

        public void LoadContent(ContentManager content)
        {
	    media = content;
	    
	    pl_manager.init_content(content);
        }

        public void Update()
        {
	    pl_manager.update();
        }

        public void Draw(SpriteBatch sb)
        {
	    pl_manager.render(sb);
        }
    }
}
