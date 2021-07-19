using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimFactory
{

    public class Land : MonoBehaviour
    {
        public LandType.LandsType landType;

        public LandType.IsJob work;

        private GameObject golds;

        public int[] WareRaw;

        private GameObject DesPanel;

        private void Awake()
        {
            //添加事件
            EventCenter.AddListener(EventType.ADDFACTLABEL, AddFactLabel);
            EventCenter.AddListener<int[]>(EventType.CHANGELANDWARERAW, ChangeLandWareRaw);
            landType = LandType.LandsType.EMPTY;//默认为空地
            work = LandType.IsJob.NO;
            golds = GameObject.FindGameObjectWithTag("Gold").gameObject;
            WareRaw = new int[6] { 0, 0, 0, 0, 0, 0 };
            DesPanel = GameObject.FindGameObjectWithTag("DesPanel").gameObject;
        }

        private void OnDestroy()
        {
            EventCenter.RemoveListener(EventType.ADDFACTLABEL, AddFactLabel);
            EventCenter.RemoveListener<int[]>(EventType.CHANGELANDWARERAW, ChangeLandWareRaw);
        }

        /// <summary>
        /// string转化int
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private int Set_Str(string str)
        {
            int n = 0;
            for(int i = 0; i < str.Length; i++)
            {
                if(str[i] >= '0' && str[i] <= '9')
                n = n * 10 + (str[i] - '0'); 
            }
            return n;
        }

        /// <summary>
        /// 更新仓库
        /// </summary>
        /// <param name="raw"></param>
        private void ChangeLandWareRaw(int[] raw)
        {
            for(int i = 0; i < WareRaw.Length; i++)
            {
                WareRaw[i] = raw[i];
            }
        }

        private void SelectMusic(LandType.LandsType landType)
        {
            //TODO
            switch(landType)
            {
                case LandType.LandsType.OB1:
                    EventCenter.Broadcast<Music>(EventType.PLAYMUSIC, Music.PLAYFACTROY);
                    break;
                case LandType.LandsType.OB2:
                    EventCenter.Broadcast<Music>(EventType.PLAYMUSIC, Music.PLAYFACTROY);
                    break;
                case LandType.LandsType.OB3:
                    EventCenter.Broadcast<Music>(EventType.PLAYMUSIC, Music.PLAYFACTROY);
                    break;
                case LandType.LandsType.OB4:
                    EventCenter.Broadcast<Music>(EventType.PLAYMUSIC, Music.PLAYFACTROY);
                    break;
                case LandType.LandsType.OB5:
                    EventCenter.Broadcast<Music>(EventType.PLAYMUSIC, Music.PLAYROAD);
                    break;
                case LandType.LandsType.OB6:
                    EventCenter.Broadcast<Music>(EventType.PLAYMUSIC, Music.PLAYWATER);
                    break;
                case LandType.LandsType.OB7:
                    EventCenter.Broadcast<Music>(EventType.PLAYMUSIC, Music.PLAYTEAR);
                    break;
            }
        }

        private void OnMouseDown()
        {
            if (DesPanel.transform.localScale.x != 0)//当工厂界面打开时，不允许进入其他工厂(防误触)
            {
                return;
            }
            //水面和庄稼禁止打开
            if(this.landType == LandType.LandsType.OB6 || this.landType == LandType.LandsType.PLANT1 || this.landType == LandType.LandsType.PLANT2 || this.landType == LandType.LandsType.PLANT3 || this.landType == LandType.LandsType.PLANT4 || this.landType == LandType.LandsType.PLANT5)
            {
                //判断能否收获
                if(this.work == LandType.IsJob.YES)
                {
                    
                    GameObject point = Managers.m_Managers.m_GameManager.pointer;
                    if (point == null)//当鼠标为空时
                    {
                        GameObject go = Instantiate(new GameObject());
                        go.AddComponent<Image>().sprite = this.transform.parent.GetComponent<LandType>().Build[(int)landType];
                        go.AddComponent<Lat>().Raw = (int)landType + 5;
                        go.transform.SetParent(GameObject.FindGameObjectWithTag("UIGame1").transform);
                        Managers.m_Managers.m_GameManager.pointer = go;
                        Managers.m_Managers.m_GameManager.mouse = MouseType.mouseType.INTO;
                        this.work = LandType.IsJob.NO;
                    }
                }
                return;
            }
            //测试
            //Debug.Log("点击该物体坐标为" + this.transform.position.x + " " + this.transform.position.y);
            if(Managers.m_Managers.m_GameManager.mouse != MouseType.mouseType.EMPTY)//当鼠标附着事件不为空(建设)
            {
                if(Managers.m_Managers.m_GameManager.mouse == MouseType.mouseType.BUILDTYPE && landType == LandType.LandsType.EMPTY)//建造
                {
                    if(!Gold.IsEnoughGold(golds.GetComponent<Gold>().gold, (int)Set_Str(Managers.m_Managers.m_GameManager.pointer.name.ToString()))) 
                    {
                        return;
                    }
                    isFinishTask();
                    landType = (LandType.LandsType)Set_Str(Managers.m_Managers.m_GameManager.pointer.name.ToString());
                    if(landType < (LandType.LandsType)11)//建筑
                    {
                        EventCenter.Broadcast(EventType.ISJOBFACTROY, true);
                    }
                    
                    
                    EventCenter.Broadcast<int>(EventType.CHANGEGOLD, -this.transform.parent.GetComponent<LandType>().BuildPrice[(int)landType]);
                    
                    //置空
                    Destroy(Managers.m_Managers.m_GameManager.pointer);
                    Managers.m_Managers.m_GameManager.pointer = null;
                    Managers.m_Managers.m_GameManager.mouse = MouseType.mouseType.EMPTY;

                    if(this.landType != LandType.LandsType.OB5)
                    {
                        this.gameObject.AddComponent<SewageTreat>();
                    }

                }else if(Managers.m_Managers.m_GameManager.mouse == MouseType.mouseType.TEARTYPE && landType == LandType.LandsType.OB5)//拆除
                {
                    if (!Gold.IsEnoughGold(golds.GetComponent<Gold>().gold, (int)Set_Str(Managers.m_Managers.m_GameManager.pointer.name.ToString())))
                    {
                        return;
                    }
                    isFinishTask();
                    //属性置空
                    landType = LandType.LandsType.EMPTY;
                    EventCenter.Broadcast(EventType.ISJOBFACTROY, false);
                    EventCenter.Broadcast<int>(EventType.CHANGEGOLD, -this.transform.parent.GetComponent<LandType>().BuildPrice[(int)landType]);

                    Destroy(Managers.m_Managers.m_GameManager.pointer);
                    Managers.m_Managers.m_GameManager.pointer = null;
                    Managers.m_Managers.m_GameManager.mouse = MouseType.mouseType.EMPTY;
                }else if(Managers.m_Managers.m_GameManager.mouse == MouseType.mouseType.BUYRAW && landType != LandType.LandsType.EMPTY && landType != LandType.LandsType.OB5)//买入
                {
                    isFinishTask();
                }
                else if(Managers.m_Managers.m_GameManager.mouse == MouseType.mouseType.INTO && landType != LandType.LandsType.EMPTY && landType != LandType.LandsType.OB5)
                {
                    if(!isEnoughRaw())//如果仓库没有空间，退出
                    {
                        return;
                    }
                    //存入仓库
                    DepRaw(Managers.m_Managers.m_GameManager.pointer.GetComponent<Lat>().Raw);
                    //销毁物体
                    Destroy(Managers.m_Managers.m_GameManager.pointer);
                    Managers.m_Managers.m_GameManager.mouse = MouseType.mouseType.EMPTY;
                    Managers.m_Managers.m_GameManager.pointer = null;
                }else
                {

                }
                
                
                ChangeLand();
            }else//当鼠标附着事件为空
            {
                if(this.work == LandType.IsJob.YES)
                {
                    //Debug.Log("可以工作");
                    
                    EventCenter.Broadcast(EventType.CLICKOPENDESPANEL, this.gameObject);
                   
                }
            }

            SelectMusic(this.landType);
        }

        /// <summary>
        /// 判断仓库是否有空间
        /// </summary>
        /// <returns></returns>
        private bool isEnoughRaw()
        {
            for(int i = 0; i < WareRaw.Length; i++)
            {
                if(WareRaw[i] == 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 存入仓库
        /// </summary>
        /// <param name="value">存入的数据</param>
        private void DepRaw(int value)
        {
            for(int i = 0; i < WareRaw.Length; i++)
            {
                if(WareRaw[i] == 0)
                {
                    WareRaw[i] = value;
                    break;
                }
            }
        }

        /// <summary>
        /// 附着该地上的物体
        /// </summary>
        /// <param name="go"></param>
        public void ChangeLand()
        {
            this.transform.Find("Spr_Land").GetComponent<SpriteRenderer>().sprite = this.transform.parent.GetComponent<LandType>().Build[(int)landType];
        }
        /// <summary>
        /// 添加标签
        /// </summary>
        public void AddFactLabel()
        {
            this.GetComponent<WareHouse>().Label = this.transform.parent.GetComponent<LandType>().Label++;
        }

        private void isFinishTask()
        {
            GameObject task = GameObject.FindGameObjectWithTag("TaskBar");
            TaskType taskType = task.GetComponent<TaskType>();
            TaskType.Task t = new TaskType.Task("");
            switch (Managers.m_Managers.m_GameManager.pointer.name)
            {
                case "F1":
                    t = taskType.GetDicTask(TaskType.TASKTYPE.PRODUCEF1);
                    break;
                case "F2":
                    t = taskType.GetDicTask(TaskType.TASKTYPE.PRODUCEF2);
                    break;
                case "F3":
                    t = taskType.GetDicTask(TaskType.TASKTYPE.PRODUCEF3);
                    break;
                case "F4":
                    t = taskType.GetDicTask(TaskType.TASKTYPE.PRODUCEF4);
                    break;
                case "T5":
                    t = taskType.GetDicTask(TaskType.TASKTYPE.PRODUCET5);
                    break;
                case "T6":
                    t = taskType.GetDicTask(TaskType.TASKTYPE.PRODUCET6);
                    break;
                case "T7":
                    t = taskType.GetDicTask(TaskType.TASKTYPE.PRODUCET7);
                    break;
                case "P11":
                    t = taskType.GetDicTask(TaskType.TASKTYPE.PRODUCEP11);
                    break;
                case "P12":
                    t = taskType.GetDicTask(TaskType.TASKTYPE.PRODUCEP12);
                    break;
                case "P13":
                    t = taskType.GetDicTask(TaskType.TASKTYPE.PRODUCEP13);
                    break;
                case "P14":
                    t = taskType.GetDicTask(TaskType.TASKTYPE.PRODUCEP14);
                    break;
                case "P15":
                    t = taskType.GetDicTask(TaskType.TASKTYPE.PRODUCEP15);
                    break;
                default:
                    break;
            }
            EventCenter.Broadcast<TaskType.Task>(EventType.REMOVETASK, t);
        }
    }
}
