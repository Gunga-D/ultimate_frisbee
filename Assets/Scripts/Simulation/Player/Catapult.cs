using Simulation.Controls;
using UnityEngine;

namespace Simulation.Player
{
    [RequireComponent(typeof(DragControl))]
    public class Catapult : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _projectileTemplate;
        [SerializeField] private float _forcePerUnit = 300f;

        private DragControl _control;

        private Vector3 _referencePoint;
        private Vector3 _throwDirection;

        private void Awake()
        {
            _control = GetComponent<DragControl>();

            _control.OnDragStarted += Pull;
            _control.OnDragProcessed += AdjustPulling;
            _control.OnDragStopped += Throw;
        }

        private void Pull(Vector3 referencePoint)
        {
            _referencePoint = referencePoint;
        }

        private void AdjustPulling(Vector3 length)
        {

        }

        private void Throw(Vector3 endPoint)
        {
            _throwDirection = (_referencePoint - endPoint);

            var projectile = Instantiate(_projectileTemplate);
            projectile.AddForce((new Vector2(_throwDirection.x, _throwDirection.y)) * _forcePerUnit, ForceMode2D.Impulse);
        }
    }
}
