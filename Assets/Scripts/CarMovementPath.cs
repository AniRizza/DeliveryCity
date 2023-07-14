using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarMovementPath : MonoBehaviour
{
    private Transform target;
    private NavMeshPath path;
    private float elapsed = 0.0f;
    [SerializeField]
    private bool isTargetSet = false;

    void Start()
    {
        path = new NavMeshPath();
        elapsed = 0.0f;
    }

    void Update()
    {
        if (isTargetSet) {
            // Update the way to the goal every second.
            elapsed += Time.deltaTime;
            if (elapsed > 1.0f)
            {
                elapsed -= 1.0f;
                NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
            }
            //Debug.Log("path length " + path.corners.Length);
            for (int i = 0; i < path.corners.Length - 1; i++) 
                Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);

            for (int i = 1; i < path.corners.Length; i++) {
                Collider[] colliders = Physics.OverlapSphere(path.corners[i], 1f /* Radius */);
                //Debug.Log("colliders amount " + colliders.Length + " at point " + path.corners[i].x + " " + path.corners[i].z);
                foreach(var collider in colliders) {
                    if (collider.tag == "Tile"){
                        collider.gameObject.GetComponent<TileClickReaction>().AddTileToPath();
                    }
                }
            }
            if (path.corners.Length > 0) isTargetSet = false;
        }
    }

    public void SetTarget(Transform target) {
        isTargetSet = true;
        this.target = target;
    }
}
