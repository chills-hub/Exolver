using UnityEngine;

public class PlayerMovementTester : MonoBehaviour
{
    void Update() { }
    public Vector2 Move(float xInput, float speed, float yInput) 
    {
        return new Vector2(xInput * speed, yInput);
    }
}
