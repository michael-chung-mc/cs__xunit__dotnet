using LibColor;
using LibTuple;
using LibComparinator;
namespace LibLight;

public class PointSource {
    public Point mbrPosition;
    public Color mbrIntensity;
    public PointSource()
    {
        mbrPosition = new Point(0,0,0);
        mbrIntensity = new Color(0,0,0);
    }
    public PointSource(Point position, Color intensity)
    {
        mbrPosition = position;
        mbrIntensity = intensity;
    }
    public bool CheckEqual(PointSource other)
    {
        Comparinator ce = new Comparinator();
        return ce.CheckTuple(mbrPosition,other.mbrPosition) && mbrIntensity.CheckEqual(other.mbrIntensity);
    }
};