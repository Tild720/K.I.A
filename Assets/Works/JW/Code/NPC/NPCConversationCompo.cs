using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.NPC
{
    public enum LineType
    {
        RequestFood, //음식 내놔
        GoodFood,
        NormalFood,
        BadFood,
        Complaint
    }
    
    [Serializable]
    public struct NPCLine
    {
        public LineType lineType;
        public string line;
        
        public NPCLine(LineType lineType, string line)
        {
            this.lineType = lineType;
            this.line = line;
        }
    }
    
    public class NPCConversationCompo : MonoBehaviour
    {
         [SerializeField] private NPCLine[] npcLines;
        
         public string Speech(LineType lineType)
         {
             var lines = npcLines.Where(line => line.lineType == lineType).ToArray();
                 
             if (lines.Length > 0)
             {
                int idx = Random.Range(0, lines.Length);
                return lines[idx].line;
                //ShowTextUI(lines[idx].line);
             }

             return " ";
         }

         
    }
}