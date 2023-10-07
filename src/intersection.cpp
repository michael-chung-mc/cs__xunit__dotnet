#include "intersection.h"
#include "form.h"
#include "ray.h"
#include "tuple.h"
#include "pch.h"
#include <algorithm>

IntersectionState::IntersectionState() {
	mbrTime = 0;
	mbrObject = std::make_unique<Form>();
	mbrPoint = Point();
	mbrEye = Vector();
	mbrNormal = Vector();
	mbrInside = false;
	mbrOverPoint = Point();
}
IntersectionState::IntersectionState(const IntersectionState &argOther) {
	mbrTime = argOther.mbrTime;
	setObject(argOther.mbrObject.get());
	mbrPoint = argOther.mbrPoint;
	mbrEye = argOther.mbrEye;
	mbrNormal = argOther.mbrNormal;
	mbrInside = argOther.mbrInside;
	mbrOverPoint = argOther.mbrOverPoint;
}
IntersectionState& IntersectionState::operator=(const IntersectionState &argOther) {
	if (this == &argOther) return *this;
	mbrTime = argOther.mbrTime;
	setObject(argOther.mbrObject.get());
	mbrPoint = argOther.mbrPoint;
	mbrEye = argOther.mbrEye;
	mbrNormal = argOther.mbrNormal;
	mbrInside = argOther.mbrInside;
	mbrOverPoint = argOther.mbrOverPoint;
	return *this;
}
void IntersectionState::setObject(Form* argObject) {
    if (Sphere *varSphere = dynamic_cast<Sphere *>(argObject))
    {
        mbrObject = std::make_unique<Sphere>(*varSphere);
    }
    else if (Plane *varPlane = dynamic_cast<Plane *>(argObject))
    {
        mbrObject = std::make_unique<Plane>(*varPlane);
    }
    else {
        mbrObject = std::make_unique<Form>(*argObject);
    }
}

Intersection::Intersection()
{
	mbrObject = std::make_unique<Form>();
	mbrTime = 0;
	mbrExists = true;
}
Intersection::Intersection(double time, Form *s)
{
	setObject(s);
	this->mbrTime = time;
	mbrExists = true;
}
// Intersection::Intersection(double time, std::unique_ptr<Form> s)
// {
// 	setObject(s.get());
// 	this->mbrTime = time;
// 	mbrExists = true;
// }
Intersection::Intersection(const Intersection &argOther) {
	setObject(argOther.mbrObject.get());
	mbrTime = argOther.mbrTime;
	mbrExists = argOther.mbrExists;
}
Intersection& Intersection::operator=(const Intersection argOther){
	if (this == &argOther) return *this;
	setObject(argOther.mbrObject.get());
	mbrTime = argOther.mbrTime;
	mbrExists = argOther.mbrExists;
	return *this;
}
bool Intersection::checkEqual(Intersection other)
{
	return mbrTime == other.mbrTime && mbrObject->checkEqual(*other.mbrObject.get());
}
IntersectionState Intersection::getState(Ray argRay)
{
	IntersectionState is = IntersectionState();
	is.mbrTime = mbrTime;
	is.setObject(mbrObject.get());
	is.mbrPoint = argRay.getPosition(is.mbrTime);
	is.mbrEye = -(argRay.mbrDirection);
	is.mbrNormal = is.mbrObject->getNormal(is.mbrPoint);
	if (is.mbrNormal.dot(is.mbrEye) < 0) {
		is.mbrInside = true;
		is.mbrNormal = -is.mbrNormal;
	}
	else {
		is.mbrInside = false;
	}
	is.mbrOverPoint = is.mbrPoint + is.mbrNormal * getEpsilon();
	return is;
}
void Intersection::setObject(Form* argObject) {
    if (Sphere *varSphere = dynamic_cast<Sphere *>(argObject))
    {
        mbrObject = std::make_unique<Sphere>(*varSphere);
    }
    else if (Plane *varPlane = dynamic_cast<Plane *>(argObject))
    {
        mbrObject = std::make_unique<Plane>(*varPlane);
    }
    else {
        mbrObject = std::make_unique<Form>(*argObject);
    }
}

Intersections::Intersections()
{
	//Intersection i = Intersection(0, Sphere());
	//this->intersections.push_back(i);
}
Intersections::Intersections(const Intersections& other)
{
	mbrIntersections.clear();
	for (int i = 0; i < other.mbrIntersections.size(); i++)
	{
		setIntersection(other.mbrIntersections[i].get());
		// Intersection varIs = other.mbrIntersections[i];
		// mbrIntersections.push_back(varIs);
	}
	//this->mbrIntersections = other.mbrIntersections;
}
Intersections::Intersections(double t, Form *s)
{
	setIntersection(t, s);
	// Intersection i = Intersection(t,std::make_unique<Form>(*s.get()));
	// mbrIntersections.push_back(i);
}
// Intersections::Intersections(double t, std::unique_ptr<Form> s)
// {
// 	setIntersection(t, s.get());
// 	// Intersection i = Intersection(t,std::make_unique<Form>(*s.get()));
// 	// mbrIntersections.push_back(i);
// }
Intersections& Intersections::operator=(const Intersections other)
{
	if (this == &other) return *this;
	mbrIntersections.clear();
	for (int i = 0; i < other.mbrIntersections.size(); i++)
	{
		setIntersection(other.mbrIntersections[i].get());
		// Intersection varIs = other.mbrIntersections[i];
		// mbrIntersections.push_back(varIs);
	}
	// this->mbrIntersections = other.mbrIntersections;
	return *this;
}
// void Intersections::intersect(double t, std::unique_ptr<Form> argObject)
// {
// 	auto funcComp = [&](const std::unique_ptr<Intersection> &argA, const std::unique_ptr<Intersection> &argB) -> bool {
// 		return argA->mbrTime < argB->mbrTime;
// 	};
// 	setIntersection(t, argObject.get());
// 	//mbrIntersections.push_back(Intersection(t, std::make_unique<Form>(*argObject)));
//     sort(mbrIntersections.begin(), mbrIntersections.end(), funcComp);
// }
void Intersections::intersect(double t, Form* argObject)
{
	auto funcComp = [&](const std::unique_ptr<Intersection> &argA, const std::unique_ptr<Intersection> &argB) -> bool {
		return argA->mbrTime < argB->mbrTime;
	};
	setIntersection(t, argObject);
	//mbrIntersections.push_back(Intersection(t, std::make_unique<Form>(*argObject)));
    sort(mbrIntersections.begin(), mbrIntersections.end(), funcComp);
}
Intersection Intersections::hit()
{
	int index = -1;
	for (int i = 0; i < mbrIntersections.size(); i++)
	{
		if ((mbrIntersections[i]->mbrTime > 0)
			&& ((index < 0) || (mbrIntersections[i]->mbrTime <= mbrIntersections[index]->mbrTime)))
		{
			index = i;
			break;
		}
	}
	if (index < 0) {
		//std::cout << "hit missed" << std::endl;
		Intersection varIx = Intersection();
		varIx.mbrExists = false;
		return varIx;
	}
	else {
		//std::cout << "hit" << std::endl;
		return *mbrIntersections[index].get();
	}
}
void Intersections::setIntersection(Intersection* argIx) {
	mbrIntersections.push_back(std::make_unique<Intersection>(*argIx));
}
void Intersections::setIntersection(double t, Form* argObject) {
	mbrIntersections.push_back(std::make_unique<Intersection>(t, argObject));
}