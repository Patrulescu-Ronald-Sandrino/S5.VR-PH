using UnityEngine;

// unity create wall of bricks https://youtu.be/cCdx-MC7XiQ
public class BrickGenerator : MonoBehaviour
{
    public int rows = 10;
    void Start()
    {
        var rainbow = new Color[] { Color.red, Color.yellow, Color.green, Color.blue, Color.magenta };
        var prefab = Resources.Load("Brick") as GameObject;

        for (int row = 0; row < rows; row++)
        {
            for (int z = -5; z < 5; z++)
            {
                var brick = Instantiate(prefab, new Vector3(-10, 0.5f + row, z), Quaternion.identity);
                brick.GetComponent<Renderer>().material.SetColor("_Color", new Color(Random.value, Random.value, Random.value));
                // brick.GetComponent<Renderer>().material.SetColor("_Color", Color.HSVToRGB(Mathf.PingPong(Time.time, 1), (float)z/rows, (float)row/rows));
                brick.GetComponent<Renderer>().material.SetColor("_Color", rainbow[(int)((float)(row + 1)/rows * (rainbow.Length - 1.0f))]);
            }
        }
    }
}
