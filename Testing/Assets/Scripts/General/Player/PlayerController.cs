﻿namespace AI.Master
{
    using UnityEngine;
    using System.Collections;

    public class PlayerController : MonoBehaviour
    {

        public float speed = 5.0f;
        private float currentSpeed = 5.0f;
        public float gravity = 20.0f;
        public float maxVelocityChange = 10.0f;
        private float fastSpeed;

        private Vector3 moveDirection = Vector3.zero;
        public Rigidbody rigC;
        public GameObject bullet;
        public Transform point;
        private float shootTime = 0;

        void Start()
        {
            rigC = GetComponent<Rigidbody>();
            rigC.useGravity = false;
            fastSpeed = speed + speed;
        }

        void FixedUpdate()
        {

            bool isShift = false;

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                isShift = true;
            } else
            {
                isShift = false;
            }

            if(isShift == true)
            {
                currentSpeed = fastSpeed;
            } else if(isShift == false)
            {
                currentSpeed = speed;
            }

            if (rigC != null)
            {
                Plane playerPlane = new Plane(Vector3.up, transform.position);

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                float hitdist = 0.0f;

                if(playerPlane.Raycast (ray, out hitdist))
                {
                    Vector3 targetPoint = ray.GetPoint(hitdist);

                    Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, currentSpeed * Time.deltaTime);
                }

                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= currentSpeed;

                Vector3 velocity = rigC.velocity;
                Vector3 velocityChange = (moveDirection - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = 0;
                rigC.AddForce(velocityChange, ForceMode.VelocityChange);

            }

            rigC.AddForce(new Vector3(0, -gravity * rigC.mass, 0));

            if(Input.GetKey(KeyCode.Q))
            {
                GameObject.FindGameObjectWithTag("companion").GetComponent<CompanionAISteering>().ReturnToPlayer = true;
            }


            shootTime += Time.deltaTime;
            if(shootTime >= 100)
            {
                shootTime = 0;
            }

            if(Input.GetKey(KeyCode.Space))
            {
                GameObject tempBullet;
                if(shootTime >= 0.2f)
                {
                    tempBullet = Instantiate(bullet, point.transform.position, point.transform.rotation) as GameObject;
                    shootTime = 0;
                }
            }

        }


    }

}
