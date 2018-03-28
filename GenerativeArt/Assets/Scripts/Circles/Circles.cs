using UnityEngine;
using System.Collections;

public class Circles : MonoBehaviour {

    public GameObject sphere;
    public float maxRadius = 10f;
    public float circleCount = 3f;
    float sphereCount = 180;

	// Use this for initialization
	void Start () {
        sphereCount = maxRadius * 27;
        Application.runInBackground = true;
        sphere = GameObject.Find("BlackSphere");
    }
	
    void DrawCircle(float range) {
        Vector3 center = transform.position;
        float angle = 0;
        for (angle = 0; angle < 360; angle = angle + (360/sphereCount)) {
            Vector3 newPos = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad) * range,  Mathf.Cos(angle * Mathf.Deg2Rad) * range, center.z);
            Instantiate(sphere, newPos , Quaternion.identity);
        }
    }

    void DrawCircles(float numCircles, float maxRadius) {
        float radius = maxRadius;
        float radiusStep = radius / numCircles;
        for (int i = 0; i < numCircles; i++) {
            DrawCircle(radius);
            radius -= radiusStep;
        }
    }



    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.A)) {
            DrawCircles(circleCount, maxRadius);
        }
	
	}
}
