using UnityEngine;

namespace anogame
{
    public interface IMove2D
    {
        void OnMoveHandle(Vector2 direction, float velocity);
    }
}
