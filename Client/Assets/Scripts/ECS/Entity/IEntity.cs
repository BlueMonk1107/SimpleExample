using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity<T> where T: IComponent
{
    int ID { get; }
    void AddComponent<T>();
    void RemoveComponent(T component);
    bool HasComponent<T>();
}
