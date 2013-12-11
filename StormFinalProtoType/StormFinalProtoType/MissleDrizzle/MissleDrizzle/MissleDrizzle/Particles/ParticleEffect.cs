using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MissileDrizzle.Particles
{
    public enum EffectType
    {
        smoke = 0,
        fire,
        explosionFromCannon,
        rain,
        explosionOnGround
    }

    class ParticleEffect
    {
        public EffectType
            mType;
        public Texture2D
            particleTexture;
        public static Texture2D
            starTexture;

        public BlendState
            mBlendState;
        public Vector2
            mOrigin;
        public float
            mOriginRadius;
        public Random
            myRandom;
        public float
            mAngle;

        public int 
            mEffectDuration;
        public int
            mNewParticleAmt;
        public int
            mBurstFreqMS;
        public int
            mBurstCountdownMS;
        public Particle[] 
            mParticles {get; private set;}

        #region General Stuff
            public ParticleEffect()
            {
            }

            public void initialize(EffectType pType, Vector2 pOrigin, ref Particle[] particleCollection, float angle = 0.0f)
            {
                mParticles = particleCollection;
                mType = pType;
                mOrigin = pOrigin;
                mAngle = angle;

                switch (mType)
                {
                    case EffectType.explosionOnGround:
                        initFountain();
                        break;
                    case EffectType.explosionFromCannon:
                        initExplosion();
                        break;
                }   
            }

            public static void loadContent(ContentManager content)
            {
                //circleTexture = content.Load<Texture2D>("Rain");
                starTexture = content.Load<Texture2D>("FPO/pixelParticle");
            }

            public void createParticle(int particleNum)
            {
                myRandom = new Random();
                switch (mType)
                {
                    case EffectType.explosionOnGround:
                        createFountainEffect(particleNum);
                        break;
                    case EffectType.explosionFromCannon:
                        createExplosionFromCannonEffect(particleNum);
                        break;
                }

            }

            public void update(GameTime pGameTime)
            {
                mEffectDuration -= pGameTime.ElapsedGameTime.Milliseconds;
                mBurstCountdownMS -= pGameTime.ElapsedGameTime.Milliseconds;

                if ((mBurstCountdownMS <= 0) && (mEffectDuration >= 0))
                {
                    int particleNum = 0;
                    for (int i = 0; i < mNewParticleAmt; i++)
                    {
                        for (int j = 0; j < mParticles.Length; j++)
                        {
                            if (!mParticles[j].isAlive)
                            {
                                particleNum = j;
                                break;
                            }
                        }
                        createParticle(particleNum);
                    }

                    mBurstCountdownMS = mBurstFreqMS;
                }
            }

            public bool isAlive()
            {
                bool isAlive = false;
                if (mEffectDuration > 0)
                    isAlive = true;
                return isAlive;
            }

        #endregion

        #region Explosion From Cannon
            private void initExplosion()
            {
                particleTexture = starTexture;
                mEffectDuration = 100;
                mNewParticleAmt = 20;
                mBurstFreqMS = 2;
                mBurstCountdownMS = 2;
                mBlendState = BlendState.AlphaBlend;
                //mOrigin = new Vector2(400, 300);
            }

            private void createExplosionFromCannonEffect(int particleNum)
            {
                int initAge = 200; // 1000 per second
                int fadeAge = 200;

                float angle = (float)myRandom.Next(-50, 50) / 100.0f;

                angle = mAngle - angle;

                Vector2 initPos = mOrigin;
                Vector2 initVel = new Vector2((float)myRandom.Next(100, 200) * (float)Math.Cos(angle), (float)myRandom.Next(100, 200) * (float)Math.Sin(angle));

                Vector2 initAcc = new Vector2(0, 0);
                float initDamp = 1.0f;

                float initRot = 90.0f;
                float initRotVel = 0.0f;
                float initRotDamp = 0.99f;

                float initScale = 1.0f;
                float initScaleVel = -0.2f;
                float initScaleAcc = -0.1f;
                float maxScale = 1.0f;

                Color initColor = Color.Gray;
                Color finalColor = Color.DarkGray;
                finalColor.A = 0;


                //Particle tempParticle = new Particle();
                mParticles[particleNum].createParticle(particleTexture, initAge, initPos, initVel, initAcc, initDamp, initRot, initRotVel, initRotDamp,
                    initScale, initScaleVel, initScaleAcc, maxScale, initColor, finalColor, fadeAge);
                //mParticles.Add(tempParticle);

            }


        #endregion

        #region Ground Explosion Effect

            private void initFountain()
            {
                particleTexture = starTexture;
                
                mEffectDuration = 200;
                mNewParticleAmt = 1;
                mBurstFreqMS = 1;
                mBurstCountdownMS = mBurstFreqMS;
                mBlendState = BlendState.Additive;
               // mOrigin = new Vector2(200, 200);
            }

            private void createFountainEffect(int particleNum)
            {
                int initAge = 1000; // 1000 per second
                int fadeAge = 100;

                float random = myRandom.Next(-100, 100);
                //random = random / 100;

                Vector2 initPos = mOrigin;
                Vector2 initVel = new Vector2(random, .5f *(float)-myRandom.Next(50, 150));

                Vector2 initAcc = new Vector2(0, 50);
                float initDamp = 1.0f;

                float initRot = 0.0f;
                float initRotVel = 0.0f;
                float initRotDamp = 1.0f;

                float initScale = 1.0f;
                float initScaleVel = 0.0f;
                float initScaleAcc = 0.0f;
                float maxScale = 1.0f;

                Color initColor = Color.OrangeRed;
                Color finalColor = Color.Yellow;
                finalColor.A = 0;


                mParticles[particleNum].createParticle(particleTexture, initAge, initPos, initVel, initAcc, initDamp, initRot, initRotVel, initRotDamp,
                    initScale, initScaleVel, initScaleAcc, maxScale, initColor, finalColor, fadeAge);
            }

        #endregion
    }
}
