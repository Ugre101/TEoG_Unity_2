using UnityEngine;

public class GenetricChangeFontSize : ChangeFontSize
{
    [SerializeField] private float currSize = 14f;
    protected override float CurrSize => currSize;
    protected override float DownSized => currSize -= 0.5f;
    protected override float UpSized => currSize += 0.5f;
}
