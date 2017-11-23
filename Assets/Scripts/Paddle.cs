using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    private float radius = 0.0f;
    private float height = 0.0f;
    private Vector2 screenCenter;
    public Circle drawnCircle;

    // Use this for initialization
    void Start()
    {
        screenCenter = new Vector2((Screen.width / 2.0f / Screen.width) * 16.0f, (Screen.height / 2.0f / Screen.height) * 12.0f);
        height = GetComponent<BoxCollider2D>().size.y;
        float screenHeight = Screen.height / 2.0f / Screen.height * 12.0f;
        radius = screenHeight - height;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x / Screen.width * 16.0f, Input.mousePosition.y / Screen.height * 12.0f);
        Vector2 resultVector = mousePosition - screenCenter;
        float coefficient = (1 / (resultVector.magnitude / radius));
        Vector2 mresultVector = resultVector * coefficient;
        Vector2 vv = screenCenter + mresultVector;
        Vector3 vector = new Vector3(vv.x, vv.y, 0);
        transform.position = vector;
        transform.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(new Vector2(1, 0), mresultVector) + 90.0f);
        float angle = Vector2.SignedAngle(new Vector2(-1, 0), mresultVector);
        float convertedAngle = (angle + 180.0f) / 360.0f;
        //print(angle + " -> " + convertedAngle);
        drawnCircle.ProcessPaddle(convertedAngle);
    }
}
