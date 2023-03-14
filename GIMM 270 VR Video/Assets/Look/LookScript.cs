using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

// Code and package created by GIMM Student Jayden Quallio

public class LookScript : MonoBehaviour
{

    // MAKE SURE THE CAMERA THAT IS BEING USED IS TAGGED WITH THE MAINCAMERA TAG

    [Tooltip("Tag of the object that you want to look at")]
    [SerializeField]
    private string gameTag = "";

    [Tooltip("Name of the scene you want to load")]
    [SerializeField]
    private string sceneName = "";

    [Tooltip("Time you to look at the object in seconds")]
    [SerializeField]
    private float maxTimer = 2f;

    [Tooltip("GameObject of the progress bar prefab")]
    [SerializeField]
    private GameObject progressBar;

    [Tooltip("GameObject of the player prefab")]
    [SerializeField]
    private GameObject player;

    // Variable to hold a refereance to the instantiated GameOnject to destroy for later
    private GameObject TempBar;

    // Timer that counts up to your max timer
    private float timer = 0f;

    // Bools to stop from instantiating multiple prefabs
    private bool isHit, isSpawn;

    private void Start()
    {
        timer = 0f;
    }

    void FixedUpdate()
    {
        // Declares a hit point of a raycast
        RaycastHit hit;

        try
        {
            Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity);
        }

        catch
        {
            Debug.LogError("Make sure the camera has the 'MainCamera' tag assaigned.");
            return;
        }


        // Creates a raycast from hit out the camera's transform forward
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            try
            {
                // Hits tag reduces time
                if (hit.collider.gameObject.CompareTag(gameTag))
                {
                    timer += Time.deltaTime;
                    isHit = true;
                    SpawnBar(hit.collider.gameObject);

                    //Debug Ray yellow when hitting desired tag
                    Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 100, Color.yellow);
                }

                // Resets timer to the max time when ray pov breaks
                else
                {
                    timer = 0;
                    isHit = false;
                    isSpawn = false;
                    Destroy(TempBar);

                    //Debug Ray red when not hitting desired tag
                    Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 100, Color.red);
                }
            }

            catch
            {
                Debug.LogError("Make sure the variables are properly assaigned.");
            }
        }

        else
        {
            timer = 0;
            isHit = false;
            isSpawn = false;
            Destroy(TempBar);

            //Debug Ray red when not hitting desired tag
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 100, Color.red);
        }

        // Time is less than 0 load new scene
        if (timer >= maxTimer)
            SceneManager.LoadScene(sceneName);

    }

    // Method to spawn the progress bar and set its value over time based off of timer
    private void SpawnBar(GameObject parent)
    {
        if (isHit && !isSpawn)
        {
            TempBar = Instantiate(progressBar, Vector3.zero, Quaternion.identity);
            TempBar.transform.position = new Vector3(parent.transform.position.x, parent.transform.position.y + parent.transform.localScale.y / 2 + 1f, parent.transform.position.z);
            TempBar.GetComponentInChildren<Slider>().maxValue = maxTimer;
            isSpawn = true;
        }

        else if (isSpawn && isHit)
        {
            TempBar.GetComponentInChildren<Slider>().value = timer;
            TempBar.transform.LookAt(player.transform);
        }
    }

}

