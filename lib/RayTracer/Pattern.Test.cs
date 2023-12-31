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
    public void PatternTestCanary_WithDefault_ExpectDefault() {
        Assert.True(true);
    }
    [Fact]
    public void PatternCtor_WithDefault_ExpectDefault ()
    {
        Pattern varP = new Pattern();
        Assert.True(varP._fieldBlack.CheckEqual(new Color(0,0,0)));
        Assert.True(varP._fieldWhite.CheckEqual(new Color(1,1,1)));
        Assert.True(varP._fieldTransform.CheckEqual(new IdentityMatrix(4,4)));
    }
    [Fact]
    public void PatternTransformationAssignment_WithGiven_ExpectGiven()
    {
        Pattern varP = new Pattern();
        TranslationMatrix varTM = new TranslationMatrix(1,2,3);
        varP.SetTransform(varTM);
        Assert.True(varP._fieldTransform.CheckEqual(varTM));
    }
    [Fact]
    public void ObjectPatternTransformation_withGiven_ExpectGiven()
    {
        UnitSphere varObj = new UnitSphere();
        varObj.SetTransform(new ScalingMatrix(2,2,2));
        varObj._fieldMaterial.SetPattern(new Pattern(new Color(0,0,0), new Color(1,1,1)));
        Color varColor = varObj.GetColorLocal(new Point(2,3,4));
        Assert.True(varColor.CheckEqual(new Color(1,1.5,2)));
    }
    [Fact]
    public void PatternSetTransformation_WithGiven_ExpectGiven()
    {
        UnitSphere varObj = new UnitSphere();
        varObj._fieldMaterial.SetPattern(new Pattern(new Color(0,0,0), new Color(1,1,1)));
        ScalingMatrix varM = new ScalingMatrix(2,2,2);
        varObj._fieldMaterial._fieldPattern.SetTransform(varM);
        Color varColor = varObj.GetColorLocal(new Point(2,3,4));
        Assert.True(varColor.CheckEqual(new Color(1,1.5,2)));
    }
    [Fact]
    public void ObjectPatternSetTransformation_WithGiven_ExpectGiven()
    {
        UnitSphere varObj = new UnitSphere();
        varObj.SetTransform(new ScalingMatrix(2,2,2));
        varObj._fieldMaterial.SetPattern(new Pattern(new Color(0,0,0), new Color(1,1,1)));
        varObj._fieldMaterial._fieldPattern.SetTransform(new TranslationMatrix(0.5,1,1.5));
        Color varColor = varObj.GetColorLocal(new Point(2.5,3,3.5));
        Assert.True(varColor.CheckEqual(new Color(0.75,0.5,0.25)));
    }
    [Fact]
    public void StripePatternCtor_WithDefault_ExpectWhiteBlack()
    {
        PatternStripe varP = new PatternStripe();
        Assert.True(varP._fieldColors[0].CheckEqual(varP._fieldWhite));
        Assert.True(varP._fieldColors[1].CheckEqual(varP._fieldBlack));
    }
    [Fact]
    public void StripePatternGetColorLocal_WithDefault_ExpectConstantInY()
    {
        PatternStripe varP = new PatternStripe();
        Assert.True(varP.GetColorLocal(new Point(0,0,0)).CheckEqual(varP._fieldWhite));
        Assert.True(varP.GetColorLocal(new Point(0,1,0)).CheckEqual(varP._fieldWhite));
        Assert.True(varP.GetColorLocal(new Point(0,2,0)).CheckEqual(varP._fieldWhite));
    }
    [Fact]
    public void StripePatternGetColorLocal_WithDefault_ExpectConstantInZ()
    {
        PatternStripe varP = new PatternStripe();
        Assert.True(varP.GetColorLocal(new Point(0,0,0)).CheckEqual(varP._fieldWhite));
        Assert.True(varP.GetColorLocal(new Point(0,0,1)).CheckEqual(varP._fieldWhite));
        Assert.True(varP.GetColorLocal(new Point(0,0,2)).CheckEqual(varP._fieldWhite));
    }
    [Fact]
    public void StripePatternGetColorLocal_WithDefault_ExpectAlternateInX()
    {
        PatternStripe varP = new PatternStripe();
        Assert.True(varP.GetColorLocal(new Point(0,0,0)).CheckEqual(varP._fieldWhite));
        Assert.True(varP.GetColorLocal(new Point(0.9,0,0)).CheckEqual(varP._fieldWhite));
        Assert.True(varP.GetColorLocal(new Point(1,0,0)).CheckEqual(varP._fieldBlack));
        Assert.True(varP.GetColorLocal(new Point(-0.1,0,0)).CheckEqual(varP._fieldBlack));
        Assert.True(varP.GetColorLocal(new Point(-1,0,0)).CheckEqual(varP._fieldBlack));
        Assert.True(varP.GetColorLocal(new Point(-1.1,0,0)).CheckEqual(varP._fieldWhite));
        Assert.True(varP.GetColorLocal(new Point(-1.9,0,0)).CheckEqual(varP._fieldWhite));
        Assert.True(varP.GetColorLocal(new Point(-2,0,0)).CheckEqual(varP._fieldWhite));
    }
    [Fact]
    public void ObjectStripePatternSetTransformation_WithGiven_ExpectGiven()
    {
        UnitSphere varObj = new UnitSphere();
        varObj.SetTransform(new ScalingMatrix(2,2,2));
        varObj._fieldMaterial.SetPattern(new PatternStripe());
        Color varColor = varObj.GetColorLocal(new Point(1.5,0,0));
        Assert.True(varColor.CheckEqual(new Color(1,1,1)));
    }
    [Fact]
    public void StripePatternSetTransformation_WithGiven_ExpectGiven()
    {
        UnitSphere varObj = new UnitSphere();
        ScalingMatrix varMS = new ScalingMatrix(2,2,2);
        varObj._fieldMaterial.SetPattern(new PatternStripe());
        varObj._fieldMaterial._fieldPattern.SetTransform(varMS);
        Color varColor = varObj.GetColorLocal(new Point(1.5,0,0));
        Assert.True(varColor.CheckEqual(new Color(1,1,1)));
    }
    [Fact]
    public void ObjectStripePatternSetTransformationGetColor_WithGiven_ExpectGiven()
    {
        UnitSphere varObj = new UnitSphere();
        TranslationMatrix varMT = new TranslationMatrix(0.5,0,0);
        varObj.SetTransform(new ScalingMatrix(2,2,2));
        varObj._fieldMaterial.SetPattern(new PatternStripe());
        varObj._fieldMaterial._fieldPattern.SetTransform(varMT);
        Color varColor = varObj.GetColorLocal(new Point(2.5,0,0));
        Assert.True(varColor.CheckEqual(new Color(1,1,1)));
    }
    [Fact]
    public void GradientPattern_WithDefault_ExpectDefault()
    {
        PatternGradient varPG = new PatternGradient();
        Assert.True(varPG.GetColorGradientBasicLerp(new Point(0,0,0)).CheckEqual(new Color(1,1,1)));
        Assert.True(varPG.GetColorGradientBasicLerp(new Point(0.25,0,0)).CheckEqual(new Color(0.75,.75,.75)));
        Assert.True(varPG.GetColorGradientBasicLerp(new Point(0.5,0,0)).CheckEqual(new Color(0.5,0.5,0.5)));
        Assert.True(varPG.GetColorGradientBasicLerp(new Point(0.75,0,0)).CheckEqual(new Color(.25,.25,.25)));
    }
    [Fact]
    public void RingPattern_WithDefault_ExpectDefault()
    {
        PatternRing varPR = new PatternRing();
        Assert.True(varPR.GetColorLocal(new Point(0,0,0)).CheckEqual(new Color(1,1,1)));
        Assert.True(varPR.GetColorLocal(new Point(1,0,0)).CheckEqual(new Color(0,0,0)));
        Assert.True(varPR.GetColorLocal(new Point(0,0,1)).CheckEqual(new Color(0,0,0)));
        Assert.True(varPR.GetColorLocal(new Point(0.708,0,0.708)).CheckEqual(new Color(0,0,0)));
    }
    [Fact]
    public void RingChecker_WithDefault_ExpectDefault()
    {
        PatternChecker varCh = new PatternChecker();
        Assert.True(varCh.GetColorLocal(new Point(0,0,0)).CheckEqual(new Color(1,1,1)));
        Assert.True(varCh.GetColorLocal(new Point(0.99,0,0)).CheckEqual(new Color(1,1,1)));
        Assert.True(varCh.GetColorLocal(new Point(1.01,0,0)).CheckEqual(new Color(0,0,0)));
        Assert.True(varCh.GetColorLocal(new Point(0,0.99,0)).CheckEqual(new Color(1,1,1)));
        Assert.True(varCh.GetColorLocal(new Point(0,1.01,0)).CheckEqual(new Color(0,0,0)));
        Assert.True(varCh.GetColorLocal(new Point(0,0,0.99)).CheckEqual(new Color(1,1,1)));
        Assert.True(varCh.GetColorLocal(new Point(0,0,1.01)).CheckEqual(new Color(0,0,0)));
    }
}