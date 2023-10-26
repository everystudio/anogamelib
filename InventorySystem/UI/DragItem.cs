using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace anogame.inventory
{
    public class DragItem<T> : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler where T : class
    {
        // PRIVATE STATE
        private Vector2 startPosition;
        private Transform originalParent;

        private RectTransform rectTransform;
        private Canvas parentCanvas;            //あまり好ましくないけどね

        private IDragContainer<T> myContainer;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            parentCanvas = GetComponentInParent<Canvas>();

            //Debug.Log(parentCanvas);

            myContainer = GetComponentInParent<IDragContainer<T>>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            startPosition = rectTransform.anchoredPosition;
            originalParent = transform.parent;

            transform.SetParent(parentCanvas.transform, true);
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / GetComponentInParent<Canvas>().scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            transform.SetParent(originalParent, true);
            rectTransform.anchoredPosition = startPosition;

            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                var target = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<IDragContainer<T>>();
                if (target != null && target != myContainer)
                {
                    //Debug.Log("どっかのコンテナ");
                    DropItemContainer(target);
                }
            }
        }

        private void DropItemContainer(IDragContainer<T> target)
        {
            // 同じ場合は返る
            if (object.ReferenceEquals(target, myContainer))
            {
                Debug.Log("同じ判定らしい");
                return;
            }

            //Debug.Log(target);

            // 相手側がなにもない場合はApplyするだけ
            if (target.GetItem() == null)
            {
                //Debug.Log("apply");
                ApplyItemSource(target);
            }
            else
            {
                //Debug.Log("swap");
                SwapItemSource(myContainer, target);
            }
        }

        private void ApplyItemSource(IDragContainer<T> target)
        {
            var item = myContainer.GetItem();
            var amount = myContainer.GetAmount();
            if (item == null || amount <= 0)
            {
                Debug.Log("aaa");
                return;
            }

            /*
            var maxAcceptable = myContainer.MaxAcceptable(item);
            if (maxAcceptable <= 0)
            {
                Debug.Log("bbb");
                return;
            }
            */

            myContainer.Clear();
            target.Set(item, amount);
        }

        private void SwapItemSource(IDragContainer<T> source, IDragContainer<T> target)
        {
            var sourceItem = source.GetItem();
            var sourceAmount = source.GetAmount();
            var targetItem = target.GetItem();
            var targetAmount = target.GetAmount();

            if (sourceItem == null || sourceAmount <= 0)
            {
                return;
            }

            if (targetItem == null || targetAmount <= 0)
            {
                return;
            }

            // 今のところはAcceptableでの量溢れは計算に入れない
            source.Clear();
            target.Clear();

            source.Set(targetItem, targetAmount);
            target.Set(sourceItem, sourceAmount);
        }

        private int CalculateTakeBack(T moveItem, int moveAmount, IDragContainer<T> source, IDragContainer<T> target)
        {

            int takeBackNumber = 0;

            var targetMaxAcceptable = target.MaxAcceptable(moveItem);
            if (targetMaxAcceptable < moveAmount)
            {
                takeBackNumber = moveAmount - targetMaxAcceptable;
            }
            return takeBackNumber;


        }



    }
}