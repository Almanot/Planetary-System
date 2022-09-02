using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetarySystemFactory : MonoBehaviour, IPlanetarySystemFactory
{
    [SerializeField]
    PlanetarySystem planetarySystem;
    [SerializeField]
    GameObject prefab;
    public int maximumPlanets = 10;
    public double totalMass = 100;

    private void Start()
    {
        Create(totalMass);
    }

    public void Create(double totalMass)
    {
        // Generating random number of planets to add some random in generation system
        int planetsCount = Random.Range(0, maximumPlanets);

        // To generate jovian planets as often as asteroid we disribute random value between amount of planets.
        for (;  planetsCount > 0; planetsCount--)
        {
            double currentPlanetMass = totalMass - Random.Range(0, (float)totalMass);

            // Add object to planetary system with some specification.
            GameObject planetRoot = new GameObject();
            planetRoot.transform.parent = planetarySystem.transform;
            GameObject planet = Instantiate(prefab, planetRoot.transform, false);
            planet.GetComponent<PlanetaryObject>().CalculateStats(currentPlanetMass);
            planetarySystem.GetNewPlanetPosition(planet.GetComponent<PlanetaryObject>());
        }
    }
}
