using static Interop;

public class WindowManager
{
    private class WindowLayout
    {
        public string Name { get; set; }
        public Window[] Windows { get; set; }
    }

    private List<WindowLayout> _windowLayouts;

    public WindowManager()
    {
        _windowLayouts = new();
    }

    public void SaveWindowLayout(string layoutName, bool ignoreCurrentWindow)
    {
        var windows = Interop.GetCurrentWindows();
        var wl = _windowLayouts.FirstOrDefault(wl => wl.Name == layoutName);
        if (wl is null)
        {
            wl = new WindowLayout { Name = layoutName };
            _windowLayouts.Add(wl);
        }
        wl.Windows = windows;

        if (ignoreCurrentWindow)
        {
            Console.WriteLine("ignoreCurrentWindow");
            var currentHwnd = Interop.GetCurrentWindowHwnd();
            Console.WriteLine($"current {currentHwnd}");
            wl.Windows = wl.Windows
                .Where(window => window.MainWindowHandle != currentHwnd)
                .ToArray();
        }
    }

    private void SetWindowLayout(int i) => _windowLayouts[i]
        .Windows
        .ToList()
        .ForEach(SetWindowPosition);

    public void SetWindowLayout(string layoutName)
    {
        int i = -1;
        if (!int.TryParse(layoutName, out i))
            i = _windowLayouts.FindIndex(wl => wl.Name == layoutName);
        if (i < 0 || i >= _windowLayouts.Count || _windowLayouts.Count == 0)
        {
            Console.WriteLine($"Error: Could not find layout {layoutName}");
            return;
        }
        SetWindowLayout(i);
    }

    public void PrintDebug()
    {
        for (int i = 0; i < _windowLayouts.Count; i++)
        {
            Console.WriteLine($"{i}: {_windowLayouts[i].Name}");
            foreach (var window in _windowLayouts[i].Windows)
            {
                Console.WriteLine(window);
            }
        }
    }

    public void PrintLayouts()
    {
		if (!_windowLayouts.Any()) Console.WriteLine("No saved layouts");
        for (int i = 0; i < _windowLayouts.Count; i++)
        {
            Console.WriteLine($"{i}: {_windowLayouts[i].Name}");
        }
    }
}
