using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MissileDrizzle.Actors;
using MissileDrizzle.Screen;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MissileDrizzle.Manager
{
    class BallManager
    {
        public const int
            MAX_BALLS = 10;
        public CannonShot[]
            mCannonBalls;
        Vector2
            cannon1Angle,
            cannon2Angle;

        const float
            CANNON_RADIUS = 50;

        ParticleManager
            ref_ParticleManager;
        List<Player>
            ref_PLAYERS;
           // ref_PlayerOne,
           // ref_PlayerTwo;


        public BallManager(List<Player> references, ref ParticleManager pPManager)
        {
            mCannonBalls = new CannonShot[MAX_BALLS];
            ref_PLAYERS = references;
            ref_ParticleManager = pPManager;
        }

        public void loadBall(ContentManager pContent)
        {
            Texture2D ballTex = pContent.Load<Texture2D>("FPO/ball");
            for (int i = 0; i < MAX_BALLS; i++)
            {
                mCannonBalls[i] = new CannonShot();
                mCannonBalls[i].init(ballTex);
            }
        }

        public void update(GameTime pGameTime)
        {
            foreach (CannonShot ball in mCannonBalls)
            {
                if (ball.isActive)
                {
                    ball.update(pGameTime);
                }
            }
        }

        public void draw(SpriteBatch pSpriteBatch)
        {
            foreach (CannonShot ball in mCannonBalls)
            {
                if (ball.isActive)
                {
                    ball.draw(pSpriteBatch);
                }
            }
        }

        //Event for plazyer one shooting
        public void playerOneShotHandler()
        {
            foreach (CannonShot ball in mCannonBalls)
            {
                if (ball.isActive == false)
                {
                    ball.spawn(ref_PLAYERS[(int)PlayerIndex.One].mCannon.mPos, ref_PLAYERS[(int)PlayerIndex.One].mCannon.mRealRotation);
                    cannon1Angle.X = ref_PLAYERS[(int)PlayerIndex.One].mCannon.mPos.X + CANNON_RADIUS * (float)Math.Cos((double)ref_PLAYERS[(int)PlayerIndex.One].mCannon.mRealRotation);
                    cannon1Angle.Y = ref_PLAYERS[(int)PlayerIndex.One].mCannon.mPos.Y + CANNON_RADIUS * (float)Math.Sin((double)ref_PLAYERS[(int)PlayerIndex.One].mCannon.mRealRotation);
                    ref_ParticleManager.addEffect(Particles.EffectType.explosionFromCannon, cannon1Angle, ref_PLAYERS[(int)PlayerIndex.One].mCannon.mRealRotation);

                    break;
                }
            }
        }

        //Event for Player two shooting;
        public void playerTwoShotHandler()
        {
            foreach (CannonShot ball in mCannonBalls)
            {
                if (ball.isActive == false)
                {
                    ball.spawn(ref_PLAYERS[(int)PlayerIndex.Two].mCannon.mPos, ref_PLAYERS[(int)PlayerIndex.Two].mCannon.mRealRotation);
                    break;
                }
            }
        }






    }

}
