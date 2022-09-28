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
        InvokeRepeating("SerchEnemy", 0f, 0.5f); // ���۰� ���ÿ� 0.5�ʸ��� �ݺ��ؼ� ȣ��
        StartCoroutine(Fire());
    }

    private void Update()
    {
        // Ÿ���� ���ٸ� ������ ȸ���� �Ѵ�
        if (_target == null)
        {
            // ������ ����ġ ��Ų��
            Gun.transform.eulerAngles = new Vector3(0f, Gun.transform.eulerAngles.y, Gun.transform.eulerAngles.z);
            //Debug.Log("�� ����");
            RotatedTurret.transform.Rotate(new Vector3(0f, 45f, 0f) * Time.deltaTime);
        }

        else
        {
            Vector3 dir = _target.position - BulletSpawnPoint.position;
            Quaternion _lookRotation = Quaternion.LookRotation(dir);
            Gun.transform.rotation = _lookRotation;

            // �ε巴�� ����ȸ��
            // RotateTowards : a���� b���� c�� �ӵ��� ȸ��
            Vector3 _euler = Quaternion.RotateTowards(RotatedTurret.transform.rotation, _lookRotation, RotateSpeed * Time.deltaTime).eulerAngles; // ���Ϸ������� ��ȯ�� ���Ϳ� ������
            RotatedTurret.transform.rotation = Quaternion.Euler(0, _euler.y, 0);
        }
    }

    private void SerchEnemy()
    {
        Collider[] _colliders = Physics.OverlapSphere(transform.position, _range, _layerMask); // ��ü �ֺ��� �ݶ��̴� �����ϰ� �����
        Transform _shortTarget = null; // �ͷ��� ���� ����� ��� �ӽ÷� ����

        //�ݶ��̴��� �ϳ��� ����Ǹ� ����
        if (_colliders.Length > 0)
        {
            onFire = true;

            // ª���Ÿ��� ���Ϸ��� ���� �� ��ü�� ����
            float _shortTempDistance = Mathf.Infinity; // ���Ѵ� ���� ����
            // ����� �ݶ��̴���ŭ �ݺ����ֱ�
            foreach (Collider _colTarget in _colliders)
            {
                // SqrMagnitude - ������ȯ(�����Ÿ� * �����Ÿ�) <- ��ġ����� ��������� ���Ե�
                // Distance - ��Ʈ������ ��ȯ(�����Ÿ�)
                float _distance = Vector3.SqrMagnitude(transform.position - _colTarget.transform.position);
                // ����� �Ÿ����� �񱳸����� ������ �۴ٸ�
                if (_shortTempDistance > _distance)
                {
                    // �� ���� �ٽ� �������� �����
                    _shortTempDistance = _distance;
                    // ���� ����� ����� transform�� �־���
                    _shortTarget = _colTarget.transform;
                }
            }
        }

        // �ݺ����� �Ϸ�Ǹ� ���� ����� ����� �����
        // ����Ÿ�ٿ� ���尡������� �־���
        // �Ÿ����� Ÿ���� ���ٸ� null�� �־���
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
