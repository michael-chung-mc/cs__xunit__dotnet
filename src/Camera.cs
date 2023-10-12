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
    public Camera(int argH, int argV, double argFOV)
    {
        _fieldCanvasHorizontal = argH;
        _fieldCanvasVertical = argV;
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
    // public static Camera operator=(Camera argOther) {
    //     mbrCanvasHorizontal = argOther.mbrCanvasHorizontal;
    //     mbrCanvasVertical = argOther.mbrCanvasVertical;
    //     mbrFieldOfView = argOther.mbrFieldOfView;
    //     mbrHalfWidth = argOther.mbrHalfWidth;
    //     mbrHalfHeight = argOther.mbrHalfHeight;
    //     mbrPixelSquare = argOther.mbrPixelSquare;
    //     // setTransform(argOther.mbrTransform);
    //     SetTransform(argOther.mbrTransform);
    // }
    public Ray GetRay(int argPxX, int argPxY)
    {
        double varOffsetX = (argPxX + 0.5) * _fieldPixelSquare;
        double varOffsetY = (argPxY + 0.5) * _fieldPixelSquare;
        double varWorldX = _fieldHalfWidth - varOffsetX;
        double varWorldY = _fieldHalfHeight - varOffsetY;
        // Point varPixelPos = *mbrTransform->invert() * Point(varWorldX, varWorldY, -1);
        // Point varOrigin = *mbrTransform->invert() * Point(0,0,0);
        Point varPixelPos = _fieldTransformInverse * new Point(varWorldX, varWorldY, -1);
        Point varOrigin = _fieldTransformInverse * new Point(0,0,0);
        Vector varDirection = (varPixelPos - varOrigin).GetNormal();
        return new Ray(varOrigin, varDirection);
    }

    public Canvas RenderCanvas(World argWorld)
    {
        int varCounterLimit = 1000;
        Canvas varCanvas = new Canvas(_fieldCanvasHorizontal, _fieldCanvasVertical);
        for (int i = 0; i < _fieldCanvasVertical; ++i)
        {
            for (int j = 0; j < _fieldCanvasHorizontal; ++j)
            {
                Ray varRay = GetRay(j, i);
                Color varColor = argWorld.GetColor(varRay);
                varCanvas.setPixel(j, i, varColor);
                if ((i * _fieldCanvasVertical + j) % varCounterLimit == 0) { Console.Write($"rendered pixel: {i * _fieldCanvasVertical + j}/{_fieldCanvasVertical*_fieldCanvasHorizontal})"); }
            }
        }
        return varCanvas;
    }
    public void SetTransform (Matrix argMatrix) {
        _fieldTransform = argMatrix;
        _fieldTransformInverse = _fieldTransform.GetInverse();
    }
}