using UnityEngine;

namespace DefaultNamespace
{
    public abstract class BaseToggleComponent : MonoBehaviour
    {
        public abstract void Toggle(bool state);
    }
}