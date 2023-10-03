using ShowMeGraph.Blazor.Events;
using ShowMeGraph.Contracts;
using ShowMeGraph.Pages;

namespace ShowMeGraph.Managers;

public class HoverManager
{
    private readonly IndexViewModel _viewModel;

    public bool Enabled { get; set; } = true;

    public HoverManager(IndexViewModel viewModel)
    {
        _viewModel = viewModel;
        _viewModel.NodeHoverEnter += Index_NodeHoverEnter;
        _viewModel.NodeHoverLeave += Index_NodeHoverLeave;
        _viewModel.EdgeHoverEnter += Index_EdgeHoverEnter;
        _viewModel.EdgeHoverLeave += Index_EdgeHoverLeave;
    }

    ~HoverManager()
    {
        _viewModel.NodeHoverEnter -= Index_NodeHoverEnter;
        _viewModel.NodeHoverLeave -= Index_NodeHoverLeave;
        _viewModel.EdgeHoverEnter -= Index_EdgeHoverEnter;
        _viewModel.EdgeHoverLeave -= Index_EdgeHoverLeave;
    }

    private void Index_NodeHoverEnter(object? sender, NodeEventArgs e)
    {
        if (!Enabled)
        {
            return;
        }

        var node = e.Node as IHoverable;

        if (node is null)
        {
            return;
        }

        node.Hovered = true;
    }

    private void Index_NodeHoverLeave(object? sender, NodeEventArgs e)
    {
        if (!Enabled)
        {
            return;
        }

        var node = e.Node as IHoverable;

        if (node is null)
        {
            return;
        }

        node.Hovered = false;
    }

    private void Index_EdgeHoverEnter(object? sender, EdgeEventArgs e) 
    {
        if (!Enabled)
        {
            return;
        }

        var edge = e.Edge as IHoverable;

        if (edge is null) 
        {
            return;
        }

        edge.Hovered = true;
    }

    private void Index_EdgeHoverLeave(object? sender, EdgeEventArgs e)
    {
        if (!Enabled)
        {
            return;
        }

        var edge = e.Edge as IHoverable;

        if (edge is null)
        {
            return;
        }

        edge.Hovered = false;
    }
}
