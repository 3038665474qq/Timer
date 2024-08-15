using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class TimeNode
{
   public TimerMgr.TimeHandler callback;//����ص�����
    public float duration;//������ʱ����
    public float delay;//��һ�δ������ʱ��
    public int repeat;//��������
    public float passedTime;//��ȥʱ��
    public object param;//�û����ݲ���
    public bool isRemoved;//�Ƿ�ɾ��
    public int timerid;//��ʶTImerΨһID
}
public class TimerMgr : MonoBehaviour
{
    public delegate void TimeHandler(object parme);
    public static TimerMgr Instance;
    private int autoIncID = 1;
    private Dictionary<int, TimeNode> dic = new Dictionary<int, TimeNode>();
    //����б�
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
            //ȫ��Ψһ
            GameObject.Destroy(this);
            return;
        }
        this.Init();
    }
    /// <summary>
    /// ��ʼ�����
    /// </summary>
    public void Init()
    {
        autoIncID = 1;
    }
    private void Update()
    {
        float dt = Time.deltaTime;//ʱ����

        //�¼ӽ���ķ���
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
                //����
                item.callback(item.param);
                item.repeat--;
                item.passedTime -= (item.delay + item.duration);
                item.delay = 0;//����Ҫ

                if (item.repeat==0)
                {
                    item.isRemoved = true;
                    //��������   �Ƿ�ɾ��
                     
                }
            }
        }
        //���
        for (int i = 0; i < removetimer.Count; i++)
        {
            this.dic.Remove(removetimer[i].timerid);
        }
        removetimer.Clear();



    }

    /// <summary>
    /// ��ʱ��
    /// </summary>
    /// <param name="time">timer�ص�ϣ�� �ٴ�������ָ���Ĳ�������</param>
    /// <param name="delay">������������ʱ��</param>
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
    /// repeat==-1���ߴ���
    /// </summary>
    /// <param name="time">�ص�</param>
    /// <param name="repeat">����</param>
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
        //����
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
    /// ȡ��ί��
    /// </summary>
    /// <param name="timeid"></param>
    public void UnSchedule(TimeHandler func)
    {

    }


}
