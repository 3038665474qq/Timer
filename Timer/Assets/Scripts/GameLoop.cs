
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��Ϸ���
/// </summary>
public class GameLoop : MonoBehaviour
{

    private void Awake()
    {
        //��Ϸ��ܳ�ʼ��
        this.gameObject.AddComponent<TimerMgr>();
        //end
        //��Ϸҵ���߼��ĳ�ʼ��
        this.gameObject.AddComponent<GameApp>().EnterGame();
        this.gameObject.name = "sddd";


    }
}
