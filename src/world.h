#pragma once
#ifndef WORLD_H
#define WORLD_H

class Sphere;
class Intersection;
class IntersectionState;
class Ray;
class Color;
#include "light.h"
#include <vector>

class World {
public:
    std::vector<Sphere> objects;
    std::vector<PointSource> lights;
    World();
    std::vector<Intersection> intersect(Ray argRay);
    Color getShade(IntersectionState argIntersectionState);
};

class DefaultWorld : public World {
public:
    DefaultWorld();
};

#endif