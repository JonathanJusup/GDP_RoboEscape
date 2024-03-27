using UnityEngine;
namespace DefaultNamespace
{ 
    public abstract class BaseToggleComponent : MonoBehaviour
    {
        /// <summary>
        /// Toggles a state as active or inactive.
        /// </summary>
        /// <param name="state"> State of a property </param>
        public abstract void Toggle(bool state);
    }
}