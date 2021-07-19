using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimFactory
{

    public class UIGame1 : MonoBehaviour
    {
        //建造盒子
        public GameObject BuildBox;
        //工具盒子
        public GameObject ToolsBox;
        //种植盒子
        public GameObject PlantBox;
        //返回按钮
        public GameObject Btn_Back;

        public GameObject DesPanel;
        //仓库格子
        public GameObject WareLattices;

        private GameObject Btn_Factory1;

        private GameObject Btn_Factory2;

        private GameObject Btn_Factory3;

        private GameObject Btn_Factory4;

        private GameObject Btn_Tool1;

        private GameObject Btn_Tool2;

        private GameObject Btn_Tool3;

        private GameObject Btn_Plant1;

        private GameObject Btn_Plant2;

        private GameObject Btn_Plant3;

        private GameObject Btn_Plant4;

        private GameObject Btn_Plant5;

        private GameObject Btn_DesShowDown;

        private GameObject Btn_Ware;

        private GameObject Btn_Proce;

        private GameObject FactroyWare;

        private GameObject FactroyProce;
        //工厂图片
        private GameObject Img_Fact;

        private GameObject CurrentLand;

        private GameObject Btn_Purchase;

        private GameObject FactroyPurchase;

        public void Awake()
        {
            //添加事件
            EventCenter.AddListener<GameObject>(EventType.CLICKOPENDESPANEL, ClickOpenDesPanel);

            Btn_Factory1 = BuildBox.transform.Find("Btn_Factory1").gameObject;
            Btn_Factory2 = BuildBox.transform.Find("Btn_Factory2").gameObject;
            Btn_Factory3 = BuildBox.transform.Find("Btn_Factory3").gameObject;
            Btn_Factory4 = BuildBox.transform.Find("Btn_Factory4").gameObject;
            Btn_Tool1 = ToolsBox.transform.Find("Btn_Tool1").gameObject;
            Btn_Tool2 = ToolsBox.transform.Find("Btn_Tool2").gameObject;
            Btn_Tool3 = ToolsBox.transform.Find("Btn_Tool3").gameObject;
            Btn_Plant1 = PlantBox.transform.Find("Btn_Plant1").gameObject;
            Btn_Plant2 = PlantBox.transform.Find("Btn_Plant2").gameObject;
            Btn_Plant3 = PlantBox.transform.Find("Btn_Plant3").gameObject;
            Btn_Plant4 = PlantBox.transform.Find("Btn_Plant4").gameObject;
            Btn_Plant5 = PlantBox.transform.Find("Btn_Plant5").gameObject;
            Btn_DesShowDown = DesPanel.transform.Find("Btn_DesShowDown").gameObject;
            Btn_Ware = DesPanel.transform.Find("Btn_Ware").gameObject;
            Btn_Proce = DesPanel.transform.Find("Btn_Proce").gameObject;
            FactroyWare = DesPanel.transform.Find("FactroyWare").gameObject;
            FactroyProce = DesPanel.transform.Find("FactroyProce").gameObject;
            Img_Fact = DesPanel.transform.Find("Img_Fact").gameObject;
            Btn_Purchase = DesPanel.transform.Find("Btn_Purchase").gameObject;
            FactroyPurchase = DesPanel.transform.Find("FactroyPurchase").gameObject;
        }

        public void Start()
        {
            //建造盒子按钮
            Button bt1 = Btn_Factory1.AddComponent<Button>();
            Button bt2 = Btn_Factory2.AddComponent<Button>();
            Button bt3 = Btn_Factory3.AddComponent<Button>();
            Button bt4 = Btn_Factory4.AddComponent<Button>();
            //返回按钮
            Button bt5 = Btn_Back.AddComponent<Button>();
            //工具盒子按钮
            Button bt6 = Btn_Tool1.AddComponent<Button>();
            Button bt7 = Btn_Tool2.AddComponent<Button>();
            Button bt8 = Btn_Tool3.AddComponent<Button>();
            //工厂框按钮
            Button bt9 = Btn_DesShowDown.AddComponent<Button>();
            Button bt10 = Btn_Ware.AddComponent<Button>();
            Button bt11 = Btn_Proce.AddComponent<Button>();
            Button bt12 = Btn_Purchase.AddComponent<Button>();
            //种植盒子按钮
            Button bt13 = Btn_Plant1.AddComponent<Button>();
            Button bt14 = Btn_Plant2.AddComponent<Button>();
            Button bt15 = Btn_Plant3.AddComponent<Button>();
            Button bt16 = Btn_Plant4.AddComponent<Button>();
            Button bt17 = Btn_Plant5.AddComponent<Button>();
 
            bt1.onClick.AddListener(ClickFactory1);
            bt2.onClick.AddListener(ClickFactory2);
            bt3.onClick.AddListener(ClickFactory3);
            bt4.onClick.AddListener(ClickFactory4);
            bt5.onClick.AddListener(ClickBack);
            bt6.onClick.AddListener(ClickTool1);
            bt7.onClick.AddListener(ClickTool2);
            bt8.onClick.AddListener(ClickTool3);
            bt9.onClick.AddListener(ClickDesShowDown);
            bt10.onClick.AddListener(ClickFactroyWare);
            bt11.onClick.AddListener(ClickFactroyProce);
            bt12.onClick.AddListener(ClickFactroyPurchase);
            bt13.onClick.AddListener(ClickPlant1);
            bt14.onClick.AddListener(ClickPlant2);
            bt15.onClick.AddListener(ClickPlant3);
            bt16.onClick.AddListener(ClickPlant4);
            bt17.onClick.AddListener(ClickPlant5);
        }

        private void OnDestroy()
        {
            EventCenter.RemoveListener<GameObject>(EventType.CLICKOPENDESPANEL, ClickOpenDesPanel);
        }

        /// <summary>
        /// 点击工厂1按钮
        /// </summary>
        private void ClickFactory1()
        {
            GameObject go = Instantiate(Btn_Factory1);
            go.name = "F1";
            //设置父物体
            go.transform.SetParent(BuildBox.transform);
            //移除按钮组件
            Destroy(go.transform.GetComponent<Button>());
            EventCenter.Broadcast(EventType.CLICKBUILD, go);
        }

        /// <summary>
        /// 点击工厂2按钮
        /// </summary>
        private void ClickFactory2()
        {
            GameObject go = Instantiate(Btn_Factory2);
            go.name = "F2";
            //设置父物体
            go.transform.SetParent(BuildBox.transform);
            //移除按钮组件
            Destroy(go.transform.GetComponent<Button>());
            EventCenter.Broadcast(EventType.CLICKBUILD, go);
        }

        /// <summary>
        /// 点击工厂3按钮
        /// </summary>
        private void ClickFactory3()
        {
            GameObject go = Instantiate(Btn_Factory3);
            go.name = "F3";
            //设置父物体
            go.transform.SetParent(BuildBox.transform);
            //移除按钮组件
            Destroy(go.transform.GetComponent<Button>());
            EventCenter.Broadcast(EventType.CLICKBUILD, go);
        }

        /// <summary>
        /// 点击工厂4按钮
        /// </summary>
        private void ClickFactory4()
        {
            GameObject go = Instantiate(Btn_Factory4);
            go.name = "F4";
            //设置父物体
            go.transform.SetParent(BuildBox.transform);
            //移除按钮组件
            Destroy(go.transform.GetComponent<Button>());
            EventCenter.Broadcast(EventType.CLICKBUILD, go);
        }

        /// <summary>
        /// 点击工具1按钮
        /// </summary>
        private void ClickTool1()
        {
            GameObject go = Instantiate(Btn_Tool1);
            go.name = "T5";
            //设置父物体
            go.transform.SetParent(ToolsBox.transform);
            //移除按钮组件
            Destroy(go.transform.GetComponent<Button>());
            EventCenter.Broadcast(EventType.CLICKTOOLS, go);
        }

        /// <summary>
        /// 点击工具2按钮
        /// </summary>
        private void ClickTool2()
        {
            GameObject go = Instantiate(Btn_Tool2);
            go.name = "T6";
            //设置父物体
            go.transform.SetParent(ToolsBox.transform);
            //移除按钮组件
            Destroy(go.transform.GetComponent<Button>());
            EventCenter.Broadcast(EventType.CLICKTOOLS, go);
        }

        /// <summary>
        /// 点击工具3按钮
        /// </summary>
        private void ClickTool3()
        {
            GameObject go = Instantiate(Btn_Tool3);
            go.name = "T7";
            //设置父物体
            go.transform.SetParent(ToolsBox.transform);
            //移除按钮组件
            Destroy(go.transform.GetComponent<Button>());
            EventCenter.Broadcast(EventType.CLICKTEAR, go);
        }
        /// <summary>
        /// 点击种植按钮1
        /// </summary>
        private void ClickPlant1()
        {
            GameObject go = Instantiate(Btn_Plant1);
            go.name = "P11";
            //设置父物体
            go.transform.SetParent(PlantBox.transform);
            //移除按钮组件
            Destroy(go.transform.GetComponent<Button>());
            EventCenter.Broadcast(EventType.CLICKBUILD, go);
        }
        /// <summary>
        /// 点击种植按钮2
        /// </summary>
        private void ClickPlant2()
        {
            GameObject go = Instantiate(Btn_Plant2);
            go.name = "P12";
            //设置父物体
            go.transform.SetParent(PlantBox.transform);
            //移除按钮组件
            Destroy(go.transform.GetComponent<Button>());
            EventCenter.Broadcast(EventType.CLICKBUILD, go);
        }
        /// <summary>
        /// 点击种植按钮3
        /// </summary>
        private void ClickPlant3()
        {
            GameObject go = Instantiate(Btn_Plant3);
            go.name = "P13";
            //设置父物体
            go.transform.SetParent(PlantBox.transform);
            //移除按钮组件
            Destroy(go.transform.GetComponent<Button>());
            EventCenter.Broadcast(EventType.CLICKBUILD, go);
        }
        /// <summary>
        /// 点击种植按钮4
        /// </summary>
        private void ClickPlant4()
        {
            GameObject go = Instantiate(Btn_Plant4);
            go.name = "P14";
            //设置父物体
            go.transform.SetParent(PlantBox.transform);
            //移除按钮组件
            Destroy(go.transform.GetComponent<Button>());
            EventCenter.Broadcast(EventType.CLICKBUILD, go);
        }
        /// <summary>
        /// 点击种植按钮5
        /// </summary>
        private void ClickPlant5()
        {
            GameObject go = Instantiate(Btn_Plant5);
            go.name = "P15";
            //设置父物体
            go.transform.SetParent(PlantBox.transform);
            //移除按钮组件
            Destroy(go.transform.GetComponent<Button>());
            EventCenter.Broadcast(EventType.CLICKBUILD, go);
        }


        /// <summary>
        /// 点击返回按钮
        /// </summary>
        private void ClickBack()
        {
            EventCenter.Broadcast<int>(EventType.CHANGELOADSCENE, 0);
        }

        /// <summary>
        /// 点击关闭工厂框
        /// </summary>
        public void ClickDesShowDown()
        {
            
            EventCenter.Broadcast<GameObject, char, float, float>(EventType.CHANGESHAPE, DesPanel, 'x', 0, 1.0f);
        }
        /// <summary>
        /// 点击打开工厂框
        /// </summary>
        /// <param name="go"></param>
        public void ClickOpenDesPanel(GameObject go)
        {
            CurrentLand = go;
            Img_Fact.GetComponent<Image>().sprite = go.transform.parent.GetComponent<LandType>().Build[(int)go.GetComponent<Land>().landType];
            EventCenter.Broadcast<GameObject, char, float, float>(EventType.CHANGESHAPE, DesPanel, 'x', 1, 1.0f);
            DesPanel.GetComponent<WareHouse>().land = go;
            DesPanel.GetComponent<FactroyProce>().land = go;
            EventCenter.Broadcast<int[]>(EventType.CHANGERAW, go.GetComponent<Land>().WareRaw);
            EventCenter.Broadcast<GameObject>(EventType.HIDEOBJECT, FactroyPurchase);
            EventCenter.Broadcast<GameObject>(EventType.HIDEOBJECT, FactroyProce);
            EventCenter.Broadcast<GameObject>(EventType.HIDEOBJECT, FactroyWare);
            if (go.GetComponent<Land>().landType == (LandType.LandsType)8)
            {
                Btn_Purchase.SetActive(true);
                Btn_Ware.SetActive(true);
                Btn_Proce.SetActive(false);
            }else
            {
                Btn_Purchase.SetActive(false);
                Btn_Ware.SetActive(true);
                if (go.GetComponent<Land>().landType != LandType.LandsType.OB4 && go.GetComponent<Land>().landType != (LandType.LandsType)9 && go.GetComponent<Land>().landType != (LandType.LandsType)10)
                {
                    Btn_Proce.SetActive(true);
                }else
                {
                    Btn_Proce.SetActive(false);
                }
            }
            
        }

        /// <summary>
        /// 点击仓库
        /// </summary>
        public void ClickFactroyWare()
        {
            EventCenter.Broadcast<GameObject>(EventType.SHOWOBJECT, FactroyWare);
            EventCenter.Broadcast<GameObject>(EventType.HIDEOBJECT, FactroyPurchase);
            EventCenter.Broadcast<GameObject>(EventType.HIDEOBJECT, FactroyProce);
            EventCenter.Broadcast<int[]>(EventType.CHANGERAW, CurrentLand.GetComponent<Land>().WareRaw);
        }
        /// <summary>
        /// 点击加工
        /// </summary>
        public void ClickFactroyProce()
        {
            EventCenter.Broadcast<GameObject>(EventType.SHOWOBJECT, FactroyProce);
            EventCenter.Broadcast<GameObject>(EventType.HIDEOBJECT, FactroyWare);
            EventCenter.Broadcast<GameObject>(EventType.HIDEOBJECT, FactroyPurchase);
            EventCenter.Broadcast<int[]>(EventType.CHANGESIMWARE, DesPanel.GetComponent<WareHouse>().land.GetComponent<Land>().WareRaw);
            DifProce();
        }

        /// <summary>
        /// 不同加工
        /// </summary>
        private void DifProce()
        {
            if (DesPanel.GetComponent<FactroyProce>().land.GetComponent<Land>().landType == LandType.LandsType.OB1)
            {
                //-1为原料处理
                DesPanel.GetComponent<FactroyProce>().InitRightGo(-1, DesPanel.GetComponent<DesPanel>().RawMaterial);
            }
            else if (DesPanel.GetComponent<FactroyProce>().land.GetComponent<Land>().landType == LandType.LandsType.OB3)
            {
                //-2为打包处理
                DesPanel.GetComponent<FactroyProce>().InitRightGo(-2, DesPanel.GetComponent<DesPanel>().Packaged);
            }
            else
            {
                DesPanel.GetComponent<FactroyProce>().InitRightLat();
            }
        }

        /// <summary>
        /// 点击进货
        /// </summary>
        public void ClickFactroyPurchase()
        {
            EventCenter.Broadcast<GameObject>(EventType.SHOWOBJECT, FactroyPurchase);
            EventCenter.Broadcast(EventType.RANDOMRAW);
            EventCenter.Broadcast<int[]>(EventType.CHANGERAW, DesPanel.GetComponent<WareHouse>().Raw);
            EventCenter.Broadcast<GameObject>(EventType.HIDEOBJECT, FactroyWare);
            EventCenter.Broadcast<GameObject>(EventType.HIDEOBJECT, FactroyProce);

        }

    }
}
