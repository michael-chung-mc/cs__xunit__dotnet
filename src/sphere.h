#pragma once
#ifndef Sphere_H
#define Sphere_H

class Intersection;

class Sphere {
public:
	Point origin;
	double radius;
	Matrix transform;
	Sphere();
	bool checkEqual(Sphere other);
	std::vector<Intersection> intersect(Ray r);
	Sphere& operator=(const Sphere other);
	void setTransform(const Matrix m);
};
#endif