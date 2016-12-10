// =====================================================================
// Copyright 2013-2016 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

using UnityEngine;
using System.Collections;
using FluffyUnderware.Curvy.Controllers;

namespace FluffyUnderware.Curvy.Examples
{
    public class SplineControllerInputRail : MonoBehaviour
    {
        public float acceleration = 0.1f;
        public float limit = 30.0f;
        public SplineController splineController;

        private IEnumerator Start ()
        {
            while (!splineController.IsInitialized)
                yield return 0;
            
        }

        private void Update ()
        {
            float velVert = Mathf.Clamp (Input.GetAxis("Vertical"), -1f, 1f);
            splineController.Speed = Mathf.Clamp (splineController.Speed + velVert * acceleration * Time.deltaTime, 0.001f, limit);
        }        
    }
}
