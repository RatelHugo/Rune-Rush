using UnityEngine;

public class DrawerOnPanel : MonoBehaviour
{
    public LineRenderer lineRendererPrefab; 
    public RectTransform drawingPanel;
    public ValidationRunePoints runeValidator;

    private GameObject lineObject;
    private LineRenderer currentLineRenderer; 
    private Camera uiCamera;
    private Vector3 lastMousePosition;

    void Start()
    {
        Canvas canvas = drawingPanel.GetComponentInParent<Canvas>();
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            uiCamera = canvas.worldCamera;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing();
        }

        if (Input.GetMouseButton(0) && currentLineRenderer != null)
        {
            Draw();
        }

        if (Input.GetMouseButtonUp(0) && currentLineRenderer != null)
        {
            FinishDrawing();
        }
    }

    private void StartDrawing()
    {
        Vector2 mousePosition = Input.mousePosition;
        if (!RectTransformUtility.RectangleContainsScreenPoint(drawingPanel, mousePosition, uiCamera))
        {
            return;
        }

        lineObject = Instantiate(lineRendererPrefab.gameObject, drawingPanel);
        lineObject.transform.localPosition = Vector3.zero;
        currentLineRenderer = lineObject.GetComponent<LineRenderer>();
        currentLineRenderer.positionCount = 0;

        AddPointToLine(mousePosition);
    }

    private void Draw()
    {
        Vector2 mousePosition = Input.mousePosition;

        if (!RectTransformUtility.RectangleContainsScreenPoint(drawingPanel, mousePosition, uiCamera))
        {
            return;
        }

        if (Vector3.Distance(mousePosition, lastMousePosition) > 10f)
        {
            AddPointToLine(mousePosition);
        }
    }

    public void FinishDrawing()
    {
        currentLineRenderer = null;
        runeValidator.ResetValidation();
        Destroy(lineObject);
    }

    private void AddPointToLine(Vector2 screenPosition)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(drawingPanel, screenPosition, uiCamera, out Vector2 localPoint);

        Vector3 localPosition = new Vector3(localPoint.x, localPoint.y, 0f);

        currentLineRenderer.positionCount++;
        currentLineRenderer.SetPosition(currentLineRenderer.positionCount - 1, localPosition);

        lastMousePosition = screenPosition;

        runeValidator.ValidatePoint(new Vector3(localPoint.x, localPoint.y, 0f));
    }
}
