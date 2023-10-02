#include "world.h"

World::World ()
{
}

DefaultWorld::DefaultWorld() : World()
{
    PointSource varDefaultLight = PointSource(Point(-10,-10,-10), Color(1,1,1));
    Sphere s = Sphere();
    s.material = Material();
    s.material.color = Color (0.8,1.0,0.6);
    s.material.diffuse = 0.7;
    s.material.specular = 0.2;
    Sphere t = Sphere();
    t.transform = ScalingMatrix(0.5,0.5,0.5);
    this->light = varDefaultLight;
    this->objects.push_back(s);
    this->objects.push_back(t);
}