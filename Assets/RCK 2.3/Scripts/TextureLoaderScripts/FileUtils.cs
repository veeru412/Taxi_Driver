using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FileUtils
{
  public static void WriteImageOnDisk(Sprite sprite,string nameToSave,System.Action onCompleted)
  {
        byte[] bytes = sprite.texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.persistentDataPath+"/"+nameToSave, bytes);
        onCompleted?.Invoke();
  }
    public static Sprite GetLocalFile(string path)
    {
        if (string.IsNullOrEmpty(path)) return null;
        if (System.IO.File.Exists(path))
        {
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return sprite;
        }
        return null;
    }
}
