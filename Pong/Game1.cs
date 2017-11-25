using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Pong.Sprites;
using System;
using System.Collections.Generic;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static int screenWidth;
        public static int screenHeight;
        public static Random random;
        public static bool isGameOver = false;

        private Score _score;
        private WinMessage _winMessage;
        private Title _title;
        private List<Sprite> _sprites;
        private SoundEffect _SFBounce;
        private SoundEffect _SFScore;
        private Song _songGameTitle;
        private Song _songGameStart;
        private Song _songGameOver;
        public static GamePhase _gamePhase;

        public enum GamePhase
        {
            gameTitle,
            gameStart,
            gameOver
        }
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeight = graphics.PreferredBackBufferHeight;
            random = new Random();
            _gamePhase = GamePhase.gameTitle;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var batTexture = Content.Load<Texture2D>("Bat");
            var ballTexture = Content.Load<Texture2D>("Ball");

            _score = new Score(Content.Load<SpriteFont>("Font"));
            _winMessage = new WinMessage(Content.Load<SpriteFont>("Win"));
            _SFBounce = Content.Load<SoundEffect>("SFBounce");
            _SFScore = Content.Load<SoundEffect>("SFScore");
            _songGameTitle = Content.Load<Song>("SongGameTitle");
            _songGameStart = Content.Load<Song>("SongGameStart");
            _songGameOver = Content.Load<Song>("SongGameOver");
            _title = new Title(Content.Load<Texture2D>("Title"));
            _sprites = new List<Sprite>()
            {
                new Sprite(Content.Load<Texture2D>("Background")),
                new Bat(batTexture)
                {
                    position = new Vector2(20, (screenHeight / 2) - (batTexture.Height / 2)),
                    input = new Models.Input()
                    {
                        up = Keys.W,
                        down = Keys.S
                    }
                },
                new Bat(batTexture)
                {
                    position = new Vector2(screenWidth - 20 - batTexture.Width, (screenHeight / 2) - (batTexture.Height / 2)),
                    input = new Models.Input()
                    {
                        up = Keys.Up,
                        down = Keys.Down
                    }
                },
                new Ball(ballTexture)
                {
                    position = new Vector2((screenWidth / 2) - (ballTexture.Width / 2), (screenHeight / 2) - (ballTexture.Height / 2)),
                    score = _score,
                    winMessage = _winMessage,
                    SFBounce = _SFBounce,
                    SFScore = _SFScore
                }
            };
            PlayMusic();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (_gamePhase == GamePhase.gameTitle)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    _gamePhase = GamePhase.gameStart;
                    PlayMusic();
                }
            }
            else if (_gamePhase == GamePhase.gameStart)
            {
                foreach (var sprite in _sprites)
                {
                    sprite.Update(gameTime, _sprites);
                }

                if (_score.score1 == 1 || _score.score2 == 1)
                {
                    _gamePhase = GamePhase.gameOver;
                    PlayMusic();
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            if (_gamePhase == GamePhase.gameTitle)
            {
                _title.Draw(spriteBatch);
            }
            else if (_gamePhase == GamePhase.gameStart)
            {
                foreach (var sprite in _sprites)
                {
                    sprite.Draw(spriteBatch);
                }
                _score.Draw(spriteBatch);
            }
            else
            {
                _winMessage.Draw(spriteBatch);
            }
            
            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected void PlayMusic()
        {
            switch (_gamePhase)
            {
                case GamePhase.gameTitle:
                    MediaPlayer.Play(_songGameTitle);
                    MediaPlayer.IsRepeating = true;
                    break;
                case GamePhase.gameStart:
                    MediaPlayer.Stop();
                    MediaPlayer.Play(_songGameStart);
                    MediaPlayer.IsRepeating = true;
                    break;
                case GamePhase.gameOver:
                    MediaPlayer.Stop();
                    MediaPlayer.Play(_songGameOver);
                    break;
                default:
                    break;
            }
        }
    }
}
