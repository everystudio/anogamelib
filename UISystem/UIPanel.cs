using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace anogame
{
    public class UIPanel : MonoBehaviour
    {
        public bool IsVisible { get; private set; }


        public async Task Show(float dulation = 0.25f)
        {
            if (IsVisible)
            {
                return;
            }
            await show(dulation);
            IsVisible = true;
        }

        protected virtual async Task show(float dulation)
        {
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }

            // dulation秒かけて透明度を1に変化させる
            // ただし、すでに設定されている透明度がある場合はその値から変化させる
            float time = canvasGroup.alpha * dulation;

            while (time < dulation)
            {
                time += Time.deltaTime;
                canvasGroup.alpha = time / dulation;
                // awaitで1Frame待たせる
                await Task.Yield();
            }
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;

        }

        public async Task Hide(float dulation = 0.25f)
        {
            if (!IsVisible)
            {
                return;
            }
            await hide(dulation);
            IsVisible = false;
        }

        protected virtual async Task hide(float dulation)
        {
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                return;
            }

            // dulation秒かけて透明度を0に変化させる
            // ただし、すでに設定されている透明度がある場合はその値から変化させる
            float time = dulation * (1 - canvasGroup.alpha);
            while (time < dulation)
            {
                time += Time.deltaTime;
                canvasGroup.alpha = 1 - time / dulation;
                // awaitで1Frame待たせる
                await Task.Yield();
            }

            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }


        public void Initialize()
        {
            initialize();
        }

        protected virtual void initialize() { }

        public void Shutdown()
        {
            shutdown();
        }

        protected virtual void shutdown() { }
    }
}