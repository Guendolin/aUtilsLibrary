//	============================================================
//	Name:		Jiggle Bone v.1.0
//	Author: 	Michael Cook (Fishypants)
//	Date:		9-25-2011
//	License:	Free to use. Any credit would be nice :)
//
//	To Use:
// 		Drag this script onto a bone. (ideally bones at the end)
//		Set the boneAxis to be the front facing axis of the bone.
//		Done! Now you have bones with jiggle dynamics.
//
//	============================================================

using UnityEngine;
using System.Collections;

namespace aSystem.aUtilsLibrary
{
    public class JiggleBone : MonoBehaviour
    {
        public bool debugMode = true;

        // Target and dynamic positions
        private Vector3 targetPos = new Vector3();
        private Vector3 dynamicPos = new Vector3();

        // Bone settings
        public Vector3 boneAxis = new Vector3(0, 1, 0);
        public Vector3 boneLookAxis = new Vector3(0, 0, -1);
        public Vector3 boneFixRotation = new Vector3(90, 0, 0);
        public float targetDistance = 2.0f;

        // Dynamics settings
        //public float bStiffness = 0.2f;
        public Vector3 axisStiffness = new Vector3(0.2f, 0.2f, 0.2f);
        public float bMass = 0.9f;
        public float bDamping = 0.75f;
        public float bGravity = 0.75f;

        private bool _enabled = true;

        // Dynamics variables
        Vector3 force = new Vector3();
        Vector3 acc = new Vector3();
        Vector3 vel = new Vector3();

        // Squash and stretch variables
        public bool SquashAndStretch = false;
        public float sideStretch = 0.15f;
        public float frontStretch = 0.2f;

        private Quaternion _originalRotation;
        private Vector3 _originalScale;
        private Quaternion _rotation;

        void Awake()
        {
            // Set targetPos and dynamicPos at startup
            targetPos = transform.position + transform.TransformDirection(new Vector3((boneAxis.x * targetDistance), (boneAxis.y * targetDistance), (boneAxis.z * targetDistance)));
            dynamicPos = targetPos;
            axisStiffness.x = Mathf.Clamp(axisStiffness.x, 0.0f, 1.0f);
            axisStiffness.y = Mathf.Clamp(axisStiffness.y, 0.0f, 1.0f);
            axisStiffness.z = Mathf.Clamp(axisStiffness.z, 0.0f, 1.0f);

            _originalRotation = transform.localRotation;
            _rotation = transform.rotation;
        }

        public void SetEnabled(bool enabled, Vector3 originalScale)
        {
            _originalScale = originalScale;
            _enabled = enabled;

            if (enabled && !this.enabled)
                this.enabled = true;
        }

        public void ResetJiggleValues()
        {
            transform.localRotation = _originalRotation;
            _rotation = transform.rotation;
            dynamicPos = transform.position + transform.TransformDirection(new Vector3((boneAxis.x * targetDistance), (boneAxis.y * targetDistance), (boneAxis.z * targetDistance)));
        }

        void LateUpdate()
        {
            if (!_enabled)
            {
                Vector3 newScale = Vector3.MoveTowards(transform.localScale, _originalScale, Time.deltaTime * 10.0f);
                transform.localScale = newScale;
                Quaternion newRot = Quaternion.RotateTowards(transform.localRotation, _originalRotation, Time.deltaTime * 10.0f);
                //transform.localRotation = newRot;
                if (newScale == _originalScale && newRot == _originalRotation)
                    this.enabled = false;
            }
            else
            {
                // Reset the bone rotation so we can recalculate the upVector and forwardVector
                Quaternion prevRot = transform.rotation;
                transform.rotation = new Quaternion();

                // Update forwardVector and upVector
                Vector3 forwardVector = transform.TransformDirection(new Vector3((boneAxis.x * targetDistance), (boneAxis.y * targetDistance), (boneAxis.z * targetDistance)));

                // Calculate target position
                Vector3 targetPos = transform.position + forwardVector;

                force = Vector3.Scale((targetPos - dynamicPos), axisStiffness);
                acc = force / bMass;
                vel += acc * (1.0f - bDamping);

                // Update dynamic postion
                dynamicPos += vel + force;

                // Set bone rotation to look at dynamicPos
                transform.LookAt(dynamicPos, transform.parent.rotation * boneLookAxis);
                Quaternion rot = transform.rotation * Quaternion.Euler(boneFixRotation);

                _rotation = Quaternion.Slerp(_rotation, rot, Time.deltaTime * 20.0f);
                transform.rotation = _rotation;
                _rotation = transform.rotation;


                // ==================================================
                // Squash and Stretch section
                // ==================================================
                if (SquashAndStretch)
                {
                    // Create a vector from target position to dynamic position
                    // We will measure the magnitude of the vector to determine
                    // how much squash and stretch we will apply
                    Vector3 dynamicVec = dynamicPos - targetPos;

                    // Get the magnitude of the vector
                    float stretchMag = dynamicVec.magnitude;

                    // Here we determine the amount of squash and stretch based on stretchMag
                    // and the direction the Bone Axis is pointed in. Ideally there should be
                    // a vector with two values at 0 and one at 1. Like Vector3(0,0,1)
                    // for the 0 values, we assume those are the sides, and 1 is the direction
                    // the bone is facing
                    float xStretch;
                    if (boneAxis.x == 0) xStretch = 1 + (-stretchMag * sideStretch);
                    else xStretch = 1 + (stretchMag * frontStretch);

                    float yStretch;
                    if (boneAxis.y == 0) yStretch = 1 + (-stretchMag * sideStretch);
                    else yStretch = 1 + (stretchMag * frontStretch);

                    float zStretch;
                    if (boneAxis.z == 0) zStretch = 1 + (-stretchMag * sideStretch);
                    else zStretch = 1 + (stretchMag * frontStretch);

                    // Set the bone scale
                    transform.localScale = new Vector3(xStretch, yStretch, zStretch);
                }

                // ==================================================
                // DEBUG VISUALIZATION
                // ==================================================
                // Green line is the bone's local up vector
                // Blue line is the bone's local foward vector
                // Yellow line is the target postion
                // Red line is the dynamic postion
                if (debugMode)
                {
                    UnityEngine.Debug.DrawRay(transform.position, forwardVector, Color.blue);
                    UnityEngine.Debug.DrawRay(transform.position, boneLookAxis, Color.green);
                    UnityEngine.Debug.DrawRay(targetPos, Vector3.up * 0.2f, Color.yellow);
                    UnityEngine.Debug.DrawRay(dynamicPos, Vector3.up * 0.2f, Color.red);
                }
                // ==================================================

            }
        }
    }
}