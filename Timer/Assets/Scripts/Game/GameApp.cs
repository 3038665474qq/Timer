using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameApp : MonoBehaviour
{
    public void EnterGame()
    {
        //业务逻辑
        this.EnterLoginScene();
    }
    int timerid;

    private void EnterLoginScene()
    {
        //3D地图场景放入游戏

        //检测我的计时器
        //   TimerMgr.Instance.Schedule(this.OnTimerTest, 3, 3, 5); ;
        //TimerMgr.Instance.ScheduleOnce(this.OnTimerTest, 3); ;
        // TimerMgr.Instance.Schedule(this.OnTimerTest, 0, 0, 0); ;
        //end
        //添加参数
        timerid= TimerMgr.Instance.Schedule(this.OnTimerTest, "按计划的随访",0, 1, 0); ;
        Invoke("Clear", 1);
     
    }
    void Clear()
    {
      
        TimerMgr.Instance.UnSchedule(timerid);
    }

    private void OnTimerTest(object parme)
    {
        string str = (string)parme;
        Debug.Log("接受"+ str);
    }
}
