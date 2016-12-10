// =====================================================================
// Copyright 2013-2016 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

using UnityEngine;
using System.Collections;
using FluffyUnderware.Curvy.Controllers;
using UnityEngine.UI;

namespace FluffyUnderware.Curvy.Examples
{
    [ExecuteInEditMode]
    public class TrainCarDrifter : MonoBehaviour
    {
        public float speed = 30f;
        public float wheelSpacing = 9.72f;
        public Vector3 bodyOffset = new Vector3 (0f, 1f, 0f);
        public SplineController controllerWheelLeading;
        public SplineController controllerWheelTrailing;
        public Transform trainCar;


        // Use this for initialization
        private IEnumerator Start ()
        {
            if (controllerWheelLeading)
            {
                while (!controllerWheelLeading.Spline.IsInitialized) yield return 0;
            }
            if (controllerWheelTrailing)
            {
                while (!controllerWheelTrailing.Spline.IsInitialized) yield return 0;
            }
            controllerWheelLeading.Speed = speed;
        }

        // Update is called once per frame
        private void Update ()
        {
            if (controllerWheelLeading && controllerWheelTrailing && controllerWheelLeading.Spline && controllerWheelTrailing.Spline && controllerWheelLeading.Spline != controllerWheelTrailing.Spline && controllerWheelLeading.Spline.IsInitialized && controllerWheelTrailing.Spline.IsInitialized && trainCar)
            {
                // var lookupPos = Spline.transform.InverseTransformPoint (Lookup.position);
                // float nearestTF = Spline.GetNearestPointTF (lookupPos);
                // transform.position = Spline.transform.TransformPoint (Spline.Interpolate (nearestTF));
                

                Vector3 lookupPos = controllerWheelTrailing.Spline.transform.InverseTransformPoint (controllerWheelLeading.transform.position);
                float nearestTF = controllerWheelTrailing.Spline.GetNearestPointTF (lookupPos);

                Vector3 trailingPosition = controllerWheelTrailing.Spline.Interpolate (nearestTF);
                controllerWheelTrailing.RelativePosition = nearestTF; // Mathf.SmoothDamp (controllerWheelTrailing.RelativePosition, nearestTF, ref controllerWheelTrailingVelocity, 0.1f);

                float trackDistance = Vector3.Distance (controllerWheelLeading.transform.position, trailingPosition);
                float wheelOffset = Mathf.Clamp (Mathf.Sqrt ((wheelSpacing * wheelSpacing) - (trackDistance * trackDistance)), 0f, 20f);

                controllerWheelTrailing.AbsolutePosition -= wheelOffset;
                trainCar.position = (controllerWheelLeading.transform.position + controllerWheelTrailing.transform.position) / 2f + bodyOffset;

                Vector3 targetPostition = new Vector3 (controllerWheelLeading.transform.position.x, trainCar.transform.position.y, controllerWheelLeading.transform.position.z);
                trainCar.LookAt (targetPostition, controllerWheelLeading.transform.up);
            }
        }
    }
}
