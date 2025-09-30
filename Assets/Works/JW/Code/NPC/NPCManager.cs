using System;
using System.Collections;
using System.Collections.Generic;
using Code.Core.EventSystems;
using KWJ.Define;
using KWJ.Food;
using TMPro;
using UnityEngine;
using Works.JW.Events;
using Random = UnityEngine.Random;

namespace Code.NPC
{
    public class NPCManager : MonoBehaviour
    {
        [SerializeField] private Transform npcStandingPoint;
        [SerializeField] private Vector3 npcOffset;
        [SerializeField] private Vector3 npcDeleteOffset;
        [SerializeField] private float deleteTime;
        [SerializeField] private List<NPC> npcPrefabList;
        [SerializeField] private int npcCount;
        [SerializeField] private TextMeshProUGUI ui;
        [SerializeField] private float animationSpeed;
         
        private Coroutine _textCoroutine;
        private WaitForSeconds _textWait;
        private List<NPC> _npc;

        public NPC GetCurrentNPC() => _npc[0];

        private int _eatenCount;
        
        private void Awake()
        {
            _textWait = new WaitForSeconds(animationSpeed);
            
            _npc = new List<NPC>();
            _eatenCount = 0;
            
            for (int i = 0; i < npcCount; i++)
            {
                int idx = Random.Range(0,npcPrefabList.Count);
                NPC npc = Instantiate(npcPrefabList[idx],transform);
                npc.transform.rotation = Quaternion.Euler(0,180,0);
                npc.transform.position = npcStandingPoint.position;
                npc.transform.position += npcOffset * i;
                _npc.Add(npc);
            }

            _npc[0].IsFront = true;
        }

        [ContextMenu("Delete")]
        private async void DeleteFrontNPC()
        {
            if (_npc.Count <= 0) return;
            
            _eatenCount++;

            Vector3 movePoint = _npc[0].transform.position + npcDeleteOffset;
            
            _npc[0].MoveToPoint(movePoint);
            _npc[0].IsFront = false;
            NPC npc = _npc[0];
            _npc.RemoveAt(0);
            if (_npc.Count > 0)
                _npc[0].IsFront = true;
            await Awaitable.WaitForSecondsAsync(deleteTime);
            Destroy(npc.gameObject);
        }

        [ContextMenu("Refresh")]
        private void RefreshNPCPoint()
        {
            if (_npc.Count <= 0)
            {
                GameEventBus.RaiseEvent(NPCEvents.NpcLineEndEvent);
                return;
            }
            
            for (int i = 0; i < _npc.Count; i++)
            {
                Vector3 point = npcStandingPoint.position + (npcOffset * i);
                _npc[i].MoveToPoint(point);
            }
        }

        [ContextMenu("Delete")]
        private void InteractionNPC()
        {
            if (_npc.Count <= 0) return;
            
            ShowTextUI(_npc[0].Speech(LineType.RequestFood));
        }

        private void Update()
        {
            if (_npc.Count > 0)
            {
                if (_npc[0].IsMoveCompleted)
                {
                    _npc[0].RotateToPoint(transform.position);
                }
            }
        }

        [ContextMenu("KILL")]
        private void FrontNPCKill()
        {
            if (_npc.Count <= 0) return;
            
            
            NPC npc = _npc[0];
            _npc.RemoveAt(0);
            npc.IsFront = false;
            npc.NPCDead(() => RefreshNPCPoint());
            if (_npc.Count > 0)
                _npc[0].IsFront = true;
            
            GameEventBus.RaiseEvent(NPCEvents.NpcLineEndEvent);
        }

        [ContextMenu("Skep")]
        private void SkepNPC()
        {
            if (_npc.Count <= 0) return;
            
            ShowTextUI(_npc[0].Speech(LineType.Complaint));
            DeleteFrontNPC();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_npc.Count <= 0) return;
            
            
            if (other.TryGetComponent(out Food food))
            {
                if(food.FoodType == FoodType.None || food.FoodState == FoodState.None) return;
                
                GameEventBus.RaiseEvent(FoodEvents.FoodEatEvent.Init(food.FoodType, food.FoodState));
                if (_npc[0].GetFood())
                {
                    if (food.FoodState == FoodState.Good)
                    {
                        ShowTextUI(_npc[0].Speech(LineType.GoodFood), () =>
                        {
                            DeleteFrontNPC();
                            RefreshNPCPoint();
                        });
                    }
                    else if (food.FoodState == FoodState.Normal)
                    {
                         ShowTextUI(_npc[0].Speech(LineType.NormalFood), () =>
                             {
                                DeleteFrontNPC();
                                RefreshNPCPoint();
                             });
                    }
                    else
                    {
                        ShowTextUI(_npc[0].Speech(LineType.BadFood), () =>
                            {
                                DeleteFrontNPC();
                                RefreshNPCPoint();
                            });
                    }
                }
                else
                {
                    ShowTextUI(_npc[0].Speech(LineType.RequestFood));
                }
                
                Destroy(other.gameObject);
            }
        }
        
        private void ShowTextUI(string text, Action endCallback = null)
        {
            if (ui == null) return;
             
            ui.text = text;
             
            if (_textCoroutine != null)
                StopCoroutine(_textCoroutine);
             
            _textCoroutine = StartCoroutine(TextAnimationCoroutine(endCallback));
        }

        private IEnumerator TextAnimationCoroutine(Action endCallback = null)
        {
            ui.maxVisibleCharacters = 0;
            for (int i = 0; i < ui.text.Length; i++)
            {
                yield return _textWait;
                ui.maxVisibleCharacters++;
            }
            
            yield return _textWait;
            endCallback?.Invoke();
        }
    }
}