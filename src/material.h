#pragma once
#ifndef Material_H
#define Material_H

#include "tuple.h"
#include "light.h"

class Material {
public:
    Color color;
    float ambient;
    float diffuse;
    float specular;
    double shininess;
    Material();
    bool checkEqual(Material other);
    Color getLighting(PointSource argLighting, Point argPosition, Vector argEye, Vector argNormal);
};

#endif