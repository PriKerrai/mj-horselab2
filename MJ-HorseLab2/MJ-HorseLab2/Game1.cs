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

namespace MJ_HorseLab2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Model test;
        Camera camera;
        FlyingCamera fCamera;
        //Voxel voxel;
        NewVoxel voxel;
        Texture2D stoneTexture;
        Texture2D dirtTexture;
        Texture2D grassTexture;
        Texture2D map;
        BasicEffect effect;
        ReadHue hue;
        Chunk chunk, chunk1, chunk2, chunk3, chunk4, chunk5, chunk6, chunk7;


        float moveScale = 12f;
        float rotateScale = MathHelper.PiOver2;
        private Texture2D texture2;

        float framecount = 0;
        float timeSinceLastUpdate = 0;
        float updateInterval = 1;
        float fps = 0;

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
            effect = new BasicEffect(GraphicsDevice);

            Controller controller = new Controller(this);


            hue = new ReadHue(map);
            //voxel = new NewVoxel(this.GraphicsDevice, texture);

            chunk = new Chunk(this.GraphicsDevice, stoneTexture, dirtTexture, grassTexture, map, 0);
            chunk1 = new Chunk(this.GraphicsDevice, stoneTexture, dirtTexture, grassTexture, map, 1);
            chunk2 = new Chunk(this.GraphicsDevice, stoneTexture, dirtTexture, grassTexture, map, 2);
            chunk3 = new Chunk(this.GraphicsDevice, stoneTexture, dirtTexture, grassTexture, map, 3);
            chunk4 = new Chunk(this.GraphicsDevice, stoneTexture, dirtTexture, grassTexture, map, 4);
            chunk5 = new Chunk(this.GraphicsDevice, stoneTexture, dirtTexture, grassTexture, map, 5);
            chunk6 = new Chunk(this.GraphicsDevice, stoneTexture, dirtTexture, grassTexture, map, 6);
            chunk7 = new Chunk(this.GraphicsDevice, stoneTexture, dirtTexture, grassTexture, map, 7);
            
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
                chunk.Draw(camera, effect);
                chunk1.Draw(camera, effect);
                chunk2.Draw(camera, effect);
                chunk3.Draw(camera, effect);
                chunk4.Draw(camera, effect);
                chunk5.Draw(camera, effect);
                chunk6.Draw(camera, effect);
                chunk7.Draw(camera, effect);
            }

            base.Draw(gameTime);
        }
        //FPS counter
        private void countFPS(GameTime time)
        {
            float elapsed = (float)time.ElapsedGameTime.TotalSeconds;
            framecount++;
            timeSinceLastUpdate+= elapsed;
            if (timeSinceLastUpdate> updateInterval)
            {
                fps = framecount/ timeSinceLastUpdate;
                Window.Title= "FPS: " + fps.ToString();
                framecount= 0;
                timeSinceLastUpdate-= updateInterval;
                }
        }
    
        //private void DrawModel(Model m)
        //{

        //    Matrix[] transforms = new Matrix[m.Bones.Count];
        //    m.CopyAbsoluteBoneTransformsTo(transforms);
        //    Matrix projection = camera.projection;

        //    foreach (ModelMesh mesh in m.Meshes)
        //    {
        //        foreach (BasicEffect effect in mesh.Effects)
        //        {
        //            effect.EnableDefaultLighting();
        //            effect.View = camera.View;
        //            effect.Projection = projection;
        //            effect.World = transforms[mesh.ParentBone.Index]; //Position //gameWorldRotation * 
        //        }
        //        mesh.Draw();
        //    }
        //}

    }
}
