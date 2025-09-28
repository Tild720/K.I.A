using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.NPC
{
    public class NPCManager : MonoBehaviour
    {
        [SerializeField] private Transform npcStandingPoint;
        [SerializeField] private Vector3 npcOffset;
        [SerializeField] private float deleteDistance;
        [SerializeField] private float deleteTime;
        [SerializeField] private List<NPC> npcPrefabList;
        [SerializeField] private int npcCount;

        private List<NPC> _npc;

        public NPC GetCurrentNPC() => _npc[0];
        
        private void Awake()
        {
            _npc = new List<NPC>();
            
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
            Vector3 dir = _npc[0].transform.right;
            Vector3 movePoint =  _npc[0].transform.position + dir * deleteDistance;
            
            _npc[0].MoveToPoint(movePoint);
            _npc[0].IsFront = false;
            _npc.RemoveAt(0);
            _npc[0].IsFront = true;
            await Awaitable.WaitForSecondsAsync(deleteTime);
            NPC npc = _npc[0];
            Destroy(npc.gameObject);
        }

        [ContextMenu("Refresh")]
        private void RefreshNPCPoint()
        {
            for (int i = 0; i < _npc.Count; i++)
            {
                Vector3 point = npcStandingPoint.position + (npcOffset * i);
                _npc[i].MoveToPoint(point);
            }
        }

        [ContextMenu("Delete")]
        private void InteractionNPC()
        {
            _npc[0].Speech(LineType.RequestFood);
        }
    }
}