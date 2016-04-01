namespace GameLibrary
{
    using UnityEngine;
    using System.Collections;

    [RequireComponent(typeof(Camera))]

    public class PlayerCam : MonoBehaviour
    {

        public float mouseMoveMargin;
        public float moveSpeed;
        public float scrollSpeed;

        public GameObject marker;
        public Vector2 constrictY = new Vector2(10f, 60f);

        private Vector3 targetPos;
        //private int nextMarkerNumber = 0;
        bool spawned = false;
        public GameObject markerClone;

        private void FixedUpdate()
        {
            KeyMovement();
            MoveOnEdge();
            ScrollZoom();
            PlaceMarker();
        }

        private void ScrollZoom()
        {
            var scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll == 0f)
            {
                return;
            }

            var y = Mathf.Clamp(this.transform.position.y + (-Mathf.Sign(scroll) * scrollSpeed), this.constrictY.x, this.constrictY.y);
            this.transform.position = new Vector3(this.transform.position.x, y, this.transform.position.z);

        }

        private void KeyMovement()
        {
            bool isShift = false;

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                isShift = true;
            }

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                if (isShift)
                    MoveCam(Vector3.forward * 3);
                MoveCam(Vector3.forward);
            }

            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                if (isShift)
                    MoveCam(Vector3.back * 3);
                MoveCam(Vector3.back);
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                if (isShift)
                    MoveCam(Vector3.left * 3);
                MoveCam(Vector3.left);
            }

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                if (isShift)
                    MoveCam(Vector3.right * 3);
                MoveCam(Vector3.right);
            }

        }

        private void PlaceMarker()
        {
            bool pressed = false;

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                var playerPlane = new Plane(Vector3.up, new Vector3(0, 0, 0));
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float hitdist = 0.0f;

                if (playerPlane.Raycast(ray, out hitdist))
                {
                    targetPos = ray.GetPoint(hitdist);

                }

                if (spawned == true)
                {
                    GameObject.FindGameObjectWithTag("marker").GetComponent<MarkerTest>().TurnSelfInvisible(false);
                }

                if (spawned == false)
                {
                    pressed = true;
                }


            }

            Vector3 location;
            location = new Vector3(targetPos.x, 0.5f, targetPos.z);

            if (pressed == true && spawned == false)
            {
                markerClone = Instantiate(marker, location, Quaternion.identity) as GameObject;

                spawned = true;
                pressed = false;
            }
            else if (spawned == true)
            {
                markerClone.transform.position = location;
            }

        }

        private void MoveOnEdge()
        {

            var mousePos = Input.mousePosition;

            if ((mousePos.x < 0f || mousePos.x > Screen.width) || (mousePos.y < 0f || mousePos.y > Screen.width))
            {
                return;

            }

            if (mousePos.x < this.mouseMoveMargin)
            {
                MoveCam(Vector3.left);

            }
            else if (mousePos.x > Screen.width - this.mouseMoveMargin)
            {
                MoveCam(Vector3.right);
            }

            if (mousePos.y < this.mouseMoveMargin)
            {
                MoveCam(Vector3.back);
            }
            else if (mousePos.y > Screen.height - this.mouseMoveMargin)
            {
                MoveCam(Vector3.forward);
            }

        }


        private void MoveCam(Vector3 dir)
        {
            this.transform.position += dir.normalized * this.moveSpeed * Time.fixedDeltaTime;
        }

    }
}