using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class OverrideSprite : MonoBehaviour
    {
        private SpriteRenderer sr;

        private static int idMainTex = Shader.PropertyToID("_MainTex");
        private MaterialPropertyBlock block;

        [SerializeField]
        private Texture texture = null;
        public Texture OverrideTexture
        {
            get { return texture; }
            set
            {
                texture = value;
                if (block == null)
                {
                    Init();
                }
                block.SetTexture(idMainTex, texture);
            }
        }

        void Awake()
        {
            Init();
            OverrideTexture = texture;
        }

        void LateUpdate()
        {
            sr.SetPropertyBlock(block);
        }

        void OnValidate()
        {
            OverrideTexture = texture;
        }

        void Init()
        {
            block = new MaterialPropertyBlock();
            sr = GetComponent<SpriteRenderer>();
            sr.GetPropertyBlock(block);
        }
    }
}





