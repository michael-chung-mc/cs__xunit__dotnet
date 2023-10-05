#include "form.h"
#include "comparinator.h"
#include "material.h"
#include "intersection.h"
#include "tuple.h"
#include "matrix.h"
#include "ray.h"
#include "pch.h"

Form::Form () {
	origin = Point(0, 0, 0);
	radius = 1.0;
	mbrTransform = IdentityMatrix(4, 4);
	mbrMaterial = Material();
}

Intersections Form::getIntersections(Ray argRay)
{
	Intersections varIntersections;
	Ray objectSpaceRay = argRay.transform(*(this->mbrTransform.invert()));
	Vector sphereToRay = objectSpaceRay.origin - (Point(0, 0, 0));
	double a = objectSpaceRay.direction.dot(objectSpaceRay.direction);
	double b = 2 * objectSpaceRay.direction.dot(sphereToRay);
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

bool Form::checkEqual(Form other)
{
	Comparinator ce = Comparinator();
	return ce.checkTuple(this->origin,other.origin) && this->radius == other.radius && mbrTransform.checkEqual(other.mbrTransform) && mbrMaterial.checkEqual(other.mbrMaterial);
}

Form& Form::operator=(const Form other)
{
	if (this == &other) return *this;
	origin = other.origin;
	radius = other.radius;
	this->mbrTransform = *(new Matrix(other.mbrTransform));
	this->mbrMaterial = other.mbrMaterial;
	return *this;
}

void Form::setTransform(const Matrix m)
{
	this->mbrTransform = *(new Matrix(m));
}

Vector Form::getNormal(Point argPoint)
{
	// return (argPoint-origin).normalize();
	Point varObjectPoint = *(this->mbrTransform.invert()) * argPoint;
	Vector varObjectNormal = varObjectPoint - this->origin;
	Matrix varTransform = *(*this->mbrTransform.invert()).transpose();
	Vector varWorldNormal = varTransform * varObjectNormal;
	varWorldNormal.w = 0;
	return varWorldNormal.normalize();
}

Sphere::Sphere() : Form ()
{
}