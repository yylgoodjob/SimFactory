using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimFactory
{

    public class Lat : MonoBehaviour
    {
        public int Tabel;

        public int Raw;

        private GameObject uiGame1;

        private GameObject DesPanel;

        private int[] rawPrice;

        private GameObject gold;

        private void Awake()
        {
            uiGame1 = GameObject.FindGameObjectWithTag("UIGame1").gameObject;
            DesPanel = uiGame1.transform.Find("DesPanel").gameObject;
            gold = GameObject.FindGameObjectWithTag("Gold").gameObject;
            rawPrice = DesPanel.GetComponent<DesPanel>().RawPrice;
        }

        /// <summary>
        /// 点击方块(Purchase)
        /// </summary>
        public void ClickLat()
        {
            //检查仓库是否已满
            if(!DesPanel.GetComponent<WareHouse>().IsEnoughWare(DesPanel.GetComponent<WareHouse>().land.GetComponent<Land>().WareRaw))
            {
                int Money = gold.GetComponent<Gold>().gold;
                if(Money < rawPrice[Raw])
                {
                    //Debug.Log("买不起");
                    return;
                }
                int mo = rawPrice[Raw];
                GameObject go = DesPanel.GetComponent<WareHouse>().land;
                for(int i = 0; i < go.GetComponent<Land>().WareRaw.Length; i++)
                {
                    if(go.GetComponent<Land>().WareRaw[i] == 0)
                    {
                        go.GetComponent<Land>().WareRaw[i] = Raw;
                        break;
                    }
                }
                DesPanel.GetComponent<WareHouse>().ChangeRaw(go.GetComponent<Land>().WareRaw);
                ChangeLat();
                //更新价格
                EventCenter.Broadcast<int>(EventType.CHANGEGOLD, -mo);
            }else
            {
                //Debug.Log("不能买");
            }

        }
        //更新方块的方法
        private void ChangeLat()
        {
            Raw = Random.Range(16, 20);
            this.GetComponent<Image>().sprite = DesPanel.GetComponent<FactroyPurchase>().Rawsprites[Raw];
        }
        /// <summary>
        /// 点击方块(Ware)
        /// </summary>
        public void ClickWare()
        {
            if(this.Raw != 0)
            {
                DesPanel.GetComponent<WareHouse>().land.GetComponent<Land>().WareRaw[Tabel] = 0;
                Managers.m_Managers.m_GameManager.mouse = MouseType.mouseType.INTO;
                GameObject go = Instantiate(this.gameObject);
                //go.AddComponent<Image>();
                //go.GetComponent<Image>().sprite = DesPanel.GetComponent<FactroyPurchase>().Rawsprites[this.Raw];
                //移除按钮
                Destroy(go.GetComponent<Button>());
                go.transform.SetParent(uiGame1.transform);
                Managers.m_Managers.m_GameManager.pointer = go;
                this.Raw = 0;
                this.GetComponent<Image>().sprite = DesPanel.GetComponent<FactroyPurchase>().Rawsprites[Raw];
                DesPanel.transform.localScale = new Vector3(0, 1, 1);
            }
        }

        public void MoveClick()
        {
            if (this.Raw != 0)
            {
                if(Tabel != -1)
                DesPanel.GetComponent<WareHouse>().land.GetComponent<Land>().WareRaw[Tabel] = 0;
                Managers.m_Managers.m_GameManager.mouse = MouseType.mouseType.INTO;
                GameObject go = Instantiate(this.gameObject);
                Destroy(go.GetComponent<Button>());
                go.GetComponent<Image>().raycastTarget = false;
                go.transform.SetParent(uiGame1.transform);
                Managers.m_Managers.m_GameManager.pointer = go;
                this.Raw = 0;
                this.GetComponent<Image>().sprite = DesPanel.GetComponent<FactroyProce>().RawWare[Raw];
            }
            else if (this.Raw == 0 && Managers.m_Managers.m_GameManager.pointer != null)
            {
                //更新模拟仓库
                this.Raw = Managers.m_Managers.m_GameManager.pointer.GetComponent<Lat>().Raw;
                this.GetComponent<Image>().sprite = DesPanel.GetComponent<FactroyProce>().RawWare[Raw];
                Managers.m_Managers.m_GameManager.mouse = MouseType.mouseType.EMPTY;
                Destroy(Managers.m_Managers.m_GameManager.pointer);
                Managers.m_Managers.m_GameManager.pointer = null;
                
            }
        }

        /// <summary>
        /// 点击方块(SimWare)
        /// </summary>
        public void ClickSimWare()
        {
            //更新仓库
            /*DesPanel.GetComponent<WareHouse>().land.GetComponent<Land>().WareRaw[Tabel] = this.Raw;
            EventCenter.Broadcast<int[]>(EventType.CHANGERAW, DesPanel.GetComponent<WareHouse>().land.GetComponent<Land>().WareRaw);
            MoveClick();*/
            GameObject point = Managers.m_Managers.m_GameManager.pointer;
            if(point == null)//当鼠标为空时
            {
                if(Raw == 0)
                {
                    return;
                }
                GameObject go = Instantiate(this.gameObject);
                Destroy(go.GetComponent<Button>());
                go.GetComponent<Image>().raycastTarget = false;
                go.transform.SetParent(uiGame1.transform);
                Managers.m_Managers.m_GameManager.pointer = go;
                Managers.m_Managers.m_GameManager.mouse = MouseType.mouseType.INTO;
                this.Raw = 0;
                this.GetComponent<Image>().sprite = DesPanel.GetComponent<FactroyProce>().RawWare[Raw];
            }
            else//当鼠标不为空时
            {
                if(Raw == 0)
                {
                    this.Raw = Managers.m_Managers.m_GameManager.pointer.GetComponent<Lat>().Raw;
                    this.GetComponent<Image>().sprite = DesPanel.GetComponent<FactroyProce>().RawWare[Raw];
                    Managers.m_Managers.m_GameManager.mouse = MouseType.mouseType.EMPTY;
                    Destroy(Managers.m_Managers.m_GameManager.pointer);
                    Managers.m_Managers.m_GameManager.pointer = null;
                }
            }

            DesPanel.GetComponent<WareHouse>().land.GetComponent<Land>().WareRaw[Tabel] = this.Raw;

            //EventCenter.Broadcast<int[]>(EventType.CHANGELANDWARERAW, DesPanel.transform.GetComponent<FactroyProce>().Raw);
        }

        /// <summary>
        /// 点击方块(Proce)
        /// </summary>
        public void ClickProce()
        {
            if(!DesPanel.GetComponent<FactroyProce>().isProce)//当前状态没有加工
            {
                MoveClick();
            }
            
        }
    }
}
