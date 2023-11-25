using Xunit;
using LibCamera;
using LibComparinator;
using LibProjectMeta;
using LibMatrix;
using LibWorld;
using LibForm;
using LibRay;
using LibTuple;
using LibCanvas;
using LibColor;
namespace LibCamera.Test;

public class CameraTest {
	Comparinator _fieldComp = new Comparinator();
    ProjectMeta _fieldPM = new ProjectMeta();
    [Fact]
    public void CameraTestCanary_WithDefault_ExpectDefault()
    {
        Assert.Equal(1, 1);
    }
    [Theory]
    [InlineData(160,120)]
    public void CameraConstructor_WithGiven_ExpectGiven (int argHorizontal, int argVertical)
    {
        IdentityMatrix varIdentity = new IdentityMatrix(4,4);
        Camera varCamera = new Camera(argHorizontal, argVertical, _fieldPM.GetPI()/2);
        Assert.Equal(varCamera._fieldCanvasHorizontal, argHorizontal);
        Assert.Equal(varCamera._fieldCanvasVertical, argVertical);
        Assert.True(_fieldComp.CheckFloat(varCamera._fieldFieldOfView, _fieldPM.GetPI()/2));
        Assert.True(varIdentity.CheckEqual(varCamera._fieldTransform));
    }
    [Theory]
    [InlineData(200,125)]
    public void CameraConstructor_WithCanvasPixelSizeHorizontalGTVertical_ExpectMin (int argHorizontal, int argVertical)
    {
        Camera varCamera = new Camera(argHorizontal, argVertical, _fieldPM.GetPI()/2);
        Assert.True(_fieldComp.CheckFloat(varCamera._fieldPixelSquare, 0.01));
    }
    [Fact]
    public void CameraConstructor_WithCanvasPixelSizeVerticalGTHorizontal_ExpectMin ()
    {
        Camera varCamera = new Camera(125, 200, _fieldPM.GetPI()/2);
        Assert.True(_fieldComp.CheckFloat(varCamera._fieldPixelSquare, 0.01));
    }
    [Fact]
    public void CameraGetRay_WithDefault_ExpectCastToCanvasCenter ()
    {
        Camera varCamera = new Camera(201, 101, _fieldPM.GetPI()/2);
        Ray varCast = varCamera.GetRay(100,50);
        Point varExpectedOrigin = new Point(0,0,0);
        Vector varExpectedDirection = new Vector(0,0,-1);
        Assert.True(_fieldComp.CheckTuple(varCast._fieldOrigin, varExpectedOrigin));
        Assert.True(_fieldComp.CheckTuple(varCast._fieldDirection, varExpectedDirection));
    }
    [Fact]
    public void CameraGetRay_WithDefault_ExpectCastToCanvasCorner ()
    {
        Camera varCamera = new Camera(201, 101, _fieldPM.GetPI()/2);
        Ray varCast = varCamera.GetRay(0,0);
        Point varExpectedOrigin = new Point(0,0,0);
        Vector varExpectedDirection = new Vector(0.66519, 0.33259, -.66851);
        Assert.True(_fieldComp.CheckTuple(varCast._fieldOrigin, varExpectedOrigin));
        Assert.True(_fieldComp.CheckTuple(varCast._fieldDirection, varExpectedDirection));
    }
    [Fact]
    public void CameraGetRay_WithTransformedCamera_ExpectCastToCanvas ()
    {
        Camera varCamera = new Camera(201, 101, _fieldPM.GetPI()/2);
        // varCamera.setTransform(YRotationMatrix(_fieldPM.getPI()/4) * TranslationMatrix(0,-2,5));
        varCamera.SetTransform(new Matrix(new YRotationMatrix(_fieldPM.GetPI()/4) * new TranslationMatrix(0,-2,5)));
        Ray varCast = varCamera.GetRay(100,50);
        Point varExpectedOrigin = new Point(0,2,-5);
        Vector varExpectedDirection = new Vector(Math.Sqrt(2)/2,0,-Math.Sqrt(2)/2);
        Assert.True(_fieldComp.CheckTuple(varCast._fieldOrigin, varExpectedOrigin));
        Assert.True(_fieldComp.CheckTuple(varCast._fieldDirection, varExpectedDirection));
    }
    [Fact]
    public void CameraRender_WithDefaultWorld_ExpectColor ()
    {
        DefaultWorld varWorld = new DefaultWorld();
        Camera varCamera = new Camera(11, 11, _fieldPM.GetPI()/2);
        // varCamera.setTransform(new ViewMatrix(Point(0,0,-5), Point(0,0,0), Vector(0,1,0)));
        varCamera.SetTransform(new ViewMatrix(new Point(0,0,-5), new Point(0,0,0), new Vector(0,1,0)));
        Canvas varImg = varCamera.RenderCanvas(varWorld);
        Color varExpectedColor = new Color(0.38066, 0.47583, 0.2855);
        Assert.True(varImg.GetPixel(5,5).CheckEqual(varExpectedColor));
    }
}