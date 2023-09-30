namespace ShowMeGraph.Data;

public struct Vector2F
{
    public float X { get; set; } = 0;
    public float Y { get; set; } = 0;

    public float SqrMagnitude => X * X + Y * Y;
    public float Magnitude => MathF.Sqrt(SqrMagnitude);

    public Vector2F(float x, float y)
    {
        X = x;
        Y = y;
    }

    public Vector2F(float a) : this(a, a) { }

    public Vector2F Normalized() => new Vector2F(X, Y) / Magnitude;
    public float Dot(Vector2F other) => X * other.X + Y * other.Y;
    public Vector2F Rotated(float radians) => new(
        X * MathF.Cos(radians) - Y * MathF.Sin(radians),
        X * MathF.Sin(radians) + Y * MathF.Cos(radians)
    );

    public static Vector2F FromAngle(float radians) => new(MathF.Cos(radians), MathF.Sin(radians));

    public static Vector2F operator +(Vector2F a, Vector2F b) => new(a.X + b.X, a.Y + b.Y);
    public static Vector2F operator -(Vector2F a) => new(-a.X, -a.Y);
    public static Vector2F operator -(Vector2F a, Vector2F b) => new(a.X - b.X, a.Y - b.Y);
    public static Vector2F operator *(Vector2F a, float b) => new(a.X * b, a.Y * b);
    public static Vector2F operator *(float a, Vector2F b) => new(b.X * a, b.Y * a);
    public static Vector2F operator /(Vector2F a, float b) => new(a.X / b, a.Y / b);
}
