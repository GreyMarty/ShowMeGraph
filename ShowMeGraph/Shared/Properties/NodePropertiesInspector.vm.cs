﻿using ShowMeGraph.Contracts;
using ShowMeGraph.Data;

namespace ShowMeGraph.Shared.Properties
{
    public class NodePropertiesInspectorViewModel : ViewModelBase
    {
        private readonly UiVertex _model;

        public Vector2F Position 
        {
            get => _model.Position;
            set
            {
                _model.Position = value;
                OnPropertyChanged(nameof(Position), Position);
            }
        }

        public bool Fixed
        {
            get => _model.Fixed;
            set
            {
                _model.Fixed = value;
                OnPropertyChanged(nameof(Fixed), Fixed);
            }
        }

        public NodePropertiesInspectorViewModel(UiVertex model)
        {
            _model = model;
        }
    }
}
