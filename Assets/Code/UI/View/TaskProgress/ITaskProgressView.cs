using System;

namespace Code.UI.View.TaskProgressView
{
    public interface ITaskProgressView
    {
        void OnTaskProgressChanged(float progress);
    }
}