using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimFactory
{
    public class FactroyPurchase : MonoBehaviour
    {
        public int[] Raw;

        public Sprite[] Rawsprites;

        private GameObject uiGame1;

        private GameObject[] Lattices;

        


        private void Awake()
        {
            Lattices = new GameObject[6];
            Raw = new int[6];
            Rawsprites = this.GetComponent<DesPanel>().RawSprites;
            uiGame1 = GameObject.FindGameObjectWithTag("UIGame1").gameObject;
        }

        private void Start()
        {
            RandomPurchaseRaw();
            CreateLattices();
        }

        /// <summary>
        /// 创建格子
        /// </summary>
        private void CreateLattices()
        {
            Vector3 pos = new Vector3(-84.5f, 120.0f, 0);
            for (int i = 0; i < 6; i++)
            {
                Lattices[i] = InitLattice(pos, i);
                Lattices[i].name = i.ToString();
                Lattices[i].transform.Find("Lat").GetComponent<Image>().sprite = Rawsprites[Raw[i]];
                Lattices[i].transform.Find("Lat").GetComponent<Lat>().Raw = Raw[i];
                pos.x += 100;
                if (i % 2 == 1)
                {
                    pos.y -= 100;
                    pos.x = -84.5f;
                }
            }
        }

        private GameObject InitLattice(Vector3 pos, int n)
        {
            UIGame1 uIGame1 = uiGame1.GetComponent<UIGame1>();
            GameObject go = Instantiate(uIGame1.WareLattices);

            go.transform.SetParent(GameObject.FindGameObjectWithTag("FactroyPurchase").gameObject.transform);
            GameObject Lat = go.transform.Find("Lat").gameObject;
            Lat.AddComponent<Button>();
            Lat.AddComponent<Lat>();
            Lat.GetComponent<Lat>().Tabel = n;
            Lat.GetComponent<Button>().onClick.AddListener(Lat.GetComponent<Lat>().ClickLat);
            go.transform.localPosition = pos;
            return go;
        }

        /// <summary>
        /// 随机物品
        /// </summary>
        private void RandomPurchaseRaw()
        {
            for(int i = 0; i < 6; i++)
            {
                Raw[i] = Random.Range(16, 20);
                
            }
        }


        private int Set_Str(string str)
        {
            int n = 0;
            for(int i = 0; i < str.Length; i++)
            {
                n = n * 10 + (str[i] - '0');
            }
            return n;
        }
       


    }
}
