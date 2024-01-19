using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public float rotationSpeed = 200f;
    public float radius;
    public float startAngle;

    Transform centerPoint;
    private float angle = 0f;
    private float rotation = 0f;
    private GameObject player;

    void Start()
    {
        angle = startAngle;

        player = GameObject.FindGameObjectWithTag("Player");
        rotationSpeed *= GetComponent<Stat>().speed;
        centerPoint = GetComponent<Transform>();
    }

    void Update()
    {
        // Cập nhật góc quay
        angle += rotationSpeed * Time.deltaTime;
        rotation += 1500f * Time.deltaTime;
        if (angle > 360f)
        {
            angle -= 360f;
        }
        if (rotation > 360f)
        {
            rotation -= 360f;
        }

        // Tính toán vị trí mới dựa vào bán kính và góc quay
        float x = player.transform.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = player.transform.position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        Vector3 newPosition = new Vector3(x, y, 0f);

        // Di chuyển lưỡi cưa
        transform.position = newPosition;

        // Xoay lưỡi cưa
        transform.rotation = Quaternion.Euler(0, 0, rotation + 180);
    }
}