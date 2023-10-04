#include "camera.h"
#include "matrix.h"
#include "ray.h"
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

Ray Camera::getRay(int argPxX, int argPxY)
{
    double varOffsetX = (argPxX + 0.5) * mbrPixelSquare;
    double varOffsetY = (argPxY + 0.5) * mbrPixelSquare;
    double varWorldX = mbrHalfWidth - varOffsetX;
    double varWorldY = mbrHalfHeight - varOffsetY;
    Point varPixelPos = *mbrTransform.invert() * Point(varWorldX, varWorldY, -1);
    Point varOrigin = *mbrTransform.invert() * Point(0,0,0);
    Vector varDirection = (varPixelPos - varOrigin).normalize();
    return Ray(varOrigin, varDirection);
}