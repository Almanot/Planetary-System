using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanetarySystem : MonoBehaviour, IPlanetarySystem
{
    public float deltaTime;

    // List of planetary objects on the orbit of the star
    List<PlanetaryObject> planetaryobjects;
    public List<PlanetaryObject> planetaryObjects
    {
        get
        {
            return planetaryobjects;
        }
        set
        {
            planetaryobjects = planetaryObjects;
        }
    }

    [SerializeField]
    PlanetaryObject theStar;

    private void Awake()
    {
        planetaryobjects = new List<PlanetaryObject>();
        if (theStar != null) planetaryobjects.Add(theStar);
    }

    public void Update()
    {
        Time.timeScale = deltaTime;
    }

    private void FixedUpdate()
    {
        OrbitRotate();
    }

    public IEnumerator GetEnumerator()
    {
        return planetaryobjects.GetEnumerator();
    }

    public void OrbitRotate()
    {
        foreach (PlanetaryObject obj in planetaryobjects)
        {
            obj.transform.parent.Rotate(obj.orbit, obj.orbitSpeed);
        }
    }

    Vector3 SetRandomOrbit(PlanetaryObject planet)
    {
        Vector3 A = new Vector3(1,Random.Range(0, 0.1f),0);
        Vector3 B = new Vector3(0, Random.Range(0, 0.1f), 1);
        return Vector3.Cross(A, B);
    }

    float SetRandomOrbitSpeed(PlanetaryObject planet, float minimum = 0.01f, float maximum = 0.1f)
    {
        return Random.Range(minimum, maximum);
    }

    /// <summary>
    /// Get position for new planetary object, add object to list of planetary system objects
    /// </summary>
    /// <param name="planet">planetary object for calculating radius, new position and adding to list of objects</param>
    /// <param name="minimalDistance"></param>
    /// <param name="maximumDistance"></param>
    /// <returns>Vector3 of new position on radius around star</returns>
    internal void GetNewPlanetPosition(PlanetaryObject planet, float minimalDistance = 100, float maximumDistance = 1000)
    {
        // Calculate new position on the distance from previous
        GameObject lastPlanet = planetaryobjects.Last().gameObject;

        // Take biggest side radius to avoid collision
        float lastPlanetRadius = CalculateBiggestRadius(lastPlanet);
        float newPlanetRadius = CalculateBiggestRadius(planet.gameObject);

        // Return postion for new planets in random range with taking into account of old planet and new planet sizes
        float x = lastPlanet.transform.localPosition.x + lastPlanetRadius + newPlanetRadius;
        x = Random.Range(x + minimalDistance, x + maximumDistance);

        //float y = lastPlanet.transform.localPosition.y + lastPlanetRadius + newPlanetRadius;
        //y = Random.Range(y + minimalDistance, y + maximumDistance);

        //float z = lastPlanet.transform.localPosition.z + lastPlanetRadius + newPlanetRadius;
        //z = Random.Range(z + minimalDistance, z + maximumDistance);

        planet.orbit = SetRandomOrbit(planet);
        planet.orbitSpeed = SetRandomOrbitSpeed(planet);

        // Set new radius position of planet
        planet.transform.localPosition = new Vector3(x, 0, 0);

        // Add to list of planetary bodyes every object who ask new position
        planetaryobjects.Add(planet);
    }

    /// <summary>
    /// Not Completed. Calculate biggest radius from sides
    /// </summary>
    /// <param name="planet"></param>
    /// <returns>biggest radius</returns>
    float CalculateBiggestRadius(GameObject planet)
    {
        return planet.transform.localScale.x / 2;
    }
}
