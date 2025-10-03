using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerData playerData;

    void Update()
    {
        print(playerData.playerHealth);

        if (Input.GetKeyDown("="))
        {
            playerData.playerHealth++;
        }
        if(Input.GetKeyDown("-"))
        {
           playerData.playerHealth--;
        }
    }
}
