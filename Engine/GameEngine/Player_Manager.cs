using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    class Player_Manager
    {
	Texture2D player_texture;
	Texture2D threat_texture;
	
	Character[] player;

	public static readonly float max_vel = 10.0f;
        public static readonly float steer_force = 0.1f;
        public static readonly float approach_radius = 100.0f;

	bool collision  = false;
	
	public bool debug_info { get; set; }
	public static int texture_width { get; set; }
	public static int texture_heigth { get; set; }

	public Player_Manager(int amount){
	    
	    player = new Character[amount];
	    for (int i = 0; i < amount; i++) {
		player[i] = new Character();
	    }
	    debug_info = true;
	}

	public void init_content(ContentManager content){
	    player_texture = content.Load<Texture2D>("Engine/circle1");
	    threat_texture = content.Load<Texture2D>("Engine/dot");

	    texture_width = player_texture.Width;
	    texture_heigth = player_texture.Height;
	}

	public void update(){
	    int width = GraphicsDeviceManager.DefaultBackBufferWidth;
	    int heigth = GraphicsDeviceManager.DefaultBackBufferWidth;
	    
	    for (int i = 0; i < player.Length; i++) {
		player[i].check_screen_collision();
		player[i].mouse_attach();
		
		for (int j = 0; j < player.Length; j++) {
		    if(i != j)
			player[i].check_character_collision(player[j]);
		}
	    }

	    
	}

	public void render(SpriteBatch sb){
	    for (int i = 0; i < player.Length; i++) {
		player[i].render_default(sb, player_texture);
		
		if(debug_info)
		    player[i].render_mouse_thread(sb, threat_texture);
	    }
	}



	
    }
}
