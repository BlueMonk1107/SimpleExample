using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    IList<IComponent> Components { get; }

    void AddComponent(IComponent component);
    void RemoveComponent(IComponent component);
}
