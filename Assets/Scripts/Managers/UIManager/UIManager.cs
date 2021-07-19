using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace SimFactory
{

    public class UIManager : MonoBehaviour
    {

        private void Awake()
        {
            //添加事件
            EventCenter.AddListener<GameObject>(EventType.STOPANIM, OnStopAnim);
            EventCenter.AddListener<GameObject>(EventType.STARTANIM, OnStartAnim);
            EventCenter.AddListener<GameObject>(EventType.MIRRORSTARTANIM, ONMirrorStartAnim);
            EventCenter.AddListener<GameObject>(EventType.CLICKBUILD, ClickBuildFactory);
            EventCenter.AddListener<GameObject>(EventType.CLICKTOOLS, ClickToolsBox);
            EventCenter.AddListener<GameObject>(EventType.CLICKTEAR, ClickDemBox);
            EventCenter.AddListener(EventType.EXITGAME, ClickExitGame);
            EventCenter.AddListener<GameObject, Vector3, float>(EventType.UIDOMOVE, OnMoveObject);
            EventCenter.AddListener<GameObject, char, float, float>(EventType.CHANGESHAPE, OnShapeObject);
            EventCenter.AddListener<GameObject>(EventType.SHOWOBJECT, ShowObject);
            EventCenter.AddListener<GameObject>(EventType.HIDEOBJECT, HideObject);
        }


        private void OnDestroy()
        {
            EventCenter.RemoveListener<GameObject>(EventType.STOPANIM, OnStopAnim);
            EventCenter.RemoveListener<GameObject>(EventType.STARTANIM, OnStartAnim);
            EventCenter.RemoveListener<GameObject>(EventType.MIRRORSTARTANIM, ONMirrorStartAnim);
            EventCenter.RemoveListener<GameObject>(EventType.CLICKBUILD, ClickBuildFactory);
            EventCenter.RemoveListener<GameObject>(EventType.CLICKTOOLS, ClickToolsBox);
            EventCenter.RemoveListener<GameObject>(EventType.CLICKTEAR, ClickDemBox);
            EventCenter.RemoveListener(EventType.EXITGAME, ClickExitGame);
            EventCenter.RemoveListener<GameObject, Vector3, float>(EventType.UIDOMOVE, OnMoveObject);
            EventCenter.RemoveListener<GameObject, char, float, float>(EventType.CHANGESHAPE, OnShapeObject);
            EventCenter.RemoveListener<GameObject>(EventType.SHOWOBJECT, ShowObject);
            EventCenter.RemoveListener<GameObject>(EventType.HIDEOBJECT, HideObject);
        }

        /// <summary>
        /// 停止动画
        /// </summary>
        /// <param name="go">停止动画的物体</param>
        public void OnStopAnim(GameObject go)
        {
            go.GetComponent<Animator>().speed = 0.0f;
        }

        /// <summary>
        /// 播放动画
        /// </summary>
        /// <param name="go">开始动画的物体</param>
        public void OnStartAnim(GameObject go)
        {
            go.GetComponent<Animator>().speed = 1.0f;
        }

        /// <summary>
        /// 显示物体
        /// </summary>
        /// <param name="go">物体</param>
        public void ShowObject(GameObject go)
        {
            go.SetActive(true);
        }

        /// <summary>
        /// 隐藏物体
        /// </summary>
        /// <param name="go">物体</param>
        public void HideObject(GameObject go)
        {
            go.SetActive(false);
        }

        /// <summary>
        /// 反向播放动画
        /// </summary>
        /// <param name="go"></param>
        public void ONMirrorStartAnim(GameObject go)
        {
            go.GetComponent<Animator>().speed = -1.0f;
        }

        /// <summary>
        /// 移动物体
        /// </summary>
        /// <param name="go">该物体</param>
        /// <param name="endpos">结束位置</param>
        /// <param name="">时长</param>
        public void OnMoveObject(GameObject go, Vector3 endpos, float time)
        {
            go.transform.DOLocalMove(endpos, time);
        }
        /// <summary>
        /// 改变物体形状
        /// </summary>
        /// <param name="go">物体</param>
        /// <param name="c">改变方向</param>
        /// <param name="dis">改变大小</param>
        /// <param name="time">时间</param>
        public void OnShapeObject(GameObject go, char c, float dis, float time)
        {
            if(c == 'x')
            {
                go.transform.DOScaleX(dis, time);
            }else if(c == 'y')
            {
                go.transform.DOScaleY(dis, time);
            }
        }

        /// <summary>
        /// 点击建造工厂
        /// </summary>
        /// <param name="go">工厂物体</param>
        public void ClickBuildFactory(GameObject go)
        {
            //改成附着建造事件
            Managers.m_Managers.m_GameManager.mouse = MouseType.mouseType.BUILDTYPE;

            Managers.m_Managers.m_GameManager.pointer = go;
            //Debug.Log(this.transform.GetComponent<GameManager>().pointer + " " + Managers.m_Managers.m_GameManager.pointer);
        }

        /// <summary>
        /// 点击工具盒子(建造)
        /// </summary>
        /// <param name="go">工具物体</param>
        public void ClickToolsBox(GameObject go)
        {
            Managers.m_Managers.m_GameManager.mouse = MouseType.mouseType.BUILDTYPE;

            Managers.m_Managers.m_GameManager.pointer = go;
        }


        /// <summary>
        /// 点击工具盒子(拆除)
        /// </summary>
        /// <param name="go"></param>
        public void ClickDemBox(GameObject go)
        {
            Managers.m_Managers.m_GameManager.mouse = MouseType.mouseType.TEARTYPE;

            Managers.m_Managers.m_GameManager.pointer = go;
        }

        /// <summary>
        /// 点击退出游戏
        /// </summary>
        public void ClickExitGame()
        {
            Application.Quit();
        }
    }
}
