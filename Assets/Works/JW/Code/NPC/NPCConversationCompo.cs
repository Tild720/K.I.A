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
        BadFood
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
         [SerializeField] private TextMeshProUGUI ui;
         [SerializeField] private float animationSpeed;
         
         private Coroutine _textCoroutine;
         private WaitForSeconds _textWait;

         private void Awake()
         {
             _textWait = new WaitForSeconds(animationSpeed);
         }

         public void Speech(LineType lineType)
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
             ui.text = text;
             
             if (_textCoroutine != null)
                 StopCoroutine(_textCoroutine);
             
             _textCoroutine = StartCoroutine(TextAnimationCoroutine());
             Debug.Log(text);
         }

         private IEnumerator TextAnimationCoroutine()
         {
             ui.maxVisibleCharacters = 0;
             for (int i = 0; i < ui.text.Length; i++)
             {
                 yield return _textWait;
                 ui.maxVisibleCharacters++;
             }
         }
    }
}