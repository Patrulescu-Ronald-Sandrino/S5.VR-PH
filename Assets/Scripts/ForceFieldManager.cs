using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ForceFieldManager : MonoBehaviour
{
    public Text forceFieldText;
    public int StartHp = 50;

    public int _hp = -1;

    private static readonly int Color1 = Shader.PropertyToID("_Color");
    private float _initialAlpha;

    // Start is called before the first frame update
    void Start()
    {
        _hp = StartHp;
        _initialAlpha = GetComponent<Renderer>().material.color.a;
        _UpdateForceFieldText();
    }

    // Update is called once per frame
    void Update()
    {
        // if hp is 0, destroy the force field
        if (_hp <= 0)
        {
            Destroy(gameObject);
            Destroy(this);
            Destroy(GetComponent<Rigidbody>());
            Destroy(forceFieldText);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var gameObjectName = collision.gameObject.name;
        if (gameObjectName is "CannonBall(Clone)" or "Brick(Clone)")
        {
            _hp--;
            _UpdateForceFieldText();
        }
        {
            _hp--;
            
            // lower the alpha value of the force field
            _UpdateForceFieldText();
            UpdateAlpha();
        }
    }

    
    private void UpdateAlpha()
    {
        var material = GetComponent<Renderer>().material;
        var color = material.color;
        color.a = _initialAlpha * _hp / StartHp;
        material.SetColor(Color1, color);
    }

    private void _UpdateForceFieldText()
    {
        forceFieldText.text = $"Force Field HP: {_hp}";
    }
}
