using KWJ.Entities;
using UnityEngine;

namespace KWJ.Interactable
{
    public interface IInteractable
    {
        public GameObject GameObject { get; }
        public void PointerDown(Entity entity);
        public void PointerUp(Entity entity);

    }
}