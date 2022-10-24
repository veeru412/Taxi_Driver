using UnityEngine;

namespace VB.Input
{
    public class AccBreakController : MonoBehaviour
    {
        [SerializeField]
        CarCntrlProperties inPut;
        [SerializeField]
        RectTransform acc, brake, left, right;

        Vector2 mousePos;

        private void Update()
        {
            inPut.brake = false;
            inPut.shift = false;
            if (UnityEngine.Input.GetMouseButton(0))
            {
                mousePos = UnityEngine.Input.mousePosition;

                if (RectTransfomrUtils.InteractWithUI(acc, mousePos))
                {
                    // acc
                    inPut.accel = Mathf.MoveTowards(inPut.accel, 1f, 0.2f);
                }
                inPut.brake = RectTransfomrUtils.InteractWithUI(brake, mousePos);

                if (RectTransfomrUtils.InteractWithUI(left, mousePos))
                {
                    //left steer
                    inPut.steer = Mathf.MoveTowards(inPut.steer, -1f, 0.2f);
                }
                else if (RectTransfomrUtils.InteractWithUI(right, mousePos))
                {
                    //left steer
                    inPut.steer = Mathf.MoveTowards(inPut.steer, 1.0f, 0.2f);
                }
                else
                {
                    //stright drive
                    inPut.steer = Mathf.MoveTowards(inPut.steer, 0.0f, 0.2f);
                }
            }
            else
            {
                // acc down
                inPut.accel = Mathf.MoveTowards(inPut.accel, 0.0f, 0.8f);
                //stright drive
                inPut.steer = Mathf.MoveTowards(inPut.steer, 0.0f, 0.2f);
            }
        }
    }
}
