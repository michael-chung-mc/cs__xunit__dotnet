using LibColor;
using LibTuple;
using LibComparinator;
namespace LibLight;

public class PointSource {
    public Point mbrPosition;
    public Color mbrIntensity;
    private Comparinator _fieldComp = new Comparinator();
    public PointSource()
    {
        mbrPosition = new Point(0,0,0);
        mbrIntensity = new Color(1,1,1);
    }
    public PointSource(Point position, Color intensity)
    {
        mbrPosition = position;
        mbrIntensity = intensity;
    }
    public bool CheckEqual(PointSource other)
    {
        return _fieldComp.CheckTuple(mbrPosition,other.mbrPosition) && mbrIntensity.CheckEqual(other.mbrIntensity);
    }
};