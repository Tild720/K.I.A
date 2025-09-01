using UnityEngine;

namespace Works.Tild.Code
{
    public class CutableObject : MonoBehaviour
    {   
        [SerializeField] GameObject cuttedPrefab;
        [field:SerializeField] public int Health { get; private set; }
            
        public void Cutting(Vector3 hit)
        {
            Health--;
            if (Health > 0)
            {
                
                Instantiate(cuttedPrefab, hit, Quaternion.identity);
            }
            else
            {
                Destroy(gameObject);
            }
            
            
        }
    }
}