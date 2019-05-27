using UnityEngine;
using System.Collections;

namespace PlayerStates
{
    public class BaseState : State
    {
        public float h;
        public bool ControlFacingDirection { get; protected set; }
        public float facingDirection = 1;
    }
}

