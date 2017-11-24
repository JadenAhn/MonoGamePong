using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong.Sprites
{
    public class Bat : Sprite
    {
        public Bat(Texture2D texture) : base(texture)
        {
            speed = 5f;
        }
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (input == null)
            {
                throw new Exception("Please give a value to 'Input'");
            }

            if (Keyboard.GetState().IsKeyDown(input.up))
            {
                velocity.Y = -speed;
            }
            else if (Keyboard.GetState().IsKeyDown(input.down))
            {
                velocity.Y = speed;
            }

            position += velocity;
            position.Y = MathHelper.Clamp(position.Y, 0, Game1.screenHeight - _texture.Height);
            velocity = Vector2.Zero;
            //base.Update(gameTime, sprites);
        }
    }
}
