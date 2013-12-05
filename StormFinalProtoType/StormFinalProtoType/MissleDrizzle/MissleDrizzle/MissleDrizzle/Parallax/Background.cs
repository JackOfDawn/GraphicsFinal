using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MissileDrizzle.Parallax
{
    class Background
    {
        List<Sprite>
            mSmallClouds;
        List<Sprite>
            mCloudBackground;
        Vector2
            mDefaultSpeed;
        Random
            rand;

        public Background()
        {
            rand = new Random();
            mSmallClouds = new List<Sprite>();
            mCloudBackground = new List<Sprite>();

            //clouds
            for (int i = 0; i < 4; i++)
            {
                mSmallClouds.Add(new Sprite());
            }

            //cloud background
            for (int i = 0; i < 2; i++)
            {
                mCloudBackground.Add(new Sprite());
            }
        }
        public void init(ContentManager content)
        {

            mDefaultSpeed = new Vector2(-0.3f, 0);
            //TODO: Make this read in by XML
            

            Texture2D tmp = content.Load<Texture2D>("FPO/MainCLoud");
            Rectangle tmpRect = new Rectangle(0,0, tmp.Width, tmp.Height);
            foreach (Sprite back in mCloudBackground)
            {
                back.createSprite(tmp, tmpRect);
                back.updateScale(2.0f);
            }

            mCloudBackground[1].updatePos(new Vector2(mCloudBackground[0].mSourceRectangle.Width * 2f, 0));

            tmp = content.Load<Texture2D>("FPO/clouds");

            //cloud 1
            tmpRect = new Rectangle(0, 0, 316, 76);
            mSmallClouds[0].createSprite(tmp, tmpRect);

            //cloud 2
            tmpRect = new Rectangle(0, 137, 240, 39);
            mSmallClouds[1].createSprite(tmp, tmpRect);

            //cloud 3
            tmpRect = new Rectangle(0, 77, 253, 59);
            mSmallClouds[2].createSprite(tmp, tmpRect);

            //cloud 4
            tmpRect = new Rectangle(0, 177, 228, 64);
            mSmallClouds[3].createSprite(tmp, tmpRect);

            foreach (Sprite cloud in mSmallClouds)
            {
                cloud.updateScale(2.0f);
                cloud.updatePos(generateLocation());
            }

            
        }

        private Vector2 generateLocation()
        {
            Vector2 newLoc = new Vector2(rand.Next(1500, 2400), rand.Next(0, 250));
            return newLoc;
            
        }

        public void update(GameTime pGameTime)
        {
            //UPdate main cloud position
            if (mCloudBackground[0].mPos.X < mCloudBackground[1].mPos.X)
            {
                mCloudBackground[1].updatePos(mCloudBackground[1].mPos + mDefaultSpeed);
                mCloudBackground[0].updatePos(mCloudBackground[0].mPos + mDefaultSpeed);
            }
            else
            {
                mCloudBackground[0].updatePos(mCloudBackground[0].mPos + mDefaultSpeed);
                mCloudBackground[1].updatePos(mCloudBackground[1].mPos + mDefaultSpeed);
            }

            //Other Clouds
            //Cloud 1
            mSmallClouds[0].updatePos(mSmallClouds[0].mPos + new Vector2(-0.75f, 0));
            if (mSmallClouds[0].mPos.X + (mSmallClouds[0].mSourceRectangle.Width) < 0)
            {
                mSmallClouds[0].updatePos(generateLocation());
            }
            //Cloud 2
            mSmallClouds[1].updatePos(mSmallClouds[1].mPos + new Vector2(-0.75f, 0));
            if (mSmallClouds[1].mPos.X + (mSmallClouds[1].mSourceRectangle.Width) < 0)
            {
                mSmallClouds[1].updatePos(generateLocation());
            }
            //Cloud 3
            mSmallClouds[2].updatePos(mSmallClouds[2].mPos + new Vector2(-0.75f, 0));
            if (mSmallClouds[2].mPos.X + (mSmallClouds[2].mSourceRectangle.Width) < 0)
            {
                mSmallClouds[2].updatePos(generateLocation());
            }
            //Cloud 4
            mSmallClouds[3].updatePos(mSmallClouds[3].mPos + new Vector2(-0.75f, 0));
            if (mSmallClouds[3].mPos.X + (mSmallClouds[3].mSourceRectangle.Width) < 0)
            {
                mSmallClouds[3].updatePos(generateLocation());
            }


            for (int i = 0; i < mCloudBackground.Count(); i++)
            {
                mCloudBackground[i].updatePos(mCloudBackground[i].mPos + mDefaultSpeed);
            }

            //reset background cloud
            if (mCloudBackground[0].mPos.X + (mCloudBackground[0].mSourceRectangle.Width * 2) < 0)
            {
                mCloudBackground[0].updatePos(mCloudBackground[1].mPos + new Vector2(mCloudBackground[1].mSourceRectangle.Width * 2, 0));
            }
            if (mCloudBackground[1].mPos.X + (mCloudBackground[1].mSourceRectangle.Width * 2) < 0)
            {
                mCloudBackground[1].updatePos(mCloudBackground[0].mPos + new Vector2(mCloudBackground[0].mSourceRectangle.Width * 2, 0));
            }
        }

        public void draw(SpriteBatch pSpriteBatch)
        {
            foreach (Sprite s in mCloudBackground)
            {
                s.drawZeroOrigin(pSpriteBatch, SpriteEffects.None);
            }

            foreach (Sprite cloud in mSmallClouds)
            {
                cloud.drawOrigin(pSpriteBatch, SpriteEffects.None);
            }
        }


    }
}
