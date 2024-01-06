using System.Collections;
using UnityEngine;

public class GameEventTransferAfterDelay : MonoBehaviour
{
    [SerializeField] private GameEvent eventToRaise;
    [SerializeField] private FloatReference delay;

    public void RasieEvent()
    {
        StartCoroutine(RaiseAfterDelay());
    }

    private IEnumerator RaiseAfterDelay()
    {
        yield return delay;
        eventToRaise.Raise();
    }
}
