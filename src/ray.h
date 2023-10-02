#pragma once
#ifndef RAY_H
#define RAY_H

#include "tuple.h"
#include "matrix.h"

class Ray {
public:
	Point origin;
	Vector direction;
	Ray(Point origin, Vector direction);
	Point position(double time);
	Ray transform(Matrix matrix);
	Ray transform(TranslationMatrix matrix);
	Ray transform(ScalingMatrix matrix);
};

#endif