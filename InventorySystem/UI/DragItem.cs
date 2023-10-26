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
                //Debug.Log("aaa");
                return;
            }

            var maxAcceptable = target.MaxAcceptable(item);
            var transferAmount = Mathf.Min(amount, maxAcceptable);
            if (transferAmount <= 0)
            {
                //Debug.Log("bbb");
                return;
            }

            myContainer.Remove(transferAmount);
            target.Add(item, transferAmount);
        }

        private void SwapItemSource(IDragContainer<T> source, IDragContainer<T> target)
        {
            var sourceItem = source.GetItem();
            var sourceAmount = source.GetAmount();
            var targetItem = target.GetItem();
            var targetAmount = target.GetAmount();

            // 一旦中身を空にする
            source.Clear();
            target.Clear();

            int sourceTakebackAmount = CalculateTakeBack(sourceItem, sourceAmount, source, target);
            int targetTakebackAmount = CalculateTakeBack(targetItem, targetAmount, target, source);

            // takebackが発生する場合は元に戻す変数を用意する
            int calcedSourceAmount = 0;
            if (0 < sourceTakebackAmount)
            {
                source.Add(sourceItem, sourceTakebackAmount);
                calcedSourceAmount = sourceAmount - sourceTakebackAmount;
            }
            int calcedTargetAmount = 0;
            if (0 < targetTakebackAmount)
            {
                target.Add(targetItem, targetTakebackAmount);
                calcedTargetAmount = targetAmount - targetTakebackAmount;
            }

            // ダメだった場合
            if (source.MaxAcceptable(targetItem) < calcedTargetAmount ||
                target.MaxAcceptable(sourceItem) < calcedSourceAmount)
            {
                // どちらかのアイテムが受け入れられない場合は元に戻す
                source.Add(sourceItem, sourceAmount);
                target.Add(targetItem, targetAmount);
                return;
            }

            if (0 < calcedTargetAmount)
            {
                source.Add(targetItem, calcedTargetAmount);
            }
            if (0 < calcedSourceAmount)
            {
                target.Add(sourceItem, calcedSourceAmount);
            }
            // これで良い気がするが、とりあえず残しておく
            //source.Add(targetItem, targetAmount);
            //target.Add(sourceItem, sourceAmount);
        }

        private int CalculateTakeBack(
            T moveItem, int moveAmount,
            IDragContainer<T> sourceContainer, IDragContainer<T> targetContainer)
        {

            int takeBackNumber = 0;
            var targetMaxAcceptable = targetContainer.MaxAcceptable(moveItem);

            if (targetMaxAcceptable < moveAmount)
            {
                takeBackNumber = moveAmount - targetMaxAcceptable;

                // ここ良くない。テークバックの数字だけを計算すれるはずなのに、受け入れ先のことまで木にしている
                var sourceMaxAcceptable = sourceContainer.MaxAcceptable(moveItem);
                if (sourceMaxAcceptable < takeBackNumber)
                {
                    // takebackの数を受け入れられるかどうかは別のチェックメソッドを用意するべき
                    return 0;
                }
            }
            return takeBackNumber;


        }



    }
}