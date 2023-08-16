using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPredator
{
    Transform transform { get; } // Adicionando a propriedade transform à interface
    void Hunt(IPrey prey);
}
