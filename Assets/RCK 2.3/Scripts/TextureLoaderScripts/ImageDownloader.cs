
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageDownloader : MonoBehaviour
{
    Image _img;
    StringBuilder sb;

    [SerializeField]
    TMPro.TextMeshProUGUI debugTxt;

    void Awake()
    {
        _img = GetComponent<Image>();
        sb = new StringBuilder();
        UpdateText("Ready to load image: press 'Load Image'");
    }

    public void SetData(string fileName)
    {
        string path = Application.persistentDataPath+"/" +fileName;
        UpdateText("trying to load image from Local.. "+path);

        Sprite s = FileUtils.GetLocalFile(path);
        if(s != null)
        {
            UpdateText("Found a local copy..");
            _img.sprite = s;
        }
        else
        {
            string url = Path.Combine(Constants.baseUrl, fileName);
            UpdateText("Can't find local copy so, trying to download from server: " + url);
            StartCoroutine(LoadFromWeb(url,fileName));
        }
    }
  
    IEnumerator LoadFromWeb(string url,string nameToSave)
    {
        UnityWebRequest wr = new UnityWebRequest(url);
        DownloadHandlerTexture texDl = new DownloadHandlerTexture(true);
        wr.downloadHandler = texDl;
        yield return wr.SendWebRequest();
        if (wr.result == UnityWebRequest.Result.Success)
        {
            Texture2D t = texDl.texture;
            Sprite s = Sprite.Create(t, new Rect(0, 0, t.width, t.height),
                Vector2.zero, 1f);
            _img.sprite = s;
            FileUtils.WriteImageOnDisk(s, nameToSave,()=> {
                UpdateText("image downloaded and saved successfully! "+Path.Combine(Application.persistentDataPath, nameToSave));
            });
        }
    }
 
    void UpdateText(string text)
    {
        sb.Append(text);
        sb.Append("\n");
        debugTxt.text = sb.ToString();
    }
}