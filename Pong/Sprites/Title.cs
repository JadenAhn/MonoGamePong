using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Sprites
{
    public class Title
    {
        private Texture2D _spriteBatch;
        public Title(Texture2D spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_spriteBatch, new Vector2(280, 200), Color.Yellow);
        }
    }
}
