using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    private float radius = 0.0f;
    private float height = 0.0f;
    private Vector2 screenCenter;
    public Circle drawnCircle;
    public float sensitivity = 0.3f;

    void Start()
    {
        screenCenter = new Vector2((Screen.width / 2.0f / Screen.width) * 16.0f, (Screen.height / 2.0f / Screen.height) * 12.0f);
        height = GetComponent<BoxCollider2D>().size.y;
        float screenHeight = Screen.height / 2.0f / Screen.height * 12.0f;
        radius = screenHeight - height;
        transform.position = screenCenter + new Vector2(Mathf.Cos(Mathf.Deg2Rad * -90.0f) * radius, Mathf.Sin(-90.0f * Mathf.Deg2Rad) * radius);
    }

    void addAngle(float angle)
    {
        Vector2 positionFromCenter = new Vector2(transform.position.x, transform.position.y) - screenCenter;
        float currentAngle = Vector2.SignedAngle(Vector2.right, positionFromCenter);
        float newAngle = currentAngle + angle;
        Vector2 vec = screenCenter + new Vector2(Mathf.Cos(newAngle * Mathf.Deg2Rad) * radius, Mathf.Sin(newAngle * Mathf.Deg2Rad) * radius);
        transform.position = new Vector3(vec.x, vec.y, 0.0f);
        transform.eulerAngles = new Vector3(0, 0, newAngle + 90.0f);
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Mouse X") * sensitivity;
        float deltaY = Input.GetAxis("Mouse Y") * sensitivity;
        Vector2 pullVector = new Vector2(deltaX, deltaY);
        Vector2 positionFromCenter = new Vector2(transform.position.x, transform.position.y) - screenCenter;
        Vector2 result = positionFromCenter + pullVector;
        float coefficient = (1 / (result.magnitude / radius));
        Vector2 mresultVector = result * coefficient;
        transform.position = screenCenter + mresultVector;

        transform.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(new Vector2(1, 0), mresultVector) + 90.0f);
        float angle = Vector2.SignedAngle(new Vector2(-1, 0), mresultVector);
        float convertedAngle = (angle + 180.0f) / 360.0f;
        drawnCircle.ProcessPaddle(convertedAngle);
    }
}
