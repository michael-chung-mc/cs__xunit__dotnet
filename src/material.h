#pragma once
#ifndef MATERIAL_H
#define MATERIAL_H

class PointSource;
class Point;
class Vector;
class Material;
class Pattern;
#include "color.h"
#include <memory>

class Material {
public:
    double mbrAmbient;
    double mbrDiffuse;
    double mbrSpecular;
    double mbrShininess;
    Color mbrColor;
    std::unique_ptr<Pattern> mbrPattern;
    Material();
	Material(const Material& other);
    ~Material();
	Material& operator=(const Material other);
    bool checkEqual(Material other);
    Color getColorShaded(PointSource argLighting, Point argPosition, Vector argEye, Vector argNormal, bool argInShadow);
    void setPattern(Pattern *argPattern);
};

#endif