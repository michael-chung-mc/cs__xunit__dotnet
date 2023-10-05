#pragma once
#ifndef WORLD_H
#define WORLD_H

class Intersections;
class IntersectionState;
class Ray;
class Color;
#include "form.h"
#include "light.h"
#include <vector>

class World {
public:
    std::vector<Form> mbrObjects;
    std::vector<PointSource> mbrLights;
    World();
    Intersections getIntersect(Ray argRay);
    Color getColorShaded(IntersectionState argIntersectionState);
    Color getColor(Ray r);
    bool checkShadowed(Point argPoint);
    void setObject(Form argObject);
    void setLight(PointSource argLight);
};

class DefaultWorld : public World {
public:
    DefaultWorld();
};

#endif