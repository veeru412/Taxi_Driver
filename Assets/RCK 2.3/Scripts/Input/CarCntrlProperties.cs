using UnityEngine;
namespace VB.Input
{
    [CreateAssetMenu()]
    public class CarCntrlProperties : ScriptableObject
    {
        public float steer = 0;
        public float accel = 0.0f;
        [HideInInspector]
        public bool brake;
        [HideInInspector]
        public bool shifmotor;
        [HideInInspector]
        public bool shift;
    }
}
