namespace ShowMeGraph.Helpers;

public class GoldenRatioColorPalette
{
    private const double GoldenRatio = 0.618033988749895;

    private double _seed;

    public GoldenRatioColorPalette(double seed)
    {
        _seed = seed;
    }

    public string Next()
    {
        _seed += GoldenRatio;

        while (_seed > 1)
        {
            _seed--;
        }

        return ColorHelper.HsvToRgb(_seed * 360, 0.5, 0.95);
    }
}
