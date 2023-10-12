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
    public void Canary()
    {
        Assert.Equal(1, 1);
    }

    public void CameraDefault ()
    {
        int varH = 160;
        int varV = 120;
        double varFOV = _fieldPM.getPI()/2;
        IdentityMatrix varIdentity = new IdentityMatrix(4,4);
        Camera varCamera = new Camera(varH, varV, varFOV);
        Assert.Equal(varCamera._fieldCanvasHorizontal, varH);
        Assert.Equal(varCamera._fieldCanvasVertical, varV);
        Assert.True(_fieldComp.CheckFloat(varCamera._fieldFieldOfView, varFOV));
        Assert.True(varIdentity.CheckEqual(varCamera._fieldTransform));
    }

    public void CameraCanvasPixelSizeHorizontalGTVertical ()
    {
        Camera varCamera = new Camera(200, 125, _fieldPM.getPI()/2);
        Assert.True(_fieldComp.CheckFloat(varCamera._fieldPixelSquare, 0.01));
    }

    public void CameraCanvasPixelSizeVerticalGTHorizontal ()
    {
        Camera varCamera = new Camera(125, 200, _fieldPM.getPI()/2);
        Assert.True(_fieldComp.CheckFloat(varCamera._fieldPixelSquare, 0.01));
    }

    public void CameraRayCastToCanvasCenter ()
    {
        Camera varCamera = new Camera(201, 101, _fieldPM.getPI()/2);
        Ray varCast = varCamera.GetRay(100,50);
        Point varExpectedOrigin = new Point(0,0,0);
        Vector varExpectedDirection = new Vector(0,0,-1);
        Assert.True(_fieldComp.CheckTuple(varCast._fieldOrigin, varExpectedOrigin));
        Assert.True(_fieldComp.CheckTuple(varCast._fieldDirection, varExpectedDirection));
    }

    public void CameraRayCastToCanvasCorner ()
    {
        Camera varCamera = new Camera(201, 101, _fieldPM.getPI()/2);
        Ray varCast = varCamera.GetRay(0,0);
        Point varExpectedOrigin = new Point(0,0,0);
        Vector varExpectedDirection = new Vector(0.66519, 0.33259, -.66851);
        Assert.True(_fieldComp.CheckTuple(varCast._fieldOrigin, varExpectedOrigin));
        Assert.True(_fieldComp.CheckTuple(varCast._fieldDirection, varExpectedDirection));
    }

    public void TransformedCameraRayCastToCanvas ()
    {
        Camera varCamera = new Camera(201, 101, _fieldPM.getPI()/2);
        // varCamera.setTransform(YRotationMatrix(_fieldPM.getPI()/4) * TranslationMatrix(0,-2,5));
        varCamera.SetTransform(new Matrix(new YRotationMatrix(_fieldPM.getPI()/4) * new TranslationMatrix(0,-2,5)));
        Ray varCast = varCamera.GetRay(100,50);
        Point varExpectedOrigin = new Point(0,2,-5);
        Vector varExpectedDirection = new Vector(Math.Sqrt(2)/2,0,-Math.Sqrt(2)/2);
        Assert.True(_fieldComp.CheckTuple(varCast._fieldOrigin, varExpectedOrigin));
        Assert.True(_fieldComp.CheckTuple(varCast._fieldDirection, varExpectedDirection));
    }

    public void CameraRenderDefaultWorld ()
    {
        DefaultWorld varWorld = new DefaultWorld();
        Camera varCamera = new Camera(11, 11, _fieldPM.getPI()/2);
        // varCamera.setTransform(new ViewMatrix(Point(0,0,-5), Point(0,0,0), Vector(0,1,0)));
        varCamera.SetTransform(new ViewMatrix(new Point(0,0,-5), new Point(0,0,0), new Vector(0,1,0)));
        Canvas varImg = varCamera.RenderCanvas(varWorld);
        Color varExpectedColor = new Color(0.38066, 0.47583, 0.2855);
        Assert.True(varImg.getPixel(5,5).CheckEqual(varExpectedColor));
    }
}