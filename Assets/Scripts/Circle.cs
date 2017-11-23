using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Circle : MonoBehaviour {

    public int vertexCount = 40;
    public float lineWidth = 1.0f;
    public float radius = 5.5f;
    public float eraserStrengthMultiplier = 0.1f;

    private LineRenderer lineRenderer;
    private Vector3 screenCenter = Vector3.zero;

    private float oldPaddlePosition = 0.0f;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        screenCenter = new Vector3(Screen.width / 2.0f / Screen.width * 16.0f, Screen.height / 2.0f / Screen.height * 12.0f, 0.0f);
        DrawCircle();
    }

    public void ProcessPaddle(float value) {
        if(oldPaddlePosition == value)
        return;
        oldPaddlePosition = value;
        int index = (int)Mathf.Round(value * (vertexCount));
        var oldKeyFrames = lineRenderer.widthCurve.keys;
        AnimationCurve curve = new AnimationCurve();
        for(int i = 0; i < oldKeyFrames.Length; ++i) {
            float oldValue = oldKeyFrames[i].value;
            if(i == index) {
                oldValue -= lineWidth * eraserStrengthMultiplier;
                if(oldValue < 0.0f)
                    oldValue = 0.0f;
            }
            curve.AddKey((float)i/oldKeyFrames.Length, oldValue);
        }
        lineRenderer.widthCurve = curve;
    }

    public void ResetCircle() {
        AnimationCurve curve = new AnimationCurve();
        for(int i = 0; i < vertexCount + 1; i++)
        {
            curve.AddKey((float)i/vertexCount, 1.0f * lineRenderer.widthMultiplier);
        }
        lineRenderer.widthCurve = curve;
    }

    private void DrawCircle()
    {
        ResetCircle();
        lineRenderer.widthMultiplier = lineWidth;
        float alpha = (2.0f * Mathf.PI) / vertexCount;
        float angle = 0.0f;

        lineRenderer.positionCount = vertexCount + 1;
        for(int i = 0; i < vertexCount + 1; ++i) {
            Vector3 newPosition = screenCenter + new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), 0.0f);
            lineRenderer.SetPosition(i, newPosition);
            angle += alpha;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        float alpha = (2.0f * Mathf.PI) / vertexCount;
        float angle = 0.0f;
        Vector3 oldPosition = screenCenter + new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), 0.0f);

        for(int i = 0; i < vertexCount + 1; ++i) {
            angle += alpha;
            Vector3 newPosition = screenCenter + new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), 0.0f);
            Gizmos.DrawLine(oldPosition, transform.position + newPosition);
            oldPosition = transform.position + newPosition;
        }
    }
#endif
}
