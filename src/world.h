#pragma once
#ifndef WORLD_H
#define WORLD_H

#include "sphere.h"
#include "light.h"
#include <vector>

class World {
public:
    std::vector<Sphere> objects;
    PointSource light;
    World();
};

class DefaultWorld : public World {
public:
    DefaultWorld();
};

#endif