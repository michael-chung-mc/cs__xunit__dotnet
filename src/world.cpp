#include "world.h"
#include "form.h"
#include "material.h"
#include "matrix.h"
#include "ray.h"
#include "intersection.h"

World::World ()
{
}
Intersections World::getIntersect(Ray argRay)
{
    Intersections hits;
    for (int i = 0; i < mbrObjects.size(); i++)
    {
        std::vector<Intersection> hit = mbrObjects[i].getIntersections(argRay).mbrIntersections;
        for (int j = 0; j < hit.size(); j++)
        {
            hits.intersect(hit[j].mbrTime, hit[j].mbrObject);
        }
    }
    return hits;
}
Color World::getShade(IntersectionState argIxState)
{
    bool varInShadow = checkShadowed(argIxState.mbrOverPoint);
    Color varShade = Color(0,0,0);
    for (int i = 0; i < mbrLights.size();i++)
    {
        varShade = varShade + argIxState.mbrObject.mbrMaterial.getLighting(mbrLights[i], argIxState.mbrPoint, argIxState.mbrEye, argIxState.mbrNormal, varInShadow);
    }
    return varShade;
}
Color World::getColor(Ray r)
{
    Intersections xs = this->getIntersect(r);
    Intersection hit = xs.hit();
    if (!hit.mbrExists) return Color(0,0,0);
    IntersectionState is = hit.getState(r);
    return this->getShade(is);
}
bool World::checkShadowed(Point argPoint) {
    bool varFlagShadow = false;
    for (int i = 0; i < mbrLights.size(); i++)
    {
        Vector varDirection = mbrLights[i].mbrPosition - argPoint;
        double varDistance = varDirection.magnitude();
        Vector varDirectionNormalized = varDirection.normalize();
        Ray varRay = Ray (argPoint, varDirectionNormalized);
        Intersection varHit = getIntersect(varRay).hit();
        bool varShadow = varHit.mbrExists && (varHit.mbrTime < varDistance);
        varFlagShadow = varShadow ? true : varFlagShadow;
    }
    return varFlagShadow;
}
void World::setObject(Sphere argObject) {
    mbrObjects.push_back(argObject);
}
void World::setLight(PointSource argLight) {
    mbrLights.push_back(argLight);
}

DefaultWorld::DefaultWorld() : World()
{
    PointSource varDefaultLight = PointSource(Point(-10,10,-10), Color(1,1,1));
    this->mbrLights.push_back(varDefaultLight);
    Sphere s = Sphere();
    s.mbrMaterial = Material();
    s.mbrMaterial.mbrColor = Color (0.8,1.0,0.6);
    s.mbrMaterial.mbrDiffuse = 0.7;
    s.mbrMaterial.mbrSpecular = 0.2;
    Sphere t = Sphere();
    t.mbrTransform = ScalingMatrix(0.5,0.5,0.5);
    this->mbrObjects.push_back(s);
    this->mbrObjects.push_back(t);
}