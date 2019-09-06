using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity 
{
    int ID { get; }
    void AddComponent<T>() where T : IComponent,new();
    void RemoveComponent<T>(T component) where T: IComponent;
    bool HasComponent(Type type);
    T GetComponent<T>() where T : class, IComponent;
}
