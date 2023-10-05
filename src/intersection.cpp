#include "intersection.h"
#include "form.h"
#include "ray.h"
#include "tuple.h"
#include "pch.h"
#include <algorithm>

Intersection::Intersection()
{
	this->mbrObject = Sphere();
	this->mbrTime = 0;
	mbrExists = true;
}
Intersection::Intersection(double time, Form s)
{
	this->mbrObject = s;
	this->mbrTime = time;
	mbrExists = true;
}

bool Intersection::checkEqual(Intersection other)
{
	return mbrTime == other.mbrTime && mbrObject.checkEqual(other.mbrObject);
}
bool Intersection::operator<(Intersection other) const
{
	return mbrTime < other.mbrTime;
}

IntersectionState Intersection::getState(Ray argRay)
{
	IntersectionState is = IntersectionState();
	is.mbrTime = mbrTime;
	is.mbrObject = mbrObject;
	is.mbrPoint = argRay.getPosition(is.mbrTime);
	is.mbrEye = -(argRay.argDirection);
	is.argNormal = is.mbrObject.getNormal(is.mbrPoint);
	if (is.argNormal.dot(is.mbrEye) < 0) {
		is.argInside = true;
		is.argNormal = -is.argNormal;
	}
	else {
		is.argInside = false;
	}
	is.mbrOverPoint = is.mbrPoint + is.argNormal * getEpsilon();
	return is;
}

Intersections::Intersections()
{
	//Intersection i = Intersection(0, Sphere());
	//this->intersections.push_back(i);
}
Intersections::Intersections(const Intersections& other)
{
	this->mbrIntersections = other.mbrIntersections;
}
Intersections::Intersections(double t, Form s)
{
	Intersection i = Intersection(t,s);
	mbrIntersections.push_back(i);
}
void Intersections::intersect(double t, Form s)
{
	mbrIntersections.push_back(Intersection(t, s));
    sort(mbrIntersections.begin(), mbrIntersections.end());
}

Intersection Intersections::hit()
{
	int index = -1;
	for (int i = 0; i < mbrIntersections.size(); i++)
	{
		if ((mbrIntersections[i].mbrTime > 0)
			&& ((index < 0) || (mbrIntersections[i].mbrTime <= mbrIntersections[index].mbrTime)))
		{
			index = i;
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
		return mbrIntersections[index];
	}
}

Intersections& Intersections::operator=(const Intersections other)
{
	if (this == &other) return *this;
	this->mbrIntersections = other.mbrIntersections;
	return *this;
}