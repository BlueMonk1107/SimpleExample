using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    int ID { get; }
    T AddComponent<T>() where T : IComponent, new();
    void RemoveComponent<T>() where T : IComponent;
    bool HasComponent<T>() where T : IComponent;
    bool HasComponent(Type type);
    T GetComponent<T>() where T : IComponent;
}
