using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FeetCollider : MonoBehaviour
{

    private List<ZoneOrder.Zone> collidingZones;

    void Start()
    {
        collidingZones = new List<ZoneOrder.Zone>();
    }

    public IEnumerable<ZoneOrder.Zone> GetZoneCollisions()
    {
        return collidingZones;
    }

    public void OnTriggerEnter(Collider collider)
    {

        switch (collider.transform.tag)
        {
            case "Bois":
                collidingZones.Add(ZoneOrder.Zone.Bois);
                break;
            case "Mur":
                collidingZones.Add(ZoneOrder.Zone.Mur);
                break;
            case "Pierre":
                collidingZones.Add(ZoneOrder.Zone.Pierre);
                break;
            case "Tapis":
                collidingZones.Add(ZoneOrder.Zone.Tapis);
                break;
            case "Herbe":
                collidingZones.Add(ZoneOrder.Zone.Herbe);
                break;
            case "Terre":
                collidingZones.Add(ZoneOrder.Zone.Terre);
                break;
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        switch (collider.transform.tag)
        {
            case "Bois":
                collidingZones.Remove(ZoneOrder.Zone.Bois);
                break;
            case "Mur":
                collidingZones.Remove(ZoneOrder.Zone.Mur);
                break;
            case "Pierre":
                collidingZones.Remove(ZoneOrder.Zone.Pierre);
                break;
            case "Tapis":
                collidingZones.Remove(ZoneOrder.Zone.Tapis);
                break;
            case "Herbe":
                collidingZones.Remove(ZoneOrder.Zone.Herbe);
                break;
            case "Terre":
                collidingZones.Remove(ZoneOrder.Zone.Terre);
                break;
        }
    }
}
