using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MissileDrizzle.Actors
{
   
    class Player
    {

        //Sprite
          //  mStaticSprite;
        Animation
            mWalkingAnimation;
        Animation
            mIdleAnimation;

        public Vector2
            mPosition;
        Point
            mHitBoxPos;
        Vector2
            mCannonPos;
        public Cannon
            mCannon;



        Vector2
            mVelocity = Vector2.Zero;
        float
            mGravity = -30;
        float
            mJumpPower = -15;

        bool
            isJumping,
            isFacingRight,
            isIdol; //no you're not an Idol

        Rectangle 
            mHitBox;

     
        public Player(bool FacingRight)
        {
            //mStaticSprite = new Sprite();
            mWalkingAnimation = new Animation();
            mIdleAnimation = new Animation();
            mCannon = new Cannon(FacingRight);
            mPosition = Vector2.Zero;
            isFacingRight = FacingRight;

           
            //mCannonShots = new List<CannonShot>();
        }

        public void init(ContentManager content)
        {
            Texture2D tmpTexture = content.Load<Texture2D>("FPO/walksheet");
            //mStaticSprite.createSprite(tmpTexture, new Rectangle(0, 0, tmpTexture.Width, tmpTexture.Height));
            mWalkingAnimation.createAnimation(tmpTexture, 8, 84, 112, 100);

            tmpTexture = content.Load<Texture2D>("FPO/idol");
            mIdleAnimation.createAnimation(tmpTexture, 1, 84, 112, 100);

            tmpTexture = content.Load<Texture2D>("FPO/cannon");
            mCannon.init(tmpTexture);

            tmpTexture = content.Load<Texture2D>("FPO/ball");

            //Set up hit box.
            mHitBoxPos.X = (84 / 3);
            mHitBoxPos.Y = 0;

            mHitBox = new Rectangle((int)mHitBoxPos.X, (int)mHitBoxPos.Y, (int)84 / 3, (int)112);

            isJumping = false;
            isIdol = true;
        }

        public void update(GameTime pGameTime)
        {
            
            mHitBox.Location = mHitBoxPos;

            //Condense these into a list
            mWalkingAnimation.updatePos(mPosition);
           
            mWalkingAnimation.update(pGameTime);

            mIdleAnimation.updatePos(mPosition);
            mIdleAnimation.update(pGameTime);

            mCannonPos = new Vector2(mPosition.X, mPosition.Y + 10);
            
            mCannon.update(pGameTime, mCannonPos, isFacingRight);
            //mCannon.updateScale(2.0f);
            //Update Cannon rotation

            checkJumping();
            updatePlayerYVelocity(pGameTime);

            if (mVelocity.X < 0.25f && mVelocity.X > -0.25f)
            {
                isIdol = true;
            }
            else
            {
                isIdol = false;
            }

            if (mPosition.Y + 1 > 590)
            {
                mPosition.Y = 589;
                mVelocity.Y = 0;
            }
 
            mPosition.Y += mVelocity.Y;
            mVelocity.X = 0;
        }

        private void checkJumping()
        {
            if (mVelocity.Y < 0)
                isJumping = true;
            if (mVelocity.Y == 0)
                isJumping = false;
        }
        private void updatePlayerYVelocity(GameTime pGameTime)
        {
            float elapsedTime = (float)pGameTime.ElapsedGameTime.Milliseconds / 1000;
            mVelocity.Y = mVelocity.Y - mGravity * elapsedTime;
        }


        public void draw(SpriteBatch pSpriteBatch)
        {

            if (isFacingRight)
            {
                if (isIdol)
                {
                    mIdleAnimation.drawOrigin(pSpriteBatch, SpriteEffects.None);
                }
                else
                {
                    mWalkingAnimation.drawOrigin(pSpriteBatch, SpriteEffects.None);
                }
            }
            else
                if (isIdol)
                {
                    mIdleAnimation.drawOrigin(pSpriteBatch, SpriteEffects.FlipHorizontally);
                }
                else
                {
                    mWalkingAnimation.drawOrigin(pSpriteBatch, SpriteEffects.FlipHorizontally);
                }
            mCannon.draw(pSpriteBatch);
           
            //mCannon.drawOrigin(pSpriteBatch, SpriteEffects.None);
           
        }

        public void playerMovedHandler(Vector2 pDirection)
        {
            mPosition.X += (pDirection.X * 2);
            mVelocity.X = pDirection.X;
           //mPosition.X += mVelocity.X;
            //mPosition.Y -= pDirection.Y;
        }

        public void playerJumpHandler()
        {
            if (!isJumping)
            {
                mPosition.Y--;
                mVelocity.Y = mJumpPower;
            }
        }


    }
}
