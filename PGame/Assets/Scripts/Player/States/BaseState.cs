using UnityEngine;
using System.Collections;

namespace PlayerStates
{
    public class BaseState : State
    {
        public BasicController controller;
        public float h;
        public bool ControlFacingDirection { get; protected set; }
        public float facingDirection = 1;
        public bool CanJump = true;
        public bool CanDash = true;
    }
}

