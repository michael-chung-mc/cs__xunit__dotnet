#include "form.h"
#include "comparinator.h"
#include "material.h"
#include "intersection.h"
#include "tuple.h"
#include "matrix.h"
#include "ray.h"
#include "pch.h"

Form::Form () {
	mbrOrigin = Point(0, 0, 0);
	mbrRadius = 1.0;
	mbrTransform = IdentityMatrix(4, 4);
	mbrMaterial = Material();
}

bool Form::checkEqual(Form other)
{
	Comparinator ce = Comparinator();
	return ce.checkTuple(this->mbrOrigin,other.mbrOrigin) && this->mbrRadius == other.mbrRadius && mbrTransform.checkEqual(other.mbrTransform) && mbrMaterial.checkEqual(other.mbrMaterial);
}

Form& Form::operator=(const Form other)
{
	if (this == &other) return *this;
	mbrOrigin = other.mbrOrigin;
	mbrRadius = other.mbrRadius;
	this->mbrTransform = *(new Matrix(other.mbrTransform));
	this->mbrMaterial = other.mbrMaterial;
	return *this;
}

void Form::setTransform(const Matrix m)
{
	this->mbrTransform = *(new Matrix(m));
}
Intersections Form::getIntersections(Ray argRay)
{
}
Vector Form::getNormal(Point argPoint)
{
	// return (argPoint-origin).normalize();
	Point varObjectPoint = *(this->mbrTransform.invert()) * argPoint;
	Vector varObjectNormal = varObjectPoint - this->mbrOrigin;
	Matrix varTransform = *(*this->mbrTransform.invert()).transpose();
	Vector varWorldNormal = varTransform * varObjectNormal;
	varWorldNormal.argW = 0;
	return varWorldNormal.normalize();
}

Sphere::Sphere() : Form ()
{
}
Intersections Sphere::getIntersections(Ray argRay)
{
	Intersections varIntersections;
	mbrObjectRay = argRay.transform(*(this->mbrTransform.invert()));
	Vector sphereToRay = mbrObjectRay.argOrigin - (Point(0, 0, 0));
	double a = mbrObjectRay.argDirection.dot(mbrObjectRay.argDirection);
	double b = 2 * mbrObjectRay.argDirection.dot(sphereToRay);
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