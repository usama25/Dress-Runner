using TMPro;
using UnityEngine;

namespace GameAssets.FinalMomentum.Scripts
{
    
    public class FinalMomentumCube : MonoBehaviour
    {
        private Color       ColorToApply;
        bool                IsTriggered;
        static readonly int kColor = Shader.PropertyToID("_Color");
    
        public void SetColor(Color colorToApply)
        {
            ColorToApply = colorToApply;
        }
    
        public void SetText(double numberX, string text)
        {
           // transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = $"{numberX}{text}";
        }
        public void ShowColor()
        {
            SetMeshMaterialColorProperty();
        }
    
    
        private void SetMeshMaterialColorProperty()
        {
            MaterialPropertyBlock prop           = new MaterialPropertyBlock();
            var                   myMeshRenderer = transform.GetComponent<Renderer>();
            prop.SetColor(kColor, ColorToApply);
            myMeshRenderer.SetPropertyBlock(prop);
        }


        void OnTriggerEnter(Collider other)
        {
            if(IsTriggered || !other.CompareTag("Player")) return;
            IsTriggered = true;
            ShowColor();
        }

    }


}
