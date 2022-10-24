using UnityEngine;

namespace VB.Input
{
    public class KeyBoard : MonoBehaviour
    {
        [SerializeField]
        CarCntrlProperties inPut;
        void Update()
        {
            inPut.accel = 0;
            inPut.brake = false;
            inPut.shift = false;

            inPut.steer = Mathf.MoveTowards(inPut.steer, UnityEngine.Input.GetAxis("Horizontal"), 0.2f);
            inPut.accel = UnityEngine.Input.GetAxis("Vertical");
            inPut.brake = UnityEngine.Input.GetButton("Jump");
            inPut.shift = UnityEngine.Input.GetKey(KeyCode.LeftShift) | UnityEngine.Input.GetKey(KeyCode.RightShift);
        }
    }
}
