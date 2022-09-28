using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyHowitzerTurret : Turret
{
    public static Vector3 Target;
    [SerializeField] float _range = 0f;
    [SerializeField] LayerMask _layerMask = 0;

    public static Transform _target = null;

    private AudioSource audioSource;
    public AudioClip shootAudioClip;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
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
        }

        else
        {
            Vector3 dir = _target.position - BulletSpawnPoint.position;
            Quaternion _lookRotation = Quaternion.LookRotation(dir);
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
            onFire = true;

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
        yield return null;

        while (true)
        {
            yield return new WaitForSeconds(2f);

            if (_target != null)
            {
                audioSource.clip = shootAudioClip;
                audioSource.Play();

                HowitzerBullet bullet = HowitzerBulletPool.GetObject();
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

                bullet.gameObject.transform.position = RotatedTurret.transform.position;
                bullet.gameObject.transform.rotation = RotatedTurret.transform.rotation;

                Vector3 dir = _target.position - bullet.gameObject.transform.position;
                bulletRb.AddRelativeForce(dir.x* 22, 450f, dir.z* 22);
            }
        }
    }
}
