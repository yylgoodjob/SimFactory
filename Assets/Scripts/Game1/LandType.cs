using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimFactory
{
    public class LandType : MonoBehaviour
    {
        public enum LandsType
        {
            EMPTY,//空地
            OB1,//测试
            OB2,
            OB3,
            OB4,
            OB5,
            OB6,
            OB7,
            ENTRANCE,//原料入口
            EXPORT,//出货口
            EMPLOYHOME,//员工住宿
            PLANT1,//植物1
            PLANT2,//植物2
            PLANT3,//植物3
            PLANT4,//植物4
            PLANT5,//植物5
        }
        public enum IsJob
        {
            NO,
            YES,
        }
        //建筑
        public Sprite[] Build;
        //建筑价格
        public int[] BuildPrice;
        public int Label;

    }
}
