#include "camera.h"
#include "matrix.h"

Camera::Camera(int argH, int argV, double argFOV)
{
    membSizeHorizontal = argH;
    membSizeVertical = argV;
    membFieldOfView = argFOV;
    membTransform = IdentityMatrix(4,4);
}
