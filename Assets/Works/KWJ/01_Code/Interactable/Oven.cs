using UnityEngine;

namespace KWJ.Interactable
{
    public class Oven : Kitchenware
    {
        [SerializeField] private Door door;

        protected override void Update()
        {
            base.Update();
            m_IsOn = door.IsClose ? true : false;
        }
    }
}