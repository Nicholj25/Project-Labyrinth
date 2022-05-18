using UnityEngine;

public class SceneCounter : MonoBehaviour 
{
    /// Static reference to the instance of our SceneCounter
    public static SceneCounter instance;

    /// The number of scenes we've travelled to so far.
    public int scenesProgressed = 0;


    void Awake()
    {
        scenesProgressed++;
        // If the instance reference has not been set, yet, 
        if (instance == null)
        {
            // Set this instance as the instance reference.
            instance = this;
        }
        else if(instance != this)
        {
            // If the instance reference has already been set, and this is not the
            // the instance reference, destroy this game object.
            Destroy(gameObject);
        }

        // Do not destroy this object, when we load a new scene.
        DontDestroyOnLoad(gameObject);
    }
}