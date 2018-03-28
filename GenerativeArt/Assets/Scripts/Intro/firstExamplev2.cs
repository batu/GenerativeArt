using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class firstExamplev2 : MonoBehaviour {

    int size = 50;
    public GameObject sphere;
    GameObject spheres;
    public float noiseStep = 0.02f;

    void Start() {
        spheres = GameObject.Find("Spheres");
    }

    void runAgain() {

        float xstart = Random.Range(0, 10);
        float ynoise = Random.Range(0, 10);


        for (int x = -size; x < size; x++) {
            ynoise += noiseStep;
            float xnoise = xstart;
            for (int y = -size; y < size; y++) {
                xnoise += noiseStep;
                drawPoint(x, y, Mathf.PerlinNoise(xnoise, ynoise));

            }
        }

    }

    void drawPoint(float x, float y, float noiseFactor) {
        GameObject thisSphere = Instantiate(sphere, new Vector3(x * noiseFactor * 4, y * noiseFactor * 4, -y), Quaternion.identity) as GameObject;
        thisSphere.transform.parent = spheres.transform;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.R)) {
            runAgain();
        }
    }
}
