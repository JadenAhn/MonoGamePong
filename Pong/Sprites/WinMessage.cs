using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    public class WinMessage
    {
        private SpriteFont _font;
        public int player;
        public WinMessage(SpriteFont font)
        {
            _font = font;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, "PLAYER " + player.ToString() + " WIN!", new Vector2(240, 200), Color.Yellow);
        }
    }
}
