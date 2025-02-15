using UnityEngine;
using UnityEngine.UI; // Required for UI Images

public class MoveTest : MonoBehaviour
{
    [SerializeField] private Image characterImage; // UI-based character (optional)
    [SerializeField] private SpriteRenderer characterSprite; // 2D sprite-based character (optional)
    [SerializeField] private float speed = 5f; // Speed of movement

    void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        // Get movement input using arrow keys
        if (Input.GetKey(KeyCode.LeftArrow)) moveX = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) moveX = 1f;
        if (Input.GetKey(KeyCode.UpArrow)) moveY = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) moveY = -1f;

        // Normalize diagonal movement
        Vector3 movement = new Vector3(moveX, moveY, 0).normalized * speed * Time.deltaTime;

        // Move based on assigned character type
        if (characterImage != null)
        {
            characterImage.rectTransform.anchoredPosition += new Vector2(movement.x, movement.y);
        }
        else if (characterSprite != null)
        {
            characterSprite.transform.position += movement;
        }
        else
        {
            transform.position += movement; // Moves the GameObject itself
        }
    }
}
