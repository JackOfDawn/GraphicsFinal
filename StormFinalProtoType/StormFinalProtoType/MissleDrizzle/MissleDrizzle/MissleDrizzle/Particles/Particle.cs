using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MissileDrizzle.Particles
{    
    class Particle
    {
        public Sprite
            mSprite {get; private set;}
        public int 
            mAge {get; private set;}
        public bool
            isAlive { get; private set; }


        //Basic physics
        public Vector2
            mPos {get; private set;}
        public Vector2
            mVel;
        public Vector2
            mAcc { get; private set;}
        public float
            mDampening {get; private set;}

        //Rotations
        public float
            mRot { get; private set; }
        public float
            mRotVel { get; private set; }
        public float
            mRotDamp { get; private set; }

        //Scaling
        public float
            mScale { get; private set; }
        public float
            mScaleVel { get; private set; }
        public float
            mScaleAcc { get; private set; }
        public float
            mScaleMax { get; private set; }

        //Color
        public Color
            mColor;
        public Color
            mInitColor;
        public Color
            mFinalColor;
        public int
            mFadeAge;

        public Rectangle
            mSourceRect;



        public Particle()
        {
            mSprite = new Sprite();
        }

        //Create a particle with a texture
        public void createParticle(Texture2D pTexture, int pAgeMS, Vector2 pPos, Vector2 pVel, Vector2 pAcc, float pDamp, float pRot, float pRotVel, float pRotDamp,
           float pScale, float pScaleVel, float pScaleAcc, float pScaleMax, Color pInitColor, Color pFinalColor, int pFadeAge)
        {
            isAlive = true;
            mAge = pAgeMS;
            mVel = pVel;
            mPos = pPos;
            mAcc = pAcc;
            mDampening = pDamp;
            mRot = pRot;
            mRotDamp = pRotDamp;
            mRotVel = pRotVel;
            mScale = pScale;
            mScaleVel = pScaleVel;
            mScaleAcc = pScaleAcc;
            mScaleMax = pScaleMax;
            mColor = pInitColor;
            mInitColor = pInitColor;
            mFinalColor = pFinalColor;
            mFadeAge = pFadeAge;


            mSourceRect = new Rectangle(0, 0, pTexture.Width, pTexture.Height);
            mSprite.createSprite(pTexture, mSourceRect);
        }
        public void update(GameTime pGameTime)
        {
            if (mAge < 0)
            {
                isAlive = false;
                return;
            }
            mAge -= pGameTime.ElapsedGameTime.Milliseconds;

            updatePos(pGameTime);

            updateRot(pGameTime);
            updateScale(pGameTime);
            updateColor(pGameTime);
        }

        private void updatePos(GameTime pGameTime)
        {
            mVel *= mDampening;
            mVel += (mAcc * (float)pGameTime.ElapsedGameTime.TotalSeconds);
            mPos += (mVel * (float)pGameTime.ElapsedGameTime.TotalSeconds);
            mSprite.updatePos(mPos);
        }

        private void updateRot(GameTime pGameTime)
        {
            mRot *= mRotDamp;
            mRot += (mRotVel * (float)pGameTime.ElapsedGameTime.TotalSeconds);

            mSprite.updateRot(mRot);
        }

        private void updateScale(GameTime pGameTime)
        {
            mScaleVel += (mScaleAcc * (float)pGameTime.ElapsedGameTime.TotalSeconds);
            mScale += (mScaleVel * (float)pGameTime.ElapsedGameTime.TotalSeconds);

            mScale = MathHelper.Clamp(mScale, 0.0f, mScaleMax);

            mSprite.updateScale(mScale);
        }

        private void updateColor(GameTime pGameTime)
        {
            if ((mAge > mFadeAge) && (mFadeAge != 0))
            {
                mColor = mInitColor;
            }
            else
            {
                float amtInit = (float)mAge / (float)mFadeAge;
                float amtFinal = 1.0f - amtInit;

                mColor.R = (byte)((amtInit * mInitColor.R) + (amtFinal * mFinalColor.R));
                mColor.G = (byte)((amtInit * mInitColor.G) + (amtFinal * mFinalColor.G));
                mColor.B = (byte)((amtInit * mInitColor.B) + (amtFinal * mFinalColor.B));
                mColor.A = (byte)((amtInit * mInitColor.A) + (amtFinal * mFinalColor.A));
            }

            mSprite.updateColor(mColor);

        }
        public void draw(SpriteBatch pSpriteBatch)
        {
            if (!isAlive)
            {
                return;
            }

            mSprite.drawOrigin(pSpriteBatch, SpriteEffects.None);
        }
    }
}
