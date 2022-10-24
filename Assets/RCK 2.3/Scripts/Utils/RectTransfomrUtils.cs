using UnityEngine;

public static class RectTransfomrUtils
{
    public static bool InteractWithUI(RectTransform rect, Vector2 mousePos)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(rect, mousePos))
            return true;
        else
            return false;
    }
}
