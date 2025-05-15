using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Media;

namespace GME1003GoblinDanceParty
{
    public class Game1 : Game
    {
        //Lets get gitty with it
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //Declare some variables
        private int _numCandy;          //how many candies?
        private List<int> _candyX;      //list of candy x-coordinates
        private List<int> _candyY;      //list of candy y-coordinates
        private List<float> _candyRotation; //To make each candy rotate differently
        private List<float> _candyRotationAmount; //Make them spin at different speeds
        private List<Color> _candyColor; //Lets have fun with colour!!
        private List<float> _candyTransparency; //Each candy will have it's own transparency
        private List<float> _candyScale; //candy size

        private Texture2D _candySprite;  //the sprite image for our candy
        private Texture2D _backgroundSprite; //the sprite image for our baackground

        private Random _rng;            //for all our random number needs


        //***This is for the goblin. Ignore it.
        Goblin goblin;
        Song music;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _rng = new Random();        //finish setting up our Randon 
            _numCandy = _rng.Next(100,301);//now generates random number between 100 and 300
            _candyX = new List<int>();  //candy X coordinate
            _candyY = new List<int>();  //candy Y coordinate
            _candyRotation = new List<float>(); //candy rotation
            _candyRotationAmount = new List<float>(); //candy rotation amount
            _candyColor = new List<Color>(); //colour of our candy
            _candyTransparency = new List<float>(); //candy transparency
            _candyScale = new List<float>(); //this will affect the size of the candy

            //use a separate for loop for each list - for practice
            //List of X coordinates
            for (int i = 0; i < _numCandy; i++) 
            { 
                _candyX.Add(_rng.Next(0, 801)); //all candy x-coordinates are between 0 and 801
            }

            //List of Y coordinates
            for (int i = 0; i < _numCandy; i++)
            {
                _candyY.Add(_rng.Next(0, 481)); //all candy y-coordinates are between 0 and 480
            }

            //ToDo: List of Colors
            for (int i = 0; i < _numCandy; i++)
            {
                _candyColor.Add(new Color(128 + _rng.Next(0, 129), 128 + _rng.Next(0, 129), 128 + _rng.Next(0, 129)));
            }

            //ToDo: List of scale values
            for (int i = 0; i < _numCandy; i++)
            {
                _candyScale.Add(_rng.Next(50, 100) / 200f);
            }
            //ToDo: List of transparency values
            for (int i = 0; i < _numCandy; i++)
            {
                _candyTransparency.Add(_rng.Next(25, 101) / 100f);
            }
            //List of rotation values
            for (int i = 0; i < _numCandy; i++)
            {
                _candyRotation.Add(_rng.Next(0, 101) / 100f); //each should be random
            }
            //List of rotation speeds
            for (int i = 0; i < _numCandy; i++)
            {
                _candyRotationAmount.Add(_rng.Next(-100, 101) / 5000f);//each should spin randomly
            }


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //load our candy sprite
            _candySprite = Content.Load<Texture2D>("Candy");

            //load our background sprite
            _backgroundSprite = Content.Load<Texture2D>("Les Voix");


            //***This is for the goblin. Ignore it for now.
            goblin = new Goblin(Content.Load<Texture2D>("goblinIdleSpriteSheet"), 400, 400);
            music = Content.Load<Song>("chiptune");
            
            //if you're tired of the music player, comment this out!
            MediaPlayer.Play(music);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            for (int i = 0; i < _numCandy; i++)
            {
                _candyRotation[i] += _candyRotationAmount[i];
            }
   
            //***This is for the goblin. Ignore it for now.
            goblin.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            
            _spriteBatch.Begin();
            _spriteBatch.Draw(_backgroundSprite, new Vector2(0, 0), Color.White);


            //it would be great to have a background image here! 
            //you could make that happen with a single Draw statement.

            //this is where we draw the candy...
            for (int i = 0; i < _numCandy; i++) 
            {
                _spriteBatch.Draw(_candySprite, 
                    new Vector2(_candyX[i], _candyY[i]),    //set the candy position
                    null,                                   //ignore this
                    _candyColor[i] * _candyTransparency[i],         //set colour and transparency
                    _candyRotation[i],                          //set rotation
                    new Vector2(_candySprite.Width / 2, _candySprite.Height / 2), //ignore this
                    new Vector2(_candyScale[i]),    //set scale (same number 2x)
                    SpriteEffects.None,                     //ignore this
                    0f);                                    //ignore this
            }
            _spriteBatch.End();



            //***This is for the goblin. Ignore it for now.
            goblin.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
