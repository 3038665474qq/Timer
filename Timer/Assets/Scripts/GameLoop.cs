
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏入口
/// </summary>
public class GameLoop : MonoBehaviour
{

    private void Awake()
    {
        //游戏框架初始化
        this.gameObject.AddComponent<TimerMgr>();
        //end
        //游戏业务逻辑的初始化
        this.gameObject.AddComponent<GameApp>().EnterGame();
        this.gameObject.name = "sddd";


    }
}
