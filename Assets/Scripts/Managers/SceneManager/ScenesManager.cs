using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimFactory
{

    public class ScenesManager : MonoBehaviour
    {
        public int Order;

        private void Awake()
        {
            //添加事件
            EventCenter.AddListener<int>(EventType.CHANGELOADSCENE, ChangeLoadScene);
            Order = 0;
        }

        private void OnDestroy()
        {
            EventCenter.RemoveListener<int>(EventType.CHANGELOADSCENE, ChangeLoadScene);
        }

        /// <summary>
        /// 切换场景
        /// </summary>
        /// <param name="sceneArg">切换场景编号</param>
        private void ChangeLoadScene(int sceneArg)
        {
            Order = sceneArg;
            SceneManager.LoadScene(sceneArg);
        }
    }
}
