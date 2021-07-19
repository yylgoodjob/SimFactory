using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimFactory
{

    public class SewageTreat : MonoBehaviour
    {
        public float Capacity;//污水容量

        private void Awake()
        {
            Capacity = 0;
        }

        /// <summary>
        /// 污水均分
        /// </summary>
        /// <param name="go">流出/流入物体</param>
        public void UniformSewage(GameObject go)
        {
            float sum = go.GetComponent<SewageTreat>().Capacity + this.Capacity;

            go.GetComponent<SewageTreat>().Capacity = sum / 2;
            this.Capacity = sum / 2;
        }

        /// <summary>
        /// 减少污水(污水处理厂)
        /// </summary>
        /// <param name="value">污水处理量</param>
        public void ReduceSewage(float value)
        {
            Capacity -= value;
            if(Capacity < 0)
            {
                Capacity = 0;
            }
        }

        /// <summary>
        /// 产生污水(工厂)
        /// </summary>
        public void DisSewage(float value)
        {
            Capacity += value;
        }
    }
}
