
using UnityEngine;
[RequireComponent(typeof(Animator))]

public class TowerAnimator : MonoBehaviour
{
    private Animator _animator;

    public delegate void CreateEventHandler();
    public event CreateEventHandler OnCreateEvent;
    

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    private void OnDisable()
    {
        PlayDestroy();
    }
    public void PlayDestroy()
    {
        _animator.SetTrigger("destroy");
    }

    private void FinishCreation()
    {
        OnCreateEvent?.Invoke();
    }
}
