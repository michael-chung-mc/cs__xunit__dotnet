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
    std::unique_ptr<Matrix> mbrTransform;
    Camera(const Camera &argOther);
    Camera(int argH, int argV, double argFOV);
    ~Camera();
    Camera& operator=(const Camera &argOther);
    Ray getRay(int argPxX, int argPxY);
    Canvas render(World &argWorld);
    void setTransform (Matrix *argMatrix);
};

#endif