using System.Collections;
using UnityEngine;

class LerpMovement : Movement
{
    private Coroutine _moveCoroutine;

    public override void Move(Vector2 moveDirection)
    {
        if (_moveCoroutine != null)
        {
            CoroutineManager.Stop(_moveCoroutine);
            _moveCoroutine = null;
        }
        _moveCoroutine = CoroutineManager.Start(LerpMoveCoroutine(moveDirection));
    }
    
    private IEnumerator LerpMoveCoroutine(Vector2 moveDirection)
    {
        while (rigidbody2D != null && rigidbody2D.position != moveDirection)
        {
            rigidbody2D.MovePosition(Vector2.Lerp(rigidbody2D.gameObject.transform.position, 
                moveDirection, moveSpeed * Time.deltaTime));
            yield return new WaitForEndOfFrame();
        }
    }
}