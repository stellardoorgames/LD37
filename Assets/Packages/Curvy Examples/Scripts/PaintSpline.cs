// =====================================================================
// Copyright 2013-2016 Fluffy Underware
// All rights reserved
// 
// http://www.fluffyunderware.com
// =====================================================================

using UnityEngine;
using System.Collections;
using FluffyUnderware.Curvy;
using FluffyUnderware.DevTools.Extensions;
using FluffyUnderware.Curvy.Controllers;
using UnityEngine.UI;

/* 
 * In this example we let the user draw a spline on screen!
 * 
 */
namespace FluffyUnderware.Curvy.Examples
{
    public class PaintSpline : MonoBehaviour
    {
        public float StepDistance = 30;
        public SplineController Controller;
        public Text InfoText;

        CurvySpline mSpline;
        Vector2 mLastControlPointPos;
        bool mResetSpline = true;

        void Awake()
        {
            // for this example we assume the component is attached to a GameObject holding a spline
            mSpline = GetComponent<CurvySpline>();

        }

        void OnGUI()
        {
            // before using the spline, ensure it's initialized and the Controller is available
            if (mSpline == null || !mSpline.IsInitialized || !Controller)
                return;

            var e = Event.current;
            switch (e.type)
            {
                case EventType.MouseDrag:
                    // Start a new line?
                    if (mResetSpline)
                    {
                        mSpline.Clear(); // delete all Control Points
                        Controller.gameObject.SetActive(true);
                        Controller.Prepare();// set the Controller to start
                        mLastControlPointPos = e.mousePosition; // Store current mouse position
                        addCP(e.mousePosition); // add the first Control Point
                        mResetSpline = false;
                    }
                    else
                    {
                        // only create a new Control Point if the minimum distance is reached
                        float dist = (e.mousePosition - mLastControlPointPos).magnitude;
                        if (dist >= StepDistance)
                        {
                            mLastControlPointPos = e.mousePosition;
                            addCP(e.mousePosition);
                            if (!Controller.IsPlaying)
                                Controller.Play();
                        }
                    }

                    break;
                case EventType.MouseUp:
                    mResetSpline = true;
                    break;
            }
        }

        // Add a Control Point and set it's position
        CurvySplineSegment addCP(Vector3 mousePos)
        {
            Vector3 p = Camera.main.ScreenToWorldPoint(mousePos);
            p.y *= -1; // flip Y to get the correct world position

            var cp = mSpline.Add();
            cp.localPosition = p;

            InfoText.text = "Control Points: " + mSpline.ControlPointCount.ToString(); // set info text

            return cp;
        }

        // UI helper
        public void ToggleAdaptOnChange()
        {
            if (Controller)
                Controller.AdaptOnChange = !Controller.AdaptOnChange;
        }

    }
}
