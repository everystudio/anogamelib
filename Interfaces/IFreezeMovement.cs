using UnityEngine;
using System.Collections;

namespace anogame
{
    public interface IFreezeMovement
    {
        void OnMovementFrozen(bool isMovementFrozen);
    }
}
