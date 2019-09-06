using System;
using System.Collections;
using System.Collections.Generic;

public class Entity : IEntity 
{
    private Dictionary<Type,IComponent> _components = new Dictionary<Type,IComponent>();
    private int _idCounter = -1;
    private int _id = -1;
    public int ID
    {
        get
        {
            if(_id == -1)
            {
                _id = ++_idCounter;
            }
            return _id;
        }
    }

    public void AddComponent<T>() where T: IComponent,new()
    {
        T t = new T();
        if (!_components.ContainsKey(t.GetType()))
        {
            _components.Add(t.GetType(),t);
        }
    }

    public bool HasComponent(Type type)
    {
        return _components.ContainsKey(type);
    }

    public T GetComponent<T>() where T: class,IComponent
    {
        Type type = typeof(T);
        if (_components.ContainsKey(type))
        {
            return _components[type] as T;
        }
        else
        {
            return null;
        }
    }

    public void RemoveComponent<T>(T component) where T: IComponent
    {
        Type type = typeof(T);
        if (_components.ContainsKey(type))
        {
            _components.Remove(type);
        }
    }
}