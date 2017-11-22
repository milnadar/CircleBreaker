using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Circle : MonoBehaviour {

    public int vertexCount = 40;
    public float lineWidth = 0.2f;
    public float radius = 5.5f;

    private LineRenderer lineRenderer;
    private Vector3 screenCenter = Vector3.zero;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        screenCenter = new Vector3(Screen.width / 2.0f / Screen.width * 16.0f, Screen.height / 2.0f / Screen.height * 12.0f, 0.0f);
        DrawCircle();
    }

    void DrawCircle()
    {
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
