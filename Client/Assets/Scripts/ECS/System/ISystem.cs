using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISystem  {
    
}

public interface IInitSystem : ISystem {
    void Init();
}

public interface ILogicSystem : ISystem {
    string[] Filter();
    bool ExecuteCondition();
    void Execute();
}

public interface IUpdateSystem : ISystem {
    void Update();
}