using UnityEngine;

namespace UI
{
    public class UIPanel: MonoBehaviour
    {
        
        protected void ShowPanel(GameObject go)
        {
            go.SetActive(true);
        }
        
        protected void HidePanel(GameObject go)
        {
            go.SetActive(false);
        }
    }
}