using UnityEngine;
using UnityEngine.EventSystems;

namespace StartMenuStuff
{
    public class EnlargeOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private RectTransform rectTrans;
        private Vector2 SetRect { set { if (rectTrans != null) { rectTrans.sizeDelta = value; }; } }
        private Vector2 normal;
        [SerializeField] private Vector2 enlarged = new Vector2();

        public void OnPointerEnter(PointerEventData eventData) => SetRect = enlarged;

        public void OnPointerExit(PointerEventData eventData) => SetRect = normal;

        // Start is called before the first frame update
        private void Start()
        {
            rectTrans = rectTrans != null ? rectTrans : GetComponent<RectTransform>();
            normal = rectTrans.sizeDelta;
            if (enlarged.x <= 0 || enlarged.y <= 0)
            {
                enlarged = normal;
            }
            //enlarged = new Vector2(enlargedWidth, enlargedHeigth);
        }

        private void OnDisable() => SetRect = normal;
    }
}