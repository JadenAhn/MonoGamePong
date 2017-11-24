using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pong.Sprites
{
    public class Ball : Sprite
    {
        //To increment the speed over time
        private float _timer = 0f;
        
        //put ? to allow more types, like null
        private Vector2? _startPosition = null;
        private float? _startspeed;
        private bool _isPlaying;

        public Score score;
        public int speedIncrementSpan = 10;

        public Ball(Texture2D texture) : base(texture)
        {
            speed = 3f;
        }
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (_startPosition == null)
            {
                _startPosition = position;
                _startspeed = speed;

                Restart();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                _isPlaying = true;
            }

            if (!_isPlaying)
            {
                return;
            }

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > speedIncrementSpan)
            {
                speed++;
                _timer = 0;
            }

            foreach (var sprite in sprites)
            {
                if (sprite == this)
                {
                    continue;
                }

                if (this.velocity.X > 0 && this.IsTouchingLeft(sprite))
                {
                    this.velocity.X = -this.velocity.X;
                }
                if (this.velocity.X < 0 && this.IsTouchingRight(sprite))
                {
                    this.velocity.X = -this.velocity.X;
                }
                if (this.velocity.Y > 0 && this.IsTouchingTop(sprite))
                {
                    this.velocity.Y = -this.velocity.Y;
                }
                if (this.velocity.Y < 0 && this.IsTouchingBottom(sprite))
                {
                    this.velocity.Y = -this.velocity.Y;
                }
            }

            if (position.Y <= 0 || position.Y + _texture.Height >= Game1.screenHeight)
            {
                velocity.Y = -velocity.Y;
            }

            if (position.X <= 0)
            {
                score.score2++;
                Restart();
            }

            if (position.X + _texture.Width >= Game1.screenWidth)
            {
                score.score1++;
                Restart();
            }

            position += velocity * speed;
        }

        public void Restart()
        {
            var direction = Game1.random.Next(0, 4);
            switch (direction)
            {
                case 0:
                    velocity = new Vector2(1, 1);
                    break;
                case 1:
                    velocity = new Vector2(1, -1);
                    break;
                case 2:
                    velocity = new Vector2(-1, -1);
                    break;
                case 3:
                    velocity = new Vector2(-1, 1);
                    break;
            }

            position = (Vector2)_startPosition;
            speed = (float)_startspeed;
            _timer = 0;
            _isPlaying = false;
        }
    }
}
