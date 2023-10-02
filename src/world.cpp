#include "world.h"
#include "sphere.h"
#include "material.h"
#include "matrix.h"
#include "ray.h"
#include "intersection.h"
#include <algorithm>

World::World ()
{
}
std::vector<Intersection> World::intersect(Ray argRay)
{
    std::vector<Intersection> hits;
    for (int i = 0; i < objects.size(); i++)
    {
        std::vector<Intersection> hit = objects[i].intersect(argRay);
        for (int j = 0; j < hit.size(); j++)
        {
            hits.push_back(hit[j]);
        }
    }
    sort(hits.begin(), hits.end());
    return hits;
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