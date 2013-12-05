using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MissileDrizzle
{
    class Camera
    {
        protected float
            mZoom;
        public Matrix
            mTransform;
        public Vector2
            mPos;
        protected float
            mRotation;
        Vector2
            mOrigin,
            mZoomOffset;

        public Camera()
        {
            mZoom = 1.0f;
            mRotation = 0.0f;
            mPos = Vector2.Zero;
            mOrigin.X = 1280 / 2;
            mOrigin.Y = 720 / 2;
            //mZoomOffset = Vector2.Zero;
            mZoomOffset = new Vector2(0.0f, 300);
            
        }

        public float Zoom
        {
            get { return mZoom; }
            set { mZoom = value; if (mZoom < 1.0f) mZoom = 1.0f;  }
        }

        public Vector2 ZoomOffset
        {
            get { return mZoomOffset; }
            set { mZoomOffset = value; }
        }

        public float Rotation
        {
            get { return mRotation; }
            set { mRotation = value; }
        }

        public void Move(Vector2 pAmount)
        {
            mPos += pAmount;
        }

        public Vector2 Pos
        {
            get { return mPos; }
            set { mPos = value; }
        }

        public Matrix getTransformation(Vector2 parallax)
        {
            
            /*/
            mTransform =
                Matrix.CreateTranslation(new Vector3(-mOrigin.X, -mOrigin.Y, 0)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(mPos.X + mOrigin.X, mPos.Y + mOrigin.Y, 0));
            /*/
            
            mTransform =
               Matrix.CreateTranslation(new Vector3(-mPos * parallax, 0.0f)) *
                // The next line has a catch. See note below.
               Matrix.CreateTranslation(new Vector3(-mOrigin - mZoomOffset, 0.0f)) *
               Matrix.CreateRotationZ(Rotation) *
               Matrix.CreateScale(Zoom, Zoom, 1) *
               Matrix.CreateTranslation(new Vector3(mOrigin + mZoomOffset, 0.0f));
             
            
            
            //*/
            return mTransform;
        }

    }
}
