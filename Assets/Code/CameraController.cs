using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class CameraController : MonoBehaviour, ICameraFollow
    {
        [SerializeField]
        private float _distance;
        [SerializeField]
        private float _angle;
        [SerializeField]
        private Transform _target;
        private float _horizontalDistance;
        private float _verticalDistance;
        private float _angleInRad;

        private void Update()
        {
            //Convert degrees to radians
            _angleInRad = _angle * Mathf.Deg2Rad;

            //Calculate correct x and z values for the camera
            _horizontalDistance = Mathf.Sin(_angleInRad) * _distance;
            _verticalDistance = Mathf.Cos(_angleInRad) * _distance;

            //Move camera to the right position
            transform.position = new Vector3(_target.transform.position.x, _target.transform.position.y + _verticalDistance, _target.transform.position.z - _horizontalDistance);

            //Rotate camera to the player
            Vector3 relativePos = _target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = rotation;
        }

        public void SetAngle(float angle)
        {
            _angle = angle;
            _angleInRad = _angle * Mathf.Rad2Deg;
        }

        public void SetDistance(float distance)
        {
            _distance = distance;
        }

        public void SetTarget(Transform targetTransform)
        {
            _target = targetTransform;
        }
    }
}