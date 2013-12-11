using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MissileDrizzle.Actors;


namespace MissileDrizzle.Manager
{
    class CollisionManager
    {
        List<Player>
            ref_Players;
        CannonShot[]
            ref_Balls;
        ParticleManager
            ref_ParticleManager;

        Rectangle
            mLeftWall,
            mRightWall,
            mNet,
            mRightFloor,
            mLeftFloor;

        public CollisionManager()
        {
            
        }

        public void init(List<Player> players, CannonShot[] balls, ref ParticleManager pPManager)
        {
            ref_Balls = balls;
            ref_Players = players;
            ref_ParticleManager = pPManager;

            //Walls
            mLeftWall = new Rectangle(0, 177, 25, 474);
            mRightWall = new Rectangle(1255, 177, 25, 474);

            //Floors
            mRightFloor = new Rectangle(749, 643, 531, 87);
            mLeftFloor = new Rectangle(0, 643, 531, 87);

            //Net
            mNet = new Rectangle(614, 476, 52, 244);
        }

        public void update(GameTime pGameTime)
        {
            foreach (CannonShot ball in ref_Balls)
            {
                if (ball.isActive)
                {
                    if (ball.canDamage)
                    {
                       
                    }
                }
            }
        }
     


    }
}
