using System;
using System.Collections;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.NPC
{
    [Serializable]
    public struct NPCLine
    {
        public string lineType;
        public string line;
        
        public NPCLine(string lineType, string line)
        {
            this.lineType = lineType;
            this.line = line;
        }
    }
    
    public class NPCConversationCompo : MonoBehaviour
    {
         [SerializeField] private NPCLine[] npcLines;
         [SerializeField] private TextMeshProUGUI ui;
         [SerializeField] private float animationSpeed;
         
         private Coroutine _textCoroutine;
         
         public void Speech(string lineType)
         {
             var lines = npcLines.Where(line => line.lineType == lineType).ToArray();
                 
             if (lines.Length > 0)
             {
                int idx = Random.Range(0, lines.Length);
                
                ShowTextUI(lines[idx].line);
             }
             
         }

         private void ShowTextUI(string text)
         {
             //Debug.Log(text);
             //StringBuilder << 얘도 아쉽다=.
             ui.text = text;
             //ui.maxVisibleCharacters = 10; // 나중에 다시 공부해
             StartCoroutine(TextAnimationCoroutine());
         }

         private IEnumerator TextAnimationCoroutine()
         {
             ui.maxVisibleCharacters = 0;
             for (int i = 0; i < ui.text.Length; i++)
             {
                 yield return new WaitForSeconds(animationSpeed);
                 ui.maxVisibleCharacters++;
             }
         }
    }
}