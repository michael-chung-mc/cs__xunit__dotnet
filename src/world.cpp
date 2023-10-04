#include "world.h"
#include "sphere.h"
#include "material.h"
#include "matrix.h"
#include "ray.h"
#include "intersection.h"

World::World ()
{
}
Intersections World::intersect(Ray argRay)
{
    Intersections hits;
    for (int i = 0; i < objects.size(); i++)
    {
        std::vector<Intersection> hit = objects[i].intersect(argRay);
        for (int j = 0; j < hit.size(); j++)
        {
            hits.intersect(hit[j].time, hit[j].object);
        }
    }
    return hits;
}
Color World::getShade(IntersectionState argIntersectionState)
{
    Color varShade = Color(0,0,0);
    for (int i = 0; i < lights.size();i++)
    {
        varShade = varShade + argIntersectionState.object.material.getLighting(lights[i], argIntersectionState.point, argIntersectionState.pov, argIntersectionState.normal, false);
    }
    return varShade;
}
Color World::getColor(Ray r)
{
    Intersections xs = this->intersect(r);
    Intersection hit = xs.hit();
    if (hit.checkEqual(Intersection())) return Color(0,0,0);
    IntersectionState is = hit.getState(r);
    return this->getShade(is);
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
    this->lights.push_back(varDefaultLight);
    this->objects.push_back(s);
    this->objects.push_back(t);
}