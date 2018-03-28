using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class firstExamplev3 : MonoBehaviour {

    int size = 50;
    public GameObject sphere;

    public float noiseStep = 0.02f;

    [Header("Colors")]
    public bool red =   false;
    public bool blue =  false;
    public bool green = false;

    public float emissionStep = 2f;
    public float colorSpeed = 1f;

    Shader shader;
    GameObject spheres;

    // Use this for initialization
    void Start() {
        shader = Shader.Find("Standard");
        spheres = GameObject.Find("Spheres");
    }

    void runAgain() {
        StartCoroutine(create());
    }

    IEnumerator create() {

        float xstart = Random.Range(0, 10);
        float ynoise = Random.Range(0, 10);


        for (int x = -size; x < size; x++) {
            ynoise += noiseStep;
            float xnoise = xstart;
            for (int y = -size; y < size; y++) {
                xnoise += noiseStep;
                drawPoint(x, y, Mathf.PerlinNoise(xnoise, ynoise), (float)(100 + x + y + 1));
            }
            yield return null;
        }
    }

    void drawPoint(float x, float y, float noiseFactor, float color) {
        GameObject thisSphere = Instantiate(sphere, new Vector3(x * noiseFactor * 4, y * noiseFactor * 4, -y), Quaternion.identity) as GameObject;
        //GameObject thisSphere = Instantiate(sphere, new Vector3(x + noiseFactor * 50, y + noiseFactor * 50, 0), Quaternion.identity) as GameObject;
        thisSphere.transform.parent = spheres.transform;

        Renderer rend = thisSphere.GetComponent<Renderer>();

        color = color * colorSpeed;
        
        float r = Mathf.Clamp01(red   ? color / 255 : 0);
        float g = Mathf.Clamp01(green ? color / 255 : 0);
        float b = Mathf.Clamp01(blue  ? color / 255 : 0);

        rend.material.color = new Color(r, g, b, 1);
        rend.material.SetColor("_EmissionColor", rend.material.color * emissionStep);
    }


    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            runAgain();
        }
    }
}
