using Microsoft.AspNetCore.Components.Web;
using ShowMeGraph.Data;

namespace ShowMeGraph.Blazor.Events;

public class LocalMouseEventArgs : EventArgs
{
    public Vector2F LocalPosition { get; }

    public MouseEventArgs MouseEventArgs { get; }

    public LocalMouseEventArgs(Vector2F localPosition, MouseEventArgs mouseEventArgs)
    {
        LocalPosition = localPosition;
        MouseEventArgs = mouseEventArgs;
    }
}
