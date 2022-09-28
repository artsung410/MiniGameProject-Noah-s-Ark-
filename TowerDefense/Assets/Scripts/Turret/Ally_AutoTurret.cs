using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ally_AutoTurret : Turret
{
    [SerializeField] Transform _gunTower = null;
    [SerializeField] float _range = 0f;
    [SerializeField] LayerMask _layerMask = 0;

    public Transform _target = null;

    [SerializeField] AudioSource audioSources;

    public AudioClip shootAudioClip;

    private void Awake()
    {
        audioSources = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // 매 프레임마다 실행할필요가없어 start로 돌림
        InvokeRepeating("SerchEnemy", 0f, 0.5f); // 시작과 동시에 0.5초마다 반복해서 호출
        StartCoroutine(Fire());
    }


    private void Update()
    {
        // 타겟이 없다면 포신은 회전만 한다
        if (_target == null)
        {
            // 포신을 재위치 시킨다
            Gun.transform.eulerAngles = new Vector3(0f, Gun.transform.eulerAngles.y, Gun.transform.eulerAngles.z);
            //Debug.Log("적 없음");
            RotatedTurret.transform.Rotate(new Vector3(0f, 45f, 0f) * Time.deltaTime);

            onFire = false;
            isDetactive = false;
        }
        else
        {
            //Debug.Log("적 탐색");

            onFire = true;
            isDetactive = true;

            Quaternion _lookRotation = Quaternion.LookRotation(_target.position - BulletSpawnPoint.position);
            Gun.transform.rotation = _lookRotation;

            // 부드럽게 방향회전
            // RotateTowards : a에서 b까지 c의 속도로 회전
            Vector3 _euler = Quaternion.RotateTowards(RotatedTurret.transform.rotation, _lookRotation, RotateSpeed * Time.deltaTime).eulerAngles; // 오일러값으로 변환해 벡터에 저장함
            RotatedTurret.transform.rotation = Quaternion.Euler(0, _euler.y, 0);
        }
    }



    private void SerchEnemy()
    {
        Collider[] _colliders = Physics.OverlapSphere(transform.position, _range, _layerMask); // 객체 주변의 콜라이더 검출하고 담아줌
        Transform _shortTarget = null; // 터렛과 가장 가까운 대상 임시로 선언

        //콜라이더가 하나라도 검출되면 실행
        if (_colliders.Length > 0)
        {
            // 짧은거리를 비교하려면 가장 긴 객체가 기준
            float _shortTempDistance = Mathf.Infinity; // 무한대 길이 생성
            // 검출된 콜라이더만큼 반복해주기
            foreach (Collider _colTarget in _colliders)
            {
                // SqrMagnitude - 제곱반환(실제거리 * 실제거리) <- 위치비용이 상대적으로 적게듬
                // Distance - 루트연산후 반환(실제거리)
                float _distance = Vector3.SqrMagnitude(transform.position - _colTarget.transform.position);
                // 계산한 거리값이 비교를위한 값보다 작다면
                if (_shortTempDistance > _distance)
                {
                    // 그 값을 다시 기준으로 삼아줌
                    _shortTempDistance = _distance;
                    // 가장 가까운 대상을 transform에 넣어줌
                    _shortTarget = _colTarget.transform;
                }
            }
        }
        // 반복문이 완료되면 가장 가까운 대상이 검출됨
        // 최종타겟에 가장가까운대상을 넣어줌
        // 거리내에 타겟이 없다면 null을 넣어줌
        _target = _shortTarget;
    }

    public override void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator Fire()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);

            if (onFire)
            {
                Bullet bullet = BulletPool.GetObject();
                audioSources.clip = shootAudioClip;
                audioSources.Play();
                bullet.gameObject.transform.position = BulletSpawnPoint.position;
                bullet.gameObject.transform.rotation = BulletSpawnPoint.rotation;
            }
            else
            {

            }
        }
    }


    //public void PlayClip()
    //{
    //    // 총알소리를 재생하는 오디오소스를 여러개 둔다
    //    // 현재 총알소리를 재생하는 오디오소스가있다면
    //    // 다른 오디오소스로 총알소리를 재생하게한다
        
    //    for (int i = 0; i < audioSources.Length; i++)
    //    {
    //        if (audioSources[i].isPlaying)
    //        {
    //            break;
    //        }

    //        else
    //        {
    //            // 재생
    //            audioSources[i].clip = shootAudioClip;
    //            audioSources[i].Play();
    //            break;
    //        }
    //    }
    //}
}
