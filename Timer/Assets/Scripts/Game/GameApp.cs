using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameApp : MonoBehaviour
{
    public void EnterGame()
    {
        //ҵ���߼�
        this.EnterLoginScene();
    }
    int timerid;

    private void EnterLoginScene()
    {
        //3D��ͼ����������Ϸ

        //����ҵļ�ʱ��
        //   TimerMgr.Instance.Schedule(this.OnTimerTest, 3, 3, 5); ;
        //TimerMgr.Instance.ScheduleOnce(this.OnTimerTest, 3); ;
        // TimerMgr.Instance.Schedule(this.OnTimerTest, 0, 0, 0); ;
        //end
        //��Ӳ���
        timerid= TimerMgr.Instance.Schedule(this.OnTimerTest, "���ƻ������",0, 1, 0); ;
        Invoke("Clear", 1);
     
    }
    void Clear()
    {
      
        TimerMgr.Instance.UnSchedule(timerid);
    }

    private void OnTimerTest(object parme)
    {
        string str = (string)parme;
        Debug.Log("����"+ str);
    }
}
