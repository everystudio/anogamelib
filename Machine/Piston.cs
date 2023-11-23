using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame
{
    [RequireComponent(typeof(Rigidbody))]
    public class Piston : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;
        [SerializeField] private float swing = 1f;
        [SerializeField] private Vector3 direction = Vector3.forward;
        [SerializeField] private bool isMoveOnAwake = false;
        [SerializeField] private bool isReverse = false;

        [SerializeField] private bool isGlobal = false;

        private Rigidbody rb;
        private Vector3 startPosition;
        private float time;

        private bool isMoving = false;
        public bool IsMoving => isMoving;
        public void Stop()
        {
            isMoving = false;
        }
        public void Restart()
        {
            isMoving = true;
        }
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            startPosition = transform.position;
            time = 0f;
        }

        private void Start()
        {
            if (isMoveOnAwake)
            {
                Restart();
            }
        }

        private void FixedUpdate()
        {
            if (!isMoving)
            {
                return;
            }
            time += Time.fixedDeltaTime * speed;
            // 移動させる場合、物理的な挙動を考慮する必要がある
            // そのため、Rigidbodyを使って移動させる
            Vector3 useDirection = isReverse ? -direction : direction;

            if (!isGlobal)
            {
                // ローカル座標系で移動を行う
                useDirection = transform.TransformDirection(useDirection);
            }

            rb.MovePosition(startPosition + useDirection * Mathf.Sin(time) * swing);
        }
    }
}