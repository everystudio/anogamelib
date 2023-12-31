﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame
{
    public class UIPanel : MonoBehaviour
    {
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