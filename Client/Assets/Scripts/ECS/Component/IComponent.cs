using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IComponent
{
    bool ValueChanged { get; set; }
}