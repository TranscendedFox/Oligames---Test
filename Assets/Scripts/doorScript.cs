using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorScript : MonoBehaviour
{    

    public void changeScene(int numb)
    {
        StartCoroutine("DoorDelay", numb);
    }

    private IEnumerator DoorDelay(int numb)
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(numb, LoadSceneMode.Single);
    }


    
}
