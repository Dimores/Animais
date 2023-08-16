using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPrey
{
    Transform transform { get; } // Adicionando a propriedade transform à interface
    void Flee(IPredator predator);
}
