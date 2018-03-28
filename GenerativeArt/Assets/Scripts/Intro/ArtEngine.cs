using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class ArtEngine : MonoBehaviour {

    

    int size = 50;
    public GameObject sphere;

    int stepMax = 15;
    int noiseStepMax = 20;
    int lastX = 4;
    int lastY = 13;
    float lastStep = 0.015f;

    bool first = true;

    IEnumerator churn() {
        for(float noiseStepIndex = first ? lastStep : 0.005f; noiseStepIndex <= 0.005f * noiseStepMax; noiseStepIndex += 0.005f) {
            for (int xstartIndex = first ? lastX : 0; xstartIndex <= stepMax; xstartIndex++) {
                for (int ynoiseIndex = first ? lastY : 0; ynoiseIndex <= stepMax; ynoiseIndex++) {
                    first = false;
                    System.IO.File.WriteAllText("LastValues.txt", string.Format("Last X: {0}{3}Last Y: {1}{3}Last Step: {2}", xstartIndex, ynoiseIndex, noiseStepIndex, Environment.NewLine));
                    StartCoroutine(runAgain(xstartIndex, ynoiseIndex, noiseStepIndex));
                    yield return null;
                }
            }
        }

    }

    

    IEnumerator runAgain(int xStartValue, int yNoiseValue, float noiseStepIndex) {

        float xstart = xStartValue;
        float ynoise = yNoiseValue;
        float noiseStep = noiseStepIndex;

        for (int x = -size; x < size; x++) {
            ynoise += noiseStep;
            float xnoise = xstart;
            for (int y = -size; y < size; y++) {
                xnoise += noiseStep;
                drawPoint(x, y, Mathf.PerlinNoise(xnoise, ynoise));
            }
        }

        string name = "Images/" + noiseStepIndex.ToString("F3") + "-" + xStartValue.ToString() + "-" + yNoiseValue.ToString() + ".png";
        ScreenCapture.CaptureScreenshot(name);
        print(name);

        yield return null;
        GameObject[] spheres = GameObject.FindGameObjectsWithTag("sphere");
        for (var i = 0; i < spheres.Length; i++)
           Destroy(spheres[i]);

    }

    void drawPoint(float x, float y, float noiseFactor) {
        GameObject thisSphere = Instantiate(sphere, new Vector3(x * noiseFactor * 4, y * noiseFactor * 4, -y), Quaternion.identity) as GameObject;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.R)) {
            StartCoroutine(churn());
        }
    }
}
