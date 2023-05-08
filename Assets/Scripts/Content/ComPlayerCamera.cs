using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Witch
{
    public class ComPlayerCamera : MonoBehaviour
    {
        private static ComPlayerCamera s_Root;

        public static ComPlayerCamera Root => s_Root;


        [SerializeField] private Transform m_Target;

        [Header("Camera Position Property")]
        [SerializeField] private float distance = 10f;
        [SerializeField] private float height = 5f;

        private Vector2 m_Look;
        private InputAction m_LookAction;


        private void Awake()
        {
            s_Root = this;
            m_LookAction = new InputAction("Look", binding: "<Mouse>/delta");
        }

        private void OnEnable()
        {
            m_LookAction.Enable();
        }

        private void FixedUpdate()
        {
            if (m_Target != null)
            {
                Movement();
                LookAt();
            }
        }

        private void OnDisable()
        {
            m_LookAction.Disable();
        }

        public void SetTarget(Transform target)
        {
            m_Target = target;
        }

        private void Movement()
        {
            Vector3 targetPosition = m_Target.position + Vector3.up * height - m_Target.forward * distance;
            transform.position = targetPosition;
        }

        private void LookAt()
        {
            transform.LookAt(m_Target);
        }
    }
}