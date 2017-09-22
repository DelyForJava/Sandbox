using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bean.Hall
{
    public class Mum : MonoBehaviour
    {
        private float speed_ = -1f;
        private Vector3 currentRotation;
        // Use this for initialization
        void Start()
        {
            currentRotation = Vector3.zero;

        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(transform.forward, speed_, Space.Self);
        }

    }
}