using UnityEngine;

namespace Simulation.Controls
{
    public abstract class Control : MonoBehaviour
    {
        private void Update()
        {
            ReactInput();
        }

        protected void ReactInput()
        {
#if UNITY_EDITOR
            ReactEditorInput();
#else
            ReactMobileInput();
#endif
        }

        protected abstract void ReactMobileInput();

        protected abstract void ReactEditorInput();
    }
}
