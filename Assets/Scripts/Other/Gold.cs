using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimFactory {

    public class Gold : MonoBehaviour {

        /// <summary>
        /// 当前游戏金币
        /// </summary>
        public int gold;


        private void Awake()
        {
            //添加事件
            EventCenter.AddListener<int>(EventType.CHANGEGOLD, ChangeGold);
            gold = Managers.m_Managers.m_GameManager.MapLong * Managers.m_Managers.m_GameManager.MapWide * 8;
        }

        private void Start()
        {
            ChangeGold(0);
        }
        private void OnDestroy()
        {
            EventCenter.RemoveListener<int>(EventType.CHANGEGOLD, ChangeGold);
        }

        /// <summary>
        /// 判断金币是否足够
        /// </summary>
        /// <param name="g">金币</param>
        /// <param name="value">差值</param>
        /// <returns></returns>
        public static bool IsEnoughGold(int g, int value)
        {
            if(g >= value)
            {
                return true;
            }else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新金币
        /// </summary>
        /// <param name="value">更新金币的值</param>
        public void ChangeGold(int value)
        {
            Text text = this.GetComponent<Text>();
            this.gold += value;
            text.text = "Gold:" + gold.ToString();
        }
    }
}
