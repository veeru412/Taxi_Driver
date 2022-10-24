using UnityEngine;

namespace VB.Input
{
    public class SimpleLeft_Right : MonoBehaviour
    {
        [SerializeField]
        CarCntrlProperties inPut;
        [SerializeField]
        RectTransform brakeImg;

        Vector2 mousePos;
        float halfScreenWidth;
      
        private void OnEnable()
        {
            halfScreenWidth = Screen.width * 0.5f;
        }
        private void Update()
        {
            inPut.brake = false;
            inPut.shift = false;

            inPut.accel = Mathf.MoveTowards(inPut.accel, 1f, 0.1f);

            if (UnityEngine.Input.GetMouseButton(0))
            {
                mousePos = UnityEngine.Input.mousePosition;

                if (RectTransfomrUtils.InteractWithUI(brakeImg, mousePos))
                {
                    inPut.brake = true;
                }
                else
                {
                    if (mousePos.x > halfScreenWidth)
                    {
                        inPut.steer = Mathf.MoveTowards(inPut.steer, 1f, 0.2f);
                        inPut.accel = Mathf.MoveTowards(inPut.accel, 0.2f, 0.2f);
                    }
                    else
                    {
                        inPut.steer = Mathf.MoveTowards(inPut.steer, -1, 0.2f);
                        inPut.accel = Mathf.MoveTowards(inPut.accel, 0.2f, 0.2f);
                    }
                }
               
            }else
                inPut.steer = Mathf.MoveTowards(inPut.steer, 0.0f, 0.2f);
        }
       
    }
}
