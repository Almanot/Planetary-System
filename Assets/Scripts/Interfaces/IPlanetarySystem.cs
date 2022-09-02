using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlanetarySystem : IEnumerable
{
    List<PlanetaryObject> planetaryObjects { get; set; }
    void Update();
}
