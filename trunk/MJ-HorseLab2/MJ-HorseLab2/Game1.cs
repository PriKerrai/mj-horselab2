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
        //Voxel voxel;
        NewVoxel voxel;
        Texture2D stoneTexture;
        Texture2D dirtTexture;
        Texture2D grassTexture;
        Texture2D map;
        BasicEffect effect;
        ReadHue hue;
        Chunk chunk;


        float moveScale = 12f;
        float rotateScale = MathHelper.PiOver2;
        private Texture2D texture2;

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
            effect= new BasicEffect(GraphicsDevice);
            camera = new Camera(new Vector3(0, 10, -10), 0, MathHelper.PiOver4, 0.05f, 100f);
            effect.View = camera.View;
            effect.Projection = camera.projection;
            //effect.View = Matrix.Identity;

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
            grassTexture = Content.Load<Texture2D>("dot");

            map = Content.Load<Texture2D>("berg");
            hue = new ReadHue(map);
            //voxel = new NewVoxel(this.GraphicsDevice, texture);
            chunk = new Chunk(this.GraphicsDevice, stoneTexture, dirtTexture, grassTexture, map);

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

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState keyState = Keyboard.GetState();
            float moveAmount = 0;

            if (keyState.IsKeyDown(Keys.Right))
            {
                camera.Rotation = MathHelper.WrapAngle(camera.Rotation - (rotateScale * elapsed));
            }
            if (keyState.IsKeyDown(Keys.Left))
            {
                camera.Rotation = MathHelper.WrapAngle(camera.Rotation + (rotateScale * elapsed));
            }
            if (keyState.IsKeyDown(Keys.Up))
            {
                moveAmount = moveScale * elapsed;
            }
            if (keyState.IsKeyDown(Keys.Down))
            {
                moveAmount = -moveScale * elapsed;
            }

            if (moveAmount != 0)
            {
                Vector3 newLocation = camera.PreviewMove(moveAmount);
                bool moveOk = true;

                if (moveOk)
                    camera.MoveForward(moveAmount);
            }
            // TODO: Add your update logic here

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
            }

            base.Draw(gameTime);
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
