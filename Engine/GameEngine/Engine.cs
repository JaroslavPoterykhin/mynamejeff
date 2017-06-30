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
	Entity_Manager en_manager;

	int entity_amount = 20;

        public Engine(int pl_amount, int width, int height)
        {
	    window_width = width;
	    window_heigth = height;
	    
	    pl_manager = new Player_Manager(pl_amount);
	    en_manager = new Entity_Manager(entity_amount);
        }

        public void LoadContent(ContentManager content)
        {
	    media = content;
	    pl_manager.init_content(content);
	    en_manager.init_content(content);
        }

        public void Update()
        {
	    pl_manager.update();
	    en_manager.update();
        }

        public void Draw(SpriteBatch sb)
        {
	    pl_manager.render(sb);
	    en_manager.render(sb);
        }
    }
}
