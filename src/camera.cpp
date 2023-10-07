#include "camera.h"
#include "matrix.h"
#include "ray.h"
#include "canvas.h"
#include "world.h"
#include <cmath>

Camera::Camera(const Camera &argOther) {
    mbrCanvasHorizontal = argOther.mbrCanvasHorizontal;
    mbrCanvasVertical = argOther.mbrCanvasVertical;
    mbrFieldOfView = argOther.mbrFieldOfView;
    mbrHalfWidth = argOther.mbrHalfWidth;
    mbrHalfHeight = argOther.mbrHalfHeight;
    mbrPixelSquare = argOther.mbrPixelSquare;
    setTransform(argOther.mbrTransform.get());
}
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
    mbrTransform = std::make_unique<Matrix>(IdentityMatrix(4,4));
}
Camera::~Camera() {
}
Camera& Camera::operator=(const Camera &argOther) {
    if (this == &argOther) return *this;
    mbrCanvasHorizontal = argOther.mbrCanvasHorizontal;
    mbrCanvasVertical = argOther.mbrCanvasVertical;
    mbrFieldOfView = argOther.mbrFieldOfView;
    mbrHalfWidth = argOther.mbrHalfWidth;
    mbrHalfHeight = argOther.mbrHalfHeight;
    mbrPixelSquare = argOther.mbrPixelSquare;
    setTransform(argOther.mbrTransform.get());
    return *this;
}
Ray Camera::getRay(int argPxX, int argPxY)
{
    double varOffsetX = (argPxX + 0.5) * mbrPixelSquare;
    double varOffsetY = (argPxY + 0.5) * mbrPixelSquare;
    double varWorldX = mbrHalfWidth - varOffsetX;
    double varWorldY = mbrHalfHeight - varOffsetY;
    Point varPixelPos = *mbrTransform->invert() * Point(varWorldX, varWorldY, -1);
    Point varOrigin = *mbrTransform->invert() * Point(0,0,0);
    Vector varDirection = (varPixelPos - varOrigin).normalize();
    return Ray(varOrigin, varDirection);
}

Canvas Camera::render(World &argWorld)
{
    Canvas varCanvas = Canvas(mbrCanvasHorizontal, mbrCanvasVertical);
    for (int i = 0; i < mbrCanvasVertical; i++)
    {
        for (int j = 0; j < mbrCanvasHorizontal; j++)
        {
            Ray varRay = getRay(j, i);
            Color varColor = argWorld.getColor(varRay);
            varCanvas.setPixel(j, i, varColor);
        }
    }
    return varCanvas;
}
void Camera::setTransform (Matrix *argMatrix) {
    mbrTransform = std::make_unique<Matrix>(*argMatrix);
}