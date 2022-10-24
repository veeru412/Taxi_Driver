using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VB.Input
{
	public class SteeringWheel : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
	{

		[SerializeField]
		CarCntrlProperties inPut;
		//use for find the image center point
		RectTransform rect;
		Vector2 centerPoint;
		//ends here

		//to find whether the wheel is held or not
		bool wheelBeingHeld = false;
		private float wheelPrevAngle = 0f;
		private float wheelAngle = 0f;
		//ends here

		float maximumSteeringAngle = 200f;
		float wheelReleasedSpeed = 250f;
		private void Start()
		{
			rect = GetComponent<RectTransform>();
			getcenterPoint();

		}

		float GetAngle()
		{
			return wheelAngle / maximumSteeringAngle;
		}
		private void Update()
		{

			if (!wheelBeingHeld && !Mathf.Approximately(0f, wheelAngle))
			{
				float deltaAngle = wheelReleasedSpeed * Time.deltaTime;
				if (Mathf.Abs(deltaAngle) > Mathf.Abs(wheelAngle))
					wheelAngle = 0f;
				else if (wheelAngle > 0f)
					wheelAngle -= deltaAngle;
				else
					wheelAngle += deltaAngle;
			}

			// Rotate the wheel image
			rect.localEulerAngles = new Vector3(0, 0, -1) * wheelAngle;

			inPut.steer = GetAngle();
		}



		// to calculate the center of the image
		private void getcenterPoint()
		{    //to get the position of the corners of the image in the world
			Vector3[] corners = new Vector3[4];
			rect.GetWorldCorners(corners);
			// end here

			for (int i = 0; i < 4; i++)
			{
				corners[i] = RectTransformUtility.WorldToScreenPoint(null, corners[i]);
			}

			Vector3 bottomLeft = corners[0];
			Vector3 topRight = corners[2];
			float width = topRight.x - bottomLeft.x;
			float height = topRight.y - bottomLeft.y;

			Rect _rect = new Rect(bottomLeft.x, topRight.y, width, height);
			centerPoint = new Vector2(_rect.x + _rect.width * 0.5f, _rect.y - _rect.height * 0.5f);
		}
		//end here and we get overselves the  centerpoint of the rect image which is stored in the centerPoint



		//for the events
		//IpointerDownHandler , IDragHandler ,   IPointerUpHandler

		public void OnPointerDown(PointerEventData eventData)
		{
			Vector2 pointerPos = ((PointerEventData)eventData).position;
			wheelBeingHeld = true;
			wheelPrevAngle = Vector2.Angle(Vector2.up, pointerPos - centerPoint);

		}
		public void OnDrag(PointerEventData eventData)
		{
			Vector2 pointerPos = ((PointerEventData)eventData).position;

			float wheelNewAngle = Vector2.Angle(Vector2.up, pointerPos - centerPoint);



			// Do nothing if the pointer is too close to the center of the wheel
			if (Vector2.Distance(pointerPos, centerPoint) > 20f)
			{
				if (pointerPos.x > centerPoint.x)
					wheelAngle += wheelNewAngle - wheelPrevAngle;
				else
					wheelAngle -= wheelNewAngle - wheelPrevAngle;
			}
			// Make sure wheel angle never exceeds maximumSteeringAngle
			wheelAngle = Mathf.Clamp(wheelAngle, -maximumSteeringAngle, maximumSteeringAngle);
			wheelPrevAngle = wheelNewAngle;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			OnDrag(eventData);
			wheelBeingHeld = false;
		}
	}
}
