using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You are Frendly state");
                break;
            case "Fuel":
                Debug.Log("You are Fuel state");
                break;
            case "Finish":
                Debug.Log("You are Finish state");
                break;
        }
    }
}
