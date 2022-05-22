
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

public class LoadAsset : MonoBehaviour
{
    public static LoadAsset LoadAssetScript;
    public string[] url, sceneNames;
    [SerializeField] GameObject loader;
    public static AssetBundle assetBundle;
    WWW www;
    string[] PathStr;
    [SerializeField] TextMeshProUGUI ToastText;
    private void Awake()
    {
        if (LoadAssetScript == null)
        {
            LoadAssetScript = this;
        }

    }
    private void Start()
    {
        PathStr = new string[] { Application.persistentDataPath + "/AssetBundle/customisation.unity3d", Application.persistentDataPath + "/AssetBundle/game.unity3d" };
     
    }
    public void playGamePressed(int i)
    {
        StartCoroutine(LoadSceneCor(i));
    }
    IEnumerator LoadSceneCor(int i)
    {
        loader.SetActive(true);

        if (!File.Exists(PathStr[i]))
        {
      
            Debug.Log("Bundle Url::" + url[i]);
            using (www = new WWW(url[i]))
            {
              //  loadingStart = true;
                yield return www;
                if (!string.IsNullOrEmpty(www.error))
                {
                    print(www.error);
                    yield break;

                }

                SaveBundles(www.bytes, PathStr[i]);

            }
        }
        StartCoroutine(ShowTextCor("Asset Loaded Successfully.."));
        loader.SetActive(false);

    }

    void SaveBundles(byte[] data, string path)
    {
        //Create the Directory if it does not exist
        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        try
        {
            File.WriteAllBytes(path, data);
            Debug.Log("Saved Data to: " + path.Replace("/", "\\"));
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed To Save Data to: " + path.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
        }
    }
    IEnumerator LoadFromMemoryAsync(string path)
    {
        AssetBundleCreateRequest createRequest = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(path));
        yield return createRequest;
        AssetBundle bundle = createRequest.assetBundle;
        string[] scenes = bundle.GetAllScenePaths();
        foreach (string s in scenes)
        {
            print(Path.GetFileNameWithoutExtension(s));
          
                loadScene(Path.GetFileNameWithoutExtension(s));
        }
        assetBundle=bundle;
    }
    public void ClearAll()
    {
        foreach (var item in PathStr)
        {
            if (File.Exists(item)){
                File.Delete(item);
                StartCoroutine(ShowTextCor("Assets Cleared..."));
            }
           
        }
    }
    public void PlayScene(int i)
    {
        if (File.Exists(PathStr[i]))
        {

            StartCoroutine(LoadFromMemoryAsync(PathStr[i]));
           
        }
  
    }
    IEnumerator ShowTextCor(String str)
    {
        ToastText.gameObject.SetActive(true);
        ToastText.text = str;
        yield return new WaitForSeconds(2.0f);
        ToastText.gameObject.SetActive(false);
    }
    public void loadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
