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

namespace FluffyUnderware.Curvy.Examples
{
    [ExecuteInEditMode]
    public class TrainCarManager : MonoBehaviour
    {
        public SplineController Waggon;
        public SplineController FrontAxis;
        public SplineController BackAxis;

        public float Position
        {
            get
            {
                return Waggon.AbsolutePosition;
            }
            set
            {
                if (Waggon.AbsolutePosition != value)
                {
                    setPos(Waggon, value);
                    setPos(FrontAxis, value + mTrain.AxisDistance / 2);
                    setPos(BackAxis, value - mTrain.AxisDistance / 2);
                }
            }
        }
        
        TrainManager mTrain;

        void LateUpdate()
        {
            if (!mTrain)
                return;
            if (BackAxis.Spline == FrontAxis.Spline &&
                FrontAxis.RelativePosition > BackAxis.RelativePosition)
            {
                float carPos = Waggon.AbsolutePosition;
                float faPos = FrontAxis.AbsolutePosition;
                float baPos = BackAxis.AbsolutePosition;

                if (Mathf.Abs(Mathf.Abs(faPos - baPos) - mTrain.AxisDistance) >= mTrain.Limit)
                {
                    float df = faPos - carPos - mTrain.AxisDistance/2;
                    float db = carPos - baPos - mTrain.AxisDistance/2;
                    FrontAxis.Warp(-df);
                    BackAxis.Warp(db);
                }
            }
        }

      

        public void setup()
        {
            mTrain = GetComponentInParent<TrainManager>();
            if (mTrain.Spline)
            {
                setController(Waggon, mTrain.Spline, mTrain.Speed);
                setController(FrontAxis, mTrain.Spline, mTrain.Speed);
                setController(BackAxis, mTrain.Spline, mTrain.Speed);
            }
        }

        void setPos(SplineController c, float pos)
        {
            if (c.IsPlaying)
                c.Position = pos;
            else
                c.InitialPosition = pos;
        }

        void setController(SplineController c, CurvySpline spline, float speed)
        {
            c.Spline = spline;
            c.Speed = speed;
            c.OnControlPointReached.AddListenerOnce(OnCPReached);
            c.OnEndReached.AddListenerOnce(CurvyDefaultEventHandler.UseFollowUpStatic);
        }

        void OnCPReached(CurvySplineMoveEventArgs e)
        {
            var jc = e.ControlPoint.GetMetadata<MDJunctionControl>();
            if (jc)
            {
                if (jc.UseJunction)
                {
                    e.Follow(e.ControlPoint.Connection.OtherControlPoints(e.ControlPoint)[0]);
                    // Set the controller to use the new spline
                    SplineController controller = (SplineController)e.Sender;
                    controller.Spline = e.Spline;
                    controller.RelativePosition = e.TF;
                }
            }
        }

    }
}
