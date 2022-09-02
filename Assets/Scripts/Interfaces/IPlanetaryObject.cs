using System.Collections;
using System.Collections.Generic;

public interface IPlanetaryObject
{
    MassClassEnum MassClass { get; set; }
    double Mass { get; set; }
}