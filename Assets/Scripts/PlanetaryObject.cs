using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetaryObject : MonoBehaviour, IPlanetaryObject
{
    PlanetaryObject(double mass)
    {
        Mass = mass;
    }

    public MassClassEnum MassClass { get; set; }
    public double Mass { get; set; }
    float radius;
    internal Vector3 orbit;
    internal float orbitSpeed;

    internal void CalculateStats(double mass)
    {
        Mass = mass;
        MassClass = GetShapeByMass();
        radius = GetRadiusByShape();
        float diameter = radius * 2;
        transform.localScale = new Vector3(diameter, diameter, diameter);
    }

    // Definition shape-factor of planet by its mass
    MassClassEnum GetShapeByMass()
    {
        MassClassEnum massClass;

        if (Mass < 0.00001) massClass = MassClassEnum.Asteroidan;
        else if (Mass < 0.1) massClass = MassClassEnum.Mercurian;
        else if (Mass < 0.5) massClass = MassClassEnum.Subterran;
        else if (Mass < 2) massClass = MassClassEnum.Terran;
        else if (Mass < 10) massClass = MassClassEnum.Superterran;
        else if (Mass < 50) massClass = MassClassEnum.Neptunian;
        else if (Mass < 5000) massClass = MassClassEnum.Jovian;
        else throw new System.Exception("Planet oversized");

        return massClass;
    }

    float GetRadiusByShape()
    {
        float radius;

        switch (MassClass)
        {
            case MassClassEnum.Asteroidan:
                radius = Random.Range(0, 0.03f);
                break;
            case MassClassEnum.Mercurian:
                radius = Random.Range(0.03f, 0.7f);
                break;
            case MassClassEnum.Subterran:
                radius = Random.Range(0.5f, 1.2f);
                break;
            case MassClassEnum.Terran:
                radius = Random.Range(0.8f, 1.9f);
                break;
            case MassClassEnum.Superterran:
                radius = Random.Range(1.3f, 3.3f);
                break;
            case MassClassEnum.Neptunian:
                radius = Random.Range(2.1f, 5.7f);
                break;
            case MassClassEnum.Jovian:
                radius = Random.Range(3.5f, 27f);
                break;
            default: throw new System.Exception("Unexpected MassClass");
        }

        return radius;
    }
}
