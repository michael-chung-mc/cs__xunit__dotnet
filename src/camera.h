#pragma once
#ifndef CAMERA_H
#define CAMERA_H

#include "matrix.h"

class Camera {
public:
    int mbrCanvasHorizontal;
    int mbrCanvasVertical;
    double mbrFieldOfView;
    double mbrHalfWidth;
    double mbrHalfHeight;
    double mbrPixelSquare;
    Matrix mbrTransform;
    Camera(int argH, int argV, double argFOV);
};

#endif