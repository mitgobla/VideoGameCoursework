using System;

[Serializable]
public class ColorData
{
    public float r;
    public float g;
    public float b;

    public ColorData(float r, float g, float b)
    {
        this.r = r;
        this.g = g;
        this.b = b;
    }

    public ColorData()
    {
        this.r = 1f;
        this.g = 1f;
        this.b = 1f;
    }
}