using System;
using UnityEngine;

namespace Simulation.Controls
{
    [RequireComponent(typeof(LineRenderer))]
    public class DragControl : Control
    {
        private LineRenderer _visualEffect;

        public event Action<Vector3> OnDragStarted;
        public event Action<Vector3> OnDragProcessed;
        public event Action<Vector3> OnDragStopped;

        private void Awake()
        {
            _visualEffect = GetComponent<LineRenderer>();
        }

        protected override void ReactMobileInput()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);

                Vector2 touchPosition = touch.position;
                var touchPositionNearClipPlane = new Vector3(touchPosition.x, touchPosition.y, Camera.main.nearClipPlane);
                var dragPosition = Camera.main.ScreenToWorldPoint(touchPositionNearClipPlane);

                if (touch.phase == TouchPhase.Began)
                {
                    StartDrag(dragPosition);
                }
                if (touch.phase == TouchPhase.Moved)
                {
                    ProcessDrag(dragPosition);
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    StopDrag(dragPosition);
                }
            }
        }

        protected override void ReactEditorInput()
        {
            Vector2 mousePosition = Input.mousePosition;
            var mousePositionNearClipPlane = new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane);
            var dragPosition = Camera.main.ScreenToWorldPoint(mousePositionNearClipPlane);

            if (Input.GetMouseButtonDown(0))
            {
                StartDrag(dragPosition);
            }

            if (Input.GetMouseButton(0))
            {
                ProcessDrag(dragPosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                StopDrag(dragPosition);
            }
        }

        private void StartDrag(Vector3 dragPosition)
        {
            _visualEffect.positionCount = 1;
            _visualEffect.SetPosition(0, dragPosition);

            OnDragStarted?.Invoke(dragPosition);
        }

        private void ProcessDrag(Vector3 dragPosition)
        {
            _visualEffect.positionCount = 2;
            _visualEffect.SetPosition(1, dragPosition);

            OnDragProcessed?.Invoke(dragPosition);
        }

        private void StopDrag(Vector3 dragPosition)
        {
            _visualEffect.positionCount = 0;

            OnDragStopped?.Invoke(dragPosition);
        }
    }
}
