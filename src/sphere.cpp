#include "pch.h"

Sphere::Sphere()
{
	origin = Point(0, 0, 0);
	radius = 1.0;
	transform = IdentityMatrix(4, 4);
}

bool Sphere::checkEqual(Sphere other)
{
	Comparinator ce = Comparinator();
	return ce.checkTuple(this->origin,other.origin) && this->radius == other.radius && transform.checkEqual(other.transform);
}

std::vector<Intersection> Sphere::intersect(Ray r)
{
	std::vector<Intersection> intersections;
	Ray objectSpaceRay = r.transform(*(this->transform.invert()));
	Vector sphereToRay = objectSpaceRay.origin - (Point(0, 0, 0));
	double a = objectSpaceRay.direction.dot(objectSpaceRay.direction);
	double b = 2 * objectSpaceRay.direction.dot(sphereToRay);
	double c = sphereToRay.dot(sphereToRay) - 1;
	double discriminant = pow(b,2) - 4 * a * c;
	if (discriminant < 0) return intersections;
	double intersectOne = (-b - sqrt(discriminant)) / (2 * a);
	double intersectTwo = (-b + sqrt(discriminant)) / (2 * a);
	if (intersectOne < intersectTwo)
	{
		intersections.push_back(Intersection(intersectOne, *this));
		intersections.push_back(Intersection(intersectTwo, *this));
	}
	else
	{
		intersections.push_back(Intersection(intersectTwo, *this));
		intersections.push_back(Intersection(intersectOne, *this));
	}
	return intersections;
}

Sphere& Sphere::operator=(const Sphere other)
{
	if (this == &other) return *this;
	origin = other.origin;
	radius = other.radius;
	this->transform = *(new Matrix(other.transform));
	return *this;
}

void Sphere::setTransform(const Matrix m)
{
	this->transform = *(new Matrix(m));
}

Vector Sphere::normal(Point argPoint)
{
	// return (argPoint-origin).normalize();
	Point varObjectPoint = *(this->transform.invert()) * argPoint;
	Vector varObjectNormal = varObjectPoint - this->origin;
	Matrix varTransform = *(*this->transform.invert()).transpose();
	Vector varWorldNormal = varTransform * varObjectNormal;
	varWorldNormal.w = 0;
	return varWorldNormal.normalize();
}