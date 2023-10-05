#include "form.h"
#include "comparinator.h"
#include "material.h"
#include "intersection.h"
#include "tuple.h"
#include "matrix.h"
#include "light.h"
#include "ray.h"
#include "pattern.h"
#include "pch.h"

Form::Form () {
	mbrOrigin = Point(0, 0, 0);
	mbrTransform = new IdentityMatrix(4, 4);
	mbrMaterial = Material();
}

bool Form::checkEqual(Form other)
{
	Comparinator ce = Comparinator();
	return ce.checkTuple(this->mbrOrigin,other.mbrOrigin) && mbrTransform->checkEqual(*other.mbrTransform) && mbrMaterial.checkEqual(other.mbrMaterial) && mbrObjectRay.checkEqual(other.mbrObjectRay);
}
Form& Form::operator=(const Form other)
{
	if (this == &other) return *this;
	mbrOrigin = other.mbrOrigin;
	mbrRadius = other.mbrRadius;
	this->mbrTransform = new Matrix(*other.mbrTransform);
	this->mbrMaterial = other.mbrMaterial;
	return *this;
}
Color Form::getColorShaded(PointSource argLighting, Point argPosition, Vector argEye, Vector argNormal, bool argInShadow)
{
	return mbrMaterial.getColorShaded(argLighting, argPosition, argEye, argNormal, argInShadow);
}
Color Form::getColorLocal(Point argPosition)
{
	Point varObjP = *(mbrTransform->invert()) * argPosition;
	Point varPatternP = *(mbrMaterial.mbrPattern->mbrTransform->invert()) * varObjP;
	return mbrMaterial.mbrPattern->getColor(varPatternP);
}
void Form::setTransform(const Matrix m)
{
	this->mbrTransform = new Matrix(m);
}
Intersections Form::getIntersections(Ray argRay)
{
	Intersections varIntersections;
	mbrObjectRay = argRay.transform(*(this->mbrTransform->invert()));
	Vector sphereToRay = mbrObjectRay.mbrOrigin - (Point(0, 0, 0));
	double a = mbrObjectRay.mbrDirection.dot(mbrObjectRay.mbrDirection);
	double b = 2 * mbrObjectRay.mbrDirection.dot(sphereToRay);
	double c = sphereToRay.dot(sphereToRay) - 1;
	double discriminant = pow(b,2) - 4 * a * c;
	if (discriminant < 0) return varIntersections;
	double intersectOne = (-b - sqrt(discriminant)) / (2 * a);
	double intersectTwo = (-b + sqrt(discriminant)) / (2 * a);
	if (intersectOne < intersectTwo)
	{
		varIntersections.intersect(intersectOne, *this);
		varIntersections.intersect(intersectTwo, *this);
	}
	else
	{
		varIntersections.intersect(intersectTwo, *this);
		varIntersections.intersect(intersectOne, *this);
	}
	return varIntersections;
}
Vector Form::getNormal(Point argPoint)
{
	Point varObjectPoint = *(this->mbrTransform->invert()) * argPoint;
	Vector varObjectNormal = varObjectPoint - this->mbrOrigin;
	Matrix varTransform = *(*this->mbrTransform->invert()).transpose();
	Vector varWorldNormal = varTransform * varObjectNormal;
	varWorldNormal.mbrW = 0;
	return varWorldNormal.normalize();
}

Sphere::Sphere() : Form ()
{
	mbrRadius = 1.0;
}
Intersections Sphere::getIntersections(Ray argRay)
{
	Intersections varIntersections;
	mbrObjectRay = argRay.transform(*(this->mbrTransform->invert()));
	Vector sphereToRay = mbrObjectRay.mbrOrigin - (Point(0, 0, 0));
	double a = mbrObjectRay.mbrDirection.dot(mbrObjectRay.mbrDirection);
	double b = 2 * mbrObjectRay.mbrDirection.dot(sphereToRay);
	double c = sphereToRay.dot(sphereToRay) - 1;
	double discriminant = pow(b,2) - 4 * a * c;
	if (discriminant < 0) return varIntersections;
	double intersectOne = (-b - sqrt(discriminant)) / (2 * a);
	double intersectTwo = (-b + sqrt(discriminant)) / (2 * a);
	if (intersectOne < intersectTwo)
	{
		varIntersections.intersect(intersectOne, *this);
		varIntersections.intersect(intersectTwo, *this);
	}
	else
	{
		varIntersections.intersect(intersectTwo, *this);
		varIntersections.intersect(intersectOne, *this);
	}
	return varIntersections;
}
Vector Sphere::getNormal(Point argPoint)
{
	// return (argPoint-origin).normalize();
	Point varObjectPoint = *(this->mbrTransform->invert()) * argPoint;
	Vector varObjectNormal = varObjectPoint - this->mbrOrigin;
	Matrix varTransform = *(*this->mbrTransform->invert()).transpose();
	Vector varWorldNormal = varTransform * varObjectNormal;
	varWorldNormal.mbrW = 0;
	return varWorldNormal.normalize();
}
bool Sphere::checkEqual(Form other)
{
	Comparinator ce = Comparinator();
	return ce.checkTuple(mbrOrigin,other.mbrOrigin) && mbrRadius == other.mbrRadius && mbrTransform->checkEqual(*other.mbrTransform) && mbrMaterial.checkEqual(other.mbrMaterial);
}


Vector Plane::getNormal(Point argPoint)
{
	return Vector(0,1,0);
}
Intersections Plane::getIntersections(Ray argRay)
{
	if (abs(argRay.mbrDirection.mbrY) <= getEpsilon()) { return Intersections(); }
	double varTime = -argRay.mbrOrigin.mbrY / argRay.mbrDirection.mbrY;
	return Intersections(varTime, *this);
}