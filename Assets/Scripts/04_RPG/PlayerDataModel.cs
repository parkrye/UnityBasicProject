using UnityEngine;
using UnityEngine.Events;

public class PlayerDataModel : MonoBehaviour
{
    [SerializeField] int[] status; // level, maxHP, nowHP, maxSP, nowSP, maxEXP, nowEXP

    [SerializeField] UnityEvent<int[], int> StatusChanged;  // status, �̺�Ʈ ��ȣ
                                                            // (0:�ɷ�ġ ����, 1:ü�� ����, 2:ü�� ȸ��, 3:��� ����, 4:��� ȸ��,
                                                            //  )

    void Awake()
    {
        status = new int[7] { 1, 100, 100, 100, 100, 100, 0 };
        StatusChanged?.Invoke(status, 0);
    }

    /// <summary>
    /// ü�� ����
    /// </summary>
    /// <param name="modifier">����� ����, ������ ����</param>
    public void OnHPChanged(int modifier)
    {
        if (modifier == 0)
            return;

        if(modifier < 0)
        {
            HPDecrease(modifier);
        }
        else if(modifier > 0)
        {
            HPIncrease(modifier);
        }
        StatusChanged?.Invoke(status, 0);
    }

    void HPDecrease(int damage)
    {
        status[2] += damage;
        if(damage > 0)
            StatusChanged?.Invoke(status, 1);
    }

    void HPIncrease(int heal)
    {
        status[2] += heal;
        if(status[2] > status[1])
            status[2] = status[1];
        StatusChanged?.Invoke(status, 2);
    }

    /// <summary>
    /// ��� ����
    /// </summary>
    /// <param name="modifier">����� ����, ������ ����</param>
    public void OnSPChanged(int modifier)
    {
        if (modifier == 0)
            return;

        if(modifier < 0)
        {
            SPDecrease(modifier);
        }
        else if(modifier > 0)
        {
            SPIncrease(modifier);
        }
        StatusChanged?.Invoke(status, 0);
    }

    void SPDecrease(int damage)
    {
        status[4] += damage;
        if(damage > 0)
            StatusChanged?.Invoke(status, 3);
    }

    void SPIncrease(int heal)
    {
        status[4] += heal;
        if(status[4] > status[3])
            status[4] = status[3];
        StatusChanged?.Invoke(status, 4);
    }

    /// <summary>
    /// ����ġ ����
    /// </summary>
    /// <param name="modifier">������ ���� ����</param>
    public void OnEXPChanged(int modifier)
    {
        if (modifier <= 0)
            return;

        EXPIncrease(modifier);
        StatusChanged?.Invoke(status, 0);
    }

    void EXPIncrease(int heal)
    {
        status[6] += heal;
        while (status[6] >= status[5])
            LevelUP();
        StatusChanged?.Invoke(status, 4);
    }

    void LevelUP()
    {
        status[0]++;
        status[6] -= status[7];
        status[7] += 10;

        status[1] += 10;
        status[2] = status[1];
        status[3] += 10;
        status[4] = status[3];
    }
}
