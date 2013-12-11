using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MissileDrizzle.Actors
{
    class Cannon
    {

        Texture2D
            mCannonTexture;
        Sprite
            mCannonSprite;
        int
            isFacingRight;
        float
            mRotationRight;
        public float
            mRealRotation{get; private set;}
        float
            mScale;
        public Vector2
            mPos { get; private set; }




        public Cannon(int pFacingRight)
        {
            mCannonSprite = new Sprite();
            isFacingRight = pFacingRight;
        }

        public void init(Texture2D pCannonTexture)
        {
            mCannonTexture = pCannonTexture;
            Rectangle source = new Rectangle(0,0, mCannonTexture.Width, mCannonTexture.Height);
            mCannonSprite.createSprite(pCannonTexture, source, Vector2.Zero);

            mRotationRight = 0.0f;
            //mRotationLeft = (float)Math.PI;
            //mScale = 1.25f;
            mPos = Vector2.Zero;
            
            //isFacingRight = true;
            //mCannonSprite.updateScale(mScale);
            if (!(isFacingRight == 1))
            {
                mRealRotation = (float)Math.PI;
            }
        }

        public void update(GameTime pGameTime, Vector2 pPos, int pIsLeft)
        {
            mPos = pPos;
            isFacingRight = pIsLeft;
            mCannonSprite.updatePos(mPos);
            mCannonSprite.updateRot(mRealRotation);
        }


        public void draw(SpriteBatch pSpriteBatch)
        {
            //if (isFacingRight)
                mCannonSprite.drawOrigin(pSpriteBatch, SpriteEffects.None);
        
                //mCannonSprite.drawOrigin(pSpriteBatch, SpriteEffects.FlipHorizontally);
        }

        
        public void cannonRotationHandler(Vector2 pDirection)
        {
            //float tmpRot = Math.Abs(mRot);
            //mRotationLeft = -(float)Math.PI + mRotationRight;

            if (pDirection.Y > 0) // rotate cannon down
            {
                //if (isFacingRight)
                    mRotationRight -= 0.1f;
                //else
                  //  mRotationRight -= 0.1f;
            }
            else if (pDirection.Y < 0) // rotate cannon up
            {
                //if (isFacingRight)
                    mRotationRight += 0.1f;
                //else
                    //mRotationRight += 0.1f;
            }

            

            mRotationRight = MathHelper.Clamp(mRotationRight, -1, 1);
            //mRotationLeft = MathHelper.Clamp(mRotationLeft, (float)(-1 +  Math.PI), (float)(1 + Math.PI));

            if (isFacingRight == 1)
            {
                mRealRotation = mRotationRight;
            }
            else
            {
                mRealRotation = -mRotationRight + (float)Math.PI;
            }

            //Trace.WriteLine(Math.PI - mRotationRight);
        }

    }
}
