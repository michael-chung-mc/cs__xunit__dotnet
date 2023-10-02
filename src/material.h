#include "pch.h"

#pragma once
#ifndef Material_H
#define Material_H

class Material {
public:
    Color color;
    float ambient;
    float diffuse;
    float specular;
    double shininess;
    Material();
    bool checkEqual(Material other);
};

#endif