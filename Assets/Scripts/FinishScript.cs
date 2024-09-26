using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScript : MonoBehaviour
{
    public GameObject finishMenu;

    public BoxCollider[] truckDetectors;
    public BoxCollider[] trailerDetectors;



    // Start is called before the first frame update
    void Start()
    {
        finishMenu.SetActive(false);

        for (int i = 0; i < truckDetectors.Length; i++)
        {
            truckDetectors[i] = GetComponent<BoxCollider>();
        }

        for (int i = 0; i < trailerDetectors.Length; i++)
        {
            trailerDetectors[i] = GetComponent<BoxCollider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("player");
        }

        if (truckDetectors[0].CompareTag("Player") && truckDetectors[1].CompareTag("Player") && truckDetectors[2].CompareTag("Player") && truckDetectors[3].CompareTag("Player") &&
            trailerDetectors[0].CompareTag("Trailer") && trailerDetectors[1].CompareTag("Trailer") && trailerDetectors[2].CompareTag("Trailer") && trailerDetectors[3].CompareTag("Trailer"))
        {
            Time.timeScale = 0;
            finishMenu.SetActive(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
