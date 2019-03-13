using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace hw1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D swamp, shrek;
        Vector2 shrekPosition;
        float shrekSpeed;
        private Song backgroundMusic;
        private SpriteFont font;
        protected readonly Rectangle gameBoundaries;
        Rectangle swampRectangle = new Rectangle(50,50,200,100);
        bool colission=false;

        public int Width
        {
            get
            {
                return shrek.Width;
            }
        }
        public int Height
        {
            get
            {
                return shrek.Height;
            }
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)shrekPosition.X, (int)shrekPosition.Y, Width, Height);
            }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }
        protected override void Initialize()
        {

            shrekPosition = new Vector2(graphics.PreferredBackBufferWidth / 2,
            graphics.PreferredBackBufferHeight / 2);
            shrekSpeed = 100f;


            base.Initialize();
        }

        protected override void LoadContent()
        {
            backgroundMusic = Content.Load<Song>("Sounds/swamp");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            swamp = Content.Load<Texture2D>("swamp");
            
            shrek = Content.Load<Texture2D>("shrek");
            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.Volume = 0.1f;
            font = Content.Load<SpriteFont>("font");

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        


        
        protected override void Update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up))
                shrekPosition.Y -= shrekSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Down))
                shrekPosition.Y += shrekSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Left))
                shrekPosition.X -= shrekSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Right))
                shrekPosition.X += shrekSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);

            if (BoundingBox.Intersects(swampRectangle))
            {
                colission = true;
            }
            else colission = false;
        }

      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(shrek, shrekPosition,Color.White);
            spriteBatch.Draw(swamp, new Vector2(50, 50), Color.White);
            if(colission)
            spriteBatch.DrawString(font, "Kolizja", new Vector2(700, 0), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

