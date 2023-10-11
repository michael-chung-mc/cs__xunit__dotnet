using Xunit;
using LibPattern;
using LibComparinator;
using LibColor;
using LibMatrix;
using LibForm;
using LibTuple;
namespace LibPattern.Test;

public class PatternTest
{
	Comparinator _fieldComp = new Comparinator();
	[Fact]
    public void CanaryTest ()
    {
        Assert.True(true);
    }
    [Fact]
    public void PatternCtor ()
    {
        Pattern varP = new Pattern();
        Assert.True(varP._fieldBlack.CheckEqual(new Color(0,0,0)));
        Assert.True(varP._fieldWhite.CheckEqual(new Color(1,1,1)));
        Assert.True(varP._fieldTransform.CheckEqual(new IdentityMatrix(4,4)));
    }
    [Fact]
    public void PatternTransformationAssignment()
    {
        Pattern varP = new Pattern();
        TranslationMatrix varTM = new TranslationMatrix(1,2,3);
        varP.SetTransform(varTM);
        Assert.True(varP._fieldTransform.CheckEqual(varTM));
    }
    [Fact]
    public void PatternObjectTransformation()
    {
        Sphere varObj = new Sphere();
        varObj.SetTransform(new ScalingMatrix(2,2,2));
        // varObj._fieldMaterial.setPattern(new Pattern(new Color(0,0,0), Color(1,1,1)));
        varObj._fieldMaterial.SetPattern(new Pattern(new Color(0,0,0), new Color(1,1,1)));
        Color varColor = varObj.GetColorLocal(new Point(2,3,4));
        Assert.True(varColor.CheckEqual(new Color(1,1.5,2)));
    }
    [Fact]
    public void PatternPatternTransformation()
    {
        Sphere varObj = new Sphere();
        // varObj._fieldMaterial.setPattern(new Pattern(new Color(0,0,0), Color(1,1,1)));
        varObj._fieldMaterial.SetPattern(new Pattern(new Color(0,0,0), new Color(1,1,1)));
        ScalingMatrix varM = new ScalingMatrix(2,2,2);
        varObj._fieldMaterial.mbrPattern.SetTransform(varM);
        Color varColor = varObj.GetColorLocal(new Point(2,3,4));
        Assert.True(varColor.CheckEqual(new Color(1,1.5,2)));
    }
    [Fact]
    public void PatternObjectPatternTransformation()
    {
        Sphere varObj = new Sphere();
        varObj.SetTransform(new ScalingMatrix(2,2,2));
        // varObj._fieldMaterial.setPattern(new Pattern(new Color(0,0,0), Color(1,1,1)));
        varObj._fieldMaterial.SetPattern(new Pattern(new Color(0,0,0), new Color(1,1,1)));
        varObj._fieldMaterial.mbrPattern.SetTransform(new TranslationMatrix(0.5,1,1.5));
        Color varColor = varObj.GetColorLocal(new Point(2.5,3,3.5));
        Assert.True(varColor.CheckEqual(new Color(0.75,0.5,0.25)));
    }
    [Fact]
    public void StripePatternCtor()
    {
        PatternStripe varP = new PatternStripe();
        Assert.True(varP._fieldColors[0].CheckEqual(varP._fieldWhite));
        Assert.True(varP._fieldColors[1].CheckEqual(varP._fieldBlack));
    }
    [Fact]
    public void StripePatternConstantInX()
    {
        PatternStripe varP = new PatternStripe();
        Assert.True(varP.getColorLocal(new Point(0,0,0)).CheckEqual(varP._fieldWhite));
        Assert.True(varP.getColorLocal(new Point(0,1,0)).CheckEqual(varP._fieldWhite));
        Assert.True(varP.getColorLocal(new Point(0,2,0)).CheckEqual(varP._fieldWhite));
    }
    [Fact]
    public void StripePatternConstantInZ()
    {
        PatternStripe varP = new PatternStripe();
        Assert.True(varP.getColorLocal(new Point(0,0,0)).CheckEqual(varP._fieldWhite));
        Assert.True(varP.getColorLocal(new Point(0,0,1)).CheckEqual(varP._fieldWhite));
        Assert.True(varP.getColorLocal(new Point(0,0,2)).CheckEqual(varP._fieldWhite));
    }
    [Fact]
    public void StripePatternAlternateInX()
    {
        PatternStripe varP = new PatternStripe();
        Assert.True(varP.getColorLocal(new Point(0,0,0)).CheckEqual(varP._fieldWhite));
        Assert.True(varP.getColorLocal(new Point(0.9,0,0)).CheckEqual(varP._fieldWhite));
        Assert.True(varP.getColorLocal(new Point(1,0,0)).CheckEqual(varP._fieldBlack));
        Assert.True(varP.getColorLocal(new Point(-0.1,0,0)).CheckEqual(varP._fieldBlack));
        Assert.True(varP.getColorLocal(new Point(-1,0,0)).CheckEqual(varP._fieldBlack));
        Assert.True(varP.getColorLocal(new Point(-1.1,0,0)).CheckEqual(varP._fieldWhite));
        Assert.True(varP.getColorLocal(new Point(-1.9,0,0)).CheckEqual(varP._fieldWhite));
        Assert.True(varP.getColorLocal(new Point(-2,0,0)).CheckEqual(varP._fieldWhite));
    }
    [Fact]
    public void StripePatternObjectTransformation()
    {
        Sphere varObj = new Sphere();
        varObj.SetTransform(new ScalingMatrix(2,2,2));
        // varObj._fieldMaterial.setPattern(new PatternStripe());
        varObj._fieldMaterial.SetPattern(new PatternStripe());
        Color varColor = varObj.GetColorLocal(new Point(1.5,0,0));
        Assert.True(varColor.CheckEqual(new Color(1,1,1)));
    }
    [Fact]
    public void StripePatternPatternTransformation()
    {
        Sphere varObj = new Sphere();
        ScalingMatrix varMS = new ScalingMatrix(2,2,2);
        // varObj._fieldMaterial.setPattern(new PatternStripe());
        varObj._fieldMaterial.SetPattern(new PatternStripe());
        varObj._fieldMaterial.mbrPattern.SetTransform(varMS);
        Color varColor = varObj.GetColorLocal(new Point(1.5,0,0));
        Assert.True(varColor.CheckEqual(new Color(1,1,1)));
    }
    [Fact]
    public void StripePatternObjectPatternTransformation()
    {
        Sphere varObj = new Sphere();
        TranslationMatrix varMT = new TranslationMatrix(0.5,0,0);
        varObj.SetTransform(new ScalingMatrix(2,2,2));
        // varObj._fieldMaterial.setPattern(new PatternStripe());
        varObj._fieldMaterial.SetPattern(new PatternStripe());
        varObj._fieldMaterial.mbrPattern.SetTransform(varMT);
        Color varColor = varObj.GetColorLocal(new Point(2.5,0,0));
        Assert.True(varColor.CheckEqual(new Color(1,1,1)));
    }
    [Fact]
    public void GradientPattern()
    {
        PatternGradient varPG = new PatternGradient();
        Assert.True(varPG.getColorLocal(new Point(0,0,0)).CheckEqual(new Color(1,1,1)));
        Assert.True(varPG.getColorLocal(new Point(0.25,0,0)).CheckEqual(new Color(0.75,0.75,0.75)));
        Assert.True(varPG.getColorLocal(new Point(0.5,0,0)).CheckEqual(new Color(0.5,0.5,0.5)));
        Assert.True(varPG.getColorLocal(new Point(0.75,0,0)).CheckEqual(new Color(.25,.25,.25)));
    }
    [Fact]
    public void RingPattern()
    {
        PatternRing varPR = new PatternRing();
        Assert.True(varPR.getColorLocal(new Point(0,0,0)).CheckEqual(new Color(1,1,1)));
        Assert.True(varPR.getColorLocal(new Point(1,0,0)).CheckEqual(new Color(0,0,0)));
        Assert.True(varPR.getColorLocal(new Point(0,0,1)).CheckEqual(new Color(0,0,0)));
        Assert.True(varPR.getColorLocal(new Point(0.708,0,0.708)).CheckEqual(new Color(0,0,0)));
    }
    [Fact]
    public void RingChecker()
    {
        PatternChecker varCh = new PatternChecker();
        Assert.True(varCh.getColorLocal(new Point(0,0,0)).CheckEqual(new Color(1,1,1)));
        Assert.True(varCh.getColorLocal(new Point(0.99,0,0)).CheckEqual(new Color(1,1,1)));
        Assert.True(varCh.getColorLocal(new Point(1.01,0,0)).CheckEqual(new Color(0,0,0)));
        Assert.True(varCh.getColorLocal(new Point(0,0.99,0)).CheckEqual(new Color(1,1,1)));
        Assert.True(varCh.getColorLocal(new Point(0,1.01,0)).CheckEqual(new Color(0,0,0)));
        Assert.True(varCh.getColorLocal(new Point(0,0,0.99)).CheckEqual(new Color(1,1,1)));
        Assert.True(varCh.getColorLocal(new Point(0,0,1.01)).CheckEqual(new Color(0,0,0)));
    }
}