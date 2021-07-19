using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimFactory
{

    public class DesPanel : MonoBehaviour
    {
        //所有食品
        public Sprite[] RawSprites;

        public int[] RawPrice;
        //原料处理
        public Sprite RawMaterial;
        //打包
        public Sprite Packaged;

        private void Start()
        {
            //BUG:解决更换版本后子物体无法跟随父物体显示
            this.transform.localScale = new Vector3(0, 1, 1);
        }

    }
}
