using System;
using System.Collections.Generic;

public class StateMachine
{
    private Dictionary<Type, IState> states = new Dictionary<Type, IState>();

    public void SetState<T>(T state) where T : IState
    {
        if (!states.ContainsKey(typeof(T)))
        {
            states[typeof(T)] = state;
            state.StateMachine = this;
            state.Enter();
        }
    }

    public bool HasState<T>() where T : IState
    {
        if (states.ContainsKey(typeof(T)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public T GetState<T>() where T : IState, new()
    {
        if (states.ContainsKey(typeof(T)))
        {
            return (T)states[typeof(T)];
        }
        else
        {
            return new T();
        }
    }
    
    public bool TryGetState<T>(out T state) where T : IState, new()
    {
        if (states.ContainsKey(typeof(T)))
        {
            state = (T)states[typeof(T)];
            return true;
        }
        else
        {
            state = new T();
            return false;
        }
    }

    public void DeleteState<T>() where T : IState
    {
        if (states.ContainsKey(typeof(T)))
        {
            states[typeof(T)].Exit();
            states.Remove(typeof(T));
        }
    }

    public void DeleteState<T>(T state) where T : IState
    {
        if (states.ContainsKey(typeof(T)))
        {
            states[typeof(T)].Exit();
            states.Remove(typeof(T));
        }
    }
}