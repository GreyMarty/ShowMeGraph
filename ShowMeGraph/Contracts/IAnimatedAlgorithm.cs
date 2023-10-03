using ShowMeGraph.Animation;

namespace ShowMeGraph.Contracts;

public interface IAnimatedAlgorithm
{
    public string Name { get; }

    public IAnimation Animate(); 
}
