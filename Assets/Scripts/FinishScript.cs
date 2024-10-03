using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScript : MonoBehaviour
{
    public GameObject finishMenu;
    public GameObject userInterface;

    public Transform[] truckDetectors;
    public Transform[] trailerDetectors;

    public bool[] truckDetected;
    public bool[] trailerDetected;

    // Start is called before the first frame update
    void Start()
    {
        finishMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DetectTruckAndTrailer();

        if (truckDetected[0] && truckDetected[1] && truckDetected[2] && truckDetected[3] &&
            trailerDetected[0] && trailerDetected[1] && trailerDetected[2] && trailerDetected[3])
        {
            Time.timeScale = 0;
            finishMenu.SetActive(true);
            userInterface.SetActive(false);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void DetectTruckAndTrailer()
    {
        Ray[] truckRays = new Ray[4];
        Ray[] trailerRays = new Ray[4];

        RaycastHit[] truckHits = new RaycastHit[4];
        RaycastHit[] trailerHits = new RaycastHit[4];

        for (int i = 0; i < truckDetectors.Length; i++) // Detect Truck
        {
            truckRays[i] = new Ray(truckDetectors[i].position, transform.up);

            Debug.DrawRay(truckDetectors[i].position, transform.up);

            if (Physics.Raycast(truckRays[i], out truckHits[i], 1))
            {
                if (truckHits[i].transform.CompareTag("Truck"))
                {
                    truckDetected[i] = true;
                }
                else
                {
                    truckDetected[i] = false;
                }
            }
        }

        for (int i = 0; i < trailerDetectors.Length; i++) // Detect Trailer
        {
            trailerRays[i] = new Ray(trailerDetectors[i].position, transform.up);

            Debug.DrawRay(trailerDetectors[i].position, transform.up);

            if (Physics.Raycast(trailerRays[i], out trailerHits[i], 1))
            {
                if (trailerHits[i].transform.CompareTag("Trailer"))
                {
                    trailerDetected[i] = true;
                }
                else
                {
                    trailerDetected[i] = false;
                }
            }
        }
    }
}
