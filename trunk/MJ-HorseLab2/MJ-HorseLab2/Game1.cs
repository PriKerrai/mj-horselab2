using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Laboration3Datorgrafik;
using MJ_HorseLab2.Models;

namespace MJ_HorseLab2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera;
        FlyingCamera fCamera;
        Voxel voxel;
        Texture2D stoneTexture;
        Texture2D dirtTexture;
        Texture2D grassTexture;
        Texture2D map;
        BasicEffect effect;
        ReadHue hue;
        Chunk chunk, chunk1, chunk2, chunk3, chunk4, chunk5, chunk6, chunk7;


        float framecount = 0;
        float timeSinceLastUpdate = 0;
        float updateInterval = 1;
        float fps = 0;

        Tank tank;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Datorgrafik Lab 3";

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //test = Content.Load<Model>("test");
            stoneTexture = Content.Load<Texture2D>("stone");
            dirtTexture = Content.Load<Texture2D>("dirt");
            grassTexture = Content.Load<Texture2D>("grass");
            map = Content.Load<Texture2D>("berg");
            //map = Content.Load<Texture2D>("prick");
            effect = new BasicEffect(GraphicsDevice);

            //hue = new ReadHue(map);
            hue = new ReadHue(map, this.GraphicsDevice, stoneTexture, dirtTexture, grassTexture);
            //voxel = new NewVoxel(this.GraphicsDevice, texture);

            

            tank = new Tank();
            tank.Load(Content);

            fCamera = new FlyingCamera();
            this.camera = new Camera(GraphicsDevice, new Vector3(0, 12, 5));
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            countFPS(gameTime);

            tank.ProcessInput(gameTime);
            fCamera.ProcessInput(gameTime);
            camera.Update(fCamera.Position, fCamera.Rotation);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                //voxel.Draw(camera, effect);
                foreach(Chunk chunk in hue.ChunkList){
                    chunk.Draw(camera, effect);
                }
                
                tank.Draw(effect.World, camera.ViewMatrix, camera.ProjectionMatrix);
            }

            base.Draw(gameTime);
        }
        //FPS counter
        private void countFPS(GameTime time)
        {
            float elapsed = (float)time.ElapsedGameTime.TotalSeconds;
            framecount++;
            timeSinceLastUpdate += elapsed;
            if (timeSinceLastUpdate > updateInterval)
            {
                fps = framecount / timeSinceLastUpdate;
                Window.Title = "FPS: " + fps.ToString();
                framecount = 0;
                timeSinceLastUpdate -= updateInterval;
            }
        }

    }
}
