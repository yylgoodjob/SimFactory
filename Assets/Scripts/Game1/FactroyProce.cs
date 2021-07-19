using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace SimFactory
{

    public class FactroyProce : MonoBehaviour
    {
        public int[] Raw;

        public GameObject[] Lattices;//仓库格子

        public GameObject land;

        public Sprite[] RawWare;
        //左
        public GameObject leftLattice;
        //右
        public GameObject rightLattice;
        //合成
        public GameObject CreateLattice;
        //进度条
        public GameObject Progress;

        public GameObject iProgress;

        private GameObject uiGame1;

        public bool isProce;
        /// <summary>
        /// 为一参方法提供的参数
        /// </summary>
        struct LatticesCom
        {
            public GameObject left;
            public GameObject right;
            public GameObject cl;
            public int value;
            public float time;

            public LatticesCom(GameObject l, GameObject r, GameObject c, int v, float t) {
                left = l;
                right = r;
                cl = c;
                value = v;
                time = t;
            }
        };

        private void Awake()
        {
            //添加事件
            EventCenter.AddListener<int[]>(EventType.CHANGESIMWARE, ChangeSimWare);
            //初始化
            Raw = new int[6] { 0,0,0,0,0,0};
            Lattices = new GameObject[6];
            RawWare = this.GetComponent<DesPanel>().RawSprites;
            isProce = false;
            uiGame1 = GameObject.FindGameObjectWithTag("UIGame1").gameObject;
        }

        private void Start()
        {
            CreateLattices();
            CreateProgress();
        }

        private void Update()
        {
            SynthesisRaw();
        }

        private void OnDestroy()
        {
            EventCenter.RemoveListener<int[]>(EventType.CHANGESIMWARE, ChangeSimWare);
        }

        /// <summary>
        /// 创建进度条
        /// </summary>
        private void CreateProgress()
        {
            iProgress = Instantiate(Progress);
            //iProgress.transform.localPosition = new Vector3(250, 430, 0);
            iProgress.transform.position = new Vector3(638, 428, 0);
            iProgress.transform.Find("Green").transform.localScale = new Vector3(0, 1, 1);
            iProgress.transform.SetParent(this.transform.Find("FactroyProce").transform);
        }

        /// <summary>
        /// 创建格子
        /// </summary>
        private void CreateLattices()
        {
            //-460 120
            Vector3 pos = new Vector3(-60.0f, 120.0f, 0);
            for (int i = 0; i < 6; i++)
            {
                Lattices[i] = InitLattice(pos, i);
                Lattices[i].name = i.ToString();
                Lattices[i].transform.Find("Lat").GetComponent<Image>().sprite = RawWare[Raw[i]];
                Lattices[i].transform.Find("Lat").GetComponent<Lat>().Raw = Raw[i];
                pos.x += 100;
                if (i % 2 == 1)
                {
                    pos.y -= 100;
                    pos.x = -60.0f;
                }
            }

            leftLattice = InitRawLattice(new Vector3(-84.5f, 90.0f, 0));
            leftLattice.transform.Find("Lat").GetComponent<Lat>().Raw = 0;
            /*if(land.GetComponent<Land>().landType == LandType.LandsType.OB1)
            {
                //-1为原料处理
                InitRightGo(new Vector3(60, 88.5f, 0), -1, this.GetComponent<DesPanel>().RawMaterial);
            }else if(land.GetComponent<Land>().landType == LandType.LandsType.OB3)
            {
                //-2为打包处理
                InitRightGo(new Vector3(60, 88.5f, 0), -2, this.GetComponent<DesPanel>().Packaged);
            }
            else
            {
                rightLattice = InitRawLattice(new Vector3(60, 88.5f, 0));
                rightLattice.transform.Find("Lat").GetComponent<Lat>().Raw = 0;
            }*/
            rightLattice = InitRawLattice(new Vector3(60, 88.5f, 0));
            rightLattice.transform.Find("Lat").GetComponent<Lat>().Raw = 0;

            CreateLattice = InitRawLattice(new Vector3(-18, -93.5f, 0));
            CreateLattice.transform.Find("Lat").GetComponent<Lat>().Raw = 0;
        }

        /// <summary>
        /// 原料/打包处理(Raw:   -1为原料处理, -2为打包)
        /// </summary>
        public void InitRightGo(int n, Sprite sprite)
        {
            GameObject Lat = rightLattice.transform.Find("Lat").gameObject;
            Destroy(Lat.GetComponent<Button>());
            Lat.GetComponent<Lat>().Tabel = -1;
            Lat.GetComponent<Lat>().Raw = n;
            Lat.GetComponent<Image>().sprite = sprite;
        }

        public void InitRightLat()
        {
            GameObject Lat = rightLattice.transform.Find("Lat").gameObject;
            Button b = Lat.GetComponent<Button>();
            if(b == null)
            {
                Lat.AddComponent<Button>();
            }
            Lat.GetComponent<Button>().onClick.AddListener(Lat.GetComponent<Lat>().ClickProce);
            Lat.GetComponent<Lat>().Tabel = -1;
            Lat.GetComponent<Lat>().Raw = 0;
            Lat.GetComponent<Image>().sprite = RawWare[0];
        }

        private void ChangeSimWare(int[] ware)
        {
            for(int i = 0; i < 6; i++)
            {
                Raw[i] = ware[i];
                Lattices[i].transform.Find("Lat").GetComponent<Lat>().Raw = Raw[i];
                Lattices[i].transform.Find("Lat").GetComponent<Image>().sprite = RawWare[Raw[i]];
            }
        }

        private GameObject InitLattice(Vector3 pos, int n)
        {
            GameObject go = Instantiate(uiGame1.GetComponent<UIGame1>().WareLattices);

            go.transform.SetParent(GameObject.FindGameObjectWithTag("FactroyProce").transform.Find("SimWare").gameObject.transform);
            GameObject Lat = go.transform.Find("Lat").gameObject;
            Lat.AddComponent<Button>();
            Lat.AddComponent<Lat>();
            Lat.GetComponent<Lat>().Tabel = n;
            Lat.GetComponent<Button>().onClick.AddListener(Lat.GetComponent<Lat>().ClickSimWare);
            go.transform.localPosition = pos;
            return go;
        }

        private GameObject InitRawLattice(Vector3 pos)
        {
            GameObject go = Instantiate(uiGame1.GetComponent<UIGame1>().WareLattices);

            go.transform.SetParent(GameObject.FindGameObjectWithTag("FactroyProce").transform);
            GameObject Lat = go.transform.Find("Lat").gameObject;
            Lat.AddComponent<Button>();
            Lat.AddComponent<Lat>();
            Lat.GetComponent<Lat>().Tabel = -1;
            Lat.GetComponent<Button>().onClick.AddListener(Lat.GetComponent<Lat>().ClickProce);
            go.transform.localPosition = pos;
            return go;
        }

        /// <summary>
        /// 合成
        /// </summary>
        public void SynthesisRaw()
        {
            if (CreateLattice.transform.Find("Lat").GetComponent<Lat>().Raw == 0)
            {
                if ((leftLattice.transform.Find("Lat").GetComponent<Lat>().Raw >= 1 && leftLattice.transform.Find("Lat").GetComponent<Lat>().Raw <= 5) && (rightLattice.transform.Find("Lat").GetComponent<Lat>().Raw >= 1 && rightLattice.transform.Find("Lat").GetComponent<Lat>().Raw <= 5))
                {
                    isProce = true;
                    if (leftLattice.transform.Find("Lat").GetComponent<Lat>().Raw == rightLattice.transform.Find("Lat").GetComponent<Lat>().Raw)
                    {
                        return;
                    }
                    int minc = Mathf.Min(leftLattice.transform.Find("Lat").GetComponent<Lat>().Raw, rightLattice.transform.Find("Lat").GetComponent<Lat>().Raw);
                    int maxc = Mathf.Max(leftLattice.transform.Find("Lat").GetComponent<Lat>().Raw, rightLattice.transform.Find("Lat").GetComponent<Lat>().Raw);
                    int value = 0;

                    for (int i = minc - 1; i >= 0; i--)
                    {
                        value += 5 - i;
                    }
                    value += maxc - minc;

                    leftLattice.transform.Find("Lat").GetComponent<Lat>().Raw = 0;
                    leftLattice.transform.Find("Lat").GetComponent<Image>().sprite = RawWare[leftLattice.transform.Find("Lat").GetComponent<Lat>().Raw];

                    rightLattice.transform.Find("Lat").GetComponent<Lat>().Raw = 0;
                    rightLattice.transform.Find("Lat").GetComponent<Image>().sprite = RawWare[rightLattice.transform.Find("Lat").GetComponent<Lat>().Raw];

                    GoTimeProce(2.0f, leftLattice, rightLattice, CreateLattice, value);

                    //产生污水
                    land.GetComponent<SewageTreat>().DisSewage(50);
                }
                else if (rightLattice.transform.Find("Lat").GetComponent<Lat>().Raw == -1 && (leftLattice.transform.Find("Lat").GetComponent<Lat>().Raw >= 16 && leftLattice.transform.Find("Lat").GetComponent<Lat>().Raw <= 20))//原料处理
                {
                    isProce = true;
                    int value = leftLattice.transform.Find("Lat").GetComponent<Lat>().Raw - 15;

                    leftLattice.transform.Find("Lat").GetComponent<Lat>().Raw = 0;
                    leftLattice.transform.Find("Lat").GetComponent<Image>().sprite = RawWare[leftLattice.transform.Find("Lat").GetComponent<Lat>().Raw];

                    GoTimeProce(2.0f, leftLattice, null, CreateLattice, value);

                    //产生污水
                    land.GetComponent<SewageTreat>().DisSewage(70);
                }
                else if (rightLattice.transform.Find("Lat").GetComponent<Lat>().Raw == -2 && (leftLattice.transform.Find("Lat").GetComponent<Lat>().Raw >= 1 && leftLattice.transform.Find("Lat").GetComponent<Lat>().Raw <= 20))//打包处理
                {
                    isProce = true;

                    leftLattice.transform.Find("Lat").GetComponent<Lat>().Raw = 0;
                    leftLattice.transform.Find("Lat").GetComponent<Image>().sprite = RawWare[leftLattice.transform.Find("Lat").GetComponent<Lat>().Raw];

                    GoTimeProce(2.0f, leftLattice, null, CreateLattice, 21);

                    //产生污水
                    land.GetComponent<SewageTreat>().DisSewage(100);
                }
            }
        }

        /// <summary>
        /// 更新图片的方法
        /// </summary>
        /// <param name="Left">左格子</param>
        /// <param name="Right">右格子</param>
        /// <param name="CL">生成格子</param>
        /// <param name="value">生成格子更新的数值</param>
        void UpdateSprite(GameObject CL, int value) 
        {
            isProce = false;
            CL.transform.Find("Lat").GetComponent<Lat>().Raw = value;
            CL.transform.Find("Lat").GetComponent<Image>().sprite = RawWare[CL.transform.Find("Lat").GetComponent<Lat>().Raw];
        }

        /// <summary>
        /// 加工时间加载的方法
        /// </summary>
        /// <param name="time"></param>
        void GoTimeProce(float time, GameObject left, GameObject right, GameObject cl, int value)
        {
            LatticesCom Lc = new LatticesCom(left, right, cl, value, time);
            
            StartCoroutine("GoTime", Lc);
            /*float dis = 0.0f;
            GameObject go = iProgress.transform.Find("Green").gameObject;
            while(dis < time)
            {
                dis += Time.deltaTime * 0.001f;
                go.transform.localScale = new Vector3(dis / time, 1, 1);
                Debug.Log(dis / time);
            }
            go.transform.localScale = new Vector3(0, 1, 1);*/
            GameObject go = iProgress.transform.Find("Green").gameObject;
            go.transform.DOScaleX(1, time);
            go.transform.localScale = new Vector3(0, 1, 1);
        }

        private IEnumerator GoTime(LatticesCom lc)
        {
            yield return new WaitForSeconds(lc.time);

            UpdateSprite(lc.cl, lc.value);
        }

    }
}
