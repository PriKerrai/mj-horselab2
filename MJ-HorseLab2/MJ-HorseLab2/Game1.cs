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
        Voxel voxel;
        Texture2D texture;

        BasicEffect effect;

        float moveScale = 8f;
        float rotateScale = MathHelper.PiOver2;

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
            camera = new Camera(new Vector3(-1, 0, -5), 0, MathHelper.PiOver4, 0.05f, 100f);
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
            test = Content.Load<Model>("test");
            texture = Content.Load<Texture2D>("elda");
            voxel = new Voxel(this.GraphicsDevice, texture);
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
            voxel.Draw(camera, effect);

            base.Draw(gameTime);
        }

        private void DrawModel(Model m)
        {
            Vector3 position = new Vector3(0, 0, 0);
            Matrix[] transforms = new Matrix[m.Bones.Count];
            float aspectRatio = graphics.GraphicsDevice.Viewport.Width / graphics.GraphicsDevice.Viewport.Height;
            m.CopyAbsoluteBoneTransformsTo(transforms);
            Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), 
            aspectRatio, 1.0f, 10000.0f); 
            Matrix view = Matrix.CreateLookAt(new Vector3(0.0f, 50.0f, 1 /*Zoom*/),Vector3.Zero, Vector3.Up);
            foreach (ModelMesh mesh in m.Meshes){
                foreach (BasicEffect effect in mesh.Effects){
                    effect.EnableDefaultLighting();
                    effect.View = view; 
                    effect.Projection = projection;
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(position); //Position //gameWorldRotation * 
                    }
                mesh.Draw();
            }
        }

    }
}
