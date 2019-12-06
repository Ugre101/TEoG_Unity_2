using UnityEngine;
using UnityEngine.EventSystems;

namespace StartMenuStuff
{
    public class EnlargeOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private RectTransform rectTrans;

        [SerializeField]
        private float normalWidth = 250f, normalHeight = 55f;

        [SerializeField]
        private float enlargedWidth = 280f, enlargedHeigth = 60f;

        private Vector2 normal, enlarged;

        [SerializeField]
        public void OnPointerEnter(PointerEventData eventData)
        {
            rectTrans.sizeDelta = enlarged;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            rectTrans.sizeDelta = normal;
        }

        // Start is called before the first frame update
        private void Start()
        {
            if (rectTrans == null)
            {
                rectTrans = GetComponent<RectTransform>();
            }
            normal = new Vector2(normalWidth, normalHeight);
            enlarged = new Vector2(enlargedWidth, enlargedHeigth);
            rectTrans.sizeDelta = normal;
        }
    }
}