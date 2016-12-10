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
    public class VolumeControllerInput : MonoBehaviour
    {
        public float AngularVelocity = 0.2f;
        public ParticleSystem explosionEmitter;
        public VolumeController volumeController;
        public Transform rotatedTransform;
        public Rigidbody rigidbodyComponent;
        public float maxSpeed = 40f;
        public float accelerationForward = 20f;
        public float accelerationBackward = 40f;
        public float speedBleed = 10f;
        private bool mGameOver;

        private void Awake ()
        {
            if (!volumeController)
                volumeController= GetComponent<VolumeController>();
        }

        // Use this for initialization
        private IEnumerator Start ()
        {
            if (volumeController)
            {
                while (!volumeController.IsInitialized)
                    yield return 0;

                
            }
        }

        private void Update ()
        {
            
            if (volumeController && !mGameOver)
            {
                if (!volumeController.IsPlaying) volumeController.Play();
                float velVert = Input.GetAxis("Vertical");
                float velHoriz = Input.GetAxis("Horizontal");

                float speedPenalty = Mathf.Abs (velHoriz);
                float speedRaw = volumeController.Speed + velVert * Time.deltaTime * Mathf.Lerp (accelerationBackward, accelerationForward, (velVert + 1f) / 2f) - speedPenalty * accelerationBackward * Time.deltaTime * 0.25f - speedBleed * Time.deltaTime;

                volumeController.Speed = Mathf.Clamp (speedRaw, 0f, maxSpeed);
                volumeController.CrossPosition += AngularVelocity * Mathf.Clamp (volumeController.Speed / 10f, 0.2f, 1f) * velHoriz * Time.deltaTime;

                
                if (rotatedTransform)
                {
                    float yTarget = Mathf.Lerp(-90f, 90f, (velHoriz + 1f) / 2f);
                    rotatedTransform.localRotation = Quaternion.Euler(0f, yTarget, 0f);
                }
            }
        }

        public void OnCollisionEnter(Collision collision)
        {
           
        }


        
        public void OnTriggerEnter(Collider other)
        {
            explosionEmitter.Emit(200);
            volumeController.Stop();
            mGameOver = true;
            Invoke ("StartOver", 1);
        }

        private void StartOver ()
        {
            volumeController.Speed = 0;
            volumeController.RelativePosition = 0;
            volumeController.CrossPosition = 0;
            mGameOver = false;
        }
      

        
    }
}
