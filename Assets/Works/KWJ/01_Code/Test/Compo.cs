using System.Collections.Generic;
using UnityEngine;

namespace KWJ.Test
{
    public class Compo : MonoBehaviour
    {
        [SerializeField] private GameObject eeee;
        private List<GameObject> m_pickUpables;
        private void Awake()
        {
            eeee = new GameObject();
            m_pickUpables.Add(eeee);
            print(m_pickUpables[0].name);
        }
    }
}