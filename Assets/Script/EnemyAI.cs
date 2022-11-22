using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent navmesh_agent_;

    [SerializeField]
    private Transform target_tr_;

    [SerializeField]
    private EnemyState enemy_state_;

    [SerializeField]
    private Transform[] patrol_tr_arr_;
    private int patroll_tr_index_ = 0;
    
    private enum EnemyState
    {
        DETECT,
        NOMAL
    }

    private void Update()
    {
        patton1();
    }

    private void patton1()
    {


        switch (enemy_state_)
        {
            case EnemyState.DETECT:
                if(Vector3.Distance(target_tr_.position, transform.position) > 5f)
                {
                    navmesh_agent_.SetDestination(target_tr_.position);
                }
                break;
            case EnemyState.NOMAL:
                Vector3 curr_target_pos = patrol_tr_arr_[patroll_tr_index_].position;
                navmesh_agent_.SetDestination(curr_target_pos);

                Vector2 target_vec2 = new Vector2(curr_target_pos.x, curr_target_pos.z);
                Vector2 curr_vec2 = new Vector2(transform.position.x, transform.position.z);

                if (Vector2.Distance(target_vec2, curr_vec2) <= Time.deltaTime)
                {
                    patroll_tr_index_ = (patroll_tr_index_ + 1 >= patrol_tr_arr_.Length) ? 0 : patroll_tr_index_ + 1;
                }
                break;
            default:
                break;
        }
    }
}
