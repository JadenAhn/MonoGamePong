using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Pong.Sprites
{
    public class StartMessage
    {
        private Texture2D _spriteBatch;
        public StartMessage(Texture2D spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }

        public void Draw(SpriteBatch spriteBatch, float opacity)
        {
            spriteBatch.Draw(_spriteBatch, new Vector2(280, 280), Color.White * opacity);
        }
    }
}
