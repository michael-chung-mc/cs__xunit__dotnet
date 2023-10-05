#pragma once
#ifndef MATERIAL_H
#define MATERIAL_H

class PointSource;
class Point;
class Vector;
class Material;
#include "color.h"
#include "pattern.h"

class Material {
public:
    double mbrAmbient;
    double mbrDiffuse;
    double mbrSpecular;
    double mbrShininess;
    Color mbrColor;
    Pattern mbrPattern;
    Material();
    bool checkEqual(Material other);
    Color getLighting(PointSource argLighting, Point argPosition, Vector argEye, Vector argNormal, bool argInShadow);
};

#endif