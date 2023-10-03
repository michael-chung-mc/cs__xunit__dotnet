#include "camera.h"
#include "matrix.h"
#include <cmath>

Camera::Camera(int argH, int argV, double argFOV)
{
    mbrCanvasHorizontal = argH;
    mbrCanvasVertical = argV;
    mbrFieldOfView = argFOV;
    double varHalfView = tan(mbrFieldOfView/2);
    double varAspectRatio = (double)mbrCanvasHorizontal/(double)mbrCanvasVertical;
    if (varAspectRatio >= 1) {
        mbrHalfWidth = varHalfView;
        mbrHalfHeight = varHalfView/varAspectRatio;
    }
    else {
        mbrHalfWidth = varHalfView * varAspectRatio;
        mbrHalfHeight = varHalfView;
    }
    mbrPixelSquare = (mbrHalfWidth * 2) / mbrCanvasHorizontal;
    mbrTransform = IdentityMatrix(4,4);
}
