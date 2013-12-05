using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MJ_HorseLab2
{
    class Camera
    {

        #region Fields
        private Vector3 position = Vector3.Zero;
        private float rotation;
        private Vector3 lookAt;
        private Vector3 baseCameraReference = new Vector3(0, 0, 1);
        private bool needViewResync = true;

        private Matrix cachedViewMatrix;
        #endregion

        #region Properties
        public Matrix projection { get; private set; }

        public Vector3 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                UpdateLookAt();
            }
        }

        public float Rotation
        {
            get { return rotation; }
            set
            {
                rotation = value;
                UpdateLookAt();
            }
        }

        public Matrix View
        {
            get
            {

                if (needViewResync)
                    cachedViewMatrix = Matrix.CreateLookAt(position, lookAt, Vector3.Up);

                return cachedViewMatrix;
            }
        }
        #endregion

        #region Constructor

        public Camera(Vector3 position, float rotation, float aspectRatio, float frontClip, float backClip)
        {
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, frontClip, backClip);
            MoveTo(position, rotation);
        }
        #endregion


        #region Helper Methods

        private void UpdateLookAt()
        {
            Matrix rotationMatrix = Matrix.CreateRotationY(rotation);
            Vector3 lookAtOffset = Vector3.Transform(baseCameraReference, rotationMatrix);
            lookAt = position + lookAtOffset;
            needViewResync = true;
        }

        public void MoveTo(Vector3 position, float rotation)
        {
            this.position = position;
            this.rotation = rotation;
            UpdateLookAt();
        }

        public Vector3 PreviewMove(float scale)
        {
            Matrix rotate = Matrix.CreateRotationY(rotation);
            Vector3 forward = new Vector3(0, 0, scale);
            forward = Vector3.Transform(forward, rotate);
            return (position + forward);
        }

        public void MoveForward(float scale)
        {
            MoveTo(PreviewMove(scale), rotation);
        }

        #endregion



    }
}
