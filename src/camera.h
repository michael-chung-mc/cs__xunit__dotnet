#pragma once
#ifndef CAMERA_H
#define CAMERA_H

#include "matrix.h"

class Camera {
public:
    int membSizeHorizontal;
    int membSizeVertical;
    double membFieldOfView;
    Matrix membTransform;
    Camera(int argH, int argV, double argFOV);
};

#endif