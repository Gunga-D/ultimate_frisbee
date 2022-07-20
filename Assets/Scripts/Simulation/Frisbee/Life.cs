using UnityEngine;

namespace Simulation.Frisbee
{
    public class Life : MonoBehaviour
    {
        [SerializeField] float _livingTime = 0.4f;

        private void Start()
        {
            Destroy(gameObject, _livingTime);
        }
    }
}
