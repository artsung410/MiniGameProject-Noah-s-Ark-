using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovePosition : MonoBehaviour
{
    //public GameObject player;

    //private Camera mainCam;
    //private NavMeshAgent agent;


    //private bool _isMove;
    //private Vector3 _destination;

    //#region public 필드
    ////public float MoveSpeed;

    //#endregion


    //private void Awake()
    //{
    //    mainCam = Camera.main;
    //    agent = GetComponent<NavMeshAgent>();

        
    //}

    //private void Start()
    //{
    //    agent.updateRotation = false;
    //}

    //private void Update()
    //{
    //    //agent.speed = Speed * 4f;
    //    // 마우스클릭 위치를 캐릭터의 목적지로 설정
    //    if (Input.GetMouseButton(1))
    //    {
    //        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;

    //        if (Physics.Raycast(ray, out hit, 5000f))
    //        {
    //            Debug.DrawLine(mainCam.transform.position, hit.point, Color.blue, 3f);

    //            _isMove = true;
    //            _destination = hit.point;
    //            agent.SetDestination(_destination);
    //        }
    //    }
        
    //    if (IsDead == false)
    //    {
    //        Move();
    //    }
        

    //}

    //private void Move()
    //{
    //    if (_isMove)
    //    {
    //        if (agent.velocity.magnitude == 0f)
    //        {
    //            _isMove = false;
    //            return;
    //        }

            

    //        Vector3 direction = new Vector3(agent.steeringTarget.x, transform.position.y, agent.steeringTarget.z) - transform.position;
    //        Quaternion targetRot = Quaternion.LookRotation(direction);

    //        player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, targetRot, Time.deltaTime * 500f);
    //    }
    //}

}
