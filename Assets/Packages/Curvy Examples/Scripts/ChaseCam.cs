// =====================================================================
// Copyright 2013-2016 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

using UnityEngine;
using System.Collections;
using FluffyUnderware.DevTools;

namespace FluffyUnderware.Curvy.Examples
{
    [ExecuteInEditMode]
    public class ChaseCam : MonoBehaviour
    {
        public Transform LookAt;
        public Transform MoveTo;
        public Transform RollTo;
        [Positive]
        public float ChaseTime=0.5f;
        

        Vector3 mLastPos;
        Vector3 mVelocity;
        Vector3 mRollVelocity;

#if UNITY_EDITOR
        void Update()
        {
            if (!Application.isPlaying)
            {
                if (MoveTo)
                    transform.position = MoveTo.position;
                if (LookAt)
                {
                    if (!RollTo) transform.LookAt (LookAt);
                    else transform.LookAt (LookAt, RollTo.up);
                }
                // if (RollTo)
                //     transform.rotation = Quaternion.Euler (new Vector3 (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, RollTo.rotation.eulerAngles.z));
            }
        }
#endif

        // Update is called once per frame
        void LateUpdate()
        {
            if (MoveTo)
                transform.position=Vector3.SmoothDamp(transform.position, MoveTo.position, ref mVelocity, ChaseTime);
            if (LookAt)
            {
                if (!RollTo) transform.LookAt (LookAt);
                else transform.LookAt (LookAt, Vector3.SmoothDamp(transform.up, RollTo.up, ref mRollVelocity, ChaseTime));
            }
            // if (RollTo)
            //     transform.rotation = Quaternion.Euler (new Vector3 (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, RollTo.rotation.eulerAngles.z));
        }
    }
}
