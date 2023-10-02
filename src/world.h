#pragma once
#ifndef WORLD_H
#define WORLD_H

class Sphere;
class Intersection;
class Ray;
#include "light.h"
#include <vector>

class World {
public:
    std::vector<Sphere> objects;
    PointSource light;
    World();
    std::vector<Intersection> intersect(Ray argRay);
};

class DefaultWorld : public World {
public:
    DefaultWorld();
};

#endif