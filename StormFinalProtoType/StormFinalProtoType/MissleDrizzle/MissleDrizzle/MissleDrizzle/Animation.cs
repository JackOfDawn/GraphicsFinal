using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MissileDrizzle
{
    class Animation
    {
        Texture2D
            mAnimationSpriteSheet;

        //Sprite specifics
        List<Sprite>
            mSprites;
        Vector2
            mPos;
        Color
            mColor;
        float
            mRot;
        float
            mScale;
        float
            mDepth;

        //AnimationSpecs
        int
            mNumCels;
        int
            mCelWidth;
        int
            mCelHeight;
        int
            mCurrentFrame;
        double
            mAnimationMax,
            mAnimationTime;
        

        public Animation()
        {
            mSprites = new List<Sprite>();
        }

        public void createAnimation(Texture2D pTexture, int pNumCels, int pCelWidth, int pCelHeight, double pAnimationMax, Vector2 pPos = new Vector2(), float pRot = 0.0f, float pScale = 1.0f,
            float pDepth = 1.0f)
        {
            mAnimationSpriteSheet = pTexture;
            mNumCels = pNumCels;
            mCelWidth = pCelWidth;
            mCelHeight = pCelHeight;

            mPos = pPos;
            mRot = pRot;
            mScale = pScale;
            mDepth = pDepth;
            mColor = Color.White;

            mCurrentFrame = 0;
            mAnimationMax = pAnimationMax;
            mAnimationTime = pAnimationMax;

            

            Sprite sprite;
            Rectangle sourceRectangle;
            for (int position = 0; position < mNumCels; position++)
            {
                sprite = new Sprite();
                sourceRectangle = new Rectangle(position * mCelWidth, 0, mCelWidth, mCelHeight);

                sprite.createSprite(mAnimationSpriteSheet, sourceRectangle, mPos, mRot, mScale, mDepth);
                mSprites.Add(sprite);
            }

        }

        public void update(GameTime pGameTime)
        {
            mAnimationTime -= pGameTime.ElapsedGameTime.Milliseconds;

            if (mAnimationTime <= 0)
            {
                mCurrentFrame = (mCurrentFrame + 1) % mNumCels;
                mAnimationTime = mAnimationMax;
            }

            mSprites[mCurrentFrame].updateDepth(mDepth);
            mSprites[mCurrentFrame].updatePos(mPos);
            mSprites[mCurrentFrame].updateRot(mRot);
            mSprites[mCurrentFrame].updateScale(mScale);
            mSprites[mCurrentFrame].updateColor(mColor);
        }


        public void updatePos(Vector2 pPos)
        {
            mPos = pPos;
        }

        public void updateRot(float pRot)
        {
            mRot = pRot;
        }

        public void updateScale(float pScale)
        {
            mScale = pScale;
        }

        public void updateColor(Color pColor)
        {
            mColor = pColor;
        }

        public void updateDepth(float pDepth)
        {
            mDepth = pDepth;
        }


        //Drawing 
        public void drawBaseOrigin(SpriteBatch pSpriteBatch, SpriteEffects pSpriteEffect)
        {
            mSprites[mCurrentFrame].drawBaseOrigin(pSpriteBatch, pSpriteEffect);
        }

        public void drawZeroOrigin(SpriteBatch pSpriteBatch, SpriteEffects pSpriteEffect)
        {
            mSprites[mCurrentFrame].drawZeroOrigin(pSpriteBatch, pSpriteEffect);
        }

        public void drawOrigin(SpriteBatch pSpriteBatch, SpriteEffects pSpriteEffect)
        {
            mSprites[mCurrentFrame].drawOrigin(pSpriteBatch, pSpriteEffect);
        }
    }
}
