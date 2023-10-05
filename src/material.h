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
    Color mbrColor;
    double mbrAmbient;
    double mbrDiffuse;
    double mbrSpecular;
    double mbrShininess;
    Material();
    bool checkEqual(Material other);
    Color getLighting(PointSource argLighting, Point argPosition, Vector argEye, Vector argNormal, bool argInShadow);
};

#endif