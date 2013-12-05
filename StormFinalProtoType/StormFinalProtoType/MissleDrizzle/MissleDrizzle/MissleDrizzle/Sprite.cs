using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MissileDrizzle
{
    class Sprite
    {
        Texture2D
            mSpriteTexture; //{get; private set;}
        public Vector2
            mPos { get; private set; }
        Vector2
            mOrigin,
            mBaseOrigin;
        Color
            mColor;
        float
            mRot;
        float
            mScale;
        float
            mDepth;
        public Rectangle
            mSourceRectangle { get; private set; }

        public Sprite()
        {
            //mOrigin = Vector2.Zero;
        }

        public void createSprite(Texture2D pTexture, Rectangle pSource, Vector2 pPos = new Vector2(), float pRot = 0.0f, float pScale = 1.0f,
            float pDepth = 1.0f)
        {
            mSpriteTexture = pTexture;
            mPos = pPos;
            mRot = pRot;
            mScale = pScale;
            mColor = Color.White;
            mDepth = pDepth;
            mSourceRectangle = pSource;
            mOrigin = new Vector2(mSourceRectangle.Width / 2 , mSourceRectangle.Height / 2);
            mBaseOrigin = new Vector2(mSourceRectangle.Width / 2, mSourceRectangle.Height);
            
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

        public Rectangle getRectangle()
        {
            return mSourceRectangle;
        }

        public void drawOrigin(SpriteBatch pSpriteBatch, SpriteEffects pSpriteEffect)
        {
            //pSpriteBatch.Draw(mSpriteTexture, mPos, Color.White);
            pSpriteBatch.Draw(mSpriteTexture, mPos, mSourceRectangle, mColor, mRot, mOrigin, mScale, pSpriteEffect, 0.0f);
        }

        public void drawZeroOrigin(SpriteBatch pSpriteBatch, SpriteEffects pSpriteEffect)
        {
            //pSpriteBatch.Draw(mSpriteTexture, mPos, Color.White);
            pSpriteBatch.Draw(mSpriteTexture, mPos, mSourceRectangle, mColor, mRot, Vector2.Zero, mScale, pSpriteEffect, 0.0f);
        }

        public void drawBaseOrigin(SpriteBatch pSpriteBatch, SpriteEffects pSpriteEffect)
        {
            //pSpriteBatch.Draw(mSpriteTexture, mPos, Color.White);
            pSpriteBatch.Draw(mSpriteTexture, mPos, mSourceRectangle, mColor, mRot, mBaseOrigin, mScale, pSpriteEffect, 0.0f);
        }
    }
}
