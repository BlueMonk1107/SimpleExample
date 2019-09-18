using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : IEntity
{
    private Dictionary<Type,IComponent> _components = new Dictionary<Type, IComponent>();
    private Action<IEntity> _onChangeComponent;

    public Entity(Action<IEntity> onChangeComponent)
    {
        _onChangeComponent = onChangeComponent;
    }

    private static int _idCounter = -1;
    private int _id = -1;

    public int ID
    {
        get
        {
            if (_id == -1)
            {
                _id = ++_idCounter;
            }

            return _id;
        }
    }

    public T AddComponent<T>() where T : IComponent, new()
    {
        T t = default(T);
        Type type = typeof(T);
        if (!_components.ContainsKey(type))
        {
            t = new T();
            _components.Add(type,t);
            if (_onChangeComponent != null)
                _onChangeComponent(this);
        }
        else
        {
            t = (T)_components[type];
        }

        return t;
    }

    public void RemoveComponent<T>() where T : IComponent
    {
        Type type = typeof(T);
        if (_components.ContainsKey(type))
        {
            _components.Remove(type);
            if (_onChangeComponent != null)
                _onChangeComponent(this);
        }
    }

    public bool HasComponent<T>() where T : IComponent
    {
        return _components.ContainsKey(typeof(T));
    }

    public bool HasComponent(Type type)
    {
        return _components.ContainsKey(type);
    }

    public T GetComponent<T>() where T : IComponent
    {
        Type type = typeof(T);
        if (_components.ContainsKey(type))
        {
            return (T) _components[type];
        }
        else
        {
            return default(T);
        }
    }
}