using UnityEngine;

public class Explosion : WarEntity
{
    [SerializeField, Range(0f, 1f)]
    private float _duration = 0.5f;

    [SerializeField]
    private AnimationCurve _scaleCurve;

    [SerializeField]
    private AnimationCurve _colorCurve;

    private float _age;

    private static int _colorPropId = Shader.PropertyToID("_Color");
    private static MaterialPropertyBlock _propertyBlock;

    private float _scale;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Initialize(Vector3 position, float blastRadius, float damage = 0f)
    {
        if (damage > 0)
        {
            TargetPoint.FillBufer(position, blastRadius);
            for (int i = 0; i < TargetPoint.BufferedCount; i++)
            {
                TargetPoint.GetBuffered(i).Enemy.TakeDamage(damage);
            }
        }

        transform.localPosition = position;
        _scale = 2f * blastRadius;
    }

    public override bool GameUpdate()
    {
        _age += Time.deltaTime;
        if (_age >= _duration)
        {
            OriginFactory.Reclaim(this);
            return false;
        }

        if(_propertyBlock == null)
        {
            _propertyBlock = new MaterialPropertyBlock();
        }

        float time = _age / _duration;
        Color color = Color.yellow;
        color.a = _colorCurve.Evaluate(time);
        _propertyBlock.SetColor(_colorPropId, color);
        _meshRenderer.SetPropertyBlock(_propertyBlock);
        transform.localScale = Vector3.one * (_scale * _scaleCurve.Evaluate(time));

        return true;
    }
}
