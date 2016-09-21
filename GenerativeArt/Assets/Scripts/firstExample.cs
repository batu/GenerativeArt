using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class firstExample : MonoBehaviour {

    int counter = 0;
    GameObject sphere;
    // Use this for initialization
    void Start() {

        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    }
	
    void runAgain() {



        float xstart = Random.Range(0, 10);
        float ynoise = Random.Range(0, 10);

        for (int x = 0; x < 100; x++) {
            ynoise += 0.02f;
            float xnoise = xstart;
            for (int y = 0; y < 100; y++) {
                xnoise += 0.02f;
                counter++;
                drawPoint(x, y, Mathf.PerlinNoise(xnoise, ynoise));
 
            }
        }

    }
  
    void drawPoint(float x, float y, float noiseFactor) {
        GameObject thisSphere = Instantiate(sphere, new Vector3(x * noiseFactor * 4, y * noiseFactor * 4, -y), Quaternion.identity) as GameObject;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.R)) {
            runAgain();
        }

        if (Input.GetKey(KeyCode.T)) {
            SceneManager.LoadScene("Intro");
        }
    }       
}
