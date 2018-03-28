using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using LineUtil;


public class Processing : MonoBehaviour {

    Vector3 topLeft, topRight, botLeft, botRight;
    float width;
    float height;

    int PLANE_SCALE_CONSTANT = 10;


    void GetBorders() {

        topLeft = new Vector3((transform.position.x - transform.localScale.x * PLANE_SCALE_CONSTANT / 2),
                              (transform.position.y + transform.localScale.z * PLANE_SCALE_CONSTANT / 2),
                              transform.position.z);

        topRight = new Vector3((transform.position.x + transform.localScale.x * PLANE_SCALE_CONSTANT / 2),
                              (transform.position.y + transform.localScale.z * PLANE_SCALE_CONSTANT / 2),
                              transform.position.z);

        botLeft = new Vector3((transform.position.x - transform.localScale.x * PLANE_SCALE_CONSTANT / 2),
                              (transform.position.y - transform.localScale.z * PLANE_SCALE_CONSTANT / 2),
                              transform.position.z);

        botRight = new Vector3((transform.position.x + transform.localScale.x * PLANE_SCALE_CONSTANT / 2),
                              (transform.position.y - transform.localScale.z * PLANE_SCALE_CONSTANT / 2),
                              transform.position.z);
    }

    void line(float x1, float y1, float x2, float y2) {
        LineDrawer lineDrawer = new LineDrawer();
        Vector3 startPoint = new Vector3(topLeft.x + x1, topLeft.y - y1, transform.position.z);
        Vector3 endPoint = new Vector3(topLeft.x + x2, topLeft.y - y2, transform.position.z);
        lineDrawer.DrawLineInGameView(startPoint, endPoint, Color.white);
        Debug.DrawLine(startPoint, endPoint);
    }

    void line(float x1, float y1, float x2, float y2, Color color) {
        LineDrawer lineDrawer = new LineDrawer();
        Vector3 startPoint = new Vector3(topLeft.x + x1, topLeft.y - y1, transform.position.z);
        Vector3 endPoint = new Vector3(topLeft.x + x2, topLeft.y - y2, transform.position.z);
        lineDrawer.DrawLineInGameView(startPoint, endPoint, color);
        Debug.DrawLine(startPoint, endPoint, color);
    }

    void line(float x1, float y1, float x2, float y2, Color color, float thickness) {
        LineDrawer lineDrawer = new LineDrawer(thickness);
        Vector3 startPoint = new Vector3(topLeft.x + x1, topLeft.y - y1, transform.position.z);
        Vector3 endPoint = new Vector3(topLeft.x + x2, topLeft.y - y2, transform.position.z);
        lineDrawer.DrawLineInGameView(startPoint, endPoint, color);
        Debug.DrawLine(startPoint, endPoint, color);
    }


    // Use this for initialization


    void OnDrawGizmosSelected() {
        if (EditorApplication.isPlaying) {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(topLeft, 0.5f);
            Gizmos.DrawSphere(topRight, 0.5f);
            Gizmos.DrawSphere(botLeft, 0.5f);
            Gizmos.DrawSphere(botRight, 0.5f);
        }
    }

    void Setup() {
        GetBorders();
        width = transform.localScale.x * PLANE_SCALE_CONSTANT;
        height = transform.localScale.z * PLANE_SCALE_CONSTANT;
    }

    void Awake() {
        Setup();
    }


    float random(float number) {
        return Random.Range(0, number);
    }

    IEnumerator RandomLine(float lastx, float lasty) {

        float waitTime = 0.05f;
        int step = 10 + Random.Range(0, 10);
        float y = 50 + Random.Range(0, 30);
        int borderx = 2;
        int bordery = 2;
        for (float x = borderx; x <= width - borderx; x += step) {
            y = bordery + Random.Range(0, height - 2 * bordery);
            if (lastx > -999) {
                line(x, y, lastx, lasty, Random.ColorHSV(0f,1f,1f,1f,1f,1f));
                yield return new WaitForSeconds(waitTime);
            }
            lastx = x;
            lasty = y;
        }

        for (float x = lastx; x >= borderx; x -= step) {
            y = bordery + Random.Range(0, height - 2 * bordery);
            if (lastx > -999) {
                line(x, y, lastx, lasty, Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f));
                yield return new WaitForSeconds(waitTime);
            }
            lastx = x;
            lasty = y;
        }
        StartCoroutine(RandomLine(lastx, lasty));
    }

    IEnumerator SwiglyLine(float lastx, float lasty) {

        float waitTime = 0.05f;
        float xstep = 10;
        float ystep = 10;
        lastx = 20;
        lasty = 50;
        float y = 50;
        for (float x = 20; x <= 480; x += xstep) {
            ystep = random(20) - 10; //range -10 to 10
            y += ystep;
            line(x, y, lastx, lasty);
            lastx = x;
            lasty = y;
            yield return new WaitForSeconds(waitTime);
        }

        StartCoroutine(SwiglyLine(lastx, lasty));
    }


    IEnumerator Run(float waitTime) {
        while (true) {
            yield return new WaitForSeconds(waitTime);
        }
    }

    void Start() {
        StartCoroutine(RandomLine(-999, -999));
    }

    // Update is called once per frame
    void Update () {

    }
}

