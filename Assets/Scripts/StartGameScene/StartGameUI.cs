using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

namespace SimFactory
{

    public class StartGameUI : MonoBehaviour
    {

        /// <summary>
        /// 开始游戏按钮
        /// </summary>
        public GameObject Btn_StartGame;
        /// <summary>
        /// 设置按钮
        /// </summary>
        public GameObject Btn_SetUp;
        /// <summary>
        /// 退出游戏按钮
        /// </summary>
        public GameObject Btn_Exit;

        public GameObject SetUpPanel;

        public GameObject Btn_ShutDown;

        public GameObject inputLong;

        public GameObject inputWide;

        public GameObject Btn_Tasker;

        private bool isOpenTasker;

        private void Start()
        {
            Button Bt1 = Btn_StartGame.AddComponent<Button>();//添加按钮脚本
            Button Bt2 = Btn_SetUp.AddComponent<Button>();
            Button Bt3 = Btn_Exit.AddComponent<Button>();
            Button Bt4 = Btn_ShutDown.AddComponent<Button>();
            Button Bt5 = Btn_Tasker.AddComponent<Button>();
            EventTrigger Et1 = Btn_StartGame.AddComponent<EventTrigger>();
            isOpenTasker = true;

            //添加事件
            Bt1.onClick.AddListener(ClickStartGame);
            Bt2.onClick.AddListener(ClickSetUp);
            Bt3.onClick.AddListener(ClickExitGame);
            Bt4.onClick.AddListener(ClickShutDown);
            Bt5.onClick.AddListener(ClickTasker);

            EventTrigger.Entry entry_Enter1 = new EventTrigger.Entry();
            //鼠标离开时触发事件
            EventTrigger.Entry entry_Exit1 = new EventTrigger.Entry();
            //赋值功能标签
            entry_Enter1.eventID = EventTriggerType.PointerEnter;
            entry_Exit1.eventID = EventTriggerType.PointerExit;
            //功能标签添加事件
            entry_Enter1.callback = new EventTrigger.TriggerEvent();
            entry_Enter1.callback.AddListener(MouseEnterStartGame);
            entry_Exit1.callback = new EventTrigger.TriggerEvent();
            entry_Exit1.callback.AddListener(MouseExitStartGame);
            //加入触发事件
            Et1.triggers.Add(entry_Enter1);
            Et1.triggers.Add(entry_Exit1);
        }

        /// <summary>
        /// 点击开始游戏
        /// </summary>
        private void ClickStartGame()
        {
            EventCenter.Broadcast<int>(EventType.CHANGELOADSCENE, 1);
        }

        /// <summary>
        /// 鼠标进入Btn_StartGame
        /// </summary>
        /// <param name="arg0"></param>
        private void MouseEnterStartGame(BaseEventData arg0)
        {
            EventCenter.Broadcast<GameObject>(EventType.STOPANIM, Btn_StartGame);
        }

        /// <summary>
        /// 鼠标离开Btn_StartGame
        /// </summary>
        /// <param name="arg0"></param>
        private void MouseExitStartGame(BaseEventData arg0)
        {
            EventCenter.Broadcast<GameObject>(EventType.STARTANIM, Btn_StartGame);
        }

        /// <summary>
        /// 点击设置
        /// </summary>
        private void ClickSetUp()
        {
            EventCenter.Broadcast<GameObject, Vector3, float>(EventType.UIDOMOVE, SetUpPanel, new Vector3(300, 0, 0), 1.0f);
        }

        /// <summary>
        /// 设置中的退出并保存键
        /// </summary>
        private void ClickShutDown()
        {
            int l = Set_Str(inputLong.GetComponent<Text>().text);
            int w = Set_Str(inputWide.GetComponent<Text>().text);
            if(l < 5 && w < 5)//防止地图太小,判定出问题
            {
                l = 5;
                w = 5;
            }
            Debug.Log(inputLong.GetComponent<Text>().text + " " + inputWide.GetComponent<Text>().text);
            EventCenter.Broadcast<int, int>(EventType.CHANGEMAPSIZE, l, w);
            EventCenter.Broadcast<GameObject, Vector3, float>(EventType.UIDOMOVE, SetUpPanel, new Vector3(-660, 0, 0), 1.0f);
        }

        int Set_Str(string str)
        {
            int n = 0;
            if (str.Length == 0)
                return 5;
            for(int i = 0; i < str.Length; i++)
            {
                if(str[i] >= '0' && str[i] <= '9')
                {
                    n = n * 10 + (str[i] - '0');
                }else
                {
                    n = 5;
                    break;
                }
            }
            return n;
        }

        /// <summary>
        /// 点击退出游戏
        /// </summary>
        private void ClickExitGame()
        {
            EventCenter.Broadcast(EventType.EXITGAME);
        }
        /// <summary>
        /// 打开或者关闭任务器
        /// </summary>
        private void ClickTasker()
        {
            isOpenTasker = !isOpenTasker;
            EventCenter.Broadcast<bool>(EventType.TASKBASE, isOpenTasker);

            Text t = Btn_Tasker.transform.GetComponentInChildren<Text>();

            if(isOpenTasker)//显示（关闭任务器）
            {
                t.text = "关闭任务器";
            }else//显示（打开任务器）
            {
                t.text = "打开任务器";
            }
        }
    }
}
