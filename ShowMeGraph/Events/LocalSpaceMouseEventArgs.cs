using Microsoft.AspNetCore.Components.Web;
using ShowMeGraph.Data;

namespace ShowMeGraph.Events;

public class LocalSpaceMouseEventArgs : EventArgs
{
    public Vector2F LocalPosition { get; }
    public MouseEventArgs MouseEventArgs { get; }
}
