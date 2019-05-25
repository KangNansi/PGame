using UnityEngine;
using System.Collections;

namespace MaskedShooterStates
{
    public class Idle : State
    {
        private float minWait;
        private float maxWait;
        private Timer timer = new Timer();

        private float waitTime;

        public Idle(float minWait, float maxWait)
        {
            this.minWait = minWait;
            this.maxWait = maxWait;
        }

        public override void Start()
        {
            base.Start();
            timer.Restart();
            waitTime = UnityEngine.Random.Range(minWait, maxWait);
        }

        public override void Update()
        {
            if(timer.Elapsed > waitTime)
            {
                Stop();
            }
        }
    }
}
