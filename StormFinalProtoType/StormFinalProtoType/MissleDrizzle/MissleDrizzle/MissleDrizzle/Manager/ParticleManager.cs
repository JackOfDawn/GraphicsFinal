using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MissileDrizzle.Particles;


namespace MissileDrizzle.Manager
{
   
    class ParticleManager
    {
        public ParticleEffect[]
            mAllEffects;
        public const int
            MAX_EFFECTS = 5;
        public int
            ParticleCountAlive { get; private set; }
        public int
            ParticleCount { get; private set; }

        public const int
            MAX_PARTICLES = 1500;

        public Particle[]
            mParticles;



        public ParticleManager()
        {
            mAllEffects = new ParticleEffect[MAX_EFFECTS];
            mParticles = new Particle[MAX_PARTICLES];
        }

        public void loadContent(ContentManager pContent)
        {
            ParticleEffect.loadContent(pContent);

            for (int i = 0; i < MAX_EFFECTS; i++)
            {
                mAllEffects[i] = new ParticleEffect();
            }

            for (int i = 0; i < MAX_PARTICLES; i++)
            {
                mParticles[i] = new Particle();
            }
        }

        public void addEffect(EffectType pType, Vector2 pOrigin)
        {
            for (int i = 0; i < MAX_EFFECTS; i++)
            {
                if (!mAllEffects[i].isAlive())
                {
                    mAllEffects[i].initialize(pType, pOrigin, ref mParticles);
                    break;
                }
            }
        }

        public void update(GameTime pGameTime)
        {
            ParticleCount = 0;
            ParticleCountAlive = 0;
            foreach (ParticleEffect pe in mAllEffects)
            {
                //ParticleCount += mAllEffects[i].mParticles.Count();
                if (pe.isAlive())
                {
                    pe.update(pGameTime);
                }
            }

            foreach (Particle p in mParticles)
            {
                ParticleCount++;
                if (p.isAlive)
                {
                    p.update(pGameTime);
                    ParticleCountAlive++;
                }
            }
        }

        public void draw(SpriteBatch pSpriteBatch)
        {
            foreach (Particle p in mParticles)
            {
                if (p.isAlive)
                {
                    p.draw(pSpriteBatch);
                }
            }
        }

    }
}
