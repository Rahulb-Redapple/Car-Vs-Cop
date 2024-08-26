using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RacerVsCops
{
    [RequireComponent(typeof(UnityEngine.InputSystem.PlayerInput))]
    public class InputHandler : MonoBehaviour
    {
        private Vector2 _move = Vector2.zero;
        private Touch _touch;

        internal Vector2 Move => _move;
        internal Touch Touch => _touch;

        internal void OnMove(InputValue inputValue)
        {
            MoveInput(inputValue.Get<Vector2>());
        }

        internal void OnTouch(InputValue inputValue)
        {
            TouchInput(inputValue.Get<Touch>());
        }

        private void MoveInput(Vector2 newMoveDirection)
        {
            _move = newMoveDirection;
        }

        private void TouchInput(Touch touch)
        {
            _touch = touch;
        }
    }
}
