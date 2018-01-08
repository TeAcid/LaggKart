using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AutoScreenshot : MonoBehaviour {
    /*public int resWidth = 1920;
    public int resHeight = 1280;*/

    private bool noDuplicates = false;
    private int tmpLapCount;
    private Camera cam;
    private GameObject car;
    private LapCounter lc;
    public static string ScreenShotName(int width, int height)
    {
        return string.Format("{0}/Screenshots/screen_{1}x{2}_{3}.jpg",
                             Application.dataPath,
                             width, height,
                             System.DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss"));
    }

	// Use this for initialization
	void Start () {
        // clear dir
        DirectoryInfo di = new DirectoryInfo("Assets/Screenshots/");
        foreach (FileInfo file in di.GetFiles())
        {
            file.Delete();
        }

        car = GameObject.Find("Car");
        lc = car.GetComponent<LapCounter>();
        tmpLapCount = 0;
        cam = GameObject.FindGameObjectWithTag("CameraFollow").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator CaptureScreenshot()
    {
        yield return new WaitForEndOfFrame();

        Texture2D screenShot = new Texture2D(cam.pixelWidth, cam.pixelHeight, TextureFormat.RGB24, false);
        cam.Render();
        RenderTexture.active = cam.targetTexture;
        screenShot.ReadPixels(new Rect(0, 0, cam.pixelWidth, cam.pixelHeight), 0, 0);
        //cam.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
       // Destroy(cam.targetTexture);
        byte[] bytes = screenShot.EncodeToJPG();
        string filename = ScreenShotName(cam.pixelWidth, cam.pixelHeight);
        System.IO.File.WriteAllBytes(filename, bytes);
        noDuplicates = true;
    }

    void OnTriggerExit(Collider other)
    {
        if(tmpLapCount < lc.laps)
        {
            noDuplicates = false;
            tmpLapCount = lc.laps;
        }

        if (!noDuplicates)
        {
            /*RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            cam.targetTexture = rt;*/
            StartCoroutine(CaptureScreenshot());

            /*RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            cam.targetTexture = rt;
            Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            cam.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            cam.targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors
            Destroy(rt);
            byte[] bytes = screenShot.EncodeToJPG();
            string filename = ScreenShotName(resWidth, resHeight);
            System.IO.File.WriteAllBytes(filename, bytes);
            noDuplicates = true;*/
        }
    }
}
