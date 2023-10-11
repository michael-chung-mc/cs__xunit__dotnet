using Xunit;
using LibMaterial;
using LibColor;
using LibLight;
using LibComparinator;
using LibTuple;
using LibPattern;
namespace LibMaterial.Test;

public class MaterialTest
{
	Comparinator _fieldComp = new Comparinator();
    [Fact]
    public void Canary()
    {
        Assert.Equal(1, 1);
    }
    [Fact]
    public void PointSourceCtor() {
        Color c = new Color(1,1,1);
        Point p = new Point(0,0,0);
        PointSource ps = new PointSource(p,c);
        Assert.True(_fieldComp.CheckTuple(c,ps.mbrIntensity));
        Assert.True(_fieldComp.CheckTuple(p,ps.mbrPosition));
    }

   public void MaterialCtor ()
    {
        Material varMat = new Material();
        Assert.True(_fieldComp.CheckTuple(varMat.mbrColor, new Color(1,1,1)));
        Assert.True(_fieldComp.CheckFloat(varMat.mbrAmbient, 0.1));
        Assert.True(_fieldComp.CheckFloat(varMat.mbrDiffuse, 0.9));
        Assert.True(_fieldComp.CheckFloat(varMat.mbrSpecular, 0.9));
        Assert.True(_fieldComp.CheckFloat(varMat.mbrShininess, 200.0));
        Assert.True(_fieldComp.CheckFloat(varMat.mbrReflective, 0.0));
        Assert.True(_fieldComp.CheckFloat(varMat.mbrTransparency, 0.0));
        Assert.True(_fieldComp.CheckFloat(varMat.mbrRefractiveIndex, 1.0));
    }

   public void LightingStraightOn()
    {
        Material varMat = new Material();
        Point p = new Point(0,0,0);
        Vector pov = new Vector(0,0,-1);
        Vector normal = new Vector(0,0,-1);
        PointSource light = new PointSource(new Point(0,0,-10), new Color(1,1,1));
        Color res = varMat.GetColor(light, p, pov, normal, false);
        Color expectedLight = new Color(1.9,1.9,1.9);
        Assert.True(_fieldComp.CheckTuple(res,expectedLight));
    }

   public void Lighting45PovShift()
    {
        Material m = new Material();
        Point p = new Point(0,0,0);
        Vector pov = new Vector(0,Math.Sqrt(2)/2,Math.Sqrt(2)/2);
        Vector normal = new Vector(0,0,-1);
        PointSource light = new PointSource(new Point(0,0,-10), new Color(1,1,1));
        Color res = m.GetColor(light, p, pov, normal, false);
        Color expectedLight = new Color(1.0,1.0,1.0);
        Assert.True(_fieldComp.CheckTuple(res,expectedLight));
    }

   public void Lighting45LightShift()
    {
        Material m = new Material();
        Point p = new Point(0,0,0);
        Vector pov = new Vector(0,0,-1);
        Vector normal = new Vector(0,0,-1);
        PointSource light = new PointSource(new Point(0,10,-10), new Color(1,1,1));
        Color res = m.GetColor(light, p, pov, normal, false);
        Color expectedLight = new Color(0.7364,0.7364,0.7364);
        Assert.True(_fieldComp.CheckTuple(res,expectedLight));
    }

   public void Lighting45EyeLightShift()
    {
        Material m = new Material();
        Point p = new Point(0,0,0);
        Vector pov = new Vector(0,-Math.Sqrt(2)/2,-Math.Sqrt(2)/2);
        Vector normal = new Vector(0,0,-1);
        PointSource light = new PointSource(new Point(0,10,-10), new Color(1,1,1));
        Color res = m.GetColor(light, p, pov, normal, false);
        Color expectedLight = new Color(1.6364,1.6364,1.6364);
        Assert.True(_fieldComp.CheckTuple(res,expectedLight));
    }

   public void LightingBehindSurface()
    {
        Material m = new Material();
        Point p = new Point(0,0,0);
        Vector pov = new Vector(0,0,-1);
        Vector normal = new Vector(0,0,-1);
        PointSource light = new PointSource(new Point(0,0,10), new Color(1,1,1));
        Color res = m.GetColor(light, p, pov, normal, false);
        Color expectedLight = new Color(0.1,0.1,0.1);
        Assert.True(_fieldComp.CheckTuple(res,expectedLight));
    }

   public void LightingShadow()
    {
        Material m = new Material();
        Point p = new Point(0,0,0);
        Vector pov = new Vector(0,0,-1);
        Vector normal = new Vector(0,0,-1);
        PointSource light = new PointSource(new Point(0,0,-10), new Color(1,1,1));
        Color res = m.GetColor(light, p, pov, normal, true);
        Color expectedLight = new Color(0.1,0.1,0.1);
        Assert.True(_fieldComp.CheckTuple(res,expectedLight));
    }

   public void LightingPatternCtor()
    {
        Material varMat = new Material();
        // varMat.setPattern(new PatternStripe(Color(1,1,1), Color(0,0,0)));
        varMat.SetPattern(new PatternStripe(new Color(1,1,1), new Color(0,0,0)));
        varMat.mbrAmbient = 1;
        varMat.mbrDiffuse = 0;
        varMat.mbrSpecular = 0;
        Vector varPov = new Vector(0,0,-1);
        Vector varNormal = new Vector(0,0,-1);
        PointSource varLight = new PointSource(new Point(0,0,-10),new Color(1,1,1));
        Color varStripeA = varMat.GetColor(varLight, new Point(0.9,0,0), varPov, varNormal, true);
        Color varStripeB = varMat.GetColor(varLight, new Point(1.1,0,0), varPov, varNormal, true);
        Assert.True(_fieldComp.CheckTuple(varStripeA,new Color(1,1,1)));
        Assert.True(_fieldComp.CheckTuple(varStripeA,new Color(0,0,0)));
    }
}


