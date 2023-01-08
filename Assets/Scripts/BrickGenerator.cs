using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// unity create wall of bricks https://youtu.be/cCdx-MC7XiQ
public class BrickGenerator : MonoBehaviour
{
    public const int rows = 10;
    public  int columns = 10;

    private List<GameObject> _bricks = new();
    private bool _enabled = true;
    
    void Start()
    {
        var rainbow = new Color[] { Color.red, Color.yellow, Color.green, Color.blue, Color.magenta };
        var prefab = Resources.Load("Brick") as GameObject;
        var position = transform.position;

        for (int row = 0; row < rows; row++)
        {
            // z = position.z - columns/2
            // int startZ = position.z - columns / 2.0f;
            for (int z = 0; z < columns; z++)
            {
                // var brick = Instantiate(prefab, new Vector3(-10, 0.5f + row, z), Quaternion.identity);
                var brick = Instantiate(prefab, new Vector3(position.x, position.y + row, position.z - z), Quaternion.identity);
                _bricks.Add(brick);
                brick.GetComponent<Renderer>().material.SetColor("_Color", new Color(Random.value, Random.value, Random.value));
                // brick.GetComponent<Renderer>().material.SetColor("_Color", Color.HSVToRGB(Mathf.PingPong(Time.time, 1), (float)z/rows, (float)row/rows));
                brick.GetComponent<Renderer>().material.SetColor("_Color", rainbow[(int)((float)(row + 1)/rows * (rainbow.Length - 1.0f))]);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
            Debug.Log("B pressed");
            if (_enabled)
            {
                foreach (var brick in _bricks)
                {
                    Destroy(brick);
                }
            }
            else
            {
                Start();
            }
            _enabled = !_enabled;
        }
    }
}
