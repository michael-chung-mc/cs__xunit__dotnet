#pragma once
#ifndef MATERIAL_H
#define MATERIAL_H

#include "color.h"
class PointSource;
class Point;
class Vector;
class Material;

class Material {
public:
    Color color;
    double ambient;
    double diffuse;
    double specular;
    double shininess;
    Material();
    bool checkEqual(Material other);
    Color getLighting(PointSource argLighting, Point argPosition, Vector argEye, Vector argNormal);
};

#endif