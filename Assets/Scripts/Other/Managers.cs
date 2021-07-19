using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimFactory {
    public class Managers : MonoBehaviour {

        public static Managers m_Managers;

        public GameManager m_GameManager;

        public UIManager m_UIManager;

        public ScenesManager m_ScenesManager;

        private void Awake()
        {
            if(m_Managers != null)
            {
                return;
            }else
            {
                m_Managers = this;
            }
            

            m_GameManager = this.transform.GetComponent<GameManager>();
            m_UIManager = this.transform.GetComponent<UIManager>();
            m_ScenesManager = this.transform.GetComponent<ScenesManager>();
        }
    }
}
