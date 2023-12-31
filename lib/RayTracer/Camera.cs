using LibMatrix;
using LibTuple;
using LibRay;
using LibWorld;
using LibCanvas;
using LibColor;

namespace LibCamera;

public class Camera {
    public int _fieldCanvasHorizontal;
    public int _fieldCanvasVertical;
    public double _fieldFieldOfView;
    public double _fieldHalfWidth;
    public double _fieldHalfHeight;
    public double _fieldPixelSquare;
    public Matrix _fieldTransform;
    public Matrix _fieldTransformInverse;
    public Camera(Camera argOther) {
        _fieldCanvasHorizontal = argOther._fieldCanvasHorizontal;
        _fieldCanvasVertical = argOther._fieldCanvasVertical;
        _fieldFieldOfView = argOther._fieldFieldOfView;
        _fieldHalfWidth = argOther._fieldHalfWidth;
        _fieldHalfHeight = argOther._fieldHalfHeight;
        _fieldPixelSquare = argOther._fieldPixelSquare;
        _fieldTransform = new Matrix();
        _fieldTransformInverse = new Matrix();
        SetTransform(argOther._fieldTransform);
    }
    public Camera(int argHorizontal, int argVertical, double argFOV)
    {
        _fieldCanvasHorizontal = argHorizontal;
        _fieldCanvasVertical = argVertical;
        _fieldFieldOfView = argFOV;
        double varHalfView = Math.Tan(_fieldFieldOfView/2);
        double varAspectRatio = (double)_fieldCanvasHorizontal/(double)_fieldCanvasVertical;
        if (varAspectRatio >= 1) {
            _fieldHalfWidth = varHalfView;
            _fieldHalfHeight = varHalfView/varAspectRatio;
        }
        else {
            _fieldHalfWidth = varHalfView * varAspectRatio;
            _fieldHalfHeight = varHalfView;
        }
        _fieldPixelSquare = (_fieldHalfWidth * 2) / _fieldCanvasHorizontal;
        SetTransform(new IdentityMatrix(4,4));
    }
    public Ray GetRay(int argPxX, int argPxY)
    {
        double varOffsetX = (argPxX + 0.5) * _fieldPixelSquare;
        double varOffsetY = (argPxY + 0.5) * _fieldPixelSquare;
        double varWorldX = _fieldHalfWidth - varOffsetX;
        double varWorldY = _fieldHalfHeight - varOffsetY;
        SpaceTuple varPixelPos = _fieldTransformInverse * new Point(varWorldX, varWorldY, -1);
        SpaceTuple varOrigin = _fieldTransformInverse * new Point(0,0,0);
        SpaceTuple varDirection = (varPixelPos - varOrigin).GetNormal();
        return new Ray(varOrigin, varDirection);
    }

    public Canvas RenderCanvas(World argWorld)
    {
        Console.WriteLine($"{DateTime.Now.ToLocalTime()} Rendering to Canvas");
        int varCounterLimit = 1000;
        Canvas varCanvas = new Canvas(_fieldCanvasHorizontal, _fieldCanvasVertical);
        for (int i = 0; i < _fieldCanvasVertical; ++i)
        {
            for (int j = 0; j < _fieldCanvasHorizontal; ++j)
            {
                Ray varRay = GetRay(j, i);
                Color varColor = argWorld.GetColor(varRay);
                varCanvas.SetPixel(j, i, varColor);
            }
            if ((i * _fieldCanvasHorizontal) % varCounterLimit == 0) { Console.WriteLine($"{DateTime.Now.ToLocalTime()} Rendered Pixel: {i * _fieldCanvasHorizontal}/{_fieldCanvasVertical*_fieldCanvasHorizontal})"); }
        }
        return varCanvas;
    }
    public void SetTransform (Matrix argMatrix) {
        _fieldTransform = argMatrix;
        _fieldTransformInverse = _fieldTransform.GetInverse();
    }
}