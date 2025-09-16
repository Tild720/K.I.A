using KWJ.Entities;
using UnityEngine;

namespace KWJ.Interactable
{
    public interface IInteractable
    {
        public void PointerDown(Entity entity);
        public void PointerUp(Entity entity);

        public GameObject GameObject { get; }
    }
}