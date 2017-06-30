using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    class Entity
    {
	Vector2 pos;
	Vector2 vel;

	public Entity(){
	    pos = new Vector2(0, 0);
	    vel = new Vector2(0f, 1f);
	}
	public Entity(float x, float y){
	    pos = new Vector2(x, y);
	    vel = new Vector2(0f, 1f);   
	}

	public void update(){
	    check_edges();
            Vector2.Add(ref pos, ref vel, out pos);
	}

	private void check_edges(){
	    int width = Engine.window_width;
	    int heigth = Engine.window_heigth;

	    Random rand = new Random();
	    if(pos.Y >= heigth){
		pos.Y = -Entity_Manager.texture_heigth;
		pos.X = rand.Next(width);
	    }
	}

	public void render(SpriteBatch sb, Texture2D texture){
	    sb.Draw(texture, pos);
	}
    }
}
