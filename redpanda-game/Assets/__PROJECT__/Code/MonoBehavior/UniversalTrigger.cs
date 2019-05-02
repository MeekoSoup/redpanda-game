using UnityEngine;
using UnityEngine.Events;

public class UniversalTrigger : MonoBehaviour
{

    public UnityEvent onEnter;
    public UnityEvent onStay;
    public UnityEvent onExit;

    private void OnTriggerEnter(Collider other)
    {
        if (onEnter == null)
            return;

        onEnter.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {

        if (onStay == null)
            return;

        onStay.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {

        if (onExit == null)
            return;

        onExit.Invoke();
    }
}
