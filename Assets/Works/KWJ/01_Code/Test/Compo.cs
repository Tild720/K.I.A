using System;
using KWJ.Interactable.PickUpable;
using UnityEngine;

namespace KWJ.Test
{
    public class Compo : MonoBehaviour
    {
        private void Awake()
        {
            Ingredient a =GetComponentInChildren<Ingredient>();
            print(a == null);
        }
    }
}