using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : LivingEntity
{
    [SerializeField] private Transform TargetPoint;
    //[SerializeField] private Transform FinishPoint;
    [SerializeField] private GameObject Sword;

    [SerializeField] private GameObject targetIsPlayer;
    [SerializeField] private GameObject targetIsNexus;

    private NavMeshAgent agent;

    public bool isDetactive = false;
    public float DetectRange;
    public float AttackRange;

    private int _giveGold;
    private AudioSource audioSource;
    [SerializeField] AudioClip attackAudioClip;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        StartCoroutine(MoveAndAttack());
        StartCoroutine(TakeDamage());

        _giveGold = 300;
    }

    private IEnumerator MoveAndAttack()
    {
        while (true)
        {
            if (Health <= 0)
            {
                GameManager.Instance.PlayerGold += _giveGold;
                ++GameManager.Instance.EnemyDeathCount;
                Health = 100;
                Destroy(gameObject);
            }

            yield return new WaitForSeconds(0.01f);

            FindPlayer();
            if (!agent.pathPending)
            {
                if (false == isDetactive)
                {
                    AttackNexus();

                }
                else
                {
                    AttackPlayer();
                }

            }
        }
    }

    IEnumerator TakeDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (Sword.GetComponent<Animator>().GetBool("onAttack") && isDetactive == true) // 플레이어가 타겟
            {
                targetIsPlayer.GetComponent<LivingEntity>().TakeDamage(Damage);
                AttackSound();
            }
            else if(Sword.GetComponent<Animator>().GetBool("onAttack") && isDetactive == false) // 넥서스가 타겟
            {
                targetIsNexus.GetComponent<LivingEntity>().TakeDamage(Damage);
                AttackSound();
            }
            //if (Sword.GetComponent<Animator>().GetBool("onAttack"))
            //{
            //    target.GetComponent<LivingEntity>().TakeDamage(Damage);
            //}
        }
    }

    private void AttackSound()
    {
        if (Sword.GetComponent<Animator>().GetBool("onAttack"))
        {
            audioSource.clip = attackAudioClip;
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void AttackPlayer()
    {
        //Debug.Log("플레이어 공격");
        Vector3 dir = TargetPoint.position - transform.position;
        float Distance = Vector3.Distance(TargetPoint.position, transform.position);
        transform.rotation = Quaternion.LookRotation(dir);
        agent.SetDestination(TargetPoint.position);

        if (Distance < AttackRange)
        {
            Sword.GetComponent<Animator>().SetBool("onAttack", true);


            agent.speed = 0f;
        }
        else
        {
            Sword.GetComponent<Animator>().SetBool("onAttack", false);
            audioSource.Stop();
            agent.speed = MoveSpeed;

        }
    }


    private void AttackNexus()
    {
        //Debug.Log("넥서스 공격");
        // 네비메쉬를 따라 움직이는 부분
        float distance = Vector3.Distance(targetIsNexus.gameObject.transform.position, transform.position);
        Vector3 direction = new Vector3(agent.steeringTarget.x, transform.position.y, agent.steeringTarget.z) - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, Time.deltaTime * 500f);
        }

        if (distance <= AttackRange + 3)
        {

            agent.speed = 0;
            Sword.GetComponent<Animator>().SetBool("onAttack", true);
        }
        else
        {

            // Debug.Log("가는중");
            agent.speed = MoveSpeed;
            agent.SetDestination(targetIsNexus.gameObject.transform.position);
            Sword.GetComponent<Animator>().SetBool("onAttack", false);
        }
    }


    private void FindPlayer()
    {
        Vector3 dir = transform.position - TargetPoint.position;

        // 플레이어가 사정거리 안으로 들어옴
        if (dir.sqrMagnitude <= DetectRange)
        {
            // 넥서스로 향하지 않음
            isDetactive = true;
        }
        else
        {
            // 사정거리 벗어나면 넥서스를 향함
            isDetactive = false;
        }
    }


    public override void TakeDamage(int damage)
    {
        Health -= damage;
    }
}
