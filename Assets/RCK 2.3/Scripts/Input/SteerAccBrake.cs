using UnityEngine;

namespace VB.Input
{
    public class SteerAccBrake : MonoBehaviour
    {
        [SerializeField]
        CarCntrlProperties inPut;
        [SerializeField]
        RectTransform acc, brake;

        Vector2 mousePos;
        private void Update()
        {
            if (UnityEngine.Input.GetMouseButton(0))
            {
                mousePos = UnityEngine.Input.mousePosition;

                if (RectTransfomrUtils.InteractWithUI(acc, mousePos))
                {
                    // acc
                    inPut.accel = Mathf.MoveTowards(inPut.accel, 1f, 0.2f);
                }
                else
                {
                    // acc
                    inPut.accel = Mathf.MoveTowards(inPut.accel, 0.0f, 0.2f);
                }
                inPut.brake = RectTransfomrUtils.InteractWithUI(brake, mousePos);
            }
            else
            {
                // acc
                inPut.accel = Mathf.MoveTowards(inPut.accel, 0.0f, 0.2f);
            }
        }
    }
}
