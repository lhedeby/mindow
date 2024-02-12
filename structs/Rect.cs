public struct Rect
{
    public int X { get; set; }
    public int Y { get; set; }
    public int XEnd { get; set; }
    public int YEnd { get; set; }

    public override string ToString()
    {
        return $"X:{X}, Y:{Y}, Width:{XEnd}, Height:{YEnd}";
    }
}
