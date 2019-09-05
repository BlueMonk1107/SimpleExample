using System.Collections;
using System.Collections.Generic;

public class Entity<T> : IEntity<T>  where T: IComponent
{
    private Dictionary<string,IComponent> _components = new Dictionary<string,IComponent>();
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

    public void AddComponent<T>()
    {
        
    }

    public bool HasComponent<T>()
    {
        throw new System.NotImplementedException();
    }

    public void RemoveComponent(T component)
    {
        throw new System.NotImplementedException();
    }
}