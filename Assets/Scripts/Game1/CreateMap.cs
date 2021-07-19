using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimFactory
{

    public class CreateMap : MonoBehaviour
    {
        private GameObject land;

        private void Awake()
        {
            if(Managers.m_Managers.m_GameManager.Lands != null)//将地图置空
            {
                Managers.m_Managers.m_GameManager.Lands = new List<List<GameObject>>();
            }
            land = GameObject.FindGameObjectWithTag("Precast").GetComponent<Precast>().Land;
        }

        private void Start()
        {
            //生成地图
            EventCenter.Broadcast<GameObject, GameObject, Vector3, float>(EventType.CREATELANDS, this.gameObject, land, new Vector3(2.5f, 2.5f, 0), 1.0f);
            //随机指定
            EventCenter.Broadcast(EventType.RANDOMMAP);
        }


    }
}
