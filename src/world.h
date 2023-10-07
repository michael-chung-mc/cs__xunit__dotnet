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
    std::vector<std::unique_ptr<Form>> mbrObjects;
    std::vector<PointSource> mbrLights;
    World();
    World(const World &argOther);
    ~World() = default;
    World& operator=(const World &argOther);
    Intersections getIntersect(Ray argRay);
    Color getColorShaded(IntersectionState argIntersectionState);
    Color getColor(const Ray &r);
    bool checkShadowed(Point argPoint);
    void setObject(Form* argObject);
    // void setObject(std::unique_ptr<Form> &&argObject);
    void setLight(PointSource argLight);
};

class DefaultWorld : public World {
public:
    DefaultWorld();
};

#endif