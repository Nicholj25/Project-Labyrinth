using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;

public class LoadRoom : MonoBehaviour
{
    // Room we want to transition to
    [SerializeField] public string roomToLoad;
    // Scene with loading screen
    public string loadingScreen = "Loading Screen";
    [SerializeField] private bool loadOnClick;
    public GameObject LoadingPanel;
    public Slider LoadingBar;

    public void Start()
    {
        if (!loadOnClick)
        {
            enabled = false;
        }

    }

    async void OnMouseDown()
    {
        if (loadOnClick)
        {
            Debug.Log("onclick");
            LoadLevel(loadingScreen);
            await WaitOneSecondAsync();
            LoadLevel(roomToLoad);
            await WaitOneSecondAsync();
        }
    }

    async public void LoadNextRoom()
    {
        LoadLevel(loadingScreen);
        await WaitOneSecondAsync();
        LoadLevel(roomToLoad);
        await WaitOneSecondAsync();
    }

    void Update()
    {
        // Gets the spot the user clicked at
       if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null) {
                Debug.Log(hit.collider.gameObject.name);
                hit.collider.attachedRigidbody.AddForce(Vector2.up);
            }
        }
    }

    // Add a short delay--otherwise loading screen isn't on screen long enough to see
    public async Task WaitOneSecondAsync()
    {
        await Task.Delay(TimeSpan.FromSeconds(2));
    }

    public void LoadLevel (string levelName)
    {
        StartCoroutine(LoadSceneAsync(levelName));
    }

    IEnumerator LoadSceneAsync ( string levelName )
    {
        LoadingPanel.SetActive(true);

        AsyncOperation op = SceneManager.LoadSceneAsync(levelName);

        while ( !op.isDone )
        {
            float progress = Mathf.Clamp01(op.progress / .9f);
            LoadingBar.value = progress;

            yield return null;
        }
    }
}
