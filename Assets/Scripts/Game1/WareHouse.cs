using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimFactory
{

    public class WareHouse : MonoBehaviour
    {
        public int[] Raw;

        private GameObject[] Lattices;//仓库格子

        private GameObject uiGame1;

        public GameObject land;

        public int Label;
        //原料仓库
        public Sprite[] RawWare;

        private void Awake()
        {
            //添加事件
            EventCenter.AddListener<int[]>(EventType.CHANGERAW, ChangeRaw);
            EventCenter.AddListener(EventType.RANDOMRAW, RandomRaw);
            //初始化仓库
            Raw = new int[6];
            Lattices = new GameObject[6];
            RawWare = this.GetComponent<DesPanel>().RawSprites;
            uiGame1 = GameObject.FindGameObjectWithTag("UIGame1").gameObject;
        }

        private void Start()
        {
            CreateLattices();
        }

        private void OnDestroy()
        {
            EventCenter.RemoveListener<int[]>(EventType.CHANGERAW, ChangeRaw);
            EventCenter.RemoveListener(EventType.RANDOMRAW, RandomRaw);
        }
        /// <summary>
        /// 创建格子
        /// </summary>
        private void CreateLattices()
        {
            Vector3 pos = new Vector3(-84.5f, 120.0f, 0);
            for(int i = 0; i < 6; i++)
            {
                Lattices[i] = InitLattice(pos, i);
                pos.x += 100;
                if(i % 2 == 1)
                {
                    pos.y -= 100;
                    pos.x = -84.5f;
                }
            }
        }

        private GameObject InitLattice( Vector3 pos, int n)
        {
            UIGame1 uIGame1 = uiGame1.GetComponent<UIGame1>();
            GameObject go = Instantiate(uIGame1.WareLattices);
            
            go.transform.SetParent(GameObject.Find("FactroyWare").gameObject.transform);

            go.transform.Find("Lat").gameObject.AddComponent<Button>();
            go.transform.Find("Lat").gameObject.AddComponent<Lat>();
            go.transform.Find("Lat").GetComponent<Button>().onClick.AddListener(go.transform.Find("Lat").GetComponent<Lat>().ClickWare);
            go.transform.Find("Lat").GetComponent<Lat>().Tabel = n;
            go.transform.localPosition = pos;
            return go;
        }

        /// <summary>
        /// 仓库是否已满
        /// </summary>
        /// <returns></returns>
        public bool IsEnoughWare(int[] Mol)
        {
            for(int i = 0; i < Mol.Length; i++)
            {
                if(Mol[i] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 更新原料
        /// </summary>
        public void ChangeRaw(int[] Mol)
        {
            for(int i = 0; i < Lattices.Length; i++)
            {
                Raw[i] = Mol[i];
                Lattices[i].GetComponentInChildren<Image>().sprite = RawWare[Mol[i]];
                Lattices[i].GetComponentInChildren<Lat>().Raw = Mol[i];
            }
        }

        public void RandomRaw()
        {
            for(int i = 0; i < 6; i++)
            {
                int rand = Random.Range(0, RawWare.Length - 1);
                Raw[i] = rand;
                //Debug.Log(rand);
            }
        }

        public void RemoveRaw(GameObject go)
        {

        }
    }
}
