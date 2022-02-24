using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int patientsRescued;
    [SerializeField] private Text helicopterPatients;
    [SerializeField] private Text hospitalPatients;

    public void SetHelicopterPatientsText(int patientCount){
        helicopterPatients.text = "Soldiers in Helicopter: " + patientCount;
    }

    public void SetHospitalPatientsText(int patientCount){
        patientsRescued += patientCount;
        Debug.Log("Soldiers in Hospital: " + patientsRescued);
        hospitalPatients.text = "Soldiers in Hospital: " + patientsRescued;
    }

    void Update()
    {
        if(Input.GetKeyDown("r")){
            SceneManager.LoadScene(0);
        }
    }
}
