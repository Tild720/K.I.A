using System.Collections.Generic;
using System.Linq;
using KWJ.Entities;
using KWJ.Food;
using UnityEngine;

namespace KWJ.Interactable.PickUpable
{
    public class Pot : PickUpable
    {
        public float CurrentAmountWater => currentAmountWater;
        [SerializeField] [Range(0, 1)] private float currentAmountWater;
        [SerializeField] private float minY = 0f;   // 시작 높이
        [SerializeField] private float maxY = 1.5f;
        [Space]
        [ColorUsage(true, false)] [SerializeField] private Color cookingColor;
        [ColorUsage(true, false)] [SerializeField] private Color baseColor;
        [SerializeField] private GameObject water;
        
        public LiquidFood LiquidFood => liquidFood;
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
            _material = water.GetComponentInChildren<MeshRenderer>().materials.ToList();
            
            foreach (var material in _material)
            {
                material.color = baseColor;
            }
        }
        

        private void Update()
        {
            if ((transform.rotation.eulerAngles.x > 90 && transform.rotation.eulerAngles.x < 270) 
                || (transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 270) )
                ResetWater();

            ApplyHeight();
        }
        
        private void ApplyHeight()
        {
            Vector3 lp = water.transform.localPosition;
            lp.z = Mathf.Lerp(minY, maxY, currentAmountWater);
            water.transform.localPosition = lp;
        }

        public void SubtractionAmountWater(float amount)
        {
            currentAmountWater -= amount;

            if (currentAmountWater == 0)
                ResetWater();
        }

        public void CreateFood()
        {
            if(currentAmountWater < 0.02f) return;
            
            liquidFood.CreateFood();
            
            if(liquidFood.IsCompleteEvaluation == false) return;
            
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
        }
        
        public override void PointerDown(Entity entity)
        {
            base.PointerDown(entity);

            if (_isPutdown == false) return;
            
            SetCanPickUp(true);
            _gasStove.SetHasPot(false);
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
        
        private void ResetWater()
        {
            foreach (var material in _material)
            {
                material.color = baseColor;
            }

            liquidFood.Reset();
            water.SetActive(false);
            currentAmountWater = 0;
        }
    }
}