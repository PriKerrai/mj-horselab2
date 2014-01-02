﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace MJ_HorseLab2.Models
{
    /// <summary>
    /// Helper class for drawing a tank model with animated wheels and turret.
    /// </summary>
    public class Tank
    {
        #region Fields

        Model tankModel;

        ModelBone leftBackWheelBone;
        ModelBone rightBackWheelBone;
        ModelBone leftFrontWheelBone;
        ModelBone rightFrontWheelBone;
        ModelBone leftSteerBone;
        ModelBone rightSteerBone;
        ModelBone turretBone;
        ModelBone cannonBone;
        ModelBone hatchBone;


        Matrix leftBackWheelTransform;
        Matrix rightBackWheelTransform;
        Matrix leftFrontWheelTransform;
        Matrix rightFrontWheelTransform;
        Matrix leftSteerTransform;
        Matrix rightSteerTransform;
        Matrix turretTransform;
        Matrix cannonTransform;
        Matrix hatchTransform;


        Matrix[] boneTransforms;

        float wheelRotationValue;
        float steerRotationValue;
        float turretRotationValue;
        float cannonRotationValue;
        float hatchRotationValue;

        public Vector3 Position;
        public Quaternion Rotation;
        public float Scale;
        public float MoveSpeed;

        KeyboardState keys;
        float leftRightRot;

        #endregion

        #region Properties

        public float WheelRotation
        {
            get { return wheelRotationValue; }
            set { wheelRotationValue = value; }
        }

        public float SteerRotation
        {
            get { return steerRotationValue; }
            set { steerRotationValue = value; }
        }

        public float TurretRotation
        {
            get { return turretRotationValue; }
            set { turretRotationValue = value; }
        }

        /// <summary>
        /// Gets or sets the cannon rotation amount.
        /// </summary>
        public float CannonRotation
        {
            get { return cannonRotationValue; }
            set { cannonRotationValue = value; }
        }

        /// <summary>
        /// Gets or sets the entry hatch rotation amount.
        /// </summary>
        public float HatchRotation
        {
            get { return hatchRotationValue; }
            set { hatchRotationValue = value; }
        }


        #endregion

        public Tank()
        {
            Position = new Vector3(15, 7, 20);

            Scale = 0.01f;
            MoveSpeed = 0.01f;
            Rotation = Quaternion.Identity;
        }

        /// <summary>
        /// Loads the tank model.
        /// </summary>
        public void Load(ContentManager content)
        {
            // Load the tank model from the ContentManager.
            tankModel = content.Load<Model>("Models\\Tank\\tank");

            // Look up shortcut references to the bones we are going to animate.
            leftBackWheelBone = tankModel.Bones["l_back_wheel_geo"];
            rightBackWheelBone = tankModel.Bones["r_back_wheel_geo"];
            leftFrontWheelBone = tankModel.Bones["l_front_wheel_geo"];
            rightFrontWheelBone = tankModel.Bones["r_front_wheel_geo"];
            leftSteerBone = tankModel.Bones["l_steer_geo"];
            rightSteerBone = tankModel.Bones["r_steer_geo"];
            turretBone = tankModel.Bones["turret_geo"];
            cannonBone = tankModel.Bones["canon_geo"];
            hatchBone = tankModel.Bones["hatch_geo"];


            // Store the original transform matrix for each animating bone.
            leftBackWheelTransform = leftBackWheelBone.Transform;
            rightBackWheelTransform = rightBackWheelBone.Transform;
            leftFrontWheelTransform = leftFrontWheelBone.Transform;
            rightFrontWheelTransform = rightFrontWheelBone.Transform;
            leftSteerTransform = leftSteerBone.Transform;
            rightSteerTransform = rightSteerBone.Transform;
            turretTransform = turretBone.Transform;
            cannonTransform = cannonBone.Transform;
            hatchTransform = hatchBone.Transform;


            // Allocate the transform matrix array.
            boneTransforms = new Matrix[tankModel.Bones.Count];
        }

        public void Rotatewheels(float rotationChange)
        {
            wheelRotationValue += rotationChange;
        }

        public void Move(Vector3 addedVector)
        {
            Position += MoveSpeed * addedVector;
        }

        public void ProcessInput(GameTime gameTime)
        {

            keys = Keyboard.GetState();

            float turningSpeed = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;
            turningSpeed *= 1.6f * 0.5f;
            leftRightRot = 0;

            if (keys.IsKeyDown(Keys.Up))
            {
                Move(-Matrix.CreateFromQuaternion(Rotation).Forward);
                Rotatewheels(0.01f);
            }
            if (keys.IsKeyDown(Keys.Down))
            {
                Move(Matrix.CreateFromQuaternion(Rotation).Forward);
                Rotatewheels(-0.01f);
            }
            if (keys.IsKeyDown(Keys.Left))
            {
                leftRightRot += turningSpeed;
                SteerRotation = 90;
            }
            if (keys.IsKeyDown(Keys.Right))
            {
                leftRightRot -= turningSpeed;
                SteerRotation = -90;
            }
            if (keys.IsKeyUp(Keys.Left) && keys.IsKeyUp(Keys.Right))
            {
                SteerRotation = 0;
            }
            if (keys.IsKeyDown(Keys.NumPad0))
            {
                TurretRotation += turningSpeed;
            }
            if (keys.IsKeyDown(Keys.NumPad1))
            {
                TurretRotation -= turningSpeed;
            }
            if (keys.IsKeyDown(Keys.NumPad2))
            {
                CannonRotation += turningSpeed;
            }
            if (keys.IsKeyDown(Keys.NumPad3))
            {
                CannonRotation -= turningSpeed;
            }

            if (keys.IsKeyDown(Keys.NumPad4))
            {
                HatchRotation += turningSpeed;
            }
            if (keys.IsKeyDown(Keys.NumPad5))
            {
                HatchRotation -= turningSpeed;
            }


            Quaternion changeInRotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), leftRightRot)
                                       * Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), 0)
                                       * Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), 0);

            Rotation *= changeInRotation;
        }

        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            // Set the world matrix as the root transform of the model.
            tankModel.Root.Transform = world;

            // Calculate matrices based on the current animation position.
            Matrix wheelRotation = Matrix.CreateRotationX(wheelRotationValue);
            Matrix steerRotation = Matrix.CreateRotationY(steerRotationValue);
            Matrix turretRotation = Matrix.CreateRotationY(turretRotationValue);
            Matrix cannonRotation = Matrix.CreateRotationX(cannonRotationValue);
            Matrix hatchRotation = Matrix.CreateRotationX(hatchRotationValue);

            // Apply matrices to the relevant bones.
            leftBackWheelBone.Transform = wheelRotation * leftBackWheelTransform;
            rightBackWheelBone.Transform = wheelRotation * rightBackWheelTransform;
            leftFrontWheelBone.Transform = wheelRotation * leftFrontWheelTransform;
            rightFrontWheelBone.Transform = wheelRotation * rightFrontWheelTransform;
            leftSteerBone.Transform = steerRotation * leftSteerTransform;
            rightSteerBone.Transform = steerRotation * rightSteerTransform;
            turretBone.Transform = turretRotation * turretTransform;
            cannonBone.Transform = cannonRotation * cannonTransform;
            hatchBone.Transform = hatchRotation * hatchTransform;


            // Look up combined bone matrices for the entire model.
            tankModel.CopyAbsoluteBoneTransformsTo(boneTransforms);

            // Draw the model.
            foreach (ModelMesh mesh in tankModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = boneTransforms[mesh.ParentBone.Index] * Matrix.CreateScale(Scale) * Matrix.CreateFromQuaternion(Rotation) * Matrix.CreateTranslation(Position);
                    effect.View = view;
                    effect.Projection = projection;
                    effect.EnableDefaultLighting();
                }
                mesh.Draw();
            }
        }

    }    
    
}