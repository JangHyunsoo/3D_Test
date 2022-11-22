using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent navmesh_agent_;

    private Vector3 target_pos_;

    // Start is called before the first frame update
    void Start()
    {
        navmesh_agent_ = GetComponent<NavMeshAgent>();
        navmesh_agent_.acceleration = 1000f;
        navmesh_agent_.speed = 4;
        navmesh_agent_.angularSpeed = 100000f;
        target_pos_ = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mouse_pos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mouse_pos);
            if(Physics.Raycast(ray, out RaycastHit raycastHit, 100f))
            {
                target_pos_ = raycastHit.point;
            }
        }
        navmesh_agent_.SetDestination(target_pos_);
    }
}
