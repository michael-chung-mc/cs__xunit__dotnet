#pragma once
#ifndef WORLD_H
#define WORLD_H

class Intersections;
class IntersectionState;
class Ray;
class Color;
#include "sphere.h"
#include "light.h"
#include <vector>

class World {
public:
    std::vector<Sphere> objects;
    std::vector<PointSource> lights;
    World();
    Intersections intersect(Ray argRay);
    Color getShade(IntersectionState argIntersectionState);
    Color getColor(Ray r);
};

class DefaultWorld : public World {
public:
    DefaultWorld();
};

#endif