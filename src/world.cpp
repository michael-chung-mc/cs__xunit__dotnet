#include "world.h"
#include "form.h"
#include "material.h"
#include "matrix.h"
#include "ray.h"
#include "intersection.h"

World::World ()
{
    mbrObjects.clear();
}
World::World (const World &argOther)
{
    mbrObjects.clear();
    for (int i = 0; i < argOther.mbrObjects.size(); i++)
    {
        setObject(argOther.mbrObjects[i].get());
        // mbrObjects.push_back(std::make_unique<Form>(*argOther.mbrObjects[i].get()));
    }
    mbrLights = argOther.mbrLights;
}
World& World::operator=(const World &argOther)
{
    if (this == &argOther) return *this;
    mbrObjects.clear();
    for (int i = 0; i < argOther.mbrObjects.size(); i++)
    {
        setObject(argOther.mbrObjects[i].get());
        //mbrObjects.push_back(std::make_unique<Form>(*argOther.mbrObjects[i].get()));
    }
    mbrLights = argOther.mbrLights;
    return *this;
}
Intersections World::getIntersect(Ray argRay)
{
    Intersections hits = Intersections();
    for (int i = 0; i < mbrObjects.size(); i++)
    {
        Intersections varIx = mbrObjects[i]->getIntersections(argRay);
        for (int j = 0; j < varIx.mbrIntersections.size(); j++)
        {
            hits.intersect(varIx.mbrIntersections[j]->mbrTime, varIx.mbrIntersections[j]->mbrObject.get());
        }
    }
    return hits;
}
Color World::getColorShaded(IntersectionState argIxState)
{
    bool varInShadow = checkShadowed(argIxState.mbrOverPoint);
    Color varShade = Color(0,0,0);
    for (int i = 0; i < mbrLights.size();i++)
    {
        varShade = varShade + argIxState.mbrObject->getColorShaded(mbrLights[i], argIxState.mbrPoint, argIxState.mbrEye, argIxState.mbrNormal, varInShadow);
    }
    return varShade;
}
Color World::getColor(const Ray &r)
{
    Intersections xs = getIntersect(r);
    Intersection hit = xs.hit();
    // if (!hit.mbrExists) return Color(50,205,50);
    if (!hit.mbrExists) return Color(0,0,0);
    IntersectionState is = hit.getState(r);
    return getColorShaded(is);
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
void World::setObject(Form* argObject) {
    if (Sphere *varSphere = dynamic_cast<Sphere *>(argObject))
    {
        mbrObjects.push_back(std::make_unique<Sphere>(*varSphere));
    }
    else if (Plane *varPlane = dynamic_cast<Plane *>(argObject))
    {
        mbrObjects.push_back(std::make_unique<Plane>(*varPlane));
    }
    else {
        mbrObjects.push_back(std::make_unique<Form>(*argObject));
    }
}
// void World::setObject(std::unique_ptr<Form> &&argObject) {
//     //mbrObjects.push_back(std::make_unique<Form>(*argObject.get()));
//     mbrObjects.push_back(std::move(argObject));
// }
void World::setLight(PointSource argLight) {
    mbrLights.push_back(argLight);
}

DefaultWorld::DefaultWorld() : World()
{
    PointSource varDefaultLight = PointSource(Point(-10,10,-10), Color(1,1,1));
    setLight(varDefaultLight);
    Sphere s = Sphere();
    s.setMaterial(Material());
    s.mbrMaterial->mbrColor = Color (0.8,1.0,0.6);
    s.mbrMaterial->mbrDiffuse = 0.7;
    s.mbrMaterial->mbrSpecular = 0.2;
    Sphere t = Sphere();
    t.setTransform(ScalingMatrix(0.5,0.5,0.5));
    setObject(new Sphere(s));
    setObject(new Sphere(t));
    // this->mbrObjects.push_back(std::make_unique<Sphere>(s));
    // this->mbrObjects.push_back(std::make_unique<Sphere>(t));
}