using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimFactory
{

    public class Global : MonoBehaviour
    {

        public static Global m_Golbal;

        private void Awake()
        {
            if (m_Golbal != null)
            {
                Destroy(this);
            }
            else
            {
                m_Golbal = this;
                DontDestroyOnLoad(this);
            }
        }
    }
}
