using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour {
    /*public int resWidth = 2550;
    public int resHeight = 3300;*/

    private bool takeScreenshot = false;
    private Camera cam;
    private bool noDuplicates = false;
    private int tmpLapCount;
    private GameObject car;
    private LapCounter lc;
    public static string ScreenShotName(int width, int height)
    {
        return string.Format("{0}/Screenshots/screen_{1}x{2}_{3}.jpg",
                             Application.dataPath,
                             width, height,
                             System.DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss"));
    }

    public void TakeScreenshot()
    {
        takeScreenshot = true;
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
        //Destroy(cam.targetTexture);
        byte[] bytes = screenShot.EncodeToJPG();
        string filename = ScreenShotName(cam.pixelWidth, cam.pixelHeight);
        System.IO.File.WriteAllBytes(filename, bytes);
        noDuplicates = true;
        takeScreenshot = false;
    }

	// Use this for initialization
	void Start () {
        car = GameObject.Find("Car");
        lc = car.GetComponent<LapCounter>();
        tmpLapCount = lc.laps;
        cam = GameObject.FindGameObjectWithTag("CameraFollow").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        takeScreenshot |= Input.GetKeyDown("p");
        if (takeScreenshot)
        {
            StartCoroutine(CaptureScreenshot());
        }
	}
}
