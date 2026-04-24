using UnityEngine;
using UnityEngine.UI;

public class TurretSpot : MonoBehaviour
{
    public Transform spawnPoint;
    private GameObject currentTurret;
    private Image plusImage;

    public bool HasTurret() {  return currentTurret != null; }

    void Awake()
    {
        plusImage = GetComponent<Image>(); // gets the + image
    }

    // Called when player taps the "+" button
    public void OnSpotClicked()
    {
        Debug.Log("Spot clicked");
        if (currentTurret != null)
        {
            Debug.Log("Removing turret");
            RemoveTurret();
            return;
        }

        TurretUIManager.instance.SelectSpot(this);
    }
    public void PlaceTurret(GameObject turretPrefab) 
    {
        if (currentTurret != null)
        {
            Debug.Log("Spot Occupied");
            return;
        }
        currentTurret = Instantiate(turretPrefab, spawnPoint.position, Quaternion.identity);


        if (plusImage != null)
        {
            plusImage.color = new Color(1, 1, 1, 0f);
        }
    }

    public void RemoveTurret()
    {
        if(currentTurret != null)
        {
            Destroy(currentTurret);
            currentTurret = null;
        }
        if (plusImage != null)
        {
            plusImage.color = new Color(1, 1, 1, 1f);
        }
    }

}
