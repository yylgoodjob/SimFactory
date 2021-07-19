using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace SimFactory
{

    public class TaskBar : MonoBehaviour
    {
        private GameObject Btn_TaskBar;
        private GameObject TheTaskBar;
        private GameObject Task;
        private bool isOpenTaskBar;
        private List<TaskType.Task> TaskList;//任务列表 
        private List<GameObject> gameTaskList;//任务列表(视图)
        private TaskType m_TaskType;

        private void Awake()
        {
            EventCenter.AddListener<TaskType.TASKTYPE>(EventType.ADDTASK, AddTask);
            EventCenter.AddListener<TaskType.Task>(EventType.REMOVETASK, RemoveTask);
        }

        void Start()
        {
            InitTaskBar();
        }

        private void OnDestroy()
        {
            EventCenter.RemoveListener<TaskType.TASKTYPE>(EventType.ADDTASK, AddTask);
            EventCenter.RemoveListener<TaskType.Task>(EventType.REMOVETASK, RemoveTask);
        }

        private void InitTaskBar()
        {
            isOpenTaskBar = true;
            TaskList = new List<TaskType.Task>();
            gameTaskList = new List<GameObject>();
            m_TaskType = this.GetComponent<TaskType>();
            Btn_TaskBar = this.transform.Find("Btn_TaskBar").gameObject;
            TheTaskBar = this.transform.Find("TheTaskBar").gameObject;
            Task = Resources.Load<GameObject>("Tasks");
            Button btn = Btn_TaskBar.AddComponent<Button>();
            btn.onClick.AddListener(IsOpenTaskBar);
        }


        /// <summary>
        /// 任务栏
        /// </summary>
        private void IsOpenTaskBar()
        {
            if (isOpenTaskBar)
                TheTaskBar.transform.DOLocalMoveY(-37.5f, 1);
            else
                TheTaskBar.transform.DOLocalMoveY(437.5f, 1);
            isOpenTaskBar = !isOpenTaskBar;
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="n">任务标号</param>
        private void AddTask(TaskType.TASKTYPE taskType)
        {
            TaskType.Task task = m_TaskType.GetDicTask(taskType);
            if(task == null)
            {
                return;
            }
            if(TaskList.Count >= 5)
            {
                //Debug.Log("移除");
                RemoveTask(0);//任务栏满，移除任务
                //TODO 扣除一部分金币(每次扣除50金)
                EventCenter.Broadcast<int>(EventType.CHANGEGOLD, -50);
            }
            TaskList.Add(task);
            //TODO 更新任务栏
            InitTask(taskType);
        }

        private void RemoveTask(TaskType.Task task)
        {
            for(int i = 0; i < TaskList.Count; i++)
            {
                if(TaskList[i] == task)
                {
                    //TODO 更新任务栏
                    DesTask(i);
                    TaskList.RemoveAt(i);
                }
            }
            
        }
        private void RemoveTask(int n)
        {
            DesTask(n);
            TaskList.RemoveAt(n);
            //TODO 更新任务栏
        }
        /// <summary>
        /// 添加任务(视图)
        /// </summary>
        private void InitTask(TaskType.TASKTYPE taskType)
        {
            GameObject go = Instantiate(Task);
            gameTaskList.Add(go);
            Text t = go.transform.GetComponentInChildren<Text>();
            t.text = this.GetComponent<TaskType>().GetDicTask(taskType).GetDesTask();
            go.transform.SetParent(TheTaskBar.transform);
            go.transform.localScale = new Vector3(0, 0, 0);
            go.transform.localPosition = new Vector3(0, -TaskList.Count * 60, 0);
            go.transform.DOScale(new Vector3(1, 1, 1), 1);
        }

        /// <summary>
        /// 移除任务
        /// </summary>
        private void DesTask(int n)
        {
            Destroy(gameTaskList[n]);
            gameTaskList.RemoveAt(n);
            //Debug.Log(gameTaskList.Count);
            for(int i = 0; i < gameTaskList.Count; i++)
            {
                gameTaskList[i].transform.DOLocalMoveY(-(i + 1) * 60, 0.5f);
            }
        }
    }
}
