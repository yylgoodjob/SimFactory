using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimFactory
{
    
    public class TaskType : MonoBehaviour
    {
        private int taskClass = 0;
        public int TaskClass() { return taskClass; }
        public enum TASKTYPE
        {
            EMPTY,//无任务
            PRODUCEF1,//建造工厂1
            PRODUCEF2,//建造工厂2
            PRODUCEF3,//建造工厂3
            PRODUCEF4,//建造工厂4
            PRODUCET5,//使用工具1
            PRODUCET6,//使用工具2
            PRODUCET7,//使用工具3
            PRODUCEP11,//获取原料1
            PRODUCEP12,//获取原料2
            PRODUCEP13,//获取原料3
            PRODUCEP14,//获取原料4
            PRODUCEP15,//获取原料5
            
        }
        public class Task
        {
            private bool isFinish;
            private string DesTask;
            public string GetDesTask() { return DesTask; }
            public Task(string des)
            {
                isFinish = false;
                DesTask = des;
            }
            public bool GetisFinish()
            {
                return isFinish;
            }

        }
        private Dictionary<TASKTYPE, Task> DicTask;
        /// <summary>
        /// 获取任务
        /// </summary>
        /// <param name="taskType">任务标号</param>
        /// <returns></returns>
        public Task GetDicTask(TASKTYPE taskType)
        {
            if (DicTask.ContainsKey(taskType))
            {
                return DicTask[taskType];
            }
            return null;
        }

        private void Start()
        {
            //初始化任务
            InitTask();
        }

        private void InitTask()
        {
            DicTask = new Dictionary<TASKTYPE, Task>();

            taskClass++;//统计任务个数
            DicTask.Add(TASKTYPE.PRODUCEF1, new Task("玩家建造原料处理厂"));
            taskClass++;//统计任务个数
            DicTask.Add(TASKTYPE.PRODUCEF2, new Task("玩家建造合成加工厂"));
            taskClass++;//统计任务个数
            DicTask.Add(TASKTYPE.PRODUCEF3, new Task("玩家建造包装厂"));
            taskClass++;//统计任务个数
            DicTask.Add(TASKTYPE.PRODUCEF4, new Task("玩家建造污水处理厂"));

            taskClass++;//统计任务个数
            DicTask.Add(TASKTYPE.PRODUCET5, new Task("玩家建造路段"));
            taskClass++;//统计任务个数
            DicTask.Add(TASKTYPE.PRODUCET6, new Task("玩家建造水段"));
            taskClass++;//统计任务个数
            DicTask.Add(TASKTYPE.PRODUCET7, new Task("玩家拆除路段"));

            taskClass++;//统计任务个数
            DicTask.Add(TASKTYPE.PRODUCEP11, new Task("玩家任意生成或者购买原料小麦，并点击右键提交（卖出）"));
            taskClass++;//统计任务个数
            DicTask.Add(TASKTYPE.PRODUCEP12, new Task("玩家任意生成或者购买原料玉米，并点击右键提交（卖出）"));
            taskClass++;//统计任务个数
            DicTask.Add(TASKTYPE.PRODUCEP13, new Task("玩家任意生成或者购买原料萝卜，并点击右键提交（卖出）"));
            taskClass++;//统计任务个数
            DicTask.Add(TASKTYPE.PRODUCEP14, new Task("玩家任意生成或者购买原料土豆，并点击右键提交（卖出）"));
            taskClass++;//统计任务个数
            DicTask.Add(TASKTYPE.PRODUCEP15, new Task("玩家任意生成或者购买原料番茄，并点击右键提交（卖出）"));
        }
    }
}
