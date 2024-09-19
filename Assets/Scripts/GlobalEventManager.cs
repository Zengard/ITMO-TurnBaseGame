using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager 
{
    public static readonly UnityEvent OnPlayerMove = new UnityEvent();
    public static readonly UnityEvent OnPlayerCanMove = new UnityEvent();

    public static void SendPlayerMove()
    {
        OnPlayerMove.Invoke();
    }

    public static void SendCanMove()
    {
        OnPlayerCanMove.Invoke();
    }
}
