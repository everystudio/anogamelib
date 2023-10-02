using UnityEngine;
using System.Collections;

namespace anogame.dg01
{
    public interface IFreezeMovement
    {
        void OnMovementFrozen(bool isMovementFrozen);
    }
}
