using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Witch.Actor;

namespace Witch
{
    public enum PlayerState
    {
        Idle, Walk, Run, Attack,
    }

    [RequireComponent(typeof(Rigidbody))]
    public class ComPlayerController : MonoBehaviour
    {
        private Rigidbody m_Rigidbody;
        private ComPlayer m_Player;

        [Header("Movement")]
        [SerializeField] private float m_MoveSpeed = 10.0f;
        [SerializeField] private Vector3 m_Direction;
        [SerializeField] private Vector3 m_Movement;

        [Header("Rotation")]
        [SerializeField] private Vector2 m_Look;
        [SerializeField] private float yRotation;

        [Header("Animation")]
        [SerializeField] private PlayerState m_PlayerState;
        [SerializeField] private bool m_IsIdle;
        [SerializeField] private string m_CurrentAnimationName;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Player = GetComponent<ComPlayer>();

            if (m_Rigidbody == null) throw new System.Exception();
        }

        private void Start()
        {
            ComPlayerCamera.Root.SetTarget(this.transform);
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            m_Movement = transform.forward * m_Direction.z + transform.right * m_Direction.x;
            m_Movement = Vector3.ClampMagnitude(m_Movement, 1.0f);
            m_Movement *= m_MoveSpeed * Time.deltaTime;

            m_Rigidbody.MovePosition(transform.position + m_Movement);
        }

        public void OnEventMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            m_Direction = new Vector3(input.x, 0, input.y);
            m_Player.IsMove = m_Direction != Vector3.zero;
        }

        public void OnEventRun(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                m_Player.ChangeState(Actor.PlayerState.Run);
                m_MoveSpeed *= 1.5f;
            }
            else
            {
                m_MoveSpeed = 10.0f;
            }
        }

        public void OnEventLook(InputAction.CallbackContext context)
        {
            m_Look = context.ReadValue<Vector2>();
            yRotation = m_Look.x * 10f * Time.deltaTime;
            transform.Rotate(Vector3.up * yRotation);
        }

        public void OnEventAttack(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                m_Player.ChangeState(Actor.PlayerState.Attack01);
            }
        }
    }
}