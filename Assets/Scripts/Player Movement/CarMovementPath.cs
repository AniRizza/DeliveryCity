using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarMovementPath : MonoBehaviour
{
    private Vector3 target;
    private NavMeshPath path;
    [SerializeField]
    private bool isTargetSet = false;
    private float lineHeight = 0.6f;
    private List<Vector3> tilesPath;
    [SerializeField]
    private float pathDotRemovingDistance = 0.6f;
    
    public LineRenderer lineRenderer;
    
    void Start()
    {
        path = new NavMeshPath();
        tilesPath = new List<Vector3>();
    }

    void FixedUpdate()
    {
        if (isTargetSet) {
            DrawPathLine();
            if (Vector3.Distance(transform.position, tilesPath[0]) < pathDotRemovingDistance) {
                tilesPath.RemoveAt(0);
                if (tilesPath.Count > 0) {
                    GetComponent<NavMeshAgent>().SetDestination(tilesPath[0]);
                }
                else isTargetSet = false;
            }
        }
    }

    public void SetTarget(Vector3 target) {
        isTargetSet = true;
        this.target = target;
        tilesPath.Clear();
        CalculateCarMovementPath();
        GetComponent<NavMeshAgent>().SetDestination(tilesPath[0]);
    }

    public void CalculateCarMovementPath() {
        NavMeshPath actualPath = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, actualPath);
        for (int i = 1; i < actualPath.corners.Length; i++) {
            Collider[] colliders = Physics.OverlapSphere(actualPath.corners[i], 1f /* Radius */);
            foreach(var collider in colliders) {
                if (collider.tag == "Tile"){
                    Vector3 tilePosition = collider.gameObject.transform.position;
                    tilesPath.Add(new Vector3(tilePosition.x, lineHeight, tilePosition.z));
                }
            }
        }
    }

    public void DrawPathLine() {
        List<Vector3> linePath = new List<Vector3>();
        linePath.Add(new Vector3(transform.position.x, lineHeight, transform.position.z));
        linePath.AddRange(tilesPath);
        lineRenderer.positionCount = linePath.Count;
        lineRenderer.SetPositions(linePath.ToArray());
    }
}
