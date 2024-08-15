using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class TimeNode
{
   public TimerMgr.TimeHandler callback;//保存回调函数
    public float duration;//触发的时间间隔
    public float delay;//第一次触发间隔时间
    public int repeat;//触发次数
    public float passedTime;//过去时间
    public object param;//用户传递参数
    public bool isRemoved;//是否删除
    public int timerid;//标识TImer唯一ID
}
public class TimerMgr : MonoBehaviour
{
    public delegate void TimeHandler(object parme);
    public static TimerMgr Instance;
    private int autoIncID = 1;
    private Dictionary<int, TimeNode> dic = new Dictionary<int, TimeNode>();
    //清除列表
    private List<TimeNode> removetimer = new List<TimeNode>();
    private List<TimeNode> newAddtimer = new List<TimeNode>();
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            //全局唯一
            GameObject.Destroy(this);
            return;
        }
        this.Init();
    }
    /// <summary>
    /// 初始化入口
    /// </summary>
    public void Init()
    {
        autoIncID = 1;
    }
    private void Update()
    {
        float dt = Time.deltaTime;//时间间隔

        //新加进入的放入
        for (int i = 0; i < newAddtimer.Count; i++)
        {
            dic.Add(newAddtimer[i].timerid, newAddtimer[i]);
        }
        newAddtimer.Clear();

        foreach (TimeNode item in dic.Values)
        {
            if (item.isRemoved)
            {
                removetimer.Add(item);
                continue;
            }

          
            item.passedTime += dt;
            if (item.passedTime>=item.delay+item.duration)
            {
                //触发
                item.callback(item.param);
                item.repeat--;
                item.passedTime -= (item.delay + item.duration);
                item.delay = 0;//很重要

                if (item.repeat==0)
                {
                    item.isRemoved = true;
                    //次数结束   是否删除
                     
                }
            }
        }
        //清除
        for (int i = 0; i < removetimer.Count; i++)
        {
            this.dic.Remove(removetimer[i].timerid);
        }
        removetimer.Clear();



    }

    /// <summary>
    /// 定时器
    /// </summary>
    /// <param name="time">timer回调希望 再传给我们指定的参数对象</param>
    /// <param name="delay">多少秒启动定时器</param>
    /// <returns></returns>
    public int ScheduleOnce(TimeHandler time,float delay)
    {
         
        return this.Schedule(time,1,0, delay);
    }
    public int ScheduleOnce(TimeHandler time,object param,float delay)
    {
        return this.Schedule(time, param, 1,0, delay);
    }
    public int Schedule(TimeHandler time, int repeat, float duration, float delay=0.0f)
    {
        return Schedule(time, null, repeat,duration,delay);
    }

    /// <summary>
    /// repeat==-1无线触发
    /// </summary>
    /// <param name="time">回调</param>
    /// <param name="repeat">次数</param>
    /// <param name="duration"></param>
    /// <param name="delay"></param>
    /// <returns></returns>
    public int Schedule(TimeHandler time, object param, int  repeat,float duration,float delay = 0.0f)
    {
        TimeNode timeNode = new TimeNode();
        timeNode.callback = time;
        timeNode.param = param;
        timeNode.repeat = repeat;
        timeNode.duration = duration;
        timeNode.delay = delay;
        timeNode.passedTime = timeNode.duration;

        timeNode.isRemoved = false;
        timeNode.timerid = this.autoIncID;
        //自增
        autoIncID++;

        //dic.Add(timeNode.timerid, timeNode);
        newAddtimer.Add(timeNode);
        return timeNode.timerid;
    }

    /// <summary>
    /// Time ID
    /// </summary>
    /// <param name="timeid"></param>
    public  void UnSchedule(int  timeid)
    {
       
        if (dic.ContainsKey(timeid))
        {
        
            TimeNode timeNode = dic[timeid];
            timeNode.isRemoved = true;
        } 
        else
        {
            return;
        }
       
    }


    /// <summary>
    /// 取消委托
    /// </summary>
    /// <param name="timeid"></param>
    public void UnSchedule(TimeHandler func)
    {

    }


}
