using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
    private int screenshotCount = 0;
    public GameObject sphere;

    void Start() {
        Application.runInBackground = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.T)) {
            GameObject[] spheres = GameObject.FindGameObjectsWithTag("sphere");
            for (var i = 0; i < spheres.Length; i++)
                Destroy(spheres[i]);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            sphere.transform.localScale = new Vector3(sphere.transform.localScale.x + 0.1f,
                                                      sphere.transform.localScale.y + 0.1f,
                                                      sphere.transform.localScale.z + 0.1f);
            print("Sphere Size: " + sphere.transform.localScale.z.ToString());
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            sphere.transform.localScale = new Vector3(sphere.transform.localScale.x - 0.1f,
                                                      sphere.transform.localScale.y - 0.1f,
                                                      sphere.transform.localScale.z - 0.1f);
            print("Sphere Size: " + sphere.transform.localScale.z.ToString());
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            string screenshotFilename;
            do {
                screenshotCount++;
                screenshotFilename = "ScreenShots/screenshot" + screenshotCount + ".png";
                print("Screen Shot taken: " + screenshotFilename);
            } while (System.IO.File.Exists(screenshotFilename));
            ScreenCapture.CaptureScreenshot(screenshotFilename);
        }
    }
}
