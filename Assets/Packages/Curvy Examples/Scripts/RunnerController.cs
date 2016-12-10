// =====================================================================
// Copyright 2013-2016 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

using UnityEngine;
using System.Collections;
using FluffyUnderware.Curvy.Controllers;
using FluffyUnderware.Curvy.Components;
using FluffyUnderware.DevTools;

namespace FluffyUnderware.Curvy.Examples
{

    /// <summary>
    /// Example custom Controller
    /// </summary>
    public class RunnerController : SplineController
    {
        [Section("Jump")]
        public float JumpHeight = 20;
        public float JumpSpeed = 0.5f;
        public AnimationCurve JumpCurve = AnimationCurve.Linear(0, 0, 1, 1);
        public float Gravity = 10;

        enum GuideMode
        {
            Guided,
            Jumping,
            FreeFall
        }
        GuideMode mMode;
        GuideMode mNewMode;
        float mHeightOverGround;
        float mDownSpeed;
        
        SplineRefMetadata mPossibleSwitchTarget;
        CurvySpline mFreeFallTarget;
        int mSwitchInProgress; // 0=No, 1=to right, -1=to left

        protected override void OnDisable()
        {
            base.OnDisable();
            StopAllCoroutines();
        }

        protected override void Update()
        {
            // INPUT
            
            // Jump?
            if (Input.GetButtonDown("Fire1") && mMode == GuideMode.Guided)
                StartCoroutine(Jump());
            // If allowed to switch and player wants to, initiate switching
            if (mPossibleSwitchTarget != null && mSwitchInProgress == 0)
            {
                float xAxis = Input.GetAxisRaw("Horizontal");
                if (mPossibleSwitchTarget.Options == "Right" && xAxis > 0)
                {
                    Switch(1);
                }
                else if (mPossibleSwitchTarget.Options == "Left" && xAxis < 0)
                {
                    Switch(-1);
                }
            }
            // else check if we need to finalize the switch process
            else if (mSwitchInProgress != 0 && !IsSwitching)
            {
                mSwitchInProgress = 0;
                OnCPReached(new CurvySplineMoveEventArgs(this, Spline, Spline.TFToSegment(RelativePosition), 0, 0, 0));
            }

            // MOVEMENT
            
            // If not falling
            if (mMode != GuideMode.FreeFall)
            {
                // Let the controller advance
                base.Update();
                
                // if we're jumping, translate back to the wanted height above track
                if (mHeightOverGround > 0 && mMode==GuideMode.Jumping)
                    transform.Translate(new Vector3(0, mHeightOverGround, 0), Space.Self);
            } else
            { // handling falling
                mDownSpeed += Gravity * Time.deltaTime;
                // advance using last known velocity vector
                transform.Translate(new Vector3(0, -mDownSpeed, Speed * Time.deltaTime));
                // check distance to falling target and reenter "guided" mode when near
                // NOTE: to support falling to multiple targets one would skip this method and use a trigger instead that collides with track geometry and reenters "guided" mode then
                var posInTargetSpace = mFreeFallTarget.transform.InverseTransformPoint(transform.position);
                Vector3 nearestPoint;
                var nearestPointTF = mFreeFallTarget.GetNearestPointTF(posInTargetSpace, out nearestPoint);
                if ((nearestPoint - posInTargetSpace).magnitude <= 2f)
                {
                    Spline = mFreeFallTarget;
                    RelativePosition = nearestPointTF;
                    mMode = GuideMode.Guided;
                    mDownSpeed = 0;
                    mFreeFallTarget = null;
                }
            }

           
            
        }

        // Initiate Lane Switching
        void Switch(int dir)
        {
            mSwitchInProgress = dir;
            var posInTargetSpace = mPossibleSwitchTarget.Spline.transform.InverseTransformPoint(transform.position);
            Vector3 nearestPoint;
            var nearestPointTF=mPossibleSwitchTarget.Spline.GetNearestPointTF(posInTargetSpace,out nearestPoint, mPossibleSwitchTarget.CP.SegmentIndex);
            float swSpeed = (nearestPoint - posInTargetSpace).magnitude / Speed;
            SwitchTo(mPossibleSwitchTarget.Spline, nearestPointTF, swSpeed);
        }

        // Do a Jump
        IEnumerator Jump()
        {
            mMode=GuideMode.Jumping;
            mNewMode = GuideMode.Guided;
            float start=Time.time;
            float f=0;
            while (f<1)
            {
                f=(Time.time-start)/JumpSpeed;
                mHeightOverGround = JumpCurve.Evaluate(f) * JumpHeight;
                yield return new WaitForEndOfFrame();
            }
            mMode = mNewMode;
            mDownSpeed = 0;
        }
        
        // Retrieve metadata
        public void OnCPReached(CurvySplineMoveEventArgs e)
        {
            mPossibleSwitchTarget = e.ControlPoint.GetMetadata<SplineRefMetadata>();
            // if not properly configured, ignore!
            if (mPossibleSwitchTarget && !mPossibleSwitchTarget.Spline)
                mPossibleSwitchTarget = null;
        }

        // A: Use Head To/Follow Up ControlPoint to proceed
        // B: Start Falling if any Control Point connected
        // C: Else Die!
        public void UseFollowUpOrFall(CurvySplineMoveEventArgs e)
        {
            // we need a SplineController as well as a following spline to work with
            if (e.Sender is SplineController)
            {
                if (e.ControlPoint.FollowUp)
                {
                    // Follow the connected spline
                    e.Follow(e.ControlPoint.FollowUp, e.ControlPoint.FollowUpHeading);
                    // Set the controller to use the new spline
                    SplineController controller = (SplineController)e.Sender;
                    controller.Spline = e.Spline;
                    controller.RelativePosition = e.TF;
                    // handle controller events for the new passed ControlPoint
                    controller.OnControlPointReached.Invoke(e);
                }
                else
                {
                    if (e.ControlPoint.Connection){
                        var otherCP=e.ControlPoint.Connection.OtherControlPoints(e.ControlPoint)[0];
                        mFreeFallTarget = otherCP.Spline;
                        if (mMode==GuideMode.Jumping)
                        {
                            e.Follow(otherCP, ConnectionHeadingEnum.Auto);
                            Spline = mFreeFallTarget;
                            RelativePosition = otherCP.LocalFToTF(0);
                            mNewMode = GuideMode.FreeFall;
                        } else
                            mMode = GuideMode.FreeFall;

                    } else
                    {
                        Debug.Log("YOU DIED!");
#if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
#else                        
                        Application.Quit();
#endif
                    } 
                }
            }
        }
        
    }

}
