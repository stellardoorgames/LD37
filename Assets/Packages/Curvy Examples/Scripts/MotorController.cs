// =====================================================================
// Copyright 2013-2016 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

using UnityEngine;
using System.Collections;
using FluffyUnderware.Curvy.Controllers;
using FluffyUnderware.DevTools;

namespace FluffyUnderware.Curvy.Examples
{
    public class MotorController : SplineController
    {
        [Section("Motor")]
        public float MaxSpeed = 30;

        protected override void Update()
        {
            Speed = Input.GetAxis("Vertical") * MaxSpeed;
            base.Update();
        }
    }
}
