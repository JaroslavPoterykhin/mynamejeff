using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    class Entity_Manager
    {
	Entity[] enties;

	Texture2D entity_texture;
	public static int texture_width { get; set; }
	public static int texture_heigth { get; set; }

	public static readonly float max_vel = 10.0f;
        public static readonly float steer_force = 0.1f;
        public static readonly float approach_radius = 100.0f;

	public Entity_Manager(int amount){
	    enties = new Entity[amount];
	    Random rand = new Random();
	    for (int i = 0; i < amount; i++) {
		enties[i] = new Entity(rand.Next(Engine.window_width),
				       rand.Next(Engine.window_heigth) - Engine.window_heigth);
	    }
	    
	}

	public void init_content(ContentManager content){
	    entity_texture = content.Load<Texture2D>("Engine/leaf");
	    
	    texture_width = entity_texture.Width;
	    texture_heigth = entity_texture.Height;
	}


	public void update(){
	    for (int i = 0; i < enties.Length; i++) {
		enties[i].update();
	    }
	}

	public void render(SpriteBatch sb){
	    for (int i = 0; i < enties.Length; i++) {
		enties[i].render(sb, entity_texture);
	    }
	}
    }
}
