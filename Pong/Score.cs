using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    public class Score
    {
        public int score1;
        public int score2;
        private SpriteFont _font;
        public Score(SpriteFont font)
        {
            _font = font;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, score1.ToString(), new Vector2(320, 70), Color.White);
            spriteBatch.DrawString(_font, score2.ToString(), new Vector2(430, 70), Color.White);
        }
    }
}
