using System;
using System.Collections.Generic;
using System.Linq;
using KWJ.Define;
using KWJ.Entities;
using KWJ.Food;
using UnityEngine;

namespace KWJ.Interactable.PickUpable
{
    public class Pot : PickUpable
    {
        [SerializeField] [Range(0, 1)] private float currentAmountWater;
        [SerializeField] private float minY = 0f;   // 시작 높이
        [SerializeField] private float maxY = 1.5f;
        [Space]
        [ColorUsage(false, false)] [SerializeField] private Color cookingColor;
        [SerializeField] private GameObject water;
        
        [SerializeField] private LiquidFood liquidFood;

        private List<Material> _material;
        private GasStove _gasStove;
        public bool IsInitComplete => _isInitComplete;
        private bool _isInitComplete;

        public bool IsPutdown => _isPutdown;
        private bool _isPutdown;
        
        private void OnValidate()
        {
            ApplyHeight();
        }

        protected override void Awake()
        {
            base.Awake();
            
            _material = new List<Material>();
            _material = GetComponentInChildren<MeshRenderer>().materials.ToList();
        }
        

        private void Update()
        {
            if ((transform.rotation.eulerAngles.x > 90 && transform.rotation.eulerAngles.x < 270) 
                || (transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 270) )
                ResetAmountWater();

            ApplyHeight();
        }
        
        private void ApplyHeight()
        {
            Vector3 lp = water.transform.localPosition;
            lp.z = Mathf.Lerp(minY, maxY, currentAmountWater);
            water.transform.localPosition = lp;
        }


        public void CreateFood()
        {
            liquidFood.CreateFood();
            
            foreach (var material in _material)
            {
                material.color = cookingColor;
            }
        }

        public void FillWater(float amount)
        {
            if(water.activeSelf == false)
                water.SetActive(true);
            
            currentAmountWater += amount;
            
            print(amount);
        }
        
        public override void PointerDown(Entity entity)
        {
            base.PointerDown(entity);

            if (_isPutdown == false) return;
            
            SetCanPickUp(true);
            _gasStove.SetHasPot(false);
        }

        public override void PointerUp(Entity entity)
        {
            base.PointerUp(entity);
        }
        
        public void Initialized(GasStove gasStove)
        {
            _gasStove = gasStove;
            _isInitComplete = true;
        }

        public override void SetCanPickUp(bool canPickUp)
        {
            m_rigidbody.isKinematic = !canPickUp;
            _isPutdown = !canPickUp;
        }
        
        private void ResetAmountWater()
        {
            water.SetActive(false);
            currentAmountWater = 0;
        }
    }
}