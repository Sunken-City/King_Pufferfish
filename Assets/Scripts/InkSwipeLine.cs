using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Linq;

public class InkSwipeLine : MonoBehaviour
{
	public int lineLength = 10;
    public GameObject bubbleParticle;

    static Camera sceneCamera;
    private Queue points;
    private LineRenderer lineRenderer;
    private float distanceTraveled = 0.0f;
    private Vector3 lastMousePosition;

	void Start () 
	{
		sceneCamera = GameObject.Find("Main Camera").GetComponent<Camera> ();
		lineRenderer = gameObject.GetComponent<LineRenderer>();
        points = new Queue();
        lastMousePosition = new Vector3(0.0f, 0.0f, 0.0f);
	}
	
	void Update () 
	{
		if (Input.GetKey(KeyCode.Mouse0) && !GameController.instance.isPaused) 
		{
            Vector3 currentMousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
            currentMousePosition.z = -5.75f;
            points.Enqueue(currentMousePosition);
            if (points.Count > lineLength)
            {
                points.Dequeue();
            }
            if (Random.Range(0, 5) == 1 && bubbleParticle && (currentMousePosition - lastMousePosition).sqrMagnitude > 0.05f)
            {
                GameObject bubble = Instantiate(bubbleParticle, currentMousePosition, Quaternion.identity) as GameObject;
                Destroy(bubble, 3.0f);
            }

            lineRenderer.SetVertexCount(points.Count);
            object[] pointArray = points.ToArray();
            for (int i = 0; i < points.Count; ++i)
            {
                lineRenderer.SetPosition(i, (Vector3)pointArray[i]);
            }
            lastMousePosition = currentMousePosition;
		}
        else
        {
            points.Clear();
            lineRenderer.SetVertexCount(0);
            distanceTraveled = 0.0f;
        }
	}
}
