using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class EnemyController : MonoBehaviour
{
    public Transform cannonTransform;
    public GameObject shellPrefab;
    NavMeshAgent navMeshAgent;
    ParticleSystem shellParticle;

    [SerializeField] float power;
    [SerializeField] float coolTime;
    [SerializeField] bool isReady;

    [SerializeField] private float viewAngle;       // �þ� ����
    [SerializeField] private float viewDistance;    // �þ� �Ÿ�
    [SerializeField] private LayerMask targetMask;  // Ÿ�� ����ũ

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        shellParticle = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerCheck())
        {
            MoveAround();
        }
        else
        {
            AttackPlayer();
        }
    }

    /// <summary>
    /// ������ ���� ���ͷ� �����ϴ� �޼ҵ�
    /// </summary>
    /// <param name="angle">����</param>
    /// <returns>���� ����</returns>
    Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }

    /// <summary>
    /// �÷��̾ Ȯ���ϴ� �þ� �޼ҵ�
    /// </summary>
    /// <returns>�÷��̾� �߰� ����</returns>
    bool PlayerCheck()
    {

        Debug.DrawRay(transform.position, AngleToDir(viewAngle * 0.5f) * viewDistance, Color.blue);
        Debug.DrawRay(transform.position, AngleToDir(viewAngle * -0.5f) * viewDistance, Color.blue);
        Debug.DrawRay(transform.position, AngleToDir(viewAngle) * viewDistance, Color.cyan);

        Collider[] targets = Physics.OverlapSphere(transform.position, viewDistance, targetMask);
        foreach (Collider target in targets)
        {
            Vector3 targetPos = target.transform.position;
            Vector3 targetDir = (targetPos - transform.position).normalized;
            float targetAngle = Mathf.Acos(Vector3.Dot(AngleToDir(viewAngle), targetDir)) * Mathf.Rad2Deg;
            if(targetAngle <= viewAngle * 0.5f && !Physics.Raycast(transform.position, targetDir, viewDistance, targetMask))
            {
                return true;
            }
        }
        return false;
    }

    void MoveAround()
    {

    }

    void AttackPlayer()
    {
        Debug.Log("Find");
    }
}
