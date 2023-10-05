#pragma once
#ifndef CAMERA_H
#define CAMERA_H

#include "ray.h"
#include "canvas.h"
#include "world.h"
#include "matrix.h"

class Camera {
public:
    int mbrCanvasHorizontal;
    int mbrCanvasVertical;
    double mbrFieldOfView;
    double mbrHalfWidth;
    double mbrHalfHeight;
    double mbrPixelSquare;
    Matrix *mbrTransform;
    Camera(int argH, int argV, double argFOV);
    ~Camera();
    Ray getRay(int argPxX, int argPxY);
    Canvas render(World argWorld);
    void setTransform (Matrix *argMatrix);
};

#endif