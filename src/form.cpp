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

Form::Form()
{
	mbrOrigin = Point(0, 0, 0);
	mbrRadius = 0;
	mbrTransform = std::make_unique<Matrix>(IdentityMatrix(4, 4));
	mbrMaterial = std::make_unique<Material>(Material());
	mbrObjectRay = Ray();
}
Form::Form(const Form &other)
{
	mbrOrigin = other.mbrOrigin;
	mbrRadius = other.mbrRadius;
	// mbrTransform = std::make_unique<Matrix>(*other.mbrTransform.get());
	setTransform(*other.mbrTransform.get());
	setMaterial(*other.mbrMaterial.get());
	mbrObjectRay = other.mbrObjectRay;
}
Form::~Form()
{
}
bool Form::checkEqual(Form other)
{
	Comparinator ce = Comparinator();
	return ce.checkTuple(this->mbrOrigin, other.mbrOrigin) && mbrTransform->checkEqual(*other.mbrTransform) && mbrMaterial->checkEqual(*other.mbrMaterial) && mbrObjectRay.checkEqual(other.mbrObjectRay);
}
Form &Form::operator=(const Form other)
{
	if (this == &other) return *this;
	mbrOrigin = other.mbrOrigin;
	mbrRadius = other.mbrRadius;
	setTransform(*other.mbrTransform.get());
	setMaterial(*other.mbrMaterial.get());
	return *this;
}
Color Form::getColorShaded(PointSource argLighting, Point argPosition, Vector argEye, Vector argNormal, bool argInShadow)
{
	Point varObjP = *(mbrTransform->invert()) * argPosition;
	Point varPatternP = *(mbrMaterial->mbrPattern->mbrTransform->invert()) * varObjP;
	return mbrMaterial->getColor(argLighting, varPatternP, argEye, argNormal, argInShadow);
	// return mbrMaterial->getColorShaded(argLighting, argPosition, argEye, argNormal, argInShadow);
}
Color Form::getColorLocal(Point argPosition)
{
	Point varObjP = *(mbrTransform->invert()) * argPosition;
	Point varPatternP = *(mbrMaterial->mbrPattern->mbrTransform->invert()) * varObjP;
	return mbrMaterial->mbrPattern->getColorLocal(varPatternP);
}
Intersections Form::getIntersections(Ray argRay)
{
    // std::cout << "default get intersections";
	mbrObjectRay = argRay.transform(*(this->mbrTransform->invert()));
	return getIntersectionsLocal(mbrObjectRay);
}
Intersections Form::getIntersectionsLocal(Ray argRay)
{
	return Intersections();
}
Vector Form::getNormal(Point argPoint)
{
	Point varObjectPoint = *(mbrTransform->invert()) * argPoint;
	Vector varObjectNormal = getNormalLocal(varObjectPoint);
	Matrix varTransform = *(*this->mbrTransform->invert()).transpose();
	Vector varWorldNormal = varTransform * varObjectNormal;
	varWorldNormal.mbrW = 0;
	return varWorldNormal.normalize();
}
Vector Form::getNormalLocal(Point argPoint)
{
	return Vector(argPoint.mbrX,argPoint.mbrY,argPoint.mbrZ);
}
void Form::setTransform(const Matrix &argMatrix)
{
	mbrTransform = std::make_unique<Matrix>(argMatrix);
}
void Form::setMaterial(const Material &argMaterial)
{
	mbrMaterial = std::make_unique<Material>(argMaterial);
}

Sphere::Sphere() : Form()
{
	mbrRadius = 1.0;
}
Intersections Sphere::getIntersectionsLocal(Ray argRay)
{
    // std::cout << "sphere get intersections";
	Intersections varIntersections;
	Vector sphereToRay = argRay.mbrOrigin - (Point(0, 0, 0));
	double a = argRay.mbrDirection.dot(argRay.mbrDirection);
	double b = 2 * argRay.mbrDirection.dot(sphereToRay);
	double c = sphereToRay.dot(sphereToRay) - 1;
	double discriminant = pow(b, 2) - 4 * a * c;
	if (discriminant < 0)
		return varIntersections;
	double intersectOne = (-b - sqrt(discriminant)) / (2 * a);
	double intersectTwo = (-b + sqrt(discriminant)) / (2 * a);
	// if (intersectOne < intersectTwo)
	// {
	// 	// varIntersections.intersect(intersectOne, std::make_unique<Sphere>(*this));
	// 	// varIntersections.intersect(intersectTwo, std::make_unique<Sphere>(*this));
	// 	varIntersections.intersect(intersectOne, this);
	// 	varIntersections.intersect(intersectTwo, this);
	// }
	// else
	// {
	// 	// varIntersections.intersect(intersectTwo, std::make_unique<Sphere>(*this));
	// 	// varIntersections.intersect(intersectOne, std::make_unique<Sphere>(*this));
	// 	varIntersections.intersect(intersectTwo, this);
	// 	varIntersections.intersect(intersectOne, this);
	// }
	varIntersections.intersect(intersectOne, this);
	varIntersections.intersect(intersectTwo, this);
	return varIntersections;
}
Vector Sphere::getNormalLocal(Point argPoint)
{
	// Point varObjectPoint = *(this->mbrTransform->invert()) * argPoint;
	Vector varObjectNormal = argPoint - this->mbrOrigin;
	return varObjectNormal.normalize();
}
// Vector Sphere::getNormal(Point argPoint)
// {
// 	// return (argPoint-origin).normalize();
// 	Point varObjectPoint = *(this->mbrTransform->invert()) * argPoint;
// 	Vector varObjectNormal = varObjectPoint - this->mbrOrigin;
// 	Matrix varTransform = *(*this->mbrTransform->invert()).transpose();
// 	Vector varWorldNormal = varTransform * varObjectNormal;
// 	varWorldNormal.mbrW = 0;
// 	return varWorldNormal.normalize();
// }
bool Sphere::checkEqual(Form other)
{
	Comparinator ce = Comparinator();
	return ce.checkTuple(mbrOrigin, other.mbrOrigin) && mbrRadius == other.mbrRadius && mbrTransform->checkEqual(*other.mbrTransform) && mbrMaterial->checkEqual(*other.mbrMaterial);
}
Vector Plane::getNormalLocal(Point argPoint)
{
	return Vector(0, 1, 0);
}
// Vector Plane::getNormal(Point argPoint)
// {
// 	return Vector(0, 1, 0);
// }
Intersections Plane::getIntersectionsLocal(Ray argRay)
{
    // std::cout << "plane get intersections";
	if (abs(argRay.mbrDirection.mbrY) <= getEpsilon())
	{
		return Intersections();
	}
	double varTime = -argRay.mbrOrigin.mbrY / argRay.mbrDirection.mbrY;
	// return Intersections(varTime, std::make_unique<Plane>(*this));
	return Intersections(varTime, this);
}