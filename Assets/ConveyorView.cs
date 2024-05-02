using UnityEngine;

public class ConveyorView : MonoBehaviour
{
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    
    [SerializeField] private float _speed;
    [SerializeField] private float _xOffset;

    private Material _material;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        var yOffset = _material.GetTextureOffset(MainTex).y + (Time.deltaTime * _speed);

        if (yOffset > 5)
            yOffset = 0;
        
        Vector2 offset = new Vector2(_xOffset, yOffset);
        _material.SetTextureOffset(MainTex, offset);
    }
}
