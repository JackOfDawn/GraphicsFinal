using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MissileDrizzle.Actors
{
    class CannonShot
    {
        Sprite
            mBallSprite;
        public Vector2
            mPos { get; private set; }

        const float 
            GRAVITY = -60;
        const float 
            POWER = -30;

        Vector2 mVelocity;

        public bool
            canDamage { get; private set; }
        public bool
            explode { get; set; }
        float
            coolDown,
            MAX_COOL = 1.0f;


        public bool
            isActive { get; private set; }
        
        public CannonShot()
        {
            mBallSprite = new Sprite();
        }

        public void init(Texture2D ballTexture)
        {
            Rectangle tmpRect = new Rectangle(0,0,ballTexture.Width, ballTexture.Height);
            mBallSprite.createSprite(ballTexture, tmpRect);
            isActive = false;
            mPos = Vector2.Zero;
            //mBallSprite.updateScale(2.0f);
            coolDown = MAX_COOL;
            canDamage = false;
            explode = false;
            
        }

        public void spawn(Vector2 pPos, float pRotation)
        {
            mVelocity = Vector2.Zero;
            mPos = pPos;
            mVelocity.Y = POWER * (float)Math.Sin(-pRotation);
            mVelocity.X = POWER * -(float)Math.Cos(pRotation);
            isActive = true;
            coolDown = MAX_COOL;
            canDamage = false;
            explode = false;
        }

        public void update(GameTime pGameTime)
        {
            if (isActive)
            {
                mVelocity.Y = mVelocity.Y - (GRAVITY * ((float)pGameTime.ElapsedGameTime.Milliseconds / 1000));
                mPos += mVelocity;
                mBallSprite.updatePos(mPos);
            }
            if (mBallSprite.mPos.Y > 630)
            {
                isActive = false;
                explode = true;
            }

            
            if (coolDown <= 0)
            {
                canDamage = true;
            }
            else
            {
                coolDown -= pGameTime.ElapsedGameTime.Seconds;
            }
        }


        public void draw(SpriteBatch pSpriteBatch)
        {
            if(isActive)
            {
                mBallSprite.drawOrigin(pSpriteBatch, SpriteEffects.None);
            }
        }

        public void kill()
        {
            isActive = false;
        }
        
    }
}
