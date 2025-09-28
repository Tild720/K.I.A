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
        }

        [ContextMenu("Test")]
        private void DeleteFrontNPC()
        {
            Vector3 dir = _npc[0].transform.right;
            Vector3 movePoint =  _npc[0].transform.position + dir * deleteDistance;
            
            _npc[0].MoveToPoint(movePoint);
        }
        
    }
}