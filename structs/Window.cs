public struct Window
{
    public Rect Rect { get; set; }
    public nint MainWindowHandle { get; set; }
    public string MainWindowTitle { get; set; }

    public override string ToString()
    {
        return $"""
				== Window ==
				mwh: {MainWindowHandle}
				title: {MainWindowTitle}
				Rect - {Rect}
			""";
    }
}
